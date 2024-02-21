using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ObjectivesView.xaml
    /// </summary>
    public partial class ObjectivesView : UserControl
    {
        public ObjectivesView(IObjectiveService objectiveService)
        {
            InitializeComponent();
            DataContext = new ObjectivesViewModel(objectiveService);
            EventAggregator.Instance.GoToObjectives += ObjectivesView_GoToObjectives;
            EventAggregator.Instance.Login += ObjectivesView_Login;
        }

        private void ObjectivesView_Login(object? sender, EventArgs e)
        {
            ((ObjectivesViewModel)DataContext).RefreshUserName();
        }

        private async void ObjectivesView_GoToObjectives(object? sender, EventArgs e)
        {
            await ((ObjectivesViewModel)DataContext).LoadObjectivesAsync();
        }
    }
}
