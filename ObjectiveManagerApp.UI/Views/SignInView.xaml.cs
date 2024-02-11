using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView(IAuthService authservice)
        {
            InitializeComponent();
            DataContext = new SignInViewModel(authservice);
        }
    }
}
