using System;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class Commit
    {
        public string CommiterName { get; set; }
        public DateTime CommittedAt { get; set; }
    }
}