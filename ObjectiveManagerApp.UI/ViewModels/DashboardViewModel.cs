using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<CategorizedObjectiveList> _categorizedObjectives;
        private CategorizedObjectiveList _currentCategorizedObjective;
        private Objective _activeObjective;
        private bool _isLoading = false;
        private int _activeTabIndex = 0;
        private Project _project = new Project();

        public DelegateCommand CreateEditObjectiveCommand { get; }

        public DashboardViewModel(ICategoryService categoryService, IProjectService projectService)
        {
            _categoryService = categoryService;
            _projectService = projectService;
            CategorizedObjectives = new ObservableCollection<CategorizedObjectiveList>();
        }

        public ObservableCollection<CategorizedObjectiveList> CategorizedObjectives
        {
            get => _categorizedObjectives;
            set
            {
                _categorizedObjectives = value;
                OnPropertyChanged(nameof(CategorizedObjectives));
            }
        }

        public CategorizedObjectiveList CurrentCategorizedObjective
        {
            get => _currentCategorizedObjective;
            set
            {
                _currentCategorizedObjective = value;
                OnPropertyChanged(nameof(CurrentCategorizedObjective));
            }
        }

        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        public Objective ActiveObjective
        {
            get => _activeObjective;
            set
            {
                _activeObjective = value;
                OnPropertyChanged(nameof(ActiveObjective));
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

        public int ActiveTabIndex
        {
            get => _activeTabIndex;
            set
            {
                _activeTabIndex = value;
                OnPropertyChanged(nameof(ActiveTabIndex));
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
            Categories.Clear();
            var categoriesData = await _categoryService.GetAllAsync();

            foreach (var category in categoriesData)
            {
                Categories.Add(category);
            }
        }

        public async Task LoadProjectAsync(int id)
        {
            _project = await _projectService.GetByIdAsync(id);
        }

        public void MakeCategorizedObjectives()
        {
            for (int i = 0; i < Categories.Count; i++)
            {
                Category category = Categories[i];

                CategorizedObjectiveList objectiveList = new CategorizedObjectiveList() { Category = category, Index = i };

                foreach (var objective in Project.Objectives.Where(o => o.Category.Name == category.Name))
                {
                    objectiveList.Objectives.Add(objective);
                }

                CategorizedObjectives.Add(objectiveList);
            }
        }

        public void ChangeActiveObjective(object objectiveData)
        {
            ActiveObjective = (Objective)objectiveData;
        }

        public void ChangeCurrentCategorizedObjective(object catigorizedObjectiveData)
        {
            CurrentCategorizedObjective = (CategorizedObjectiveList)catigorizedObjectiveData;
        }
    }
}
