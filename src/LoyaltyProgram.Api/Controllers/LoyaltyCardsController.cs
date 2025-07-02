using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Dtos;

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
            var loyaltyCardDto = new LoyaltyCardListDto(
                loyaltyCard.LoyaltyCardId,
                loyaltyCard.CardNumber ?? string.Empty,
                loyaltyCard.Points,
                loyaltyCard.DateCreated,
                loyaltyCard.Status,
                loyaltyCard.Rank
            );
            return Ok(loyaltyCardDto);
        }

        [HttpPut("{card_number}")]
        public ActionResult<LoyaltyCard> UpdateLoyaltyCard([FromRoute(Name = "card_number")] string cardNumber, LoyaltyCardUpdatetDto loyaltyCardUpdateDto)
        {
            var loyaltyCard = _loyaltyCardService.GetLoyaltyCardByCardNumber(cardNumber);
            if (loyaltyCard == null)
            {
                return NotFound();
            }
            loyaltyCard.CardNumber = loyaltyCardUpdateDto.CardNumber;
            loyaltyCard.Points = loyaltyCardUpdateDto.Points;
            loyaltyCard.Status = loyaltyCardUpdateDto.Status;
            loyaltyCard.Rank = loyaltyCardUpdateDto.Rank;
            _loyaltyCardService.UpdateLoyaltyCard(loyaltyCard);
            return Ok(loyaltyCard);
        }

        [HttpGet]
        public ActionResult<List<LoyaltyCard>> GetLoyaltyCards()
        {
            var loyaltyCards = _loyaltyCardService.GetLoyaltyCards();
            var loyaltyCardsDto = loyaltyCards.Select(card => new LoyaltyCardListDto(
                card.LoyaltyCardId,
                card.CardNumber?? string.Empty,
                card.Points,
                card.DateCreated,
                card.Status,
                card.Rank
            ));
            return Ok(loyaltyCardsDto);
        }

        [HttpGet("{card_number}/transactions")]
        public ActionResult<List<Transaction>> GetTransactionsByLoyaltyCardNumber([FromRoute(Name = "card_number")] string loyaltyCardNumber)
        {
            var transactions = _loyaltyCardService.GetTransactionsByLoyaltyCardNumber(loyaltyCardNumber);
            return Ok(transactions);
        }

        [HttpPost("{card_number}/transactions")]
        public ActionResult<Transaction> AddTransactionToLoyaltyCard([FromRoute(Name = "card_number")] string loyaltyCardNumber, Transaction transaction)
        { 
            _loyaltyCardService.AddTransactionToLoyaltyCard(loyaltyCardNumber, transaction);
            return Ok(transaction);
        }
    }
}
