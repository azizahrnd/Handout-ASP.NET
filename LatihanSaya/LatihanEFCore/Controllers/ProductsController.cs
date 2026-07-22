using LatihanEFCore.DBContext;
using LatihanEFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LatihanEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Products>> GetAll()
        {
            var result = _context.Products.ToList();
            return Ok(result);
        }

        [HttpPost("Insert")]
        public ActionResult Insert([FromBody] Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Data berhasil ditambahkan!");
        }
    }
}
