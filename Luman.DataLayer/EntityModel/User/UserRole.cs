using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.User
{
    public class UserRole
    {
        [Key] 
        public int RU_Id { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }

        public Role role { get; set; }

    }
}
