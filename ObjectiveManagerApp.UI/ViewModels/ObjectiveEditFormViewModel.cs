using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ObjectiveEditFormViewModel : ViewModelBase
    {
        private readonly IObjectiveService _objectiveService;

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<PublicUserData> _users = new ObservableCollection<PublicUserData>();
        private Objective _currentObjective;
        private Category _currentCategory;
        private PublicUserData _currentUser;
        private bool _isCreated = true;

        public DelegateCommand SubmitObjectiveCommand { get; }
        public DelegateCommand BackCommand { get; }

        public ObjectiveEditFormViewModel(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
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

        public Category CurrentCategory
        {
            get => _currentCategory;
            set
            {
                _currentCategory = value;
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }

        public PublicUserData CurrentUser
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
            }
            finally
            {
                EventAggregator.Instance.RaiseDashboardViewFinishedLoadingEvent();
            }
        }

        public void BackCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToDashboardEvent(CurrentObjective.Project.Id);
        }

        public async Task LoadDataForForm(int objectiveId)
        {
            CurrentObjective = await _objectiveService.GetByIdAsync(objectiveId);
        }

        private async Task CreateObjectiveAsync()
        {
            await _objectiveService.CreateOneAsync(CurrentObjective);
        }

        private async Task EditObjectiveAsync()
        {
            await _objectiveService.CreateOneAsync(CurrentObjective);
        }
    }
}
