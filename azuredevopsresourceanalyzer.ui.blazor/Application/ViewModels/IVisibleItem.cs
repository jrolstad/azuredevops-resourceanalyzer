namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public interface IVisibleItem
    {
        bool Visible { get; set; }
        string Type { get; }
    }
}