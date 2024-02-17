using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Security;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private bool _isSigningUp = false;
        private bool _isLoading = false;
        private InternalUserData _user = new InternalUserData();
        private const string PrincipalIsNotSetErrorMessageName = "PrincipalIsNotSetMessage";
        private const string SignedUpMessageName = "SignedUpMessage";
        private const string SignedInMessageName = "SignedInMessage";

        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand GoToSignUpCommand { get; }
        public DelegateCommand BackToSignInCommand { get; }

        public SignInViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            SubmitCommand = new DelegateCommand(SubmitCommand_Executed);
            GoToSignUpCommand = new DelegateCommand(GoToSignUpCommand_Executed);
            BackToSignInCommand = new DelegateCommand(BackToSignInCommand_Executed);
        }

        public string Username {
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

        public string Fullname
        {
            get => _user.Fullname;
            set
            {
                _user.Fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }

        public bool IsSigningUp
        {
            get => _isSigningUp;
            set
            {
                _isSigningUp = value;
                OnPropertyChanged(nameof(IsSigningUp));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public async void SubmitCommand_Executed(object sender)
        {
            try
            {
                IsLoading = true;

                if (IsSigningUp)
                { 
                    await SignUpAsync();
                }
                else
                {
                    await SignInAsync();
                }
            }
            finally
            {
                IsLoading = false;
                ClearForm();
            }
        }

        public void GoToSignUpCommand_Executed(object sender)
        {
            IsSigningUp = true;
        }

        public void BackToSignInCommand_Executed(object sender)
        {
            IsSigningUp = false;
        }

        private async Task SignInAsync()
        {
            User user = await _authenticationService.AuthenticateUserAsync(_user);

            CustomPrincipal? customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
            {
                throw new ArgumentException((string)Application.Current.FindResource(PrincipalIsNotSetErrorMessageName));
            }

            customPrincipal.Identity = new CustomIdentity(user.Username, user.Roles);

            MessageBoxStore.Information((string)Application.Current.FindResource(SignedInMessageName));
        }

        private async Task SignUpAsync()
        {
            await _authenticationService.RegisterAsync(_user);
            MessageBoxStore.Information((string)Application.Current.FindResource(SignedUpMessageName));
        }

        private void ClearForm()
        {
            Username = string.Empty;
            Fullname = string.Empty;
            EventAggregator.Instance.RaiseClearPasswordBoxEvent();
        }
    }
}
