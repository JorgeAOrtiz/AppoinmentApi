using Microsoft.AspNetCore.Mvc;
using Appointment.Core.Interfaces;
using Appointment.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<IEnumerable<Appointment.Domain.Models.Appointment>> GetAppointments()
        {
            var appointments = _appointmentService.ListAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<Appointment.Domain.Models.Appointment> GetAppointment(int id)
        {
            var appointment = _appointmentService.ListAppointments().FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<Appointment.Domain.Models.Appointment> AddAppointment(Appointment.Domain.Models.Appointment appointment)
        {
            _appointmentService.AddAppointment(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAppointment(int id, Appointment.Domain.Models.Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _appointmentService.UpdateAppointment(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAppointment(int id)
        {
            _appointmentService.DeleteAppointment(id);
            return NoContent();
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveAppointment(int id)
        {
            _appointmentService.ApproveAppointment(id);
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        [Authorize(Roles = "Admin")]
        public IActionResult CancelAppointment(int id)
        {
            _appointmentService.CancelAppointment(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<IEnumerable<Appointment.Domain.Models.Appointment>> GetAppointmentsByUser(int userId)
        {
            var appointments = _appointmentService.ListAppointmentsByUser(userId);
            return Ok(appointments);
        }
    }
}
