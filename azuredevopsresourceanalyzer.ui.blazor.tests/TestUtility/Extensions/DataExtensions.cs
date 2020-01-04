using System;
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
            string organization="default",
            string project = "the-project")
        {
            root.Context.Repositories.Add(new GitRepository
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                weburl = string.Format(url,name),
                size = size
            });
        }

        public static void WithProject(this TestCompositionRoot root,
            string name, 
            string id = null,
            string organization="default")
        {
            root.Context.Projects.Add(
                new Project
                {
                    id = id ?? Guid.NewGuid().ToString(),
                    name = name
                }
            );
        }
    }
}