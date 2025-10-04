using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectApp.Models
{
    public class Activity
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Activity Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        // Navigation: the project this comment belongs to
        [Display(Name = "Project")]
        [ForeignKey("ProjectId")]
        public required Project Project { get; set; }

        // REQUIRED FK — each activity has exactly one worker
        [Required]
        [Display(Name = "Assigned To")]
        public int? AssignedToId { get; set; } = null;

        [Display(Name = "Assigned To")]
        [ForeignKey("AssignedTo")]
        public Worker? Worker { get; set; }

    }
}
