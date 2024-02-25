using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectiveManagerApp.Common.Models
{
    [Table("VI_PublicUsers")]
    public class PublicUserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; } = -1;
        public string Username { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;
        public List<Role> Roles { get; set; }

        public PublicUserData(int id, string userName, string fullName, List<Role> roles)
        {
            Id = id;
            Username = userName;
            Fullname = fullName;
            Roles = roles;
        }

        public PublicUserData() { }

        public override string ToString()
        {
            return $"{Username} ({Fullname})";
        }
    }
}
