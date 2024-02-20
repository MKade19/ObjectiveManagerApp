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
        private ObservableCollection<CategorizedObjectiveList> _categorizedTasks;
        private Objective _activeObjective;
        private bool _isObjectiveCreated = true;
        private Project _project = new Project();

        public DelegateCommand CreateObjectiveCommand { get; }
        public DelegateCommand EditObjectiveCommand { get; }
        public DelegateCommand SubmitObjectiveCommand { get; }

        public DashboardViewModel(ICategoryService categoryService, IProjectService projectService)
        {
            _categoryService = categoryService;
            _projectService = projectService;
            CategorizedObjectives = new ObservableCollection<CategorizedObjectiveList>();
            CreateObjectiveCommand = new DelegateCommand(CreateObjectiveCommand_Executed);
            EditObjectiveCommand = new DelegateCommand(EditObjectiveCommand_Executed);
            SubmitObjectiveCommand = new DelegateCommand(SubmitObjectiveCommand_Executed);
        }

        public ObservableCollection<CategorizedObjectiveList> CategorizedObjectives
        {
            get => _categorizedTasks;
            set
            {
                _categorizedTasks = value;
                OnPropertyChanged(nameof(CategorizedObjectives));
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
            foreach (var category in Categories)
            {
                CategorizedObjectiveList objectiveList = new CategorizedObjectiveList() { Category = category };

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

        public void CreateObjectiveCommand_Executed(object sender)
        {

        }

        public void EditObjectiveCommand_Executed(object sender)
        {

        }

        public void SubmitObjectiveCommand_Executed(object sender)
        {

        }
    }
}
