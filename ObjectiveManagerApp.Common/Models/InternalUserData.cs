using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObjectiveManagerApp.Common.Models
{

    public class InternalUserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public byte[]? Salt { get; set; }

        public string Fullname { get; set; } = string.Empty;

        public List<Role> Roles { get; set; } = new();

        public InternalUserData(int id, string userName, string password, byte[] salt, string fullName)
        {
            Id = id;
            Username = userName;
            Password = password;
            Salt = salt;
            Fullname = fullName;
        }

        public InternalUserData()
        {
        }
    }
}
