using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Models;
using azuredevopsresourceanalyzer.ui.blazor.Application.Models;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class WorkSummaryViewModel
    {
        private readonly WorkSummaryManager _manager;
        private readonly ProjectManager _projectManager;

        public WorkSummaryViewModel(WorkSummaryManager manager, ProjectManager projectManager)
        {
            _manager = manager;
            _projectManager = projectManager;
        }

        public string Organization { get; set; }
        public string Project { get; set; }
        public List<string> Projects { get; set; } = new List<string>();
        public string TeamsFilter { get; set; }
        public DateTime? StartDate { get; set; }

        public string Error { get; set; }
        public List<WorkSummary> Results { get; set; } = new List<WorkSummary>();
        public List<SelectableItem> AvailableWorkItemTypes { get; set; } = new List<SelectableItem>();
        public bool IsSearching = false;
        public bool IsSearchingProjects = false;

        public async Task Initialize()
        {
            this.StartDate = DateTime.Today.AddYears(-2);
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

        public async Task Search()
        {
            if (string.IsNullOrWhiteSpace(this.Organization) || string.IsNullOrWhiteSpace(this.Project))
            {
                Error = "Unable to search; please enter an organization and project first";
                this.Results = new List<WorkSummary>();

                return;
            }

            try
            {
                Error = null;

                IsSearching = true;
                var data = await _manager.GetSummary(this.Organization, this.Project, this.TeamsFilter, this.StartDate);

                this.Results = Map(data.Teams);
                this.AvailableWorkItemTypes = Map(this.Results);
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

        private void ExecuteFilterWorkItems()
        {
            if (!this.AvailableWorkItemTypes.Any())
                return;

            var visibleWorkItemTypes = this.AvailableWorkItemTypes
                .Where(t => t.IsSelected)
                .ToDictionary(t => t.Name);

            foreach (var result in Results)
            {
                FilterWorkItemTypes(result.WorkItemTypeCounts, visibleWorkItemTypes);
                FilterWorkItemTypes(result.LifespanMetrics, visibleWorkItemTypes);

                foreach (var contributor in result.Contributors)
                {
                    FilterWorkItemTypes(contributor.ActivityDetails, visibleWorkItemTypes);
                }
                
                result.Contributors = result.Contributors
                    .OrderByDescending(SortContributorsByClosedCount)
                    .ToList();
            }

        }

        private static void FilterWorkItemTypes(IEnumerable<IVisibleItem> toFilter,
            IReadOnlyDictionary<string, SelectableItem> visibleWorkItemTypes)
        {
            foreach (var item in toFilter)
            {
                item.Visible = visibleWorkItemTypes.ContainsKey(item.Type);
            }
        }


        private List<SelectableItem> Map(IEnumerable<WorkSummary> toMap)
        {
            return toMap.SelectMany(r => r.WorkItemTypeCounts)
                .Select(r => r.Type)
                .Distinct()
                .Select(r =>
                {
                    var item = new SelectableItem
                    {
                        IsSelected = true,
                        Name = r
                    };
                    item.SelectedChanged += WorkItemTypeSelectedChanged;
                    return item;
                })
                .OrderBy(r => r.Name)
                .ToList();
        }

        private void WorkItemTypeSelectedChanged(object sender, EventArgs e)
        {
            ExecuteFilterWorkItems();
        }

        private List<WorkSummary> Map(IEnumerable<Team> toMap)
        {
            return toMap
                .Select(t=>new WorkSummary
                {
                    Team = new NavigableItem { Name = t.Name,Url=t.Url},
                    WorkItemTypeCounts = Map(t.WorkItemTypes),
                    Contributors = Map(t.Contributors),
                    LifespanMetrics = MapMetrics(t.WorkItemTypes)
                })
                .OrderBy(t=>t.Team?.Name)
                .ToList();
        }

        private List<WorkItemTypeMetrics> MapMetrics(IEnumerable<TeamWorkItemType> workItemTypes)
        {
            return workItemTypes
                .Select(t=>MapMetric(t.Type, t.Metrics))
                .OrderBy(t => t.Type)
                .ToList();
        }

        private static WorkItemTypeMetrics MapMetric(string type, TeamWorkItemTypeMetrics toMap)
        {
            return new WorkItemTypeMetrics
            {
                Type = type,
                InceptionToActiveDays = toMap?.InceptionToActiveDays,
                ActiveToResolvedDays = toMap?.ActiveToResolvedDays,
                ResolvedToDoneDays = toMap?.ResolvedToDoneDays,
                ActiveToDoneDays = toMap?.ActiveToDoneDays,
                TotalEndToEndDays = toMap?.TotalEndToEndDays,
                TotalStoryPointsCompleted = toMap?.TotalStoryPointsCompleted,
                StoryPointsCompleted = toMap?.StoryPointsCompleted
            };
        }

        private List<WorkItemTypeCount> Map(List<TeamWorkItemType> toMap)
        {
            return toMap.Select(m => new WorkItemTypeCount
                    {
                        Type = m.Type,
                        Active = GetValue(m.StateCount, WorkItemStates.Active),
                        New = GetValue(m.StateCount, WorkItemStates.New),
                        Resolved = GetValue(m.StateCount, WorkItemStates.Resolved),
                        Closed = GetValue(m.StateCount, WorkItemStates.Closed),
                        Metrics = MapMetric(m.Type,m.Metrics)
                    }
                )
                .Where(m => (m.Active + m.New + m.Resolved + m.Closed) > 0) //Only show items with work attached
                .OrderBy(m=>m.Type)
                .ToList();
        }

        private List<ActivityItem<List<WorkItemTypeCount>>> Map(IEnumerable<TeamWorkItemContributor> toMap)
        {
            var result = toMap.Select(c => new ActivityItem<List<WorkItemTypeCount>>
                {
                    Name = c.Contributor,
                    ActivityCount = c.WorkItemTypes.Count,
                    ActivityDetails = Map(c.WorkItemTypes)
                })
                .Where(c=>!string.IsNullOrWhiteSpace(c.Name))
                .Where(c=>c.ActivityDetails.Any())
                .OrderByDescending(SortContributorsByClosedCount)
                .ToList();

            return result;
        }

        private static int SortContributorsByClosedCount(ActivityItem<List<WorkItemTypeCount>> contributor)
        {
            return contributor.ActivityDetails.Max(m=> m.Visible? m.Closed:0);
        }

        private static T GetValue<T>(Dictionary<string, T> values, string key)
        {
            if (!values.ContainsKey(key))
                return default;

            return values[key];
        }
       
    }

    public class WorkSummary
    {
        public NavigableItem Team { get; set; }
        public List<WorkItemTypeCount> WorkItemTypeCounts { get; set; }
        public List<ActivityItem<List<WorkItemTypeCount>>> Contributors { get; set; }
        public List<WorkItemTypeMetrics> LifespanMetrics { get; set; }
    }

    public class WorkItemTypeCount: IVisibleItem
    {
        public string Type { get; set; }
        public int New { get; set; }
        public int Active { get; set; }
        public int Resolved { get; set; }
        public int Closed { get; set; }
        public WorkItemTypeMetrics Metrics { get; set; }
        public bool Visible { get; set; } = true;
    }
    public class WorkItemTypeMetrics: IVisibleItem
    {
        public string Type { get; set; }
        public bool Visible { get; set; } = true;
        public double? InceptionToActiveDays { get; set; }
        public double? ActiveToResolvedDays { get; set; }
        public double? ResolvedToDoneDays { get; set; }
        public double? ActiveToDoneDays { get; set; }
        public double? TotalEndToEndDays { get; set; }
        public int? TotalStoryPointsCompleted { get; set; }
        public double? StoryPointsCompleted { get; set; }
    }
}