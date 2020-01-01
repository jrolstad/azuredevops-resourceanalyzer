using System;
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

        public ProjectSummaryViewModel(ProjectSummaryManager manager)
        {
            _manager = manager;
        }
        public string Organization { get; set; }
        public string Project { get; set; }
        public string RepositoryFilter { get; set; }
        public DateTime? StartDate { get; set; }

        public List<ProjectSummary> Results { get; set; } = new List<ProjectSummary>();

        public async Task Search()
        {
            try
            {
                var data = await _manager.GetSummary(Organization, Project, RepositoryFilter, StartDate);
                Results = data?.Components
                    .Select(Map)
                    .OrderBy(d => d.Repository)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        private ProjectSummary Map(Component toMap)
        {
            return new ProjectSummary
            {
                Repository = toMap?.Repository.Name,
                RepositoryUrl = toMap?.Repository.Url
            };
        }
    }

    public class ProjectSummary
    {
        public string Repository { get; set; }
        public string RepositoryUrl { get; set; }
    }
}