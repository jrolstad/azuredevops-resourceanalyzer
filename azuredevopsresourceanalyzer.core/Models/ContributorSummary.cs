using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class ContributorSummary
    {
        public string AuthorName { get; set; }
        public int CommitCount { get; set; }
        public DateTime? LastActivity { get; set; }
        public int? Additions { get; set; }
        public int? Edits { get; set; }
        public int? Deletions { get; set; }
    }
}