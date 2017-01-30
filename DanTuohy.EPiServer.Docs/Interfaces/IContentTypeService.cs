using System.Collections.Generic;
using EPiServer.DataAbstraction;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Interfaces
{
    public interface IContentTypeService
    {
        IEnumerable<ContentType> ListAll();
        ContentTypeDoc GetContentTypeDoc(ContentType contentType);
        IEnumerable<ContentType> GetAllPages();
        IEnumerable<ContentType> GetAllBlocks();
        IEnumerable<ContentType> GetAllMedia();
    }
}
