using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Services.Abstract;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.ViewModels
{
    public class ObjectivesViewModel : ViewModelBase
    {
        private ObservableCollection<Objective> _objectives = new ObservableCollection<Objective>();
        private readonly IObjectiveService _objectiveService;

        public ObjectivesViewModel(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        public string Username
        {
            get => Thread.CurrentPrincipal.Identity.Name;
        }

        public ObservableCollection<Objective> Objectives 
        { 
            get => _objectives;
            set
            {
                _objectives = value;
                OnPropertyChanged(nameof(Objectives));
            }
        }

        public async Task LoadObjectivesAsync()
        {
            Objectives.Clear();
            
            foreach (Objective objective in await _objectiveService.GetObjectivesByUsername(Username))
            {
                Objectives.Add(objective);
            }
        }

        public void RefreshUserName()
        {
            OnPropertyChanged(nameof(Username));
        }
    }
}
