using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.Product
{
    public class CategoryProduct
    {
        [Key]
        public int CategoryProductId { get; set; }

        // کلیدهای خارجی برای ارتباط بین Category و Product
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        // ارجاع به مدل‌های مرتبط
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }



    }
}
