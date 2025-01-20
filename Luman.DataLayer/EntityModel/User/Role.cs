using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.DataLayer.EntityModel.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name ="نام ")]
        public string Name { get; set; }

        public  List<UserRole> userRoles { get; set; }

    }
}
