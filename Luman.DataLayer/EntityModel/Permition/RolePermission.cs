using System.ComponentModel.DataAnnotations;
using Luman.DataLayer.EntityModel.User;

namespace Luman.Busines.Services.Permition
{
    public class RolePermission
    {
        [Key]
        public int RP_id { get; set; }
        public int RoleId { get; set; }
        public int PermissionID { get; set; }


        public Role role { get; set; }
        public Permition permition { get; set; }
    }
}
