using ObjectiveManagerApp.UI.EventAggregation;
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
        public SignInView(IAuthenticationService authservice)
        {
            InitializeComponent();
            DataContext = new SignInViewModel(authservice);
            EventAggregator.Instance.ClearPasswordBox += SignInForm_ClearPasswordBox;
        }

        private void SignInForm_ClearPasswordBox(object? sender, EventArgs e)
        {
            PasswordBox.Password = string.Empty;
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((SignInViewModel)DataContext).Password = PasswordBox.Password;
        }
    }
}
