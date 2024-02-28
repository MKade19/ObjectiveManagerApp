using MahApps.Metro.Controls;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using ObjectiveManagerApp.UI.Views;
using System.Globalization;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IAuthenticationService authService, IProjectService projectService, ICategoryService categoryService, IObjectiveService objectiveService, IUserService userService)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            EventAggregator.Instance.GoToDashboard += MainWindow_GoToDashboard;
            EventAggregator.Instance.GoToObjectives += MainWindow_GoToObjectives;
            EventAggregator.Instance.GoToProjects += MainWindow_GoToProjects;
            EventAggregator.Instance.Login += Mainindow_Login;

            AuthTab.Content = new AuthView(authService);
            ProjectsTab.Content = new ProjectView(projectService);
            DashboardTab.Content = new DashboardView(categoryService, projectService, objectiveService, userService);
            ObjectivesTab.Content = new ObjectivesView(objectiveService);

            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            LanguageMenu.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                LanguageMenu.Items.Add(menuLang);
            }
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

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            foreach (MenuItem i in LanguageMenu.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }
    }
}