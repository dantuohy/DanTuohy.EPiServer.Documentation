using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using Tuohy.Epi.Docs.Interfaces;
using Tuohy.Epi.Docs.Models;

namespace Tuohy.Epi.Docs.Services
{
    [ServiceConfiguration(ServiceType = typeof(IScheduledJobService))]
    public class ScheduledJobService : IScheduledJobService
    {
        private Injected<ScheduledJobRepository> _jobRepository { get; set; }

        private readonly IMapToNew<ScheduledJob, JobDoc> _jobMapper;

        public ScheduledJobService(IMapToNew<ScheduledJob, JobDoc> jobMapper)
        {
            _jobMapper = jobMapper;
        }

        public IDictionary<Guid, string> ListAll()
        {
            return _jobRepository.Service.List().ToDictionary(x => x.ID, x => x.Name);
        }

        public JobDoc GetJob(Guid id)
        {
            var job = _jobRepository.Service.Get(id);
            if (job == null)
            {
                return null;
            }
            var doc = _jobMapper.Map(job);

            doc.Description = GetDescription(Type.GetType(job.TypeName + "," + job.AssemblyName));

            return doc;
        }

        private static string GetDescription(Type job)
        {
            var attribute = job.GetCustomAttribute<ScheduledPlugInAttribute>();
            if (attribute == null) return null;
            return attribute.Description;
        }

    }
}