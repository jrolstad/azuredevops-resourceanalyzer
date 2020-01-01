using System;
using System.Collections.Generic;
using System.Dynamic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class ProjectSummary
    {
        public string Organization { get; set; }
        public string Project { get; set; }
        public string RepositoryFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public IEnumerable<Component> Components { get; set; }
    }
}