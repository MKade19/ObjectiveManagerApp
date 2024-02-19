namespace ObjectiveManagerApp.UI.EventAggregation
{
    public class NavigationEventArgs : EventArgs
    {
        public int Id { get; set; }

        public NavigationEventArgs(int id)
        {
            Id = id;
        }
    }
}
