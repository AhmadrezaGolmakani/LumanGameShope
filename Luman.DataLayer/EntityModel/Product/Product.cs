using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.Product
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "نام محصول")]
        [MaxLength(50, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "قیمت محصول")]
        public long Price { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryProduct category { get; set; } 
    }
}
