using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.UserDTO
{
    public class InformationUserPanel
    {
       
        public string Name { get; set; }

       
        public string Family { get; set; }


        public string UserName { get; set; }

       
        public string Email { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
