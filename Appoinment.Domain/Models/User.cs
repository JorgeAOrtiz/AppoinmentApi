using System.ComponentModel.DataAnnotations;

namespace Appointment.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
