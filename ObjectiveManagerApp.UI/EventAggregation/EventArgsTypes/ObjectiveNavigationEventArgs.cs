namespace ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes
{
    public class ObjectiveNavigationEventArgs : EventArgs
    {
        public int ObjectiveId { get; set; }
        public int ProjectId { get; set; }

        public ObjectiveNavigationEventArgs(int id, int projectId)
        {
            ObjectiveId = id;
            ProjectId = projectId;
        }
    }
}
