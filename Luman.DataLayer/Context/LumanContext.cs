using Luman.DataLayer.EntityModel.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.Context
{
    public class LumanContext : DbContext
    {
        public LumanContext(DbContextOptions<LumanContext> options):base(options) 
        {
            
        }

        public DbSet<User> users { get; set; }

       
    }
}
