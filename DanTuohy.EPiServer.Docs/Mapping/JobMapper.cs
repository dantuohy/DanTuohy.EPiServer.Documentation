using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Mapping
{
    [ServiceConfiguration(ServiceType = typeof(IMapToNew<ScheduledJob, JobDoc>))]
    public class JobMapper : IMapToNew<ScheduledJob, JobDoc>
    {
        public JobDoc Map(ScheduledJob source)
        {
            var jobDoc = new JobDoc
            {
                Id = source.ID,
                Name = source.Name,
                IntervalName = source.IntervalType.ToString(),
                IntervalLength = source.IntervalLength,
                IsActive = source.IsEnabled,
                IsStoppable = source.IsStoppable
            };
            
            return jobDoc;
        }
    }
}