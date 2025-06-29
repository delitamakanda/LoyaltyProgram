
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ShopsController : ControllerBase
    {
        private readonly ShopService _shopService;
        public ShopsController(ShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpGet]
        public ActionResult<List<Shop>> GetShops()
        {
            var shops = _shopService.GetShops();
            return Ok(shops);
        }
        [HttpGet("{id}")]
        public ActionResult<Shop> GetShop(int id)
        {
            var shop = _shopService.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            return Ok(shop);
        }
        [HttpPost]
        public ActionResult<Shop> CreateShop(Shop shop)
        {
            _shopService.AddShop(shop);
            // return just the created item and status code 201 Created
            return CreatedAtAction(nameof(GetShop), new { id = shop.ShopId }, shop);
        }
        [HttpPut("{id}")]
        public ActionResult<Shop> UpdateShop(int id, Shop shop)
        {
            if (id!= shop.ShopId)
            {
                return BadRequest();
            }
            _shopService.UpdateShop(shop);
            return Ok(shop);
        }
    }
}