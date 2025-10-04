using System.ComponentModel.DataAnnotations;


namespace ProjectApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public required string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public required string Status { get; set; }  

        public List<ProjectWorker> ProjectWorkers { get; set; } = new List<ProjectWorker>();


        // REQUIRED FK -> the PM must be a staff member
        [Required] 
        public int WorkerCount { get; set; }
        [Required] 
        public string? WorkerName { get; set; }
        
        [Display(Name = "Project Manager")]
        public int? PMId { get; set; }

        // Navigation: the staff member who is the PM
        [Display(Name = "Project Manager")]
        public Worker? PM { get; set; }



        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Comment> Comments { get; set; } = new List<Comment>();  
    }
}
