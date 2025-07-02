using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class HistoryRewardService
    {
        private readonly LoyaltyDbContext _context;
        private readonly ClientService _clientService;
        private readonly RewardService _rewardService;
        public HistoryRewardService(LoyaltyDbContext context, ClientService clientService, RewardService rewardService)
        {
            _context = context;
            _clientService = clientService;
            _rewardService = rewardService;
        }
        public void AddHistoryReward(HistoryReward historyReward)
        {
            _context.HistoryRewards.Add(historyReward);
            _context.SaveChanges();
        }

        public HistoryReward? GetHistoryRewardsById(int rewardId)
        {
            return _context.HistoryRewards.FirstOrDefault(hr => hr.RewardId == rewardId);
        }
        public List<HistoryReward> GetHistoryRewardsByClientId(int clientId)
        {
            var client = _clientService.GetClientById(clientId);
            return _context.HistoryRewards.Where(hr => hr.ClientId == clientId).ToList();
        }
        public List<HistoryReward> GetHistoryRewardsByRewardId(int rewardId)
        {
            var reward = _rewardService.GetRewardById(rewardId);
            return _context.HistoryRewards.Where(hr => hr.RewardId == rewardId).ToList();
        }
    }
}
