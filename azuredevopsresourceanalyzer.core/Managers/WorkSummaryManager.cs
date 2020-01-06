using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<WorkSummary> Search(string organization)
        {
            var data = await _azureDevopsService.GetTeams(organization);
            var teams = data.Select(Map).ToList();

            return new WorkSummary
            {
                Teams = teams
            };
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