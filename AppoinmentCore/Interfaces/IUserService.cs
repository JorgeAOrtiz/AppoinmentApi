using Appointment.Domain.Models;
using System.Collections.Generic;

namespace Appointment.Core.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUser(int userId);
        IEnumerable<User> ListUsers();
        User Login(string email, string password);
    }
}