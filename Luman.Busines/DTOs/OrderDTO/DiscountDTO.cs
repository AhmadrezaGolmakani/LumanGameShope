using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.OrderDTO
{
    public class CreateDiscountDTO
    {
        public int DiscountId { get; set; }

        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Required]
        public int DiscountPercent { get; set; }

        public int? UsableCount { get; set; }

        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        // مشخص می‌کند که کد تخفیف برای یک محصول خاص است یا تمامی محصولات
        //public string? ProductName { get; set; }
    }
}
