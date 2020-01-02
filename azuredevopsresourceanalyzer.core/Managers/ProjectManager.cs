using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Models;
using azuredevopsresourceanalyzer.core.Services;

namespace azuredevopsresourceanalyzer.core.Managers
{
    public class ProjectManager
    {
        private readonly AzureDevopsService _azureDevopsService;

        public ProjectManager(AzureDevopsService azureDevopsService)
        {
            _azureDevopsService = azureDevopsService;
        }
        public async Task<List<Project>> Get(string organization)
        {
            var data = await _azureDevopsService.GetProjects(organization);
            var result = data.Select(Map).ToList();

            return result;
        }

        private Project Map(Models.AzureDevops.Project toMap)
        {
            return new Project
            {
                Name = toMap.name
            };
        }
    }
}