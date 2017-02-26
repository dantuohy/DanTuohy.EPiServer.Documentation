using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Services
{
    [ServiceConfiguration(ServiceType = typeof(IContentTypeService))]
    public class ContentTypeService : IContentTypeService
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IMapToNew<ContentType, PageDoc> _pageMapper;
        private readonly IMapToNew<ContentType, BlockDoc> _blockMapper;
        private readonly IMapToNew<ContentType, MediaDoc> _mediaMapper;
        private readonly IMapToNew<PropertyDefinition, PropertyDoc> _propertyMapper;

        public ContentTypeService(IContentTypeRepository contentTypeRepository, IMapToNew<ContentType, PageDoc> pageMapper, IMapToNew<ContentType, BlockDoc> blockMapper, IMapToNew<ContentType, MediaDoc> mediaMapper, IMapToNew<PropertyDefinition, PropertyDoc> propertyMapper)
        {
            _contentTypeRepository = contentTypeRepository;
            _pageMapper = pageMapper;
            _blockMapper = blockMapper;
            _mediaMapper = mediaMapper;
            _propertyMapper = propertyMapper;
        }

        public IEnumerable<ContentType> GetAllPages()
        {
            return _contentTypeRepository.List().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(PageData)));
        }

        public IEnumerable<ContentType> GetAllBlocks()
        {
            return _contentTypeRepository.List().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(BlockData)));
        }

        public IEnumerable<ContentType> GetAllMedia()
        {
            return _contentTypeRepository.List().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(MediaData)));
        }

        public IEnumerable<ContentType> ListAll()
        {
            return _contentTypeRepository.List();
        }

        public ContentTypeDoc GetContentTypeDoc(ContentType contentType)
        {
            if (contentType.ModelType.IsSubclassOf(typeof(PageData)))
            {
                var pageDoc = _pageMapper.Map(contentType);
                pageDoc.Properties = contentType.PropertyDefinitions.Select(x => GetProperty(x, contentType.ModelType)).OrderBy(x => x.Tab);

                var avaliablePages = GetAvaliablePages(contentType);
                pageDoc.AvaliablePages = avaliablePages == null ? null : avaliablePages.ToDictionary(x => x.ID.ToString(), x => x.LocalizedName);

              //  pageDoc.Access = contentType.ACL.ToDictionary(x => x.Value.Name, x => x.Value.Access.ToString());
                return pageDoc;
            }
            if (contentType.ModelType.IsSubclassOf(typeof(BlockData)))
            {
                var blocDoc = _blockMapper.Map(contentType);
                blocDoc.Properties = contentType.PropertyDefinitions.Select(x => GetProperty(x, contentType.ModelType)).OrderBy(x => x.Tab);
                return blocDoc;
            }
            if (contentType.ModelType.IsSubclassOf(typeof(MediaData)))
            {
                var mediaDoc = _mediaMapper.Map(contentType);
                mediaDoc.Properties = contentType.PropertyDefinitions.Select(x => GetProperty(x, contentType.ModelType)).OrderBy(x => x.Tab);
                mediaDoc.MediaDescriptors = GetMediaDescriptorValues(contentType.ModelType);
                return mediaDoc;
            }


            return null;
        }

        private IEnumerable<ContentType> GetAvaliablePages(ContentType page)
        {
            var contentTypeAvailablilityService = ServiceLocator.Current.GetInstance<ContentTypeAvailabilityService>();

            var setting = contentTypeAvailablilityService.GetSetting(page.Name);
            var allAvailable = setting.Availability == Availability.All ||
                               setting.Availability == Availability.Undefined &&
                               !setting.AllowedContentTypeNames.Any();

            var availableList = new List<ContentType>();

            if (allAvailable) return availableList;

            foreach (var contentType in ListAll().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(PageData))))
            {
                availableList.AddRange(from str in setting.AllowedContentTypeNames where str.Equals(contentType.Name) select contentType);
                if (!availableList.Any()) return null;
            }

            return availableList;
        }

        private PropertyDoc GetProperty(PropertyDefinition propertyDefinition, Type page)
        {
            var propertyDoc = _propertyMapper.Map(propertyDefinition);
            var pageProperty = page.GetProperties().FirstOrDefault(x => x.Name == propertyDefinition.Name);
            if (pageProperty == null)
            {
                return propertyDoc;
            }

            var attributes = GetDocumentableAttributeValues(pageProperty);
            foreach (var attribute in GetValidationAttributeValues(pageProperty))
            {
                attributes.Add(attribute);
            }
            propertyDoc.CustomAttributes = attributes;
            propertyDoc.AllowedTypes = GetAllowedTypesInContentArea(pageProperty);
            return propertyDoc;
        }

        private static IDictionary<string, string> GetDocumentableAttributeValues(PropertyInfo property)
        {
            var documentableAttributes = property
                .GetCustomAttributes(true)
                .OfType<IDocumentable>();

            return documentableAttributes.ToDictionary(documentableAttribute => 
            documentableAttribute.Name, 
            documentableAttribute => documentableAttribute.Value);
        }

        private static string GetMediaDescriptorValues(Type media)
        {
            var attribute = media.GetCustomAttributes(typeof(MediaDescriptorAttribute)).FirstOrDefault() as MediaDescriptorAttribute;
            if (attribute == null) return null;
            return attribute.ExtensionString;
        }

        private static IDictionary<string, string> GetValidationAttributeValues(PropertyInfo property)
        {
            var attributes = new Dictionary<string, string>();

            var stringLengthAttribute =
                property.GetCustomAttribute<StringLengthAttribute>();
            if (stringLengthAttribute != null)
            {
                attributes.Add("Min Characters", stringLengthAttribute.MinimumLength.ToString());
                attributes.Add("Max Characters", stringLengthAttribute.MaximumLength.ToString());
            }
            var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();
            if (rangeAttribute != null)
            {
                attributes.Add("Min Value", rangeAttribute.Minimum.ToString());
                attributes.Add("Max Value", rangeAttribute.Maximum.ToString());
            }
            var regexAttribute = property.GetCustomAttributes(true).Where(x => !(x is IDocumentable)).OfType<RegularExpressionAttribute>().FirstOrDefault();
            if (regexAttribute != null)
            {
                attributes.Add("Pattern", regexAttribute.Pattern);
            }

            return attributes;
        }

        private IDictionary<string, string> GetAllowedTypesInContentArea(PropertyInfo property)
        {
            if(property.PropertyType.FullName == typeof(ContentArea).FullName)
            {
                var allowed = new Dictionary<string, string>();
                var allowedTypes = property.GetCustomAttribute<AllowedTypesAttribute>();

                if (allowedTypes == null)
                {
                    allowed.Add("-1", "All");
                }
                else
                {
                    foreach(var type in allowedTypes.AllowedTypes.Select(x => ToContentType(x)).Where(x => x != null))
                    {
                        allowed.Add(type.ID.ToString(), type.LocalizedName);
                    }
              
                }
                return allowed;
            }
            return null;
        }

        private ContentType ToContentType(Type content)
        {
            return _contentTypeRepository.Load(content);  
        }
    }
}