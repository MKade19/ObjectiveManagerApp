using MahApps.Metro.Controls;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using ObjectiveManagerApp.UI.Views;

namespace ObjectiveManagerApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IAuthenticationService authService, IProjectService projectService, ICategoryService categoryService, IObjectiveService objectiveService)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            EventAggregator.Instance.GoToDashboard += MainWindow_GoToDashboard;
            EventAggregator.Instance.GoToObjectives += MainWindow_GoToObjectives;
            EventAggregator.Instance.GoToProjects += MainWindow_GoToProjects;
            EventAggregator.Instance.Login += Mainindow_Login;

            AuthTab.Content = new AuthView(authService);
            ProjectsTab.Content = new ProjectView(projectService);
            DashboardTab.Content = new DashboardView(categoryService, projectService, objectiveService);
            ObjectivesTab.Content = new ObjectivesView(objectiveService);
        }

        private void Mainindow_Login(object? sender, EventArgs e)
        {
            ((MainViewModel)DataContext).RefreshIsAuthencated();
        }

        private void MainWindow_GoToProjects(object? sender, EventArgs e)
        {
            ((MainViewModel)DataContext).ActiveTabIndex = 1;
        }

        private void MainWindow_GoToDashboard(object? sender, NavigationEventArgs e)
        {
            ((MainViewModel)DataContext).ActiveTabIndex = 2;
        }

        private void MainWindow_GoToObjectives(object? sender, EventArgs e)
        {
            ((MainViewModel)DataContext).ActiveTabIndex = 3;
        }
    }
}