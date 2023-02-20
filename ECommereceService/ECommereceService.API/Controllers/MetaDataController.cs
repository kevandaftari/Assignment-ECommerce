using ECommerceService.Data.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("[Controller]")]
    public class MetaDataController : ControllerBase
    {
        private readonly IOrderMetaDataRepository _orderMetaDataRepository;
        private readonly ILogger<MetaDataController> _logger;

        public MetaDataController(
            IOrderMetaDataRepository orderMetaDataRepository,
            ILogger<MetaDataController> logger
            )
        {
            _orderMetaDataRepository = orderMetaDataRepository;
            _logger = logger;
        }


        [HttpGet("[action]")]
        public IActionResult GetOrderMetaData()
        {
            try
            {
                var metaData = _orderMetaDataRepository.GetOrderMetaData();
                if (metaData == null)
                {
                    return NoContent();
                }
                return Ok(metaData);
            }
            catch (Exception e)
            {
                _logger.LogError("error occured in MetaDataController GetOrderMetaData:{Message}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
