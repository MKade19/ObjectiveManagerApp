using ObjectiveManagerApp.UI.EventAggregation;
using ObjectiveManagerApp.UI.Services.Abstract;
using ObjectiveManagerApp.UI.ViewModels;
using System.Windows.Controls;

namespace ObjectiveManagerApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ObjectiveEditFormView.xaml
    /// </summary>
    public partial class ObjectiveEditFormView : UserControl
    {
        public ObjectiveEditFormView(IObjectiveService objectiveService, ICategoryService categoryService, IUserService userService)
        {
            InitializeComponent();
            DataContext = new ObjectiveEditFormViewModel(objectiveService, categoryService, userService);

            EventAggregator.Instance.GoToObjectiveEditForm += Form_GoToObjectiveEditForm; ;
        }

        private async void Form_GoToObjectiveEditForm(object? sender, EventAggregation.EventArgsTypes.ObjectiveNavigationEventArgs e)
        {
            ((ObjectiveEditFormViewModel)DataContext).IsCreated = e.ObjectiveId == -1;
            
            await ((ObjectiveEditFormViewModel)DataContext).LoadObjectiveForForm(e.ObjectiveId, e.ProjectId);

            await ((ObjectiveEditFormViewModel)DataContext).LoadDataForForm();
        }
    }
}
