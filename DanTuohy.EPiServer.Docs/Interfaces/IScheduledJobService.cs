using System;
using System.Collections.Generic;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Interfaces
{
    public interface IScheduledJobService
    {
        IDictionary<Guid, string> ListAll();
        JobDoc GetJob(Guid id);
    }
}