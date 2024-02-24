using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for AuthView.xaml
    /// </summary>
    public partial class AuthView : UserControl
    {
        public AuthView(IAuthenticationService authenticationService)
        {
            InitializeComponent();
            DataContext = new AuthViewModel();
            SignInTab.Content = new SignInView(authenticationService);
            SignUpTab.Content = new SignUpView(authenticationService);
            EventAggregator.Instance.GoToSignIn += AuthView_GoToSignIn;
            EventAggregator.Instance.GoToSignUp += AuthView_GoToSignUp;
            EventAggregator.Instance.AuthViewIsLoading += AuthView_AuthViewIsLoading;
            EventAggregator.Instance.AuthViewFinishedLoading += AuthView_AuthViewFinishedLoading;
        }

        private void AuthView_AuthViewIsLoading(object? sender, EventArgs e)
        {
            ((AuthViewModel)DataContext).IsLoading = true;
        }

        private void AuthView_AuthViewFinishedLoading(object? sender, EventArgs e)
        {
            ((AuthViewModel)DataContext).IsLoading = false;
        }

        private void AuthView_GoToSignIn(object? sender, EventArgs e)
        {
            ((AuthViewModel)DataContext).ActiveTabIndex = 0;
        }

        private void AuthView_GoToSignUp(object? sender, EventArgs e)
        {
            ((AuthViewModel)DataContext).ActiveTabIndex = 1;
        }
    }
}
