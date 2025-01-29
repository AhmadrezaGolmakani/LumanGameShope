using System.ComponentModel.DataAnnotations;

namespace Luman.DataLayer.EntityModel.Orders
{
    public class OrderDetails
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public int Count { get; set; }


        public Order order { get; set; }

        public Product.Product product { get; set; }
    }
}