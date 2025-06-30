using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;

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
        public ActionResult<LoyaltyCard> GetLoyaltyCardByCardNumber([FromRoute(Name = "card_number")] string cardNumber)
        {
            var loyaltyCard = _loyaltyCardService.GetLoyaltyCardByCardNumber(cardNumber);
            if (loyaltyCard == null)
            {
                return NotFound();
            }
            return Ok(loyaltyCard);
        }

        [HttpPut("{card_number}")]
        public ActionResult<LoyaltyCard> UpdateLoyaltyCard([FromRoute(Name = "card_number")] string cardNumber, LoyaltyCard loyaltyCard)
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

        [HttpGet("{card_number}/transactions")]
        public ActionResult<List<Transaction>> GetTransactionsByLoyaltyCardNumber([FromRoute(Name = "card_number")] string loyaltyCardNumber)
        {
            var transactions = _loyaltyCardService.GetTransactionsByLoyaltyCardNumber(loyaltyCardNumber);
            return Ok(transactions);
        }

        [HttpPost("{card_number}/transactions")]
        public ActionResult<LoyaltyCard> AddTransactionToLoyaltyCard([FromRoute(Name = "card_number")] string loyaltyCardNumber, Transaction transaction)
        { 
            _loyaltyCardService.AddTransactionToLoyaltyCard(loyaltyCardNumber, transaction);
            return Ok(transaction);
        }
    }
}
