using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Project(int id, string name, string description, DateTime createdDate, DateTime updatedDate, int managerId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            ManagerId = managerId;
        }

        public Project() { }

        public Project(Project project) 
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            CreatedDate = project.CreatedDate;
            UpdatedDate = project.UpdatedDate;
            ManagerId = project.ManagerId;
        }

        public override string ToString()
        {
            return $"{Name} ({Description}), " +
                $"created: {CreatedDate.ToString("dd/MM/yyyy")}, " +
                $"updated: {UpdatedDate.ToString("dd/MM/yyyy")}" ;
        }
    }
}
