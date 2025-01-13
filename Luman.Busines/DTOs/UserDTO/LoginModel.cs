using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.UserDTO
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "نام نمایشی")]
        [MaxLength(75, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
    }
}
