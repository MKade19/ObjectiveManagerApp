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
        public Role Role { get; set; }
        public ICollection<Objective> Objectives { get; set; }

        public User(int id, string userName, string password, byte[] salt, string fullName, Role role, ICollection<Objective> objectives)
        {
            Id = id;
            Username = userName;
            Password = password;
            Salt = salt;
            Fullname = fullName;
            Role = role;
            Objectives = objectives;
        }

        public User() { }

        public override string ToString()
        {
            return Username;
        }
    }
}
