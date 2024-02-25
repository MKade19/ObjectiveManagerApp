using ObjectiveManagerApp.UI.Constants;
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
            EventAggregator.Instance.GoToProjectEditForm += EditForm_GoToProjectEditForm; ;
        }

        private async void EditForm_GoToProjectEditForm(object? sender, EventAggregation.EventArgsTypes.FormNavigationEventArgs e)
        {
            bool isCreated = e.Type == FormType.Create;
            ((ProjectEditFormViewModel)DataContext).IsCreated = isCreated;

            if (!isCreated)
            {
                await ((ProjectEditFormViewModel)DataContext).LoadProjectAsync(e.Id);
            }
        }
    }
}
