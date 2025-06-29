using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;

namespace LoyaltyProgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LoyaltyCardsController : ControllerBase
    {
        private readonly LoyaltyCardService _loyaltyCardService;

        public LoyaltyCardsController(LoyaltyCardService loyaltyCardService)
        {
            _loyaltyCardService = loyaltyCardService;
        }

        [HttpGet("{card_number}")]
        public ActionResult<LoyaltyCard> GetLoyaltyCardByCardNumber([FromRoute(Name="card_number")] string cardNumber)
        {
            var loyaltyCard = _loyaltyCardService.GetLoyaltyCardByCardNumber(cardNumber);
            if (loyaltyCard == null)
            {
                return NotFound();
            }
            return Ok(loyaltyCard);
        }

        [HttpPut("{card_number}")]
        public ActionResult<LoyaltyCard> UpdateLoyaltyCard([FromRoute(Name="card_number")] string cardNumber, LoyaltyCard loyaltyCard)
        {
            if (cardNumber != loyaltyCard.CardNumber)
            {
                return BadRequest();
            }
            _loyaltyCardService.UpdateLoyaltyCard(loyaltyCard);
            return Ok(loyaltyCard);
        }

        [HttpGet]
        public ActionResult<List<LoyaltyCard>> GetLoyaltyCards()
        {
            var loyaltyCards = _loyaltyCardService.GetLoyaltyCards();
            return Ok(loyaltyCards);
        }
    }
}