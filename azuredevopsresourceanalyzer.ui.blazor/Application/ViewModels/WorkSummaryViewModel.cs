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

        public WorkSummaryViewModel(WorkSummaryManager manager)
        {
            _manager = manager;
        }

        public string Organization { get; set; }
        public string Error { get; set; }
        public string TeamsFilter { get; set; }
        public List<WorkSummary> Results { get; set; } = new List<WorkSummary>();
        public bool IsSearching = false;

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
                var data = await _manager.Search(this.Organization);

                this.Results = Map(data.Teams);
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

        private List<WorkSummary> Map(IEnumerable<Team> toMap)
        {
            return toMap
                .Select(t=>new WorkSummary
                {
                    Team = new NavigableItem { Name = t.Name,Url=t.Url}
                })
                .ToList();
        }

       
    }

    public class WorkSummary
    {
        public NavigableItem Team { get; set; }
    }
}