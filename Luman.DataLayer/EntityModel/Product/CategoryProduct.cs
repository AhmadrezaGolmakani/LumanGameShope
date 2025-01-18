using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.Product
{
    public class CategoryProduct
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }


        [Required]
        [Display(Name = "دسته بندی محصول")]
        [MaxLength(50, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Name { get; set; }


        public List<Product> products { get; set; }
    }
}
