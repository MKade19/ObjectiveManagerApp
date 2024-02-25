using ObjectiveManagerApp.UI.Constants;

namespace ObjectiveManagerApp.UI.EventAggregation.EventArgsTypes
{
    public class FormNavigationEventArgs : EventArgs
    {
        public int Id { get; set; }
        public FormType Type { get; set; }

        public FormNavigationEventArgs(int id, FormType type)
        {
            Id = id;
            Type = type;
        }
    }
}
