using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProjectService _projectService;
        private readonly IObjectiveService _objectiveService;

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<CategorizedObjectiveList> _categorizedObjectives;
        private Objective _selectedObjective;
        private CategorizedObjectiveList _selectedCategorizedObjective;
        private bool _isLoading = false;
        private bool _isSelected = false;
        private int _activeTabIndex = 0;
        private Project _project = new Project();

        private const string ObjectiveDeleteMessageName = "ObjectiveDeleteMessage";
        private const string DeleteConfirmationMessageName = "DeleteConfirmationMessage";

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand PushObjectiveLeftCommand { get; }
        public DelegateCommand PushObjectiveRightCommand { get; }

        public DashboardViewModel(ICategoryService categoryService, IProjectService projectService, IObjectiveService objectiveService)
        {
            _categoryService = categoryService;
            _projectService = projectService;
            CategorizedObjectives = new ObservableCollection<CategorizedObjectiveList>();
            CreateCommand = new DelegateCommand(CreateCommand_Excuted);
            EditCommand = new DelegateCommand(EditCommand_Excuted);
            DeleteCommand = new DelegateCommand(DeleteCommand_Excuted);
            PushObjectiveLeftCommand = new DelegateCommand(PushObjectiveLeftCommand_Excuted);
            PushObjectiveRightCommand = new DelegateCommand(PushObjectiveRightCommand_Excuted);
            _objectiveService = objectiveService;
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

        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        public Objective SelectedObjective
        {
            get => _selectedObjective;
            set
            {
                _selectedObjective = value;
                OnPropertyChanged(nameof(SelectedObjective));
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

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
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
            Project = await _projectService.GetByIdAsync(id);
        }

        public void MakeCategorizedObjectives()
        {
            CategorizedObjectives.Clear();
            IsSelected = false;

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
            SelectedObjective = (Objective)objectiveData;
            IsSelected = true;
        }

        public void CreateCommand_Excuted(object sender)
        {
            EventAggregator.Instance.RaiseGoToObjectiveEditFormEvent(-1, Project.Id);
        } 
        
        public void EditCommand_Excuted(object sender)
        {
            EventAggregator.Instance.RaiseGoToObjectiveEditFormEvent(SelectedObjective.Id, Project.Id);
        } 
        
        public async void DeleteCommand_Excuted(object sender)
        {
            try
            {
                IsLoading = true;

                if (MessageBoxStore.Confirmation((string)Application.Current.FindResource(DeleteConfirmationMessageName)) != MessageBoxResult.Yes)
                {
                    return;
                }

                await _objectiveService.DeleteOneAsync(SelectedObjective);
                MessageBoxStore.Information((string)Application.Current.FindResource(ObjectiveDeleteMessageName));
                await LoadCategoriesAsync();
                MakeCategorizedObjectives();
            }
            finally
            {
                IsLoading = false;
            }
        } 
        
        public void PushObjectiveLeftCommand_Excuted(object sender)
        {

        }

        public void PushObjectiveRightCommand_Excuted(object sender)
        {

        }
    }
}
