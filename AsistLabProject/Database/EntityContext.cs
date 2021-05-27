using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistLabProject.Models.Data
{
    public class EntityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public EntityContext(DbContextOptions<EntityContext> options)
       : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
