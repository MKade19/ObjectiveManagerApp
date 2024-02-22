using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
        private Project _activeProject = new Project();
        private bool _isLoading = false;
        private bool _isSelected = false;
        private bool _isCreated = true;
        private const string DataSavedMessageName = "DataSavedMessage";
        private const string ProjectDeleteMessageName = "ProjectDeleteMessage";
        private const string DeleteConfirmationMessageName = "DeleteConfirmationMessage";

        public DelegateCommand GoToDashboardCommand { get; }
        public DelegateCommand ToggleCreateModeCommand { get; }
        public DelegateCommand SubmitCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public ProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            GoToDashboardCommand = new DelegateCommand(GoToDashboardCommand_Executed);
            ToggleCreateModeCommand = new DelegateCommand(ToggleCreateModeCommand_Executed);
            SubmitCommand = new DelegateCommand(SubmitCommand_Executed);
            DeleteCommand = new DelegateCommand(DeleteCommand_Executed);
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public Project ActiveProject
        {
            get => _activeProject;
            set
            {
                _activeProject = value;
                var proj = Projects;
                OnPropertyChanged(nameof(ActiveProject));
                IsSelected = true;
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

        public bool IsCreated
        {
            get => _isCreated;
            set
            {
                _isCreated = value;
                OnPropertyChanged(nameof(IsCreated));
            }
        }

        public async Task LoadUserProjectsAsync(int id)
        {
            try
            {
                IsLoading = true;
                Projects.Clear();
                Projects = new ObservableCollection<Project>(await _projectService.GetByUserIdAsync(id));
                ActiveProject.ManagerId = id;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void ChangeActiveProject(object newProject)
        {
            IsCreated = false;
            int managerId = ActiveProject.ManagerId;
            var proj = Projects;
            ActiveProject = new Project((Project)newProject);
            ActiveProject.ManagerId = managerId;
        }

        public void GoToDashboardCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToDashboardEvent(ActiveProject.Id);
        }

        public void ToggleCreateModeCommand_Executed(object sender)
        {
            ActiveProject = new Project();
            IsCreated = true;
        }

        public async void SubmitCommand_Executed(object sender)
        {
            try
            {
                IsLoading = true;

                if(IsCreated)
                {
                    await _projectService.CreateOneAsync(ActiveProject);
                }
                else
                {
                    await _projectService.EditByIdAsync(ActiveProject);
                }

                await LoadUserProjectsAsync(ActiveProject.ManagerId);
                MessageBoxStore.Information((string)Application.Current.FindResource(DataSavedMessageName));
                ClearActiveProject();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async void DeleteCommand_Executed(object sender)
        {
            try
            {
                IsLoading = true;

                if (MessageBoxStore.Confirmation((string)Application.Current.FindResource(DeleteConfirmationMessageName)) != MessageBoxResult.OK)
                {
                    return;
                }

                await _projectService.DeleteOneAsync(ActiveProject);
                await LoadUserProjectsAsync(ActiveProject.ManagerId);
                MessageBoxStore.Information((string)Application.Current.FindResource(ProjectDeleteMessageName));
                ClearActiveProject();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ClearActiveProject()
        {
            ActiveProject.Name = string.Empty;
            ActiveProject.Description = string.Empty;
        }
    }
}
