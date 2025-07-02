using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class HistoryRewardController : ControllerBase
    {
        private readonly HistoryRewardService _historyRewardService;
        public HistoryRewardController(HistoryRewardService historyRewardService)
        {
            _historyRewardService = historyRewardService;
        }

        [HttpGet("{reward_id}")]
        public ActionResult<HistoryReward> GetHistoryRewardByRewardId([FromRoute(Name = "reward_id")] int rewardId)
        {
            var historyReward = _historyRewardService.GetHistoryRewardsByRewardId(rewardId);
            if (historyReward == null)
            {
                return NotFound();
            }
            return Ok(historyReward);
        }

        [HttpPost]
        public ActionResult<HistoryReward> CreateHistoryReward(HistoryReward historyReward)
        {
            _historyRewardService.AddHistoryReward(historyReward);
            return Ok(historyReward);
        }
    }
}
