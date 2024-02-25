using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.Util;
using System.Windows;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ProjectEditFormViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private bool _isCreated;
        private Project _currentProject = new Project();
        private const string DataSavedMessageName = "DataSavedMessage";

        public DelegateCommand BackToProjectsCommand { get; }
        public DelegateCommand SubmitCommand { get; }

        public ProjectEditFormViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            BackToProjectsCommand = new DelegateCommand(BackToProjectsCommand_Executed);
            SubmitCommand = new DelegateCommand(SubmitCommand_Executed);
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

        public Project CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));
            }
        }

        public async Task LoadProjectAsync(int projectId)
        {
            try
            {
                EventAggregator.Instance.RaiseProjectViewIsLoadingEvent();
                CurrentProject = await _projectService.GetByIdAsync(projectId);
            }
            finally
            {
                EventAggregator.Instance.RaiseProjectViewFinishedLoadingEvent();
            }
        }

        public async void SubmitCommand_Executed(object sender)
        {
            try
            {
                EventAggregator.Instance.RaiseProjectViewIsLoadingEvent();

                if (IsCreated)
                {
                    await _projectService.CreateOneAsync(CurrentProject);
                }
                else
                {
                    await _projectService.EditByIdAsync(CurrentProject);
                }

                MessageBoxStore.Information((string)Application.Current.FindResource(DataSavedMessageName));
                BackToProjectsCommand_Executed(this);
            }
            finally
            {
                EventAggregator.Instance.RaiseProjectViewFinishedLoadingEvent();
            }
        }

        public void BackToProjectsCommand_Executed(object sender)
        {
            CurrentProject = new Project();
            EventAggregator.Instance.RaiseGoToProjectsEvent();
        }
    }
}
