using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class CommitSummary
    {
        public string AuthorName { get; set; }
        public int Count { get; set; }
        public DateTime? LastActivity { get; set; }
        public int? Additions { get; set; }
        public int? Edits { get; set; }
        public int? Deletions { get; set; }
    }
}