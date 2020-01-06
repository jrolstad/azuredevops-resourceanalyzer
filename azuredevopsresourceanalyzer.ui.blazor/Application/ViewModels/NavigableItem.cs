namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class NavigableItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class SelectableItem
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}