using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace ETickets.Repository
{
    public class MovieRepository : Repository<Movie>, IMovie
    {
        private readonly ApplicationDbContext dbContext;
        private DbSet<Movie> dbSet;
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<Movie>();
        }
    
       
    }
}
