using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.Orders
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public long OrderSum { get; set; }



        public User.User user { get; set; }

        public virtual List<OrderDetails> orderDetails { get; set; }
    }
}
