using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // REQUIRED FK -> each comment is associated with exactly one project
        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        // Navigation: the project this comment belongs to
        [Display(Name = "Project")]
        public required Project Project { get; set; }
        // REQUIRED FK -> each comment is made by exactly one worker
        [Required]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        // Navigation: the worker who authored this comment
        [Display(Name = "Author")]
        public required Worker Author { get; set; }
    }
}
