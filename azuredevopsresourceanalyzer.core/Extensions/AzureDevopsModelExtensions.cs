using System.Collections.Generic;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using Newtonsoft.Json.Linq;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class AzureDevopsModelExtensions
    {
        public static string WorkItemType(this WorkItem item)
        {
            item.fields.TryGetValue("System.WorkItemType", out object value);
            return value?.ToString();
        }

        public static string State(this WorkItem item)
        {
            item.fields.TryGetValue("System.State", out object value);
            return value?.ToString();
        }

        public static string AssignedToName(this WorkItem item)
        {
            var containsAssignedTo = item.fields.TryGetValue("System.AssignedTo", out var value);

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