using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class LoyaltyCardService
    {
        private readonly LoyaltyDbContext _context;

        public LoyaltyCardService(LoyaltyDbContext context)
        {
            _context = context;
        }

        public LoyaltyCard? GetLoyaltyCardByCardNumber(string cardNumber)
        {

            return _context.LoyaltyCards.FirstOrDefault(card => card.CardNumber == cardNumber);
        }

        public void UpdateLoyaltyCard(LoyaltyCard loyaltyCard)
        {
            _context.LoyaltyCards.Update(loyaltyCard);
            _context.SaveChanges();
        }

        public List<LoyaltyCard> GetLoyaltyCards()
        {
            return _context.LoyaltyCards.ToList();
        }
    }
}