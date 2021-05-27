using AsistLabProject.Models;
using AsistLabProject.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistLabProject.Repository
{
    public class UserRepository : IUser
    {

        private readonly EntityContext context;
        public UserRepository(EntityContext context)
        {
            this.context = context;
        }
        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
    }
}
