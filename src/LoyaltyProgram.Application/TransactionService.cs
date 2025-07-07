using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Application
{
    public class TransactionService
    {
        private readonly LoyaltyDbContext _context;
        public TransactionService(LoyaltyDbContext context)
        {
            _context = context;
        }
        public List<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }
        public Transaction? GetTransactionById(int id)
        {
            return _context.Transactions.Find(id);
        }

        public List<Transaction> SearchTransactionsByShopClientLoyaltyCard(int? shopId, int? clientId, int? loyaltyCardId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions.AsQueryable();
            if (shopId.HasValue)
            {
                query = query.Where(t => t.Shop != null && t.Shop.ShopId == shopId);
            }
            if (clientId.HasValue)
            {
                query = query.Where(t => t.LoyaltyCard != null && t.LoyaltyCard.Client != null && t.LoyaltyCard.Client.ClientId == clientId);
            }
            if (loyaltyCardId.HasValue)
            {
                query = query.Where(t => t.LoyaltyCard != null && t.LoyaltyCard.LoyaltyCardId == loyaltyCardId);
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate);
            }
            return query.ToList();
        }

        public List<Transaction> ExportTransactionsCsv()
        {
            return _context.Transactions.Include(t => t.LoyaltyCard).ThenInclude(c => c!.Client).Include(s => s.Shop).ToList();
        }
    }
}