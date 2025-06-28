using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoyaltyProgram.Infrastructure;
using LoyaltyProgram.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LoyaltyProgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly LoyaltyDbContext _context;

        public ClientsController(LoyaltyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }
    }
}