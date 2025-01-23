using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.ProductDTO
{
    public class CreateCategoriesDTO
    {
        [Required]
        [Display(Name = "دسته بندی محصول")]
        [MaxLength(50, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Name { get; set; }
    }

    public class CategoryForAdmin
    {
        public string Name { get; set; }
    }
}
