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

        private string _organization;
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                //SearchProjects()

            } }
        public string Project { get; set; }
        public List<string> Projects { get; set; } = new List<string>();
        public string RepositoryFilter { get; set; }
        public DateTime? StartDate { get; set; }
        

        public List<ProjectSummary> Results { get; set; } = new List<ProjectSummary>();

        public bool IsSearching { get; set; } = false;
        public bool IsSearchingProjects { get; set; } = false;
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

        public async Task SearchProjects()
        {
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
                Builds = toMap.BuildDefinitions?.Select(Map).OrderBy(b => b.Name).ToList(),
                Releases = toMap.ReleaseDefinitions?.Select(Map).OrderBy(b => b.Name).ToList(),
                Contributors = toMap.Repository?.CommitSummary.Select(Map).OrderByDescending(b => b.LastActivity).ToList(),
                PullRequests = toMap.Repository?.PullRequestSummary.Select(Map).OrderByDescending(b => b.LastActivity).ToList()
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

        private ActivityItem Map(ActivitySummary toMap)
        {
            return new ActivityItem
            {
                Name = toMap?.CommitterName,
                ActivityCount = toMap?.Count,
                LastActivity = toMap?.LastActivity
            };
        }
    }

    public class ProjectSummary
    {
        public NavigableItem Repository { get; set; }
        public List<NavigableItem> Builds { get; set; }
        public List<NavigableItem> Releases { get; set; }
        public List<ActivityItem> Contributors { get; set; }
        public List<ActivityItem> PullRequests { get; set; }
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