using Luman.DataLayer.EntityModel.Orders;
using Luman.DataLayer.EntityModel.Permitions;
using Luman.DataLayer.EntityModel.Product;
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
        public DbSet<Product> products{ get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryProduct> categoryProducts { get; set; }
        public DbSet<Permition> permitions  { get; set; }
        public DbSet<Discount> discounts  { get; set; }
        public DbSet<Order> orders  { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        public DbSet<RolePermission> rolePermissions { get; set; }
        public DbSet<FavoriteProduct> favoriteProducts { get; set; }

       

    }
}
