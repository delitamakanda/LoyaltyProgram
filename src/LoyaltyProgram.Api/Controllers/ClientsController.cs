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
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<List<Client>> GetClients()
        {
            var clients = _clientService.GetClients();
            return Ok(clients);
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
