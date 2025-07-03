
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Api.Controllers
{
    [Authorize]
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
            var shopDto = shops.Select(s => new ShopListCreateUpdateDto(s.Name ?? string.Empty, s.Address ?? string.Empty)).ToList();
            return Ok(shopDto);
        }
        [HttpGet("{id}")]
        public ActionResult<Shop> GetShop(int id)
        {
            var shop = _shopService.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            var shopDto = new ShopListCreateUpdateDto(shop.Name ?? string.Empty, shop.Address ?? string.Empty);
            return Ok(shopDto);
        }
        [HttpPost]
        public ActionResult<Shop> CreateShop(ShopListCreateUpdateDto shopListCreateUpdateDto)
        {
            var shop = new Shop
            {
                Name = shopListCreateUpdateDto.Name,
                Address = shopListCreateUpdateDto.Address
            };
            _shopService.AddShop(shop);
            // return just the created item and status code 201 Created
            return CreatedAtAction(nameof(GetShop), new { id = shop.ShopId }, shop);
        }
        [HttpPut("{id}")]
        public ActionResult<Shop> UpdateShop(int id, ShopListCreateUpdateDto shopListCreateUpdateDto)
        {
            var shop = _shopService.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            if (id != shop.ShopId)
            {
                return BadRequest();
            }
            shop.Name = shopListCreateUpdateDto.Name;
            shop.Address = shopListCreateUpdateDto.Address;
            _shopService.UpdateShop(shop);
            return Ok(shop);
        }
        [HttpDelete("{id}")]
        public ActionResult<Shop> DeleteShop(int id)
        {
            var shop = _shopService.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            _shopService.DeleteShop(id);
            return NoContent();
        }
        [HttpGet("rank/{shop_id}")]
        public ActionResult<List<RankSystem>> GetShopRankSystem([FromRoute(Name = "shop_id")] int shopId)
        {
            var shop = _shopService.GetShopRankSystem(shopId);
            if (shop == null)
            {
                return NotFound();
            }

            return Ok(shop);
        }

        [HttpPost("rank/{shop_id}")]
        public ActionResult<RankSystem> CreateShopRankSystem([FromRoute(Name = "shop_id")] int shopId, ShopRankSystemListCreateDto shopRankSystemListCreateDto)
        {
            var shop = _shopService.GetShopById(shopId);
            if (shop == null)
            {
                return NotFound();
            }
            var rankSystem = new RankSystem
            {
                PointsNeeded = shopRankSystemListCreateDto.PointsNeeded,
                Rank = shopRankSystemListCreateDto.Rank,
                RankDescription = shopRankSystemListCreateDto.RankDescription
            };
            shop.RankSystem.Add(rankSystem);
            _shopService.UpdateShop(shop);
            // return just the created item and status code 201 Created
            return CreatedAtAction(nameof(GetShopRankSystem), new { shop_id = shop.ShopId }, rankSystem);
        }
    }
}
