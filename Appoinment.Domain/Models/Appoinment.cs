using System.ComponentModel.DataAnnotations;

namespace Appointment.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public AppointmentState State { get; set; }
    }
}
