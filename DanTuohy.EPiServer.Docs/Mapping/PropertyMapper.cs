
using DanTuohy.EPiServer.Docs.Interfaces;
using DanTuohy.EPiServer.Docs.Models;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace DanTuohy.EPiServer.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<PropertyDefinition, PropertyDoc>))]
    public class PropertyMapper : IMapToNew<PropertyDefinition, PropertyDoc>
    {
        public PropertyDoc Map(PropertyDefinition source)
        {
            var propertyDoc = new PropertyDoc
            {
                Name = source.Name,
                Description = source.HelpText,
                Type = source.Type.LocalizedName,
                SortOrder = source.FieldOrder,
                DefaultValue = source.DefaultValue,
                IsRequired = source.Required,
                IsSearchable = source.Searchable,
                Tab = source.Tab.LocalizedName,
            };
            
            return propertyDoc;
        }
    }
}