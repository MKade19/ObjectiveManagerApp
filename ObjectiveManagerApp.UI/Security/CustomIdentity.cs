using ObjectiveManagerApp.Common.Models;
using System.Security.Principal;

namespace ObjectiveManagerApp.UI.Security
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(int userId, string name, List<Role> roles)
        {
            UserId = userId;
            Name = name;
            Roles = roles;
        }
        
        public List<Role> Roles { get; private set; }

        public int UserId { get; private set; }

        #region IIdentity Members
        public string Name { get; private set; }

        public string AuthenticationType { get { return "Basic authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
