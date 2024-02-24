using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Security;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private InternalUserData _user = new InternalUserData();
        private const string PrincipalIsNotSetErrorMessageName = "PrincipalIsNotSetMessage";

        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand GoToSignUpCommand { get; }
        

        public SignInViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            SubmitCommand = new DelegateCommand(SubmitCommand_Executed);
            GoToSignUpCommand = new DelegateCommand(GoToSignUpCommand_Executed);
            
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

        public async void SubmitCommand_Executed(object sender)
        {
            try
            {
                EventAggregator.Instance.RaiseAuthViewIsLoadingEvent();
                await SignInAsync();
            }
            finally
            {
                EventAggregator.Instance.RaiseAuthViewFinishedLoadingEvent();
                ClearForm();
            }
        }

        public void GoToSignUpCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToSignUpEvent();
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
            EventAggregator.Instance.RaiseLoginEvent();

            if (customPrincipal.IsInRole(nameof(RoleTypes.ProjectManager)))
            {
                EventAggregator.Instance.RaiseGoToProjectsEvent(user.Id);
            }
            else
            {
                EventAggregator.Instance.RaiseGoToObjectivesEvent();
            }
        }

        private void ClearForm()
        {
            Username = string.Empty;
            EventAggregator.Instance.RaiseClearPasswordBoxEvent();
        }
    }
}
