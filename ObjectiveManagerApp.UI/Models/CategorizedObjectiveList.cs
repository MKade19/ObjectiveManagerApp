using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.ViewModels;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.Models
{
    public class CategorizedObjectiveList : ViewModelBase
    {
        public CategorizedObjectiveList()
        {
            Objectives = new ObservableCollection<Objective>();
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public ObservableCollection<Objective> Objectives { get; }
    }
}
