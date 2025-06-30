using LoyaltyProgram.Infrastructure;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Application
{
    public class RewardService
    {
        private readonly LoyaltyDbContext _context;

        public RewardService(LoyaltyDbContext context)
        {
            _context = context;
        }

        public void AddReward(Reward reward)
        {
            _context.Rewards.Add(reward);
            _context.SaveChanges();
        }

        public List<Reward> GetRewards()
        {
            return _context.Rewards.ToList();
        }

        public Reward? GetRewardById(int rewardId)
        {
            return _context.Rewards.FirstOrDefault(reward => reward.RewardId == rewardId);
        }

        public void UpdateReward(Reward reward)
        {
            _context.Rewards.Update(reward);
            _context.SaveChanges();
        }

        public void DeleteReward(int rewardId)
        { 
            var reward = _context.Rewards.Find(rewardId);
            if (reward != null)
            {
                _context.Rewards.Remove(reward);
                _context.SaveChanges();
            }
        }
        
    }
}