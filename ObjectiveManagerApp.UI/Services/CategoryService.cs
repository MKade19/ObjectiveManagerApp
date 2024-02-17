using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;

namespace ObjectiveManagerApp.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        
    }
}
