using EventsData.Interface;
using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EventDbContext _context;

        public UserRepository(EventDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.ID == id);
        }

        public void AddUser(User newUser)
        {
            _context.Users.Add(newUser);
        }

        public void UpdateUser(User updatedUser)
        {
            _context.Users.Update(updatedUser);
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}