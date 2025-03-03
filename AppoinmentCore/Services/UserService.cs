using System.Collections.Generic;
using System.Linq;
using Appointment.Domain.Data;
using Appointment.Domain.Models;
using Appointment.Core.Interfaces;

namespace Appointment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppointmentDbContext _context;

        public UserService(AppointmentDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId) ?? throw new KeyNotFoundException("User not found");
        }

        public IEnumerable<User> ListUsers()
        {
            return _context.Users.ToList();
        }

        public User Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user ?? throw new KeyNotFoundException("User not found");
        }
    }
}
