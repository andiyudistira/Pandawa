namespace Siska.Service
{
    using AutoMapper;
    using Siska.Core;
    using Siska.Data.Dto.Auth;
    using Siska.Data.Model.Auth;

    public abstract class ServiceBase
    {
        public ErrorType ErrorType { get; set; }

        public ServiceBase()
        {
            InitiateMapper();
            ErrorType = new ErrorType();
            ErrorType.Initialize();
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
