using LoyaltyProgram.Infrastructure;
using LoyaltyProgram.Domain;

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

        public List<Transaction> GetTransactionsByLoyaltyCardNumber(string loyaltyCardNumber)
        {
            var loyaltyCard = _context.LoyaltyCards.FirstOrDefault(card => card.CardNumber == loyaltyCardNumber);
            return loyaltyCard?.Transactions ?? new List<Transaction>();
        }

        public void AddTransactionToLoyaltyCard(string loyaltyCardNumber, Transaction transaction)
        {
            var loyaltyCard = _context.LoyaltyCards.FirstOrDefault(card => card.CardNumber == loyaltyCardNumber);
            if (loyaltyCard != null)
            {
                loyaltyCard.AddTransaction(transaction);
                _context.SaveChanges();
            }
        }
    }
}
