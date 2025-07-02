using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class ShopService
    {
        private readonly LoyaltyDbContext _context;
        public ShopService(LoyaltyDbContext context)
        {
            _context = context;
        }
        public void AddShop(Shop shop)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
        }
        public Shop? GetShopById(int id)
        {
            return _context.Shops.Find(id);
        }
        public void UpdateShop(Shop shop)
        {
            _context.Shops.Update(shop);
            _context.SaveChanges();
        }
        public void DeleteShop(int id)
        {
            var shop = _context.Shops.Find(id);
            if (shop != null)
            {
                _context.Shops.Remove(shop);
                _context.SaveChanges();
            }
        }
        public List<Shop> GetShops()
        {
            return _context.Shops.ToList();
        }

        public List<RankSystem> GetShopRankSystem(int shopId)
        {
            return _context.RankSystems.Where(rs => rs.ShopId == shopId).ToList();
        }

        public static RankStatus CalculateRank(int? points, Shop shop)
        {
            var paramsRank = shop.RankSystem.OrderBy(p => p.PointsNeeded).ToList();

            RankStatus currentRank = RankStatus.Basic;
            foreach (var rank in paramsRank)
            {
                if (points >= rank.PointsNeeded)
                {
                    currentRank = rank.Rank;
                }
                else
                {
                    break;
                }
            }
            return currentRank;
        }
    }
}
