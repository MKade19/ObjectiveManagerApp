namespace ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes
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
