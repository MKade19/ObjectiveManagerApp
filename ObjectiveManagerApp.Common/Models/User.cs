using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectiveManagerApp.Common.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[]? Salt { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public int RoleId { get; set; } = 1;

        public User(int id, string userName, string password, byte[] salt, string fullName, int roleId)
        {
            Id = id;
            Username = userName;
            Password = password;
            Salt = salt;
            Fullname = fullName;
            RoleId = roleId;
        }

        public User() { }
    }
}
