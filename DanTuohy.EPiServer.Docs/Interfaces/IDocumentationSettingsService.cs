using EPiServer.Data;
using Tuohy.Epi.Docs.Models.DynamicData;

namespace Tuohy.Epi.Docs.Interfaces
{
    public interface IDocumentationSettingsService
    {
        Identity UpdrtSettings(DocumentationSettings settings);

        DocumentationSettings GetSettings();

    }
}