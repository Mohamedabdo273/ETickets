using ETickets.Controllers;
using ETickets.Migrations;
using ETickets.Models;
using ETickets.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovie { get; set; }
        public DbSet<Cards> Cards { get; set; }       
       public DbSet<Orders> orders { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cards>().HasKey(e => new { e.MovieId, e.UserId });
            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole()
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = SD.adminRole,
                     NormalizedName = SD.adminRole,
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                 },
                 new IdentityRole()
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = SD.CustomerRole,
                     NormalizedName = SD.CustomerRole,
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                 });
        }
    }
}
