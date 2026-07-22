using CRUD.ADO.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CRUD.ADO.NET.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoriesController : ControllerBase
	{
<<<<<<< Updated upstream
		private static string connectionString = "Server=localhost,1433;Database=Northwind;User Id=sa;Password=Password123!;TrustServerCertificate=true;";
=======
		private readonly string _connectionString;

		public CategoriesController(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}
>>>>>>> Stashed changes

		[HttpPost("Insert")]
		public IActionResult Insert([FromBody] Categories categories)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					string query;
					if (categories.CategoryID == null)
					{
						query = @"
									INSERT INTO Categories
									(
										CategoryName
										,Description
										,Picture
									) 
									values 
									(
										@CategoryName
										,@Description
										,@Picture
									)
								";
					}
					else
					{
						query = @"
									INSERT INTO Categories
									(
										CategoryID
										,CategoryName
										,Description
										,Picture
									) 
									values 
									(
										@CategoryID
										,@CategoryName
										,@Description
										,@Picture
									)
								";
					}
					using (SqlCommand command = new SqlCommand
						(query, connection))
					{
						command.Parameters.AddWithValue("@CategoryID", (object?)categories.CategoryID ?? DBNull.Value);
						command.Parameters.AddWithValue("@CategoryName", (object?)categories.CategoryName ?? DBNull.Value);
						command.Parameters.AddWithValue("@Description", (object?)categories.Description ?? DBNull.Value);
						var pictureParam = command.Parameters.Add("@Picture", System.Data.SqlDbType.VarBinary, -1);
						pictureParam.Value = (object?)categories.Picture ?? DBNull.Value;
						command.ExecuteNonQuery();

					}
					connection.Close();
					connection.Dispose();
				};

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetRows")]
		public IActionResult GetRows()
		{
			try
			{
				List<Categories> categoriesList = new List<Categories>();
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					string query = @$"
										SELECT * FROM Categories

									";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								Categories category = new Categories();
								category.CategoryID = reader.GetInt32(0);
								category.CategoryName = reader.GetString(1);
								category.Description = reader.GetString(2);
								categoriesList.Add(category);
							}
						}
					}
					connection.Close();
					connection.Dispose();
				}

				return Ok(categoriesList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("UpdateByID")]
		public IActionResult UpdateById([FromBody] Categories categories)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					string query = @$"
										UPDATE Categories
										SET Description = @Description
										WHERE CategoryID = @CategoryID
									";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Description", categories.Description);
						command.Parameters.AddWithValue("@CategoryID", categories.CategoryID);
						command.ExecuteNonQuery();
					}
					connection.Close();
					connection.Dispose();

					return Ok();

				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}


		}
		[HttpDelete("DeleteByID")]
		public IActionResult DeleteById(int id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					string query = @$"
										DELETE FROM Categories
										WHERE CategoryID = @CategoryID
									";

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@CategoryID", id);
						command.ExecuteNonQuery();
					}

					return Ok();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


	}
}
