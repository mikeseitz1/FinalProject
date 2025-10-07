using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models
{
    public class SignInLog
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public DateTime SignInTime { get; set; }
        
        public string? IpAddress { get; set; }
        
        public string? UserAgent { get; set; }
    }
}