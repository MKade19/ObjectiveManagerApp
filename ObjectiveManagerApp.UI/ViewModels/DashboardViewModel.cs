using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;
        private ObservableCollection<Category> _categories;

        public DashboardViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public async Task GetCategoriesAsync()
        {
            Categories = new ObservableCollection<Category>(await _categoryService.GetAllAsync());
        }
    }
}
