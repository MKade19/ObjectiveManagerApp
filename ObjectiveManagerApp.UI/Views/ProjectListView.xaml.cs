using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ProjectListView.xaml
    /// </summary>
    public partial class ProjectListView : UserControl
    {
        public ProjectListView(IProjectService projectService)
        {
            InitializeComponent();
            DataContext = new ProjectListViewModel(projectService);
            EventAggregator.Instance.GoToProjects += ProjectView_GoToProjects;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ((ProjectListViewModel)DataContext).ChangeActiveProject(e.AddedItems[0]);
            }
        }

        private async void ProjectView_GoToProjects(object? sender, EventArgs e)
        {
            await ((ProjectListViewModel)DataContext).LoadUserProjectsAsync();
        }
    }
}
