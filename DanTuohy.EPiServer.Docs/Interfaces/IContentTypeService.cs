using System.Collections.Generic;
using EPiServer.DataAbstraction;
using DanTuohy.EPiServer.Docs.Models;

namespace DanTuohy.EPiServer.Docs.Interfaces
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
