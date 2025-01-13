using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.UserDTO
{
    public class EditeUserModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "نام")]
        [MaxLength(50, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Family { get; set; }

        [Required]
        [Display(Name = "نام نمایشی")]
        [MaxLength(75, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string? Email { get; set; }
      
    }
}
