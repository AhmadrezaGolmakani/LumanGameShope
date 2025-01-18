using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Luman.Busines.DTOs.UserDTO

{
    public class LoginResultDTO
    {
        public int userId { get; set; }

        public string Mobile { get; set; }


        /// <summary>
        /// توکن احراز هویت Bearer
        /// </summary>
        public string JwtSecret { get; set; }

        public string Role { get; set; }
    }
}
