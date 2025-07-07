using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<List<Transaction>> GetTransactions()
        {
            return Ok(_transactionService.GetTransactions());
        }

        [HttpGet("{id}")]
        public ActionResult<Transaction> GetTransaction(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        [HttpGet("search")]
        public ActionResult<List<Transaction>> SearchTransactions(
            [FromQuery(Name = "shop_id")] int? shopId,
            [FromQuery(Name = "client_id")] int? clientId,
            [FromQuery(Name = "loyalty_card_id")] int? loyaltyCardId,
            [FromQuery(Name = "start_date")] DateTime? startDate,
            [FromQuery(Name = "end_date")] DateTime? endDate)
        {
            return Ok(_transactionService.SearchTransactionsByShopClientLoyaltyCard(shopId, clientId, loyaltyCardId, startDate, endDate));
        }

        [HttpGet("transactions/csv")]
        public ActionResult ExportTransactionsCsv()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("TransactionId,ShopName,ClientLastName, ClientFirstName,LoyaltyCardNumber,Amount,CreatedAt");
            var transactions = _transactionService.ExportTransactionsCsv();
            foreach (var transaction in transactions)
            {
                sb.AppendLine($"{transaction.TransactionId},{transaction.Shop?.Name ?? string.Empty},{transaction.LoyaltyCard?.Client?.LastName ?? string.Empty}, {transaction.LoyaltyCard?.Client?.FirstName ?? string.Empty},{transaction.LoyaltyCard?.CardNumber ?? string.Empty},{transaction.Amount},{transaction.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            }

            var csvBytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(csvBytes, "text/csv", "transactions.csv");
        }
    }
}