using AsistLabProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistLabProject.Repository
{
    public interface IUser
    {
        IEnumerable<User> GetUsers();
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
        void Save();

    }
}
