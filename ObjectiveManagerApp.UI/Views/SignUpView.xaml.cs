using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    { 
        public SignUpView(IAuthenticationService authenticationService)
        {
            InitializeComponent();
            DataContext = new SignUpViewModel(authenticationService);
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).Password = PasswordBox.Password;
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).ConfirmPassword = ConfirmPasswordBox.Password;
        }
    }
}
