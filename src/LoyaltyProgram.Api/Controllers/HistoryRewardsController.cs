using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LoyaltyProgram.Api.Controllers
{
    [Authorize]
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
            var historyReward = _historyRewardService.GetHistoryRewardsById(rewardId);
            if (historyReward == null)
            {
                return NotFound();
            }

            var historyRewardDto = new HistoryRewardListDto(
                historyReward.HistoryRewardId,
                historyReward.CreatedDate,
                historyReward.Description ?? string.Empty,
                historyReward.Reward ?? new Reward(),
                historyReward.Client ?? new Client()
            );
            return Ok(historyRewardDto);
        }
        [HttpGet("client/{client_id}")]
        public ActionResult<List<HistoryReward>> GetHistoryRewardsByClientId([FromRoute(Name = "client_id")] int clientId)
        { 
            var historyRewards = _historyRewardService.GetHistoryRewardsByClientId(clientId);
            return Ok(historyRewards.Select(hr => new HistoryRewardListDto(
                hr.HistoryRewardId,
                hr.CreatedDate,
                hr.Description?? string.Empty,
                hr.Reward?? new Reward(),
                hr.Client?? new Client()
            )));
        }

        [HttpGet("reward/{reward_id}")]
        public ActionResult<List<HistoryReward>> GetHistoryRewardsByRewardId([FromRoute(Name = "reward_id")] int rewardId)
        { 
            var historyRewards = _historyRewardService.GetHistoryRewardsByRewardId(rewardId);
            return Ok(historyRewards.Select(hr => new HistoryRewardListDto(
                hr.HistoryRewardId,
                hr.CreatedDate,
                hr.Description?? string.Empty,
                hr.Reward?? new Reward(),
                hr.Client?? new Client()
            )));
        }

        [HttpPost]
        public ActionResult<HistoryReward> CreateHistoryReward(HistoryRewardCreateDto historyRewardCreateDto)
        {
            var historyReward = new HistoryReward
            {
                RewardId = historyRewardCreateDto.RewardId,
                ClientId = historyRewardCreateDto.ClientId,
                Description = historyRewardCreateDto.Description,
                CreatedDate = DateTime.UtcNow
            };
            _historyRewardService.AddHistoryReward(historyReward);
            return CreatedAtAction(nameof(GetHistoryRewardByRewardId), new { rewardId = historyReward.HistoryRewardId }, historyReward);
        }
    }
}
