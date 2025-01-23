using Luman.Busines.DTOs.UserDTO;
using Luman.Busines.Services.User;
using Luman.DataLayer.EntityModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Luman.Busines.Utility;
using Luman.Busines.Services.Permission;

namespace Luman.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Admin")]
    [ApiController]
    [ApiVersion("2.0")]
    //[PermissionChecker(1)]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IPermissionService _permissionService;

        public AdminUserController(IUserServices userServices,IPermissionService permissionService)
        {
            _userServices = userServices;
            _permissionService = permissionService;
        }

        #region Users


        [HttpGet("GetUser")]

        public IActionResult GetAllUser(int pageid = 1, string fillterEmail = "", string fillterUsername = "")
        {
            return Ok(_userServices.GetUsersForAdmin(pageid, fillterEmail, fillterUsername));
        }

        [HttpPost("AddUser")]
        public IActionResult AddUserForAdmin([FromBody] CreateUserDTO user)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(user);
            }
            if (_userServices.IsExsitEmail(user.Email))
            {
                 ModelState.AddModelError("Email","ایمیل ورودی قبلا ثبت نام کرده است");
            }
            if (_userServices.IsExsitUserName(user.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری  قبلا ثبت نام کرده است");
            }
            _userServices.CreateUserForAdmin(user);


            

            return Ok();
        }

        [HttpPatch("Editeuser/{userid:int}")]
        public IActionResult UpdateUserForAdmin([FromBody] EditeUserModel model, int userid)
        {
            if (userid != model.UserId) return NotFound(model);

            if (!ModelState.IsValid) return BadRequest(model);

            var user = _userServices.GetUserById(userid);
            user.Family = model.Family;
            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.UserName;


            if (_userServices.UpdateUser(user)) return NoContent();

            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);
        }

        [HttpDelete("DeleteUser/{userid:int}")]
        public IActionResult RemoveUser(int userid)
        {
            _userServices.DeleteUser(userid);
            return NoContent();
        }




        #endregion


        #region Roles

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var role = _permissionService.GetRole();
            return Ok(role);
        }


        #endregion

    }
}
