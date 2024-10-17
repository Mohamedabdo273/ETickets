using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ETickets.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var connect = builder.GetConnectionString("MyConnect");
            optionsBuilder.UseSqlServer(connect);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorMovie>().HasKey(e=> new {e.MovieId,e.ActorId});

        }
    }
}
