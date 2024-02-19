using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<CategorizedTaskList> _categorizedTasks;
        
        private Project _project = new Project();

        public DashboardViewModel(ICategoryService categoryService, IProjectService projectService)
        {
            _categoryService = categoryService;
            _projectService = projectService;

            CategorizedTasks = new ObservableCollection<CategorizedTaskList>();
        }

        public ObservableCollection<CategorizedTaskList> CategorizedTasks
        {
            get => _categorizedTasks;
            set
            {
                _categorizedTasks = value;
                OnPropertyChanged(nameof(CategorizedTasks));
            }
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

        public async Task LoadCategoriesAsync()
        {
            Categories = new ObservableCollection<Category>(await _categoryService.GetAllAsync());
        }

        public async Task LoadProjectAsync(int id)
        {
            _project = await _projectService.GetByIdAsync(id);
        }
    }
}
