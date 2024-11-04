using ETickets.Data;
using ETickets.Migrations;
using ETickets.Models;
using ETickets.Repository.IRepository;
using Stripe.Issuing;
using System.Linq.Expressions;

namespace ETickets.Repository
{
    public class CardRepository : Repository<Cards>,ICard
    {
        private readonly ApplicationDbContext dbContext;

        public CardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
