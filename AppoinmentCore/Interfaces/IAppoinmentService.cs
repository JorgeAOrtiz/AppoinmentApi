namespace AppoinmentCore.Interfaces
{
    public interface IAppoinmentService
    {
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int appointmentId);
        IEnumerable<Appointment> ListAppointmentsByUser(int userId);
        void ApproveAppointment(int appointmentId);
        void CancelAppointment(int appointmentId);
        IEnumerable<Appointment> ListAppointments();
    }
}
