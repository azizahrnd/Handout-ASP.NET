using System.ComponentModel.DataAnnotations;

namespace LatihanEFCore.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
