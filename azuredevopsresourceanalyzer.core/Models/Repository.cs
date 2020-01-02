﻿using System;
using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class Repository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ActivitySummary> CommitSummary { get; set; }
        public List<ActivitySummary> PullRequestSummary { get; set; }

    }
}