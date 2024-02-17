using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView(ICategoryService categoryService, IProjectService projectService)
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(categoryService, projectService);
            EventAggregator.Instance.GoToDashboard += View_GoToDashboard; ;
        }

        private async void View_GoToDashboard(object? sender, NavigationEventArgs e)
        {
            await ((DashboardViewModel)DataContext).LoadCategoriesAsync();
            await ((DashboardViewModel)DataContext).LoadProjectAsync(e.ProjectId);
        }
    }
}
