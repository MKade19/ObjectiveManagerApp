using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
