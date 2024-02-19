using ObjectiveManagerApp.UI.ViewModels;
using System.Collections.ObjectModel;

namespace ObjectiveManagerApp.UI.Models
{
    public class CategorizedTaskList : ViewModelBase
    {
        public CategorizedTaskList()
        {
            Tasks = new ObservableCollection<string>();
        }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public ObservableCollection<string> Tasks { get; }
    }
}
