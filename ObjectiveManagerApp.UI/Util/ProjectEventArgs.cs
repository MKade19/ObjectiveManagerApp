using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Util
{
    public class ProjectEventArgs : EventArgs
    {
        public Project Project { get; set; }

        public ProjectEventArgs(Project project)
        {
            Project = project;
        }
    }
}
