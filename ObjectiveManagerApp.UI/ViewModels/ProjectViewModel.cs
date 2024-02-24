namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private bool _isLoading = false;
        private int _activeTabIndex = 0;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public int ActiveTabIndex
        {
            get => _activeTabIndex;
            set
            {
                _activeTabIndex = value;
                OnPropertyChanged(nameof(ActiveTabIndex));
            }
        }
    }
}
