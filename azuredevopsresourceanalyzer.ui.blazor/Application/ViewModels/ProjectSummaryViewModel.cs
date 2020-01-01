using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Models;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class ProjectSummaryViewModel
    {
        private readonly ProjectSummaryManager _manager;

        public ProjectSummaryViewModel(ProjectSummaryManager manager)
        {
            _manager = manager;
        }

        public string Organization { get; set; }
        public string Project { get; set; }
        public string RepositoryFilter { get; set; }
        public DateTime? StartDate { get; set; }

        public List<ProjectSummary> Results { get; set; } = new List<ProjectSummary>();

        public bool IsSearching { get; set; } = false;

        public string Error { get; set; }

        public async Task Search()
        {
            try
            {
                Error = null;
                IsSearching = true;
                var data = await _manager.GetSummary(Organization, Project, RepositoryFilter, StartDate);
                Results = data?.Components
                    .Select(Map)
                    .OrderBy(d => d.Repository.Name)
                    .ToList();
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
            finally
            {
                IsSearching = false;
            }


        }

        private ProjectSummary Map(Component toMap)
        {
            return new ProjectSummary
            {
                Repository = Map(toMap.Repository),
                Builds = toMap.BuildDefinitions?.Select(Map).OrderBy(b => b.Name).ToList(),
                Releases = toMap.ReleaseDefinitions?.Select(Map).OrderBy(b => b.Name).ToList(),
                LastCommit = toMap.Repository?.LastCommit?.Date,
                Contributors = toMap.Repository?.CommitSummary.Select(Map).OrderByDescending(b => b.LastActivity).ToList()
            };
        }

        private NavigableItem Map(Repository toMap)
        {
            return new NavigableItem
            {
                Name = toMap?.Name,
                Url = toMap?.Url
            };
        }

        private NavigableItem Map(BuildDefinition toMap)
        {
            return new NavigableItem
            {
                Name = toMap?.Name,
                Url = toMap?.Url
            };
        }

        private NavigableItem Map(ReleaseDefinition toMap)
        {
            return new NavigableItem
            {
                Name = toMap?.Name,
                Url = toMap?.Url
            };
        }

        private ActivityItem Map(CommitSummary toMap)
        {
            return new ActivityItem
            {
                Name = toMap?.CommitterName,
                ActivityCount = toMap?.NumberOfCommits,
                LastActivity = toMap?.LastCommit
            };
        }
    }

    public class ProjectSummary
    {
        public NavigableItem Repository { get; set; }
        public List<NavigableItem> Builds { get; set; }
        public List<NavigableItem> Releases { get; set; }
        public DateTime? LastCommit { get; set; }
        public List<ActivityItem> Contributors { get; set; }
        
    }

    public class NavigableItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class ActivityItem
    {
        public string Name { get; set; }
        public int? ActivityCount { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}