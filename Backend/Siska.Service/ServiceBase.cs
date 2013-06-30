using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Siska.Data.Dto.Pos;
using Siska.Data.Model.Pos;

namespace Siska.Service
{
    public abstract class ServiceBase
    {
        public ServiceBase()
        {
            InitiateMapper();
        }

        private void InitiateMapper()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<Role, RoleDto>();
            Mapper.CreateMap<UserSession, UserSessionDto>();

            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<RoleDto, Role>();
            Mapper.CreateMap<UserSessionDto, UserSession>();
        }
    }
}
