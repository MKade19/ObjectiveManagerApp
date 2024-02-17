namespace ObjectiveManagerApp.UI.Util
{
    public class NavigationEventArgs : EventArgs
    {
        public int ProjectId { get; set; }

        public NavigationEventArgs(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
