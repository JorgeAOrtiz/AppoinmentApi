using Appointment.Domain.Models;

namespace Appointment.Core.Interfaces
{
    public interface IAppointmentService
    {
        void AddAppointment(Domain.Models.Appointment appointment);
        void UpdateAppointment(Domain.Models.Appointment appointment);
        void DeleteAppointment(int appointmentId);
        IEnumerable<Domain.Models.Appointment> ListAppointmentsByUser(int userId);
        void ApproveAppointment(int appointmentId);
        void CancelAppointment(int appointmentId);
        IEnumerable<Domain.Models.Appointment> ListAppointments();
    }
}
