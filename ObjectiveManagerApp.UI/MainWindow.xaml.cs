using MahApps.Metro.Controls;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using ObjectiveManagerApp.UI.ViewModels;
using ObjectiveManagerApp.UI.Views;

namespace ObjectiveManagerApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IAuthenticationService authService, IProjectService projectService, ICategoryService categoryService)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            EventAggregator.Instance.GoToDashboard += MainWindow_GoToDashboard;

            SignInTab.Content = new SignInView(authService);
            ProjectsTab.Content = new ProjectView(projectService);
            DashboardTab.Content = new DashboardView(categoryService, projectService);
        }

        private void MainWindow_GoToDashboard(object? sender, NavigationEventArgs e)
        {
            ((MainViewModel)DataContext).ActiveTabIndex = 2;
        }
    }
}