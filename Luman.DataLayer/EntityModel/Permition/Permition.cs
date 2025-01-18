using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.Permition
{
    public class Permition
    {
        [Key]
        public int PermissionID { get; set; }
        [Display(Name = "نام دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        public string PermissionTitel { get; set; }


        
        public List<RolePermission> rolePermissions { get; set; }
    }
}
