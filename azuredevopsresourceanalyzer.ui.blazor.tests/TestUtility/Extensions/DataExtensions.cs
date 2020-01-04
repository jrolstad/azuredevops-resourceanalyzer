using System;
using System.Collections.Generic;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static void WithRepository(this TestCompositionRoot root, 
            string name, 
            string url = "https://dev.azure.com/{0}",
            string id = null,
            long size = 1,
            string organization="jrolstad",
            string project = "the-project")
        {
            var key = $"{organization}:{project}";
            if (!root.Context.Repositories.ContainsKey(key))
            {
                root.Context.Repositories.Add(key, new List<GitRepository>());
            }
            root.Context.Repositories[key].Add(new GitRepository
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                weburl = string.Format(url, name),
                size = size
            });
        }

        public static void WithProject(this TestCompositionRoot root,
            string name, 
            string id = null,
            string organization="default")
        {
            if(!root.Context.Projects.ContainsKey(organization))
            {
                root.Context.Projects.Add(organization, new List<Project>());
            }
            root.Context.Projects[organization].Add(new Project
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString()
            });
        }
    }
}