using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class WorkSummary
    {
        public string Organization { get; set; }
        public string TeamFilter { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }
}