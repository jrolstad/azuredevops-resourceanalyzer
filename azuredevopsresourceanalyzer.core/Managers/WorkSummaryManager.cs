using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models;
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

            var filteredTeamData = FilterTeams(teamData, teamFilter);
            var teams = filteredTeamData.Select(Map).ToList();

            return new WorkSummary
            {
                Teams = teams
            };
        }

        private IEnumerable<Models.AzureDevops.WebApiTeam> FilterTeams(IEnumerable<Models.AzureDevops.WebApiTeam> teamData, string filter)
        {
            return teamData.Where(t => t.name.ContainsValue(filter));
        }

        private Team Map(Models.AzureDevops.WebApiTeam toMap)
        {
            return new Team
            {
                Id = toMap.id,
                Name = toMap.name,
                Description = toMap.description,
                Url = toMap.url
            };
        }
    }
}