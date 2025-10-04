namespace ProjectApp.Models
{
    public class ProjectWorker
    {
        public int Id { get; set; }

        // Composite Key (ProjectId, WorkerId)
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
