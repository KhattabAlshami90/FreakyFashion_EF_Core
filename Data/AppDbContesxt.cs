using FreakyFashion_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreakyFashion_EF_Core.Data
{
    class AppDbContesxt : DbContext
    {
        private readonly string conStr = "Server=.;Database=FreakyFashion;Integrated Security=True;Encrypt=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conStr);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User() { Id = 1, Name = "Khattab", PassWord = "123" },
                                                new User() { Id = 2, Name = "My", PassWord = "321" },
                                                new User() { Id = 3, Name = "Sara", PassWord = "456" });
        }
       
        public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<User>User { get; set; }
    }
}
