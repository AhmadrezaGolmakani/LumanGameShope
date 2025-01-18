using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.UserDTO
{
    public class UserForAdminDTO
    {
        public List<User> users { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }
    }

    public class CreateUserDTO
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

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }
    }
}
