using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProjectApp.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        //Relationship: Many-to-Many with Project via ProjectWorker
        public List<ProjectWorker> ProjectWorkers { get; set; } = new List<ProjectWorker>();
        //Relationship: One-to-Many with Activity
        public List<Activity> Activities { get; set; } = [];
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

    }
}
