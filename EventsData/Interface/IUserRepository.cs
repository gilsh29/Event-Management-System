using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EventsData.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User newUser);
        void UpdateUser(User updatedUser);
        void DeleteUser(int id);
        void Save();
    }
}
