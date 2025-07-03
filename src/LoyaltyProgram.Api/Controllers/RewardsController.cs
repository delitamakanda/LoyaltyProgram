using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Authorization;

namespace LoyaltyProgram.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RewardsController : ControllerBase
    {
        private readonly RewardService _rewardService;
        public RewardsController(RewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet]
        public ActionResult<List<Reward>> GetRewards()
        {
            var rewards = _rewardService.GetRewards();
            return Ok(rewards);
        }

        [HttpGet("{id}")]
        public ActionResult<Reward> GetReward(int id)
        {
            var reward = _rewardService.GetRewardById(id);
            if (reward == null)
            {
                return NotFound();
            }
            return Ok(reward);
        }

        [HttpPost]
        public ActionResult<Reward> CreateReward(Reward reward)
        {
            _rewardService.AddReward(reward);
            // return just the created item and status code 201 Created
            return CreatedAtAction(nameof(GetReward), new { id = reward.RewardId }, reward);
        }

        [HttpPut("{id}")]
        public ActionResult<Reward> UpdateReward(int id, Reward reward)
        {
            if (id != reward.RewardId)
            {
                return BadRequest();
            }
            _rewardService.UpdateReward(reward);
            return Ok(reward);
        }

        [HttpDelete("{id}")]
        public ActionResult<Reward> DeleteReward(int id)
        { 
            var reward = _rewardService.GetRewardById(id);
            if (reward == null)
            {
                return NotFound();
            }
            _rewardService.DeleteReward(reward.RewardId);
            return NoContent();
        }
    }
}