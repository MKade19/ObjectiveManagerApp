using MahApps.Metro.Controls;
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
        public MainWindow(IAuthService authService)
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            SignInTab.Content = new SignInView(authService);
            DashboardTab.Content = new DashboardView();
        }
    }
}