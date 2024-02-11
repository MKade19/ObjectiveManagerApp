namespace ObjectiveManagerApp.Common.Models
{
    public class Objective
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public int ProjectId { get; set; }
        public int EditorId { get; set; }

        public Objective(int id, string name, string description, DateTime createdDate, DateTime updatedDate, DateTime deadline, int projectId, int editorId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            Deadline = deadline;
            ProjectId = projectId;
            EditorId = editorId;
        }
    }
}
