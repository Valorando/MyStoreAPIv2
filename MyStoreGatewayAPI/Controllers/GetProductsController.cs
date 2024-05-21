using Microsoft.AspNetCore.Mvc;
using MyStoreGatewayAPI.Interfaces;

namespace MyStoreGatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductsController : ControllerBase
    {
        private readonly IGetProductsService _getProductsService;
        private readonly ILogger<GetProductsController> _logger;

        public GetProductsController(IGetProductsService getProductsService, ILogger<GetProductsController> logger)
        {
            _getProductsService = getProductsService;            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                _logger.LogInformation("Client requested list all products");
                var info = await _getProductsService.GetAllProductsRequest();
                return Ok(info);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing Get request");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }
}
