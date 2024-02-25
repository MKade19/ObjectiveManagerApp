using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Security;
using ObjectiveManagerApp.UI.Util;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _activeTabIndex = 0;
        private string LogoutMessageName = "LogoutMessage";

        public DelegateCommand GoToProjectsTabCommand { get; }
        public DelegateCommand GoToObjectivesTabCommand { get; }
        public DelegateCommand LogoutCommand { get; }

        public MainViewModel()
        {
            GoToProjectsTabCommand = new DelegateCommand(GoToProjectsTabCommand_Executed);
            GoToObjectivesTabCommand = new DelegateCommand(GoToObjectivesTabCommand_Executed);
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

        public bool IsAuthenticated 
        { 
            get => Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public bool IsAdmin
        {
            get => Thread.CurrentPrincipal.IsInRole(nameof(RoleType.Admin));
        }

        public bool IsProjectManager
        {
            get => Thread.CurrentPrincipal.IsInRole(nameof(RoleType.ProjectManager));
        }

        public void GoToProjectsTabCommand_Executed(object sender)
        {
            ActiveTabIndex = 1;
        }

        public void GoToObjectivesTabCommand_Executed(object sender)
        {
            ActiveTabIndex = 3;
            EventAggregator.Instance.RaiseGoToObjectivesEvent();
        }

        public void LogoutCommand_Executed(object sender)
        {
            ActiveTabIndex = 0;
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            customPrincipal.Identity = new AnonymousIdentity();

            RefreshIsAuthencated();
            MessageBoxStore.Information((string)Application.Current.FindResource(LogoutMessageName));
            EventAggregator.Instance.RaiseLogoutEvent();
        }

        public void RefreshIsAuthencated()
        {
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsProjectManager));
        }
    }
}
