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
            string teamFilter = null,
            DateTime? startDate = null)
        {
            var teamData = await _azureDevopsService.GetTeams(organization);

            var filteredTeamData = FilterTeams(teamData, teamFilter).ToList();
            var teamTasks = filteredTeamData.Select(t => ProcessTeams(organization, project, startDate, t));
            var teams = await Task.WhenAll(teamTasks);

            return new WorkSummary
            {
                Teams = teams
            };
        }

        private async Task<Team> ProcessTeams(string organization, string project, DateTime? startDate, WebApiTeam teamData)
        {
            var teamFieldValues = await _azureDevopsService.GetTeamFieldValues(organization, project, teamData.name);

            var areaPaths = teamFieldValues.values?
                                .Select(v => new Tuple<string, bool>(v.value, v.includeChildren))
                                .ToList() ?? new List<Tuple<string, bool>>();
            var workItemReferencesForTeam =
                await _azureDevopsService.GetWorkItems(organization, project, teamData.name, areaPaths) ?? new List<WorkItemReference>();

            var workItemIds = workItemReferencesForTeam
                .Select(w => w.id)
                .ToList();
            var workItems = await _azureDevopsService.GetWorkItems(organization, project, workItemIds);

            var workItemsInDateRange = workItems.Where(w => w.LastUpdatedAt() > startDate.GetValueOrDefault(DateTime.MinValue))
                .ToList();
            var team = Map(organization,project, teamData, workItemsInDateRange);
            return team;
        }

        private IEnumerable<Models.AzureDevops.WebApiTeam> FilterTeams(
            IEnumerable<Models.AzureDevops.WebApiTeam> teamData, string filter)
        {
            return teamData.Where(t => t.name.ContainsValue(filter));
        }

        private Team Map(string organization, string project,WebApiTeam toMap, List<WorkItem> workItems)
        {

            return new Team
            {
                Id = toMap.id,
                Name = toMap.name,
                Description = toMap.description,
                Url = $"https://dev.azure.com/{organization}/{project}/_backlogs/backlog/{toMap.name}",
                WorkItemTypes = MapWorkItemType(workItems),
                Contributors = MapWorkItemContributor(workItems),

            };
        }

        private List<TeamWorkItemType> MapWorkItemType(IEnumerable<WorkItem> workItems)
        {
            var workItemsByType = workItems.GroupBy(i => i.WorkItemType());
            var teamWorkItems = workItemsByType
                .AsParallel()
                .Select(t => new TeamWorkItemType
                {
                    Type = t.Key.ToString(),
                    StateCount = t.GroupBy(s => s.State())
                        .ToDictionary(k => k.Key, v => v.Count()),
                    Metrics = MapMetrics(t)
                })
                .ToList();

            return teamWorkItems;
        }

        private TeamWorkItemTypeMetrics MapMetrics(IGrouping<string, WorkItem> workItems)
        {
            var workItemsToMeasure = workItems
                .Where(w => w.State() != WorkItemStates.Removed)
                .Select(w=> new
                {
                    createdAt = w.CreatedAt(),
                    activatedAt = w.ActivedAt(),
                    resolvedAt = w.ResolvedAt(),
                    closedAt = w.ClosedAt()
                }).ToList();

            var createdToActive = workItemsToMeasure
                .Where(w=>w.activatedAt.HasValue && w.createdAt.HasValue)
                .Median(w => DaysApart(w.activatedAt, w.createdAt));

            var activeToResolved = workItemsToMeasure
                .Where(w => w.resolvedAt.HasValue && w.activatedAt.HasValue)
                .Median(w => DaysApart(w.resolvedAt, w.activatedAt));

            var resolvedToComplete = workItemsToMeasure
                .Where(w => w.closedAt.HasValue && w.resolvedAt.HasValue)
                .Median(w => DaysApart(w.closedAt, w.resolvedAt));

            var activeToComplete = workItemsToMeasure
                .Where(w => w.closedAt.HasValue && w.activatedAt.HasValue)
                .Median(w => DaysApart(w.closedAt, w.activatedAt));

            var totalEndToEnd = workItemsToMeasure
                .Where(w => w.closedAt.HasValue && w.createdAt.HasValue)
                .Median(w => DaysApart(w.closedAt, w.createdAt));

            return new TeamWorkItemTypeMetrics
            {
                InceptionToActiveDays = createdToActive,
                ActiveToResolvedDays = activeToResolved,
                ResolvedToDoneDays = resolvedToComplete,
                ActiveToDoneDays = activeToComplete,
                TotalEndToEndDays = totalEndToEnd
            };
        }

        private static double? DaysApart(DateTime? value1, DateTime? value2)
        {
            if (!value1.HasValue || !value2.HasValue)
                return null;
            var daysDifferent = value1?.Subtract(value2.Value).TotalDays;
            return daysDifferent;
        }

        private List<TeamWorkItemContributor> MapWorkItemContributor(IEnumerable<WorkItem> workItems)
        {
            var workItemsByContributor = workItems.GroupBy(i => i.AssignedToName());
            var contributors = workItemsByContributor
                .AsParallel()
                .Select(w => new TeamWorkItemContributor
                {
                    Contributor = w.Key,
                    WorkItemTypes = MapWorkItemType(w)
                })
                .ToList();
            return contributors;
        }
    }
}