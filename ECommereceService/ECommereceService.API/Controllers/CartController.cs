using ECommerceService.Data.IRepository;
using ECommerceService.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("[Controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartController> _logger;

        public CartController(
            ICartRepository cartRepository,
            ILogger<CartController> logger
            )
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }


        [HttpPost("[action]")]
        public IActionResult AddItem([FromBody] AddCartItemDTO cartItemToAdd)
        {
            try
            {
                if (cartItemToAdd == null
                    || cartItemToAdd.UserId == 0
                    || cartItemToAdd.ItemId == 0
                    || cartItemToAdd.Count == 0
                   )
                {
                    return BadRequest();
                }
                var cart = _cartRepository.AddItem(cartItemToAdd);
                if (cart == null)
                {
                    return BadRequest("Item not found or request count of item is more than inventory");
                }
                return Ok(cart);
            }
            catch (Exception e)
            {
                _logger.LogError("error occured in CartController AddItem:{Message}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
