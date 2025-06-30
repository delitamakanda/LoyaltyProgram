using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class LoyaltyProgramService
    {
        private readonly LoyaltyDbContext _context;
        private readonly NotificationService _notificationService;
        private readonly ShopService _shopService;

        public LoyaltyProgramService(LoyaltyDbContext context, NotificationService notificationService, ShopService shopService)
        {
            _context = context;
            _notificationService = notificationService;
            _shopService = shopService;
        }
        public void AddLoyaltyCard(LoyaltyCard loyaltyCard)
        {
            _context.LoyaltyCards.Add(loyaltyCard);
            _context.SaveChanges();
        }

        public void AddTransaction(Transaction transaction)
        {
            var loyaltyCard = transaction.LoyaltyCard;
            var shop = transaction.Shop;
            var currentPoints = loyaltyCard?.GetValidPoints();
            loyaltyCard?.AddTransaction(transaction);
            var newPoints = loyaltyCard?.GetValidPoints();
            var newRank = ShopService.CalculateRank(newPoints, shop!);
            if (newPoints >= 1000 && currentPoints <= 1000)
            {
                if (loyaltyCard?.Client != null)
                {
                    _notificationService.NotifyPointsEarned(loyaltyCard?.Client, newPoints);
                }
            }
            if (newRank != loyaltyCard?.Rank)
            {
                _notificationService.NotifyNewRank(loyaltyCard?.Client!, newRank);
            }
        }

    }
}
