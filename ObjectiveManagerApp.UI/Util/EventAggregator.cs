using ObjectiveManagerApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectiveManagerApp.UI.Util
{
    public class EventAggregator
    {
        private EventAggregator() { }

        private static EventAggregator? _instance;

        public static EventAggregator Instance
        {
            get => _instance ?? (_instance = new EventAggregator());
        }

        public event EventHandler? GoToDashboard;
        public event EventHandler? SaveToDbEnded;
        public event EventHandler? ShowFileDataStarted;
        public event EventHandler? ShowFileDataEnded;
        public static event EventHandler? LanguageChanged;

        public void RaiseGoToDashboardEvent()
        {
            GoToDashboard?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseSaveToDbEndedEvent()
        {
            SaveToDbEnded?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseShowFileDataStartedEvent()
        {
            ShowFileDataStarted?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseShowFileDataEndedEvent()
        {
            ShowFileDataEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
