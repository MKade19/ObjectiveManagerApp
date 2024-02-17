using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Exceptions;
using System.Windows;

namespace ObjectiveManagerApp.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IRoleRepository _roleRepository;
        private const string IncorrectPasswordErrorMessageName = "IncorrectPasswordMessage";
        private const string UserNotFoundErrorMessageName = "UserNotFoundMessage";

        public AuthService(ITokenService tokenService, IUserRepository userRepository, IHashService hashService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hashService = hashService;
            _roleRepository = roleRepository;
        }

        public async Task<AuthData> LoginAsync(InternalUserData user)
        {
            InternalUserData userFromDB = await _userRepository.GetUserByUsernameAsync(user.Username);

            if (userFromDB.Id == -1) 
            {
                throw new NotFoundException((string)Application.Current.FindResource(UserNotFoundErrorMessageName));
            }

            if (!_hashService.VerifyHash(user.Password, userFromDB.Password, userFromDB.Salt))
            {
                throw new IncorrectPasswordException((string)Application.Current.FindResource(IncorrectPasswordErrorMessageName));
            }

            //Role role = userFromDB.Role;
            //TokenPayload payload = new TokenPayload(userFromDB.Username, role.Name);
            //AuthData authData = new AuthData(await _tokenService.GetTokenAsync(payload), role.Name);

            return new AuthData();
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(InternalUserData user)
        {
            user.Password = _hashService.GenerateHash(user.Password, out byte[] salt);
            user.Salt = salt;
            //user.Role = await _roleRepository.GetRoleById(1);

            await _userRepository.CreateOneAsync(user);
        }
    }
}
