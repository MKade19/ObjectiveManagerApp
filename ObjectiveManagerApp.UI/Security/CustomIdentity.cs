using ObjectiveManagerApp.Common.Models;
using System.Security.Principal;

namespace ObjectiveManagerApp.UI.Security
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, List<Role> roles)
        {
            Name = name;
            Roles = roles;
        }
        
        public List<Role> Roles { get; private set; }

        #region IIdentity Members
        public string Name { get; private set; }

        public string AuthenticationType { get { return "Basic authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
