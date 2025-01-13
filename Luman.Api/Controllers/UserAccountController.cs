using AutoMapper;
using Luman.Api.DTOs.UserDTO;
using Luman.Api.Utility;
using Luman.Busines.DTOs.UserDTO;
using Luman.Busines.Interfaces.Users;
using Luman.DataLayer.EntityModel.User;
using Luman.DataLayer.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Luman.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserServices _services;
        private readonly IMapper _mapper;

        public UserAccountController(IUserServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        /// <param name="model">مدل ورودی جهت ثبت نام </param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [HttpPost("Register")]
        public IActionResult Register(RegisterUser model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            if(!_services.IsExsitEmail(model.Email)) return BadRequest(model);

            if(!_services.IsExsitUserName(model.UserName)) return BadRequest(model);

            User user = new(){
                Name = model.Name,
                Email = model.Email,
                Family = model.Family,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                UserName = model.UserName,
                CreateDate = DateTime.Now,
                IsDelete = false,
            };

            _services.CreateUser(user);
            return Ok(user);


        }

        /// <summary>
        /// ورود کابر با سیستم jwt
        /// </summary>
        /// <param name="model">مدل ورودی جهت ورود کاربر </param>
        /// <returns>یک کد jwt را میدهد</returns> 
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model )
        {
            if (!ModelState.IsValid) return BadRequest(model);

            if (!_services.IsCorrectpass( model.UserName , model.Password)) 
            {
                return BadRequest(new { error = "کاربر یافت نشد." });

            }

            var user = _services.LoginUser(model.UserName , model.Password);
            if (user == null)
            {
                return NotFound(new {error = "کاربر یافت نشد"});
            }
            return Ok(user);
        }

        /// <summary>
        /// ویرایش کاربر از طریق پنل کاربری
        /// </summary>
        /// <param name="model">مدل ورودی جهت ویرایش حساب کاربری</param>
        /// <param name="userid">ایدی کاربر اجاری است</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPatch("Edite/{userid:int}")]
        public IActionResult EditeUser([FromBody] EditeUserModel model , int userid)
        {
            if(userid != model.UserId) return NotFound(model);

            if (!ModelState.IsValid) return BadRequest(model);

            var user =_services.GetUserById(userid);
            user.Family = model.Family;
            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.UserName;
     

            if(_services.UpdateUser(user)) return NoContent();

            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);
        }

        /// <summary>
        /// تغیر رمز عبور از طریق حساب کاربری 
        /// </summary>
        /// <param name="model">مدل ورودی جهت تغیر رمز عبور توجه داشته باشید وارد کردن username اجباری است </param>
        /// <param name="userid">ایدی کاربر اجباری است</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPatch("changepassword/{userid:int}")]
        public IActionResult ChnagePassWord([FromBody] ChangePassword model, int userid)
        {
            if (_services.ComparePassword(model.UserName , model.OldPassword))
            {
                _services.ChangePassword(model.UserName , model.NewPassword);
            }
            return Ok();
        }
    }
}
