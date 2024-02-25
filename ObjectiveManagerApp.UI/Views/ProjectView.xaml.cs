using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ProjectView(IProjectService projectService)
        {
            InitializeComponent();
            DataContext = new ProjectViewModel();
            
            ListTab.Content = new ProjectListView(projectService);
            EditFormTab.Content = new ProjectEditFormView(projectService);

            EventAggregator.Instance.GoToProjectEditForm += ProjectView_GoToProjectEditForm;
            EventAggregator.Instance.GoToProjects += ProjectView_GoToProjects;
            EventAggregator.Instance.ProjectViewIsLoading += ProjectView_ProjectViewIsLoading;
            EventAggregator.Instance.ProjectViewFinishedLoading += ProjectView_ProjectViewFinishedLoading; ;
        }

        private void ProjectView_ProjectViewIsLoading(object? sender, EventArgs e)
        {
            ((ProjectViewModel)DataContext).IsLoading = true;
        }

        private void ProjectView_ProjectViewFinishedLoading(object? sender, EventArgs e)
        {
            ((ProjectViewModel)DataContext).IsLoading = false;
        }

        private void ProjectView_GoToProjects(object? sender, EventArgs e)
        {
            ((ProjectViewModel)DataContext).ActiveTabIndex = 0;
        }

        private void ProjectView_GoToProjectEditForm(object? sender, FormNavigationEventArgs e)
        {
            ((ProjectViewModel)DataContext).ActiveTabIndex = 1;
        }
    }
}
