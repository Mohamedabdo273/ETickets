using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;

namespace ETickets.Repository
{
    public class ActorMovieRepository : Repository<ActorMovie>, IActorMovie
    {
        private readonly ApplicationDbContext dbContext;

        public ActorMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
