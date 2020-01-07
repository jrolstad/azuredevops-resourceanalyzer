using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Models;

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
            if (string.IsNullOrWhiteSpace(this.Organization))
            {
                Error = "Unable to search; please enter an organization first";
                this.Results = new List<WorkSummary>();

                return;
            }

            try
            {
                Error = null;

                IsSearching = true;
                var data = await _manager.GetSummary(this.Organization, this.Project, this.TeamsFilter);

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

        private void FilterWorkItems()
        {
            if (!this.AvailableWorkItemTypes.Any())
                return;

            var visibleWorkItemTypes = this.AvailableWorkItemTypes
                .Where(t => t.IsSelected)
                .ToDictionary(t => t.Name);

            this.Results
                .SelectMany(r=>r.WorkItemTypeCounts)
                .ToList()
                .ForEach(w => { w.Visible = visibleWorkItemTypes.ContainsKey(w.Type); });

            this.Results
                .SelectMany(r => r.Contributors)
                .SelectMany(c=>c.ActivityDetails)
                .ToList()
                .ForEach(w => { w.Visible = visibleWorkItemTypes.ContainsKey(w.Type); });

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
            FilterWorkItems();
        }

        private List<WorkSummary> Map(IEnumerable<Team> toMap)
        {
            return toMap
                .Select(t=>new WorkSummary
                {
                    Team = new NavigableItem { Name = t.Name,Url=t.Url},
                    WorkItemTypeCounts = Map(t.WorkItemTypes),
                    Contributors = Map(t.Contributors)
                })
                .OrderBy(t=>t.Team?.Name)
                .ToList();
        }

        private List<WorkItemTypeCount> Map(List<TeamWorkItemType> toMap)
        {
            return toMap.Select(m => new WorkItemTypeCount
                    {
                        Type = m.Type,
                        Active = GetValue(m.StateCount, "Active"),
                        New = GetValue(m.StateCount, "New"),
                        Resolved = GetValue(m.StateCount, "Resolved"),
                        Closed = GetValue(m.StateCount, "Closed"),
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
                .OrderByDescending(c=>c.ActivityDetails.Max(m=>m.Closed))
                .ToList();

            return result;
        }

        private static T GetValue<T>(Dictionary<string, T> values, string key)
        {
            if (!values.ContainsKey(key))
                return default(T);

            return values[key];
        }
       
    }

    public class WorkSummary
    {
        public NavigableItem Team { get; set; }
        public List<WorkItemTypeCount> WorkItemTypeCounts { get; set; }
        public List<ActivityItem<List<WorkItemTypeCount>>> Contributors { get; set; }
    }

    public class WorkItemTypeCount
    {
        public string Type { get; set; }
        public int New { get; set; }
        public int Active { get; set; }
        public int Resolved { get; set; }
        public int Closed { get; set; }

        public bool Visible { get; set; } = true;
    }
}