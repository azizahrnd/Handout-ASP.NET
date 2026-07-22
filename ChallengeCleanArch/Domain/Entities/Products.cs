using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
