using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;
using System.Windows;

namespace ObjectiveManagerApp.UI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHashService _hashService;
        private const string IncorrectPasswordErrorMessageName = "IncorrectPasswordMessage";
        private const string UserNotFoundErrorMessageName = "UserNotFoundMessage";

        public AuthenticationService(IUserRepository userRepository, IRoleRepository roleRepository,IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _roleRepository = roleRepository;
        }

        public async Task<User> AuthenticateUserAsync(InternalUserData userFromUi)
        {
            InternalUserData userData = await _userRepository.GetUserByUsernameAsync(userFromUi.Username);

            if (userData == null)
            {
                throw new UnauthorizedAccessException((string)Application.Current.FindResource(UserNotFoundErrorMessageName));
            }

            if (!_hashService.VerifyHash(userFromUi.Password, userData.Password, userData.Salt))
            {
                throw new UnauthorizedAccessException((string)Application.Current.FindResource(IncorrectPasswordErrorMessageName));
            }

            return new User(userData.Id, userData.Username, userData.Fullname, userData.Roles);
        }

        public async Task RegisterAsync(InternalUserData user)
        {
            user.Password = _hashService.GenerateHash(user.Password, out byte[] salt);
            user.Salt = salt;
            user.Roles = new List<Role>();

            await _userRepository.CreateOneAsync(user);
        }
    }
}
