using System;
using System.Collections.Generic;
using System.Text;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions
{
    public static class DataExtensions
    {
        public static void WithRepository(this TestCompositionRoot root, 
            string name, 
            string url = "https://dev.azure.com/repo/{0}",
            string id = null,
            long size = 1,
            string organization="jrolstad",
            string project = "the-project")
        {
            var item = new GitRepository
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                weburl = string.Format(url, name),
                size = size
            };

            WithItem(organization,project,root.Context.Repositories,item);
        }

        public static void WithBuildDefinition(this TestCompositionRoot root,
            string name,
            string repositoryId,
            string url = "https://dev.azure.com/build/{0}",
            string id = null,
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new BuildDefinition
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                RepositoryId = repositoryId,
                project = new Project
                {
                    name = name,
                    id = Base64Encode(name)
                },
                _links = WithLinks(string.Format(url, name))
                
            };

            WithItem(organization, project, root.Context.BuildDefinitions, item);
        }

        public static void WithReleaseDefinition(this TestCompositionRoot root,
            string name,
            string repositoryId=null,
            string buildId=null,
            string url = "https://dev.azure.com/release/{0}",
            string id = null,
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new ReleaseDefinition
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                RepositoryId = repositoryId,
                BuildId = buildId,
                _links = WithLinks(string.Format(url, name))

            };

            WithItem(organization, project, root.Context.ReleaseDefinitions, item);
        }

        public static void WithRelease(this TestCompositionRoot root,
            string name,
            string releaseDefinitionId,
            DateTime? productionReleaseDate,
            string productionReleaseName = "the-production-release",
            string url = "https://dev.azure.com/release/{0}",
            string id = null,
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new Release
            {
                name = name,
                id = id ?? Guid.NewGuid().ToString(),
                ReleaseDefinitionId = releaseDefinitionId,
                _links = WithLinks(string.Format(url, name)),
                environments = new List<ReleaseEnvironment>
                {
                    new ReleaseEnvironment
                    {
                        id = Guid.NewGuid().ToString(),
                        name = productionReleaseName,
                        status = productionReleaseDate.HasValue ? "succeeded":"pending",
                        deploySteps = new List<DeployAttempt>
                        {
                            new DeployAttempt
                            {
                                id = Guid.NewGuid().ToString(),
                                queuedOn = productionReleaseDate,
                                status = productionReleaseDate.HasValue ? "succeeded":"pending"
                            }
                        }
                    }
                }

            };

            WithItem(organization, project, root.Context.Releases, item);
        }

        public static void WithPullRequest(this TestCompositionRoot root,
            string repositoryId,
            string createdBy,
            DateTime? createdAt = null,
            string status = "complete",
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new GitPullRequest
            {
                createdBy = new IdentifyRef
                {
                    displayName = createdBy
                },
                creationDate = createdAt.ToString(),
                status = status,
                RepositoryId = repositoryId
            };

            WithItem(organization, project, root.Context.PullRequests, item);
        }

        public static void WithBranch(this TestCompositionRoot root,
            string repositoryId,
            string name,
            int commitsAhead = 0,
            int commitsBehind = 0,
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new GitBranchStat
            {
                aheadCount = commitsAhead,
                behindCount = commitsBehind,
                name = name,
                RepositoryId = repositoryId
            };

            WithItem(organization, project, root.Context.BranchStats, item);
        }

        public static void WithCommit(this TestCompositionRoot root,
            string repositoryId,
            string authorName,
            string authorEmail=null,
            DateTime? commitDate = null,
            int deletions = 0,
            int edits = 0,
            int additions = 0,
            string organization = "jrolstad",
            string project = "the-project")
        {
            var item = new GitCommitRef
            {
                author = new GitUserDate
                {
                    date = commitDate ?? DateTime.Now.AddDays(-7),
                    email = authorEmail ?? Base64Encode(authorName),
                    name = authorName
                },
                changeCounts = new ChangeCountDictionary
                {
                    Add = additions,
                    Edit = edits,
                    Delete = deletions
                },
                commitId = Guid.NewGuid().ToString(),
                RepositoryId = repositoryId
            };
            

            WithItem(organization, project, root.Context.Commits, item);
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

        private static void WithItem<T>(string organization, string project, Dictionary<string, List<T>> dictionary, T item)
        {
            var key = $"{organization}:{project}";
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<T>());
            }
            dictionary[key].Add(item);
        }

        private static Dictionary<string,Link> WithLinks(string url)
        {
            return new Dictionary<string, Link>
            {
                {"web",new Link{href = url} }
            };
        }
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}