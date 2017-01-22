using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanTuohy.EPiServer.Docs.Interfaces
{
    public interface IDocumentable
    {
        string Name { get; }
        string Value { get; }
    }
}
