using System;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class SelectableItem
    {
        public event EventHandler SelectedChanged;
        public string Name { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                SelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}