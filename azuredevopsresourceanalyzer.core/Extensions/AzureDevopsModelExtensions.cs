using System;
using System.Collections.Generic;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using Newtonsoft.Json.Linq;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class AzureDevopsModelExtensions
    {
        public static string WorkItemType(this WorkItem item)
        {
            item.fields.TryGetValue(WorkItemFieldNames.WorkItemType, out var value);
            return value?.ToString();
        }

        public static string IterationPath(this WorkItem item)
        {
            item.fields.TryGetValue(WorkItemFieldNames.IterationPath, out var value);
            return value?.ToString();
        }

        public static string State(this WorkItem item)
        {
            item.fields.TryGetValue(WorkItemFieldNames.State, out var value);
            return value?.ToString();
        }

        public static DateTime? CreatedAt(this WorkItem item)
        {
            return item.GetWorkItemDateValue(WorkItemFieldNames.CreatedDate);
        }
        public static DateTime? ActivedAt(this WorkItem item)
        {
            return item.GetWorkItemDateValue(WorkItemFieldNames.ActivatedDate);
        }
        public static DateTime? ResolvedAt(this WorkItem item)
        {
            return item.GetWorkItemDateValue(WorkItemFieldNames.ResolvedDate);
        }
        public static DateTime? ClosedAt(this WorkItem item)
        {
            return item.GetWorkItemDateValue(WorkItemFieldNames.ClosedDate);
        }
        public static DateTime? LastUpdatedAt(this WorkItem item)
        {
            return item.GetWorkItemDateValue(WorkItemFieldNames.ChangedDate);
        }

        public static int? StoryPoints(this WorkItem item)
        {
            return item.GetWorkItemIntValue(WorkItemFieldNames.StoryPoints);
        }

        private static DateTime? GetWorkItemDateValue(this WorkItem item, string fieldName)
        {
            item.fields.TryGetValue(fieldName, out var value);
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return null;
            DateTime.TryParse(value.ToString(), out var dateTimeValue);
            return dateTimeValue;
        }

        private static int? GetWorkItemIntValue(this WorkItem item, string fieldName)
        {
            item.fields.TryGetValue(fieldName, out var value);
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return null;
            int.TryParse(value.ToString(), out var intValue);
            return intValue;
        }

        public static string AssignedToName(this WorkItem item)
        {
            var containsAssignedTo = item.fields.TryGetValue(WorkItemFieldNames.AssignedTo, out var value);

            if (!containsAssignedTo)
                return null;

            var values = (Newtonsoft.Json.Linq.JObject) value;

            var containsDisplayName = values.TryGetValue("displayName", out var name);
            if (!containsDisplayName)
                return null;

            return name?.ToString();
        }
    }
}