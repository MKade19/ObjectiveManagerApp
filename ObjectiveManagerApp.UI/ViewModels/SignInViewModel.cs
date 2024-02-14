using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private bool _isSigningUp = false;
        private bool _isLoading = false;
        private User _user = new User();

        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand GoToSignUpCommand { get; }
        public DelegateCommand BackToSignInCommand { get; }

        public SignInViewModel(IAuthService authService)
        {
            _authService = authService;
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
            AuthData authData = await _authService.LoginAsync(_user);
            Properties.Settings.Default.AccessToken = authData.Token;
            Properties.Settings.Default.Role = authData.Role;
        }

        private async Task SignUpAsync()
        {
            await _authService.RegisterAsync(_user);
        }

        private void ClearForm()
        {
            Username = string.Empty;
            Password = string.Empty;
            Fullname = string.Empty;
            _user = new User();
        }
    }
}
