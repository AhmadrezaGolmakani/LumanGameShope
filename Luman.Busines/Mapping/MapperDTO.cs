using AutoMapper;
using Luman.Busines.DTOs.UserDTO;
using Luman.Busines.Utility;
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
            CreateMap<User, InformationUserPanel>()
                .ForMember(x => x.CreateDate, d => d.MapFrom(res => res.CreateDate.ToPersainDate()));
               
               

        }
    }
}
