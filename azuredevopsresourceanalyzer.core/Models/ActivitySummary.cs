using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class ActivitySummary
    {
        public string CommitterName { get; set; }
        public int Count { get; set; }
        public DateTime? LastActivity { get; set; }
    }

    public class Branch
    {
        public string Name { get; set; }
        public int CommitsAhead { get; set; }
        public int CommitsBehind { get; set; }
    }
    
}