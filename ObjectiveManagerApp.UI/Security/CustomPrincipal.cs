using System.Security.Principal;

namespace ObjectiveManagerApp.UI.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity? _identity;

        public CustomIdentity Identity
        {
            get { return _identity ?? new AnonymousIdentity(); }
            set { _identity = value; }
        }

        #region IPrincipal Members
        IIdentity IPrincipal.Identity
        {
            get { return this.Identity; }
        }

        public bool IsInRole(string roleName)
        {
            return _identity?.Roles.FirstOrDefault(r => r.Name == roleName) != null;
        }
        #endregion
    }
}
