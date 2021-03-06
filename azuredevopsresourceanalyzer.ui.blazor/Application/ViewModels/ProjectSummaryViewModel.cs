﻿using System;
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
        private readonly ProjectManager _projectManager;

        public ProjectSummaryViewModel(ProjectSummaryManager manager, ProjectManager projectManager)
        {
            _manager = manager;
            _projectManager = projectManager;
        }

        public string Organization { get; set; }
        public string Project { get; set; }
        public List<string> Projects { get; set; } = new List<string>();
        public string RepositoryFilter { get; set; }
        public DateTime? StartDate { get; set; }
        

        public List<ProjectSummary> Results { get; set; } = new List<ProjectSummary>();

        public bool IsSearching { get; set; } = false;
        public bool IsSearchingProjects { get; set; } = false;
        public string Error { get; set; }

        public async Task Initialize()
        {
            this.StartDate = DateTime.Today.AddYears(-2);
        }

        public async Task Search()
        {
            if (string.IsNullOrWhiteSpace(this.Organization) || string.IsNullOrWhiteSpace(this.Project))
            {
                Error = "Unable to search; please enter an organization and project first";
                this.Results = new List<ProjectSummary>();

                return;
            }

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

        public async Task SearchProjects()
        {
            if (string.IsNullOrWhiteSpace(this.Organization))
            {
                Error = "Unable to search projects; please enter an organization first";
                this.Projects = new List<string>();

                return;
            }

            try
            {
                Error = null;
                
                IsSearchingProjects = true;
                var data = await _projectManager.Get(this.Organization);
                this.Projects = data?.Select(p => p.Name).OrderBy(p => p).ToList();
                this.Project = this.Projects?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
            finally
            {
                IsSearchingProjects = false;
            }
           
        }

        private ProjectSummary Map(Component toMap)
        {
            return new ProjectSummary
            {
                Repository = Map(toMap.Repository),
                Builds = toMap.Repository.BuildDefinitions?.Select(Map).OrderBy(b => b.Name).ToList(),
                Releases = GetReleaseDefinitions(toMap)?.Select(Map).OrderBy(b => b.ReleaseDefinition.Name).ToList(),
                Contributors = toMap.Repository?.CommitSummary.Select(Map).OrderByDescending(b => b.LastActivity).ToList(),
                PullRequests = toMap.Repository?.PullRequestSummary.Select(Map).OrderByDescending(b => b.LastActivity).ToList(),
                Branches = toMap.Repository?.Branches.Select(Map).ToList()
            };
        }

        private static IEnumerable<ReleaseDefinition> GetReleaseDefinitions(Component toMap)
        {
            return toMap.Repository.BuildDefinitions?.SelectMany(b=>b.ReleaseDefinitions)
                .Union(toMap.Repository.ReleaseDefinitions);
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

        private ReleaseSummary Map(ReleaseDefinition toMap)
        {
            
            return new ReleaseSummary
            {
                ReleaseDefinition = new NavigableItem { Name = toMap.Name,Url=toMap.Url},
                LastProductionRelease = Map(toMap.LastProductionRelease),
                DeployedAt = toMap.LastProductionRelease?.DeployedAt
            };
        }

        private NavigableItem Map(Release toMap)
        {
            if (toMap == null)
                return null;
            return new NavigableItem {Name = toMap.Name, Url = toMap.Url};
        }

        private ActivityItem<PullRequestDetail> Map(PullRequestSummary toMap)
        {
            return new ActivityItem<PullRequestDetail>
            {
                Name = toMap?.AuthorName,
                ActivityCount = toMap?.TotalCount,
                LastActivity = toMap?.LastActivity,
                ActivityDetails = new PullRequestDetail
                {
                    Abandoned = toMap.AbandonedCount,
                    Active = toMap.ActiveCount,
                    Complete = toMap.CompletedCount
                }
            };
        }
        private ActivityItem<CommitDetail> Map(ContributorSummary toMap)
        {
            return new ActivityItem<CommitDetail>
            {
                Name = toMap?.AuthorName,
                ActivityCount = toMap?.CommitCount,
                LastActivity = toMap?.LastActivity,
                ActivityDetails = new CommitDetail
                {
                    Additions = toMap.Additions.GetValueOrDefault(),
                    Edits = toMap.Edits.GetValueOrDefault(),
                    Deletions = toMap.Deletions.GetValueOrDefault()
                }
            };
        }

        private BranchSummary Map(Branch toMap)
        {
            return new BranchSummary
            {
                Name = new NavigableItem{Name = toMap.Name,Url = toMap.Url},
                CommitsAhead = toMap.CommitsAhead,
                CommitsBehind = toMap.CommitsBehind
            };
        }
    }

    public class ProjectSummary
    {
        public NavigableItem Repository { get; set; }
        public List<NavigableItem> Builds { get; set; }
        public List<ReleaseSummary> Releases { get; set; }
        public List<ActivityItem<CommitDetail>> Contributors { get; set; }
        public List<ActivityItem<PullRequestDetail>> PullRequests { get; set; }

        public List<BranchSummary> Branches { get; set; }
    }

    public class PullRequestDetail
    {
        public int Active { get; set; }
        public int Abandoned { get; set; }
        public int Complete { get; set; }
    }

    public class CommitDetail
    {
        public int Additions { get; set; }
        public int Edits { get; set; }
        public int Deletions { get; set; }
    }

    public class BranchSummary
    {
        public NavigableItem Name { get; set; }
        public int CommitsAhead { get; set; }
        public int CommitsBehind { get; set; }
        
    }

    public class ReleaseSummary
    {
        public NavigableItem ReleaseDefinition { get; set; }
        public NavigableItem LastProductionRelease { get; set; }
        public DateTime? DeployedAt { get; set; }

    }
}