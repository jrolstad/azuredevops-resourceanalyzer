using System;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class ActivityItem<T>
    {
        public string Name { get; set; }
        public int? ActivityCount { get; set; }
        public DateTime? LastActivity { get; set; }

        public T ActivityDetails { get; set; }
    }
}