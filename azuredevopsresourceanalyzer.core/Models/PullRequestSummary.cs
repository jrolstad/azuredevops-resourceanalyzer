using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class PullRequestSummary
    {
        public string AuthorName { get; set; }
        public int Count { get; set; }
        public int AbandonedCount { get; set; }
        public int ActiveCount { get; set; }
        public int CompletedCount { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}