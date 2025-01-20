using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Luman.DataLayer.EntityModel.User;

namespace Luman.DataLayer.EntityModel.Permitions
{
    public class RolePermission
    {
        [Key]
        public int RP_id { get; set; }
        public int RoleId { get; set; }
        public int PermissionID { get; set; }


        public virtual Role role { get; set; }
        [ForeignKey("PermissionID")]
        public virtual Permition permition { get; set; }
    }
}
