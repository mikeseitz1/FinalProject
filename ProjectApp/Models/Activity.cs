using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectApp.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        public int ProjectId { get; set; }

        // Foreign key for the assigned worker
        public int? AssignedToId { get; set; }
        
        // Navigation property for the assigned worker
        public Worker? AssignedTo { get; set; }

        // Add this property if it does not exist
        public int? WorkerId { get; set; }

        public string AssignedToEmail { get; set; }

        // Optionally, if you have a navigation property for Project, add it:
        public Project? Project { get; set; }
    }
}
