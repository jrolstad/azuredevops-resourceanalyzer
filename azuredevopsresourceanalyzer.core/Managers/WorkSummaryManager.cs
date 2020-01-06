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
            var teams = await ProcessTeams(organization,project, filteredTeamData);

            return new WorkSummary
            {
                Teams = teams
            };
        }

        private async Task<List<Team>> ProcessTeams(string organization, string project, ICollection<WebApiTeam> teamData)
        {
            var teamFieldValueTasks = teamData
                .Select(t => _azureDevopsService.GetTeamFieldValues(organization, project, t.name));

            var teamFieldValues = (await Task.WhenAll(teamFieldValueTasks))
                    .ToDictionary(t=>t.Team)
                ;
            var teams = teamData.Select(t=>Map(t,teamFieldValues)).ToList();
            return teams;
        }

        private IEnumerable<Models.AzureDevops.WebApiTeam> FilterTeams(IEnumerable<Models.AzureDevops.WebApiTeam> teamData, string filter)
        {
            return teamData.Where(t => t.name.ContainsValue(filter));
        }

        private Team Map(WebApiTeam toMap, Dictionary<string, TeamFieldValues> teamFieldValues)
        {
            var areaPaths = new List<string>();
            if (teamFieldValues.ContainsKey(toMap.name))
            {
                areaPaths = teamFieldValues[toMap.name].values?
                    .Select(t => t.value)
                    .ToList();
            }

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