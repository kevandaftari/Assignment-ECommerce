using ECommerceService.Data.IRepository;
using ECommerceService.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderRepository orderRepository,
            ILogger<OrderController> logger
            )
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }


        [HttpPost("[action]")]
        public IActionResult PlaceOrder([FromBody]PlaceOrderDTO placedOrder)
        {
            try
            {
                if (placedOrder == null || placedOrder.UserId == 0)
                {
                    return BadRequest("Incorect Data");
                }
                var response = _orderRepository.PlaceOrder(placedOrder);
                if (response.Item1 == 0)
                {
                    return Ok("Invalid Coupon Code or Coupon Code not Applicable for current order.");
                }
                if (response.Item1 == 1)
                {
                    return BadRequest("Cart is Empty.Please Add minimum one Item.");
                }
                return Ok(response.Item2);
            }
            catch (Exception e)
            {
                _logger.LogError("error occured in OrderController PlaceOrder:{Message}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }


}
