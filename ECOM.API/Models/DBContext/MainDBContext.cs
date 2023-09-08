using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enities.Models; 

namespace ECOM.API.Models
{
    public class MainDBContext :IdentityDbContext<ApplicationUser>
    {
        public MainDBContext(DbContextOptions<MainDBContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsImage> ProductsImages { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
