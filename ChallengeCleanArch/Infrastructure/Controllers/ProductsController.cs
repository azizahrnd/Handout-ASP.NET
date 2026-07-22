using Domain.Entities;
using Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsApplication _application;

        public ProductsController(IProductsApplication application)
        {
            _application = application;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _application.GetRecords();
            return Ok(result);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert([FromBody] Products product)
        {
            var result = await _application.Insert(product);
            return Ok(result);
        }
    }
}
