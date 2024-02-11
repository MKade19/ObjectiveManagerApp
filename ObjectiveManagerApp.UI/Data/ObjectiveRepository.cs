using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data
{
    public class ObjectiveRepository : IObjectiveRepository
    {
        public Task CreateOneAsync(Objective entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteOneAsync(Objective entity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IEnumerable<Objective>> GetChunkAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateByIdAsync(Objective entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
