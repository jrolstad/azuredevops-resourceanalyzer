using System;
using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class GitCommitRef
    {
        public string commitId { get; set; }
        public GitUserDate author { get; set; }
        public GitUserDate committer { get; set; }
        public ChangeCountDictionary changeCounts { get; set; }
        public List<ResourceReference> workItems { get; set; }
    }

    public class GitUserDate
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime? date { get; set; }
    }

    public class ChangeCountDictionary
    {
        public int Add { get; set; }
        public int Edit { get; set; }
        public int Delete { get; set; }
    }
}