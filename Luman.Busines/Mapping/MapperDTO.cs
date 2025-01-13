using AutoMapper;
using Luman.Busines.DTOs.UserDTO;
using Luman.DataLayer.EntityModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Mapping
{
    public class MapperDTO :Profile
    {
        public MapperDTO()
        {
            CreateMap<User, EditeUserModel>().ReverseMap();
                           
        }
    }
}
