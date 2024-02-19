using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectiveManagerApp.Common.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("RoleId")]
        public List<InternalUserData> Users { get; set; }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        
        public Role() { }
    }
}
