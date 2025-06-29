using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class LoyaltyProgramService
    {
        private readonly LoyaltyDbContext _context;
        public LoyaltyProgramService(LoyaltyDbContext context)
        {
            _context = context;
        }
        public void AddLoyaltyCard(LoyaltyCard loyaltyCard)
        {
            _context.LoyaltyCards.Add(loyaltyCard);
            _context.SaveChanges();
        }
        
    }
}