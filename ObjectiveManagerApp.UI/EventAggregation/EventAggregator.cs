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
        public event EventHandler? ClearPasswordBox;
        public event EventHandler<NavigationEventArgs>? GoToProjects;
        public event EventHandler<NavigationEventArgs>? GoToProjectEditForm;
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

        public void RaiseLogoutEvent()
        {
            Logout?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseClearPasswordBoxEvent()
        {
            ClearPasswordBox?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToProjectsEvent(int userId)
        {
            GoToProjects?.Invoke(this, new NavigationEventArgs(userId));
        }

        public void RaiseGoToProjectEditFormEvent(int projectId)
        {
            GoToProjectEditForm?.Invoke(this, new NavigationEventArgs(projectId));
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
