using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class HistoryRewardService
    {
        private readonly LoyaltyDbContext _context;
        private readonly RewardService _rewardService;
        public HistoryRewardService(LoyaltyDbContext context, RewardService rewardService)
        {
            _context = context;
            _rewardService = rewardService;
        }
        public void AddHistoryReward(HistoryReward historyReward)
        {
            _context.HistoryRewards.Add(historyReward);
            _context.SaveChanges();
        }

        public HistoryReward? GetHistoryRewardsByRewardId(int rewardId)
        {
            var reward = _rewardService.GetRewardById(rewardId);
            if (reward == null)
            {
                return null;
            }
            return _context.HistoryRewards.FirstOrDefault(hr => reward.RewardId == rewardId);
        }
    }
}
