namespace ObjectiveManagerApp.UI.Util
{

    public class EventAggregator
    {
        private EventAggregator() { }

        private static EventAggregator? _instance;

        public static EventAggregator Instance
        {
            get => _instance ?? (_instance = new EventAggregator());
        }

        public event EventHandler? ClearPasswordBox;
        public event EventHandler<NavigationEventArgs>? GoToDashboard;

        public void RaiseClearPasswordBoxEvent()
        {
            ClearPasswordBox?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseGoToDashboardEvent(int projectId)
        {
            GoToDashboard?.Invoke(this, new NavigationEventArgs(projectId));
        }
    }
}
