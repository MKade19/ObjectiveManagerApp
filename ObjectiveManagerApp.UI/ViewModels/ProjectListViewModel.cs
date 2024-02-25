using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Security;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ProjectListViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
        private Project _activeProject = new Project();
        private bool _isSelected = false;
        private const string ProjectDeleteMessageName = "ProjectDeleteMessage";
        private const string DeleteConfirmationMessageName = "DeleteConfirmationMessage";

        public DelegateCommand GoToCreateProjectCommand { get; }
        public DelegateCommand GoToEditProjectCommand { get; }
        public DelegateCommand GoToDashboardCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public ProjectListViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            GoToDashboardCommand = new DelegateCommand(GoToDashboardCommand_Executed);
            GoToCreateProjectCommand = new DelegateCommand(GoToCreateProjectCommand_Executed);
            GoToEditProjectCommand = new DelegateCommand(GoToEditProjectCommand_Executed);
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
                OnPropertyChanged(nameof(ActiveProject));
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

        public async Task LoadUserProjectsAsync()
        {
            try
            {
                //IsLoading = true;
                Projects.Clear();
                //var jopa = ((CustomIdentity)Thread.CurrentPrincipal.Identity).UserId;
                Projects = new ObservableCollection<Project>(await _projectService.GetByUserIdAsync(((CustomIdentity)Thread.CurrentPrincipal.Identity).UserId));
                //ActiveProject.ManagerId = id;
            }
            finally
            {
                //IsLoading = false;
            }
        }

        public async void DeleteCommand_Executed(object sender)
        {
            try
            {
                //IsLoading = true;

                if (MessageBoxStore.Confirmation((string)Application.Current.FindResource(DeleteConfirmationMessageName)) != MessageBoxResult.Yes)
                {
                    return;
                }

                await _projectService.DeleteOneAsync(ActiveProject);
                await LoadUserProjectsAsync();
                MessageBoxStore.Information((string)Application.Current.FindResource(ProjectDeleteMessageName));
            }
            finally
            {
                //IsLoading = false;
            }
        }

        public void ChangeActiveProject(object projectData)
        {
            ActiveProject = new Project((Project) projectData);
            IsSelected = true;
        }

        public void GoToDashboardCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToDashboardEvent(ActiveProject.Id);
        }

        public void GoToCreateProjectCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToProjectEditFormEvent(ActiveProject.Id, FormType.Create);
        }
        
        public void GoToEditProjectCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToProjectEditFormEvent(ActiveProject.Id, FormType.Edit);
        }
    }
}
