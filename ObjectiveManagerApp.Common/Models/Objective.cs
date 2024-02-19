using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObjectiveManagerApp.Common.Models
{
    public class Objective
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Project Project { get; set; }
        public Category Category { get; set; }

        public ICollection<User> Editors { get; set; }

        public Objective(int id, string name, string description, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }
}
