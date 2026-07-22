using LatihanSaya.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace LatihanSaya.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static string connectionString = "Server=localhost,1433;Database=Northwind;User Id=sa;Password=Password123!;TrustServerCertificate=true;";

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<Products> productList = new List<Products>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Products";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Products product = new Products();
                                product.ProductID = reader.GetInt32(0);
                                product.ProductName = reader.GetString(1);
                                product.Price = reader.GetDecimal(2);
                                productList.Add(product);
                            }
                        }
                    }
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] Products product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Products (ProductName, Price)
                        VALUES (@ProductName, @Price)
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", (object?)product.ProductName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Price", (object?)product.Price ?? DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Data berhasil ditambahkan!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
