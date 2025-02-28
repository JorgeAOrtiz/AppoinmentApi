﻿using System.ComponentModel.DataAnnotations;

namespace Appointment.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
    
}
