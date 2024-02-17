using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectiveManagerApp.Common.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public InternalUserData User { get; set; }


        public Role Role { get; set; }

        public UserRole(int userId, int roleId, InternalUserData user, Role role)
        {
            UserId=userId;
            RoleId=roleId;
            User=user;
            Role=role;
        }

        public UserRole()
        {
        }
    }
}
