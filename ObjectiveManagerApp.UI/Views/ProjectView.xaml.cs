using ObjectiveManagerApp.UI.EventAggregation;
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
        }

        private void ProjectView_GoToProjects(object? sender, EventAggregation.EventArgsTypes.NavigationEventArgs e)
        {
            ((ProjectViewModel)DataContext).ActiveTabIndex = 0;
        }

        private void ProjectView_GoToProjectEditForm(object? sender, EventAggregation.EventArgsTypes.NavigationEventArgs e)
        {
            ((ProjectViewModel)DataContext).ActiveTabIndex = 1;
        }
    }
}
