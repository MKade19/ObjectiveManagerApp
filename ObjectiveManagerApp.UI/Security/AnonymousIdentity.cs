using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Security
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity() : base(string.Empty, new List<Role>())
        { }
    }
}
