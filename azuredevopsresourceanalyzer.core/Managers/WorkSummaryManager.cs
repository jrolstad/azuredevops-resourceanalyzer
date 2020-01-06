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
            var teamworkItems = await _azureDevopsService.GetWorkItems(organization, project, teamData.name, areaPaths);

            var team = Map(teamData, teamFieldValues);
            return team;
        }

        private IEnumerable<Models.AzureDevops.WebApiTeam> FilterTeams(IEnumerable<Models.AzureDevops.WebApiTeam> teamData, string filter)
        {
            return teamData.Where(t => t.name.ContainsValue(filter));
        }

        private Team Map(WebApiTeam toMap, TeamFieldValues teamFieldValues)
        {
            var areaPaths = teamFieldValues.values?
                .Select(t => t.value)
                .ToList();

            return new Team
            {
                Id = toMap.id,
                Name = toMap.name,
                Description = toMap.description,
                Url = toMap.url,
                AreaPaths = areaPaths
            };
        }
    }
}