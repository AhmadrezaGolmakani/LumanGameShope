using Luman.DataLayer.EntityModel.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.Orders
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Required]
        public int DiscountPercent { get; set; }

        public int? UsableCount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsForSpecificProduct { get; set; } = false;

        // شناسه محصول خاص (در صورت فعال بودن تخفیف برای محصول خاص)
        public int? ProductId { get; set; }

        // ناوبری به محصول (در صورت وجود)
        public virtual Product.Product product { get; set; }
    }
}
