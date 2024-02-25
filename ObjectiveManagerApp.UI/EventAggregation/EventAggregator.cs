using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes;

namespace ObjectiveManagerApp.UI.EventAggregation
{

    public class EventAggregator
    {
        private EventAggregator() { }

        private static EventAggregator? _instance;

        public static EventAggregator Instance
        {
            get => _instance ?? (_instance = new EventAggregator());
        }

        public event EventHandler? Login;
        public event EventHandler? Logout;
        public event EventHandler? AuthViewIsLoading;
        public event EventHandler? AuthViewFinishedLoading;
        public event EventHandler? ProjectViewIsLoading;
        public event EventHandler? ProjectViewFinishedLoading;
        public event EventHandler? DashboardViewIsLoading;
        public event EventHandler? DashboardViewFinishedLoading;
        public event EventHandler? ClearPasswordBox;
        public event EventHandler? GoToProjects;
        public event EventHandler<FormNavigationEventArgs>? GoToProjectEditForm;
        public event EventHandler<FormNavigationEventArgs>? GoToObjectiveEditForm;
        public event EventHandler? GoToSignIn;
        public event EventHandler? GoToSignUp;
        public event EventHandler<NavigationEventArgs>? GoToDashboard;
        public event EventHandler? GoToObjectives;

        public void RaiseLoginEvent()
        {
            Login?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseAuthViewIsLoadingEvent()
        {
            AuthViewIsLoading?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseAuthViewFinishedLoadingEvent()
        {
            AuthViewFinishedLoading?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseProjectViewIsLoadingEvent()
        {
            ProjectViewIsLoading?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseProjectViewFinishedLoadingEvent()
        {
            ProjectViewFinishedLoading?.Invoke(this, EventArgs.Empty);
        }
        
        public void RaiseDashboardViewIsLoadingEvent()
        {
            DashboardViewIsLoading?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseDashboardViewFinishedLoadingEvent()
        {
            DashboardViewFinishedLoading?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseLogoutEvent()
        {
            Logout?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseClearPasswordBoxEvent()
        {
            ClearPasswordBox?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToProjectsEvent()
        {
            GoToProjects?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToProjectEditFormEvent(int projectId, FormType type)
        {
            GoToProjectEditForm?.Invoke(this, new FormNavigationEventArgs(projectId, type));
        }
        
        public void RaiseGoToObjectiveEditFormEvent(int projectId, FormType type)
        {
            GoToObjectiveEditForm?.Invoke(this, new FormNavigationEventArgs(projectId, type));
        }

        public void RaiseGoToSignInEvent()
        {
            GoToSignIn?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToSignUpEvent()
        {
            GoToSignUp?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToDashboardEvent(int projectId)
        {
            GoToDashboard?.Invoke(this, new NavigationEventArgs(projectId));
        }

        public void RaiseGoToObjectivesEvent()
        {
            GoToObjectives?.Invoke(this, EventArgs.Empty);
        }
    }
}
