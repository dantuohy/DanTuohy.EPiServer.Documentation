using System.Collections.Generic;
using EPiServer.DataAbstraction;
using Tuohy.Epi.ContentTypesInUse.Models;

namespace Tuohy.Epi.ContentTypesInUse.Interfaces
{
    public interface IContentUseService
    {
        ContentTypeGroups GetContentTypesInUse();
        IEnumerable<LinkViewModel> GetUsageUrlsForContentType(int id, int pageNumber, int pageSize, out ContentType contentType);
    }
}