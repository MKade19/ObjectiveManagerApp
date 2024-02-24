using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class SignUpViewModel: ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private const string PasswordsDoesNotMatchMessageName = "PasswordsDoesNotMatchMessage";
        private const string SignedUpMessageName = "SignedUpMessage";
        private InternalUserData _user = new InternalUserData();
        private string _confirmPassword = string.Empty;
        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand BackToSignInCommand { get; }

        public string Username
        {
            get => _user.Username;
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _user.Password;
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Fullname
        {
            get => _user.Fullname;
            set
            {
                _user.Fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }

        public SignUpViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            SubmitCommand = new DelegateCommand(SubmitCommand_Executed);
            BackToSignInCommand = new DelegateCommand(BackToSignInCommand_Executed);
        }

        public async void SubmitCommand_Executed(object sender)
        {
            try
            {
                EventAggregator.Instance.RaiseAuthViewIsLoadingEvent();

                if (!ConfirmPassword.Equals(Password))
                {
                    MessageBoxStore.Warning((string)Application.Current.FindResource(PasswordsDoesNotMatchMessageName));
                    return;
                }

                await _authenticationService.RegisterAsync(_user);
                MessageBoxStore.Information((string)Application.Current.FindResource(SignedUpMessageName));
                EventAggregator.Instance.RaiseGoToSignInEvent();
            }
            finally
            {
                EventAggregator.Instance.RaiseAuthViewFinishedLoadingEvent();
            }
        }

        public void BackToSignInCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToSignInEvent();
        }
    }
}
