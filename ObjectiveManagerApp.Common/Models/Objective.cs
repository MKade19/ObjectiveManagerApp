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
        public DateTime Deadline { get; set; } = DateTime.Now;

        public Project Project { get; set; }

        public Category Category { get; set; }

        public PublicUserData User { get; set; }

        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }


        public Objective(int id, string name, string description, DateTime createdDate, DateTime updatedDate, DateTime deadline, int userId, int projectId, int categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            Deadline = deadline;
            UserId = userId;
            ProjectId = projectId;
            CategoryId = categoryId;
        }

        public Objective()
        {
        }

        public override string ToString()
        {
            return $"{Name} - {Description}, " +
                $"created: {CreatedDate.ToString("dd/MM/yyyy")}, " +
                $"updated: {UpdatedDate.ToString("dd/MM/yyyy")}, " +
                $"deadline: {Deadline.ToString("dd/MM/yyyy")}";
        }
    }
}
