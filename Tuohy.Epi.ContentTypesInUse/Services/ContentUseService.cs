using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Tuohy.Epi.ContentTypesInUse.Interfaces;
using Tuohy.Epi.ContentTypesInUse.Models;
using System.Text;
using EPiServer.Web;
using EPiServer.Globalization;

namespace Tuohy.Epi.ContentTypesInUse.Services
{
    [ServiceConfiguration(typeof(IContentUseService))]
    public class ContentUseService : IContentUseService
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IContentModelUsage _contentModalUsageRepository;
        private readonly IContentRepository _contentRepository;

        public ContentUseService(IContentTypeRepository contentTypeRepository, IContentModelUsage contentModalUsageRepository, IContentRepository contentRepository)
        {
            _contentTypeRepository = contentTypeRepository;
            _contentModalUsageRepository = contentModalUsageRepository;
            _contentRepository = contentRepository;
        }
        public ContentTypeGroups GetContentTypesInUse()
        {
            return new ContentTypeGroups
            {
                Pages = GetAllPageTypes().Select(
                    x =>
                        new ContentTypeCountsViewModel
                        {
                            ContentTypeId = x.ID,
                            ContentTypeName = x.LocalizedName,
                            Count = _contentModalUsageRepository.ListContentOfContentType(x).Select(y => y.ContentLink.ToReferenceWithoutVersion()).Distinct().Count()
                        }),
                Blocks = GetAllBlockTypes().Select(
                    x =>
                        new ContentTypeCountsViewModel
                        {
                            ContentTypeId = x.ID,
                            ContentTypeName = x.LocalizedName,
                            Count = _contentModalUsageRepository.ListContentOfContentType(x).Count
                        })
            };
        }

        public IEnumerable<LinkViewModel> GetUsageUrlsForContentType(int id, int pageNumber, int pageSize, out ContentType contentType)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();

            contentType = _contentTypeRepository.Load(id);
            if (contentType == null) return null;

            var isBlockType = contentType.ModelType.IsSubclassOf(typeof(BlockData));

            var usages = _contentModalUsageRepository.ListContentOfContentType(contentType).Select(y => y.ContentLink.ToReferenceWithoutVersion()).Distinct().OrderByDescending(x => x.ID).Skip(pageNumber * pageSize).Take(pageSize);

            //if (isBlockType)
            //{
            //    var list = new List<LinkViewModel>();
            //    foreach (var usage in usages)
            //    {
            //        //Check if this usage is as a property on a page type
            //        var useageContent = _contentRepository.Get<IContent>(usage);
            //        if (useageContent is PageData)
            //        {
            //            list.Add(new LinkViewModel(PageEditing.GetEditUrl(useageContent.ContentLink), useageContent.Name));
            //            continue;
            //        }

            //        //todo refactor
            //        var reference = _contentRepository.GetReferencesToContent(usage, false).FirstOrDefault();
            //        if (reference == null)
            //        {
            //            list.Add(new LinkViewModel(PageEditing.GetEditUrl(usage), ));
            //            continue;
            //        }

            //        var content = _contentRepository.Get<IContent>(reference.OwnerID);
            //        while (!(content is PageData))
            //        {
            //            reference = _contentRepository.GetReferencesToContent(content.ContentLink, false).FirstOrDefault();
            //            if (reference == null)
            //            {
            //                list.Add(new LinkViewModel(PageEditing.GetEditUrl(usage), "Not on page"));
            //                break;
            //            }
            //            content = _contentRepository.Get<IContent>(reference.OwnerID);
            //        }

            //        list.Add(new LinkViewModel(PageEditing.GetEditUrl(usage),));

            //    }

            //    return list;
            //}

            return usages.Select(x => new LinkViewModel(PageEditing.GetEditUrl(x), _contentRepository.Get<IContent>(x).Name));
        }

        private IEnumerable<ContentType> GetAllPageTypes()
        {
            return _contentTypeRepository.List().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(PageData)));
        }

        private IEnumerable<ContentType> GetAllBlockTypes()
        {
            return _contentTypeRepository.List().Where(x => x.ModelType != null && x.ModelType.IsSubclassOf(typeof(BlockData)));
        }
    }
}