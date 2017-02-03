﻿using System.Linq;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models.DynamicData;

namespace Tuohy.Epi.Docs.Services
{
    [ServiceConfiguration(ServiceType = typeof(IDocumentationSettingsService))]
    public class DocumentationSettingsService : IDocumentationSettingsService
    {
        public Identity UpdrtSettings(DocumentationSettings settings)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(DocumentationSettings));
            var setting = GetSettings();
            if (setting == null)
            {
                return store.Save(settings);

            }
            settings.Id = setting.Id;

            return store.Save(settings);
        }

        public DocumentationSettings GetSettings()
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(DocumentationSettings));

            return store.Items<DocumentationSettings>().FirstOrDefault();
        }
    }
}