namespace ObjectiveManagerApp.Common.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime UpdatedDate {  get; set; }
        public int ManagerId { get; set; }

        public Project(int id, string name, string description, DateTime createdDate, DateTime updatedDate, int managerId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            ManagerId = managerId;
        }
    }
}
