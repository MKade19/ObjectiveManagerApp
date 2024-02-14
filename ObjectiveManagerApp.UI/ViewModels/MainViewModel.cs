using ObjectiveManagerApp.UI.Util;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _activeTabIndex = 0;

        public DelegateCommand GoToProjectsTabCommand { get; }
        public DelegateCommand GoToDashboardTabCommand { get; }
        public DelegateCommand LogoutCommand { get; }

        public MainViewModel()
        {
            GoToProjectsTabCommand = new DelegateCommand(GoToProjectsTabCommand_Executed);
            GoToDashboardTabCommand = new DelegateCommand(GoToDashboardTabCommand_Executed);
            LogoutCommand = new DelegateCommand(LogoutCommand_Executed);
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

        public void GoToProjectsTabCommand_Executed(object sender)
        {
            ActiveTabIndex = 1;
        }

        public void GoToDashboardTabCommand_Executed(object sender)
        {
            ActiveTabIndex = 2;
        }

        public void LogoutCommand_Executed(object sender)
        {
            ActiveTabIndex = 0;
        }
    }
}
