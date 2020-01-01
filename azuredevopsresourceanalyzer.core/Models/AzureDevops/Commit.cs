using System;
using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class CommitResult
    {
        public List<Commit> value { get; set; }
    }
    public class Commit
    {
        public string commitId { get; set; }
        public CommitAuthor author { get; set; }
        public CommitAuthor committer { get; set; }
        public CommitChangeCount changeCounts { get; set; }
    }

    public class CommitAuthor
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime? date { get; set; }
    }

    public class CommitChangeCount
    {
        public int Add { get; set; }
        public int Edit { get; set; }
        public int Delete { get; set; }
    }
}