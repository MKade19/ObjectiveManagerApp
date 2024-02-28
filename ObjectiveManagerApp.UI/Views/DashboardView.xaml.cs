using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes;
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
        public DashboardView(ICategoryService categoryService, IProjectService projectService, IObjectiveService objectiveService, IUserService userService)
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(categoryService, projectService, objectiveService);
            EditFormTab.Content = new ObjectiveEditFormView(objectiveService, categoryService, userService);
            EventAggregator.Instance.GoToDashboard += View_GoToDashboard;
            EventAggregator.Instance.DashboardViewIsLoading += View_DashboardViewIsLoading;
            EventAggregator.Instance.DashboardViewFinishedLoading += View_DashboardViewFinishedLoading;
            EventAggregator.Instance.GoToObjectiveEditForm += View_GoToObjectiveEditForm;
            EventAggregator.Instance.BackToDashboard += View_BackToDashboard;
        }

        private async void View_BackToDashboard(object? sender, EventArgs e)
        {
            await ((DashboardViewModel)DataContext).LoadCategoriesAsync();
            ((DashboardViewModel)DataContext).MakeCategorizedObjectives();
            ((DashboardViewModel)DataContext).ActiveTabIndex = 0;
        }

        private void View_GoToObjectiveEditForm(object? sender, ObjectiveNavigationEventArgs e)
        {
            ((DashboardViewModel)DataContext).ActiveTabIndex = 1;
        }

        private void View_DashboardViewIsLoading(object? sender, EventArgs e)
        {
            ((DashboardViewModel)DataContext).IsLoading = true;
        }

        private void View_DashboardViewFinishedLoading(object? sender, EventArgs e)
        {
            ((DashboardViewModel)DataContext).IsLoading = false;
        }

        private async void View_GoToDashboard(object? sender, NavigationEventArgs e)
        {
            await ((DashboardViewModel)DataContext).LoadCategoriesAsync();
            await ((DashboardViewModel)DataContext).LoadProjectAsync(e.Id);
            ((DashboardViewModel)DataContext).MakeCategorizedObjectives();
            ((DashboardViewModel)DataContext).ActiveTabIndex = 0;
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
