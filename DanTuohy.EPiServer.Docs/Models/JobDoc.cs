using System;

namespace Tuohy.Epi.Docs.Models
{
    public class JobDoc
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IntervalName { get; set; }
        public int IntervalLength { get; set; }
        public bool IsActive { get; set; }
        public bool IsStoppable { get; set; }

    }
}