using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Appointment.Domain.Data;
using Appointment.Domain.Models;
using Appointment.Core.Interfaces;

namespace Appointment.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentDbContext _context;

        public AppointmentService(AppointmentDbContext context)
        {
            _context = context;
        }

        public void AddAppointment(Domain.Models.Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void UpdateAppointment(Domain.Models.Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void DeleteAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Domain.Models.Appointment> ListAppointmentsByUser(int userId)
        {
            return _context.Appointments.Where(a => a.UserId == userId).ToList();
        }

        public void ApproveAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.State = AppointmentState.Approved;
                _context.SaveChanges();
            }
        }

        public void CancelAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.State = AppointmentState.Cancelled;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Domain.Models.Appointment> ListAppointments()
        {
            return _context.Appointments.ToList();
        }
    }
}
