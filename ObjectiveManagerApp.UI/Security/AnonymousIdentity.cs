using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Security
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity() : base(-1, string.Empty, new List<Role>())
        { }
    }
}
