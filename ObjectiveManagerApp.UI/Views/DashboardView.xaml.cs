using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
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
            await ((DashboardViewModel)DataContext).LoadProjectAsync(e.Id);
            ((DashboardViewModel)DataContext).MakeCategorizedObjectives();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] == null)
            {
                return;
            }

            ((DashboardViewModel)DataContext).ChangeActiveObjective(e.AddedItems[0]);
        }
    }
}
