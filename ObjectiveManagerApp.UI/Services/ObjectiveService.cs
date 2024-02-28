using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;

namespace ObjectiveManagerApp.UI.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly IUserRepository _userRepository;

        public ObjectiveService(IObjectiveRepository objectiveRepository, IUserRepository userRepository)
        {
            _objectiveRepository = objectiveRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Objective>> GetObjectivesByUsername(string username)
        { 
            int userId = (await _userRepository.GetByUsernameAsync(username)).Id;

            return await _objectiveRepository.GetByUserIdAsync(userId);
        }

        public async Task CreateOneAsync(Objective objective)
        {
            await _objectiveRepository.CreateOneAsync(objective);
        }

        public async Task EditByIdAsync(Objective objective)
        {
            await _objectiveRepository.UpdateByIdAsync(objective);
        }

        public async Task<Objective> GetByIdAsync(int id)
        {
            return await _objectiveRepository.GetByIdAsync(id);
        }

        public async Task DeleteOneAsync(Objective objective)
        {
            await _objectiveRepository.DeleteOneAsync(objective);
        }
    }
}
