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
        private readonly IObjectiveService _objectiveService;

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<CategorizedObjectiveList> _categorizedObjectives;
        private CategorizedObjectiveList _currentCategorizedObjective;
        private Objective _activeObjective;
        private bool _isObjectiveCreated = true;
        private bool _isLoading = false;
        private Project _project = new Project();

        public DelegateCommand CreateEditObjectiveCommand { get; }
        public DelegateCommand BackCommand { get; }
        public DelegateCommand SubmitObjectiveCommand { get; }

        public DashboardViewModel(ICategoryService categoryService, IProjectService projectService, IObjectiveService objectiveService)
        {
            _categoryService = categoryService;
            _projectService = projectService;
            _objectiveService = objectiveService;
            CategorizedObjectives = new ObservableCollection<CategorizedObjectiveList>();
            CreateEditObjectiveCommand = new DelegateCommand(CreateEditObjectiveCommand_Executed);
            SubmitObjectiveCommand = new DelegateCommand(SubmitObjectiveCommand_Executed);
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

        public bool IsObjectiveCreated
        {
            get => _isObjectiveCreated;
            set
            {
                _isObjectiveCreated = value;
                OnPropertyChanged(nameof(IsObjectiveCreated));
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

        public void CreateEditObjectiveCommand_Executed(object sender)
        {
            if (!IsObjectiveCreated)
            {
                ActiveObjective = new Objective();
            }

            IsObjectiveCreated = !IsObjectiveCreated;
        }

        public async void SubmitObjectiveCommand_Executed(object sender)
        {
            try
            {
                IsLoading = true;

                if (IsObjectiveCreated)
                {
                    await CreateObjectiveAsync();
                }
                else
                {
                    await EditObjectiveAsync();
                }

                await LoadProjectAsync(Project.Id);
                MakeCategorizedObjectives();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task CreateObjectiveAsync()
        {
            await _objectiveService.CreateOneAsync(ActiveObjective);
        }

        private async Task EditObjectiveAsync()
        {
            await _objectiveService.CreateOneAsync(ActiveObjective);
        }
    }
}
