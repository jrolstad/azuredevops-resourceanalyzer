using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using azuredevopsresourceanalyzer.core.Services;

namespace azuredevopsresourceanalyzer.core.Managers
{
    public class WorkSummaryManager
    {
        private readonly IAzureDevopsService _azureDevopsService;

        public WorkSummaryManager(IAzureDevopsService azureDevopsService)
        {
            _azureDevopsService = azureDevopsService;
        }
        public async Task<WorkSummary> GetSummary(string organization, 
            string project, 
            string teamFilter=null,
            DateTime? startDate = null)
        {
            var teamData = await _azureDevopsService.GetTeams(organization);

            var filteredTeamData = FilterTeams(teamData, teamFilter).ToList();
            var teamTasks = filteredTeamData.Select(t => ProcessTeams(organization, project, t));
            var teams = await Task.WhenAll(teamTasks);

            return new WorkSummary
            {
                Teams = teams
            };
        }

        private async Task<Team> ProcessTeams(string organization, string project, WebApiTeam teamData)
        {
            var teamFieldValues = await _azureDevopsService.GetTeamFieldValues(organization, project, teamData.name);

            var areaPaths = teamFieldValues.values?
                .Select(v => new Tuple<string, bool>(v.value, v.includeChildren))
                .ToList() ?? new List<Tuple<string, bool>>();
            var workItemReferencesForTeam = await _azureDevopsService.GetWorkItems(organization, project, teamData.name, areaPaths);
            
            var workItemIds = workItemReferencesForTeam
                .Select(w => w.id)
                .ToList();
            var workItems = await _azureDevopsService.GetWorkItems(organization, project, workItemIds);

            var team = Map(teamData, workItems);
            return team;
        }

        private IEnumerable<Models.AzureDevops.WebApiTeam> FilterTeams(IEnumerable<Models.AzureDevops.WebApiTeam> teamData, string filter)
        {
            return teamData.Where(t => t.name.ContainsValue(filter));
        }

        private Team Map(WebApiTeam toMap, List<WorkItem> workItems)
        {
            
            return new Team
            {
                Id = toMap.id,
                Name = toMap.name,
                Description = toMap.description,
                Url = toMap.url,
                WorkItemTypes = Map(workItems)
            };
        }

        private List<TeamWorkItemType> Map(List<WorkItem> workItems)
        {
            var workItemsByType = workItems.GroupBy(i => i.fields["System.WorkItemType"]);
            var teamWorkItems = workItemsByType
                .Select(t => new TeamWorkItemType
                {
                    Type = t.Key.ToString(),
                    StateCount = t.GroupBy(s=>s.fields["System.State"].ToString())
                        .ToDictionary(k=>k.Key,v=>v.Count())
                })
                .ToList();

            return teamWorkItems;
        }
    }
}