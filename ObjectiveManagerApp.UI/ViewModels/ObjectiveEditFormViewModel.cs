using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ObjectiveEditFormViewModel : ViewModelBase
    {
        private readonly IObjectiveService _objectiveService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<PublicUserData> _users = new ObservableCollection<PublicUserData>();
        private Objective _currentObjective = new Objective();
        private Category? _currentCategory;
        private PublicUserData? _currentUser;
        private bool _isCreated = true;
        private const string DataSavedMessageName = "DataSavedMessage";

        public DelegateCommand SubmitObjectiveCommand { get; }
        public DelegateCommand BackCommand { get; }

        public ObjectiveEditFormViewModel(IObjectiveService objectiveService, ICategoryService categoryService, IUserService userService)
        {
            _objectiveService = objectiveService;
            _categoryService = categoryService;
            _userService = userService;
            SubmitObjectiveCommand = new DelegateCommand(SubmitObjectiveCommand_Executed);
            BackCommand = new DelegateCommand(BackCommand_Executed);
        }

        public Objective CurrentObjective
        {
            get => _currentObjective;
            set
            {
                _currentObjective = value;
                OnPropertyChanged(nameof(CurrentObjective));
            }
        }

        public Category? CurrentCategory
        {
            get => _currentCategory;
            set
            {
                _currentCategory = value;
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }

        public PublicUserData? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public bool IsCreated
        {
            get => _isCreated;
            set
            {
                _isCreated = value;
                OnPropertyChanged(nameof(IsCreated));
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

        public ObservableCollection<PublicUserData> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public async void SubmitObjectiveCommand_Executed(object sender)
        {
            try
            {
                EventAggregator.Instance.RaiseDashboardViewIsLoadingEvent();

                if (IsCreated)
                {
                    await CreateObjectiveAsync();
                }
                else
                {
                    await EditObjectiveAsync();
                }
                
                MessageBoxStore.Information((string)Application.Current.FindResource(DataSavedMessageName));
                BackCommand_Executed(this);
            }
            finally
            {
                EventAggregator.Instance.RaiseDashboardViewFinishedLoadingEvent();
            }
        }

        public void BackCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseBackToDashboardEvent();
        }

        public async Task LoadObjectiveForForm(int objectiveId, int projectId)
        {
            if (!IsCreated)
            {
                CurrentObjective = await _objectiveService.GetByIdAsync(objectiveId);
            }
            else
            {
                CurrentObjective = new Objective();
                CurrentObjective.ProjectId = projectId;
            }
        }

        public async Task LoadDataForForm()
        {
            try
            {
                EventAggregator.Instance.RaiseDashboardViewIsLoadingEvent();

                Categories.Clear();
                var categoriesData = await _categoryService.GetAllAsync();

                foreach (var category in categoriesData)
                {
                    Categories.Add(category);
                }

                Users.Clear();
                var usersData = await _userService.GetAllAsync();

                foreach (var user in usersData)
                {
                    Users.Add(user);
                }

                CurrentUser = Users.FirstOrDefault(u => u.Id == CurrentObjective.UserId);
                CurrentCategory = Categories.FirstOrDefault(u => u.Id == CurrentObjective.CategoryId);
            }
            finally
            {
                EventAggregator.Instance.RaiseDashboardViewFinishedLoadingEvent();
            }
        }

        private async Task CreateObjectiveAsync()
        {
            _currentObjective.CategoryId = CurrentCategory.Id;
            _currentObjective.UserId = CurrentUser.Id;
            await _objectiveService.CreateOneAsync(_currentObjective);
        }

        private async Task EditObjectiveAsync()
        {
            _currentObjective.CategoryId = CurrentCategory.Id;
            _currentObjective.UserId = CurrentUser.Id;
            await _objectiveService.EditByIdAsync(_currentObjective);
        }
    }
}
