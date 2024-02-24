using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ProjectEditFormView.xaml
    /// </summary>
    public partial class ProjectEditFormView : UserControl
    {
        public ProjectEditFormView(IProjectService projectService)
        {
            InitializeComponent();
            DataContext = new ProjectEditFormViewModel(projectService);
            EventAggregator.Instance.GoToProjectEditForm += ProjectInfo_GoToProjectInfo;
        }

        private async void ProjectInfo_GoToProjectInfo(object? sender, EventAggregation.EventArgsTypes.NavigationEventArgs e)
        {
            await ((ProjectEditFormViewModel)DataContext).LoadProjectAsync(e.Id);
        }
    }
}
