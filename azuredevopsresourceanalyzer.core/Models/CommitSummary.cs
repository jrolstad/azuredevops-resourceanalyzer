using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class CommitSummary
    {
        public string CommitterName { get; set; }
        public int NumberOfCommits { get; set; }
        public DateTime? LastCommit { get; set; }
    }
}