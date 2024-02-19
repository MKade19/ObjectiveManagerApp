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
            DataContext = new ProjectViewModel(projectService);
            EventAggregator.Instance.GoToProjects += ProjectView_GoToProjects; ;
        }

        private async void ProjectView_GoToProjects(object? sender, NavigationEventArgs e)
        {
            await ((ProjectViewModel)DataContext).LoadUserProjectsAsync(e.Id);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ((ProjectViewModel)DataContext).ChangeActiveProject(e.AddedItems[0]);
            }
        }
    }
}
