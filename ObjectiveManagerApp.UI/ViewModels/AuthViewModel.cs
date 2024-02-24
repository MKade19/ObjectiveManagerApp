namespace ObjectiveManagerApp.UI.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private int _activeTabIndex = 0;
        private bool _isLoading = false;

        public int ActiveTabIndex 
        { 
            get => _activeTabIndex;
            set 
            {
                _activeTabIndex = value;
                OnPropertyChanged(nameof(ActiveTabIndex));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
    }
}
