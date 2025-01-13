using System.ComponentModel.DataAnnotations;

namespace Luman.Api.DTOs.UserDTO
{
    public class RegisterUser
    {
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

        [Required]
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0}نمیتواند بیتر از {1}  کاراکتر باشد .")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }


    }

    public class LoginUser
    {
        
    }
}
