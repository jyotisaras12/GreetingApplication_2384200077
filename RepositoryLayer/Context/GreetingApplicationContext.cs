using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class GreetingApplicationContext : DbContext
    {
        public GreetingApplicationContext(DbContextOptions<GreetingApplicationContext> options) : base(options) { }
            public virtual DbSet<GreetingEntity> Greetings { get; set; }
    }
}
