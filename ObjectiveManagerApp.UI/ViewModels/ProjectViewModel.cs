using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
        private Project _activeProject = new Project();
        private bool _isLoading = false;
        private bool _isSelected = false;

        public DelegateCommand GoToDashboardCommand { get; }

        public ProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            GoToDashboardCommand = new DelegateCommand(GoToDashboardCommand_Executed);
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

        public async Task LoadUserProjectsAsync(int id)
        {
            try
            {
                IsLoading = true;
                Projects.Clear();
                Projects = new ObservableCollection<Project>(await _projectService.GetByUserIdAsync(id));
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void ChangeActiveProject(object newProject)
        {
            ActiveProject = (Project)newProject;
        }

        public void GoToDashboardCommand_Executed(object sender)
        {
            EventAggregator.Instance.RaiseGoToDashboardEvent(ActiveProject.Id);
        }
    }
}
