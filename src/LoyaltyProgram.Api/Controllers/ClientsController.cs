using Microsoft.AspNetCore.Mvc;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LoyaltyProgram.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients(
            [FromQuery] int page = 1,
            [FromQuery(Name = "page_size")] int pageSize = 20,
            [FromQuery(Name = "sort_by")] string sortBy = "DateCreated",
            [FromQuery] bool ascending = true,
            [FromQuery(Name = "q")] string? search = null,
            [FromQuery(Name = "start_date")] DateTime? startDate = null,
            [FromQuery(Name = "end_date")] DateTime? endDate = null
        )
        {
            var clients = _clientService.GetClients();
            var query = clients.AsQueryable();
            var paginatedResult = await query.ToPagedResultAsync(
                page,
                pageSize,
                sortBy,
                ascending,
                search,
                startDate,
                endDate,
                x => x.DateCreated!.Value,
                x => x.FirstName!,
                x => x.LastName!,
                x => x.PhoneNumber!,
                x => x.Email!
            );
            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public ActionResult<Client> CreateClient(ClientCreateDto clientCreateDto)
        {
            var newClient = new Client
            {
                FirstName = clientCreateDto.FirstName,
                LastName = clientCreateDto.LastName,
                Address = clientCreateDto.Address,
                Email = clientCreateDto.Email,
                PhoneNumber = clientCreateDto.PhoneNumber,
                DateCreated = DateTime.UtcNow
            };
            _clientService.RegisterClient(newClient);
            // return just the created item and status code 201 Created
            return CreatedAtAction(nameof(GetClient), new { id = newClient.ClientId }, newClient);
        }

        [HttpPut("{id}")]
        public ActionResult<Client> UpdateClient(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }
            _clientService.UpdateClient(client);
            return Ok(client);
        }
    }
}
