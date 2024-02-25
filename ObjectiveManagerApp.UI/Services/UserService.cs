using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;

namespace ObjectiveManagerApp.UI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task DeleteOneAsync(PublicUserData project)
        {
            throw new NotImplementedException();
        }

        public Task EditByIdAsync(PublicUserData project)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PublicUserData>> GetAllAsync()
        {
            return await _userRepository.GetPublicDataAsync();
        }

        public Task<PublicUserData> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
