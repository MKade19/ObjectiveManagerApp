using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObjectiveManagerApp.Common.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime UpdatedDate {  get; set; }
        public int ManagerId { get; set; }
        public ICollection<Objective> Objectives { get; set; }

        public Project(int id, string name, string description, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }

        public Project() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
