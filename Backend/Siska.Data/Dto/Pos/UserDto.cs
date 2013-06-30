using System;
using System.Collections.Generic;
using System.Text;
using Iesi.Collections.Generic;

namespace Siska.Data.Dto.Pos {
    
    public class UserDto {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RecordStatus { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
        public ICollection<UserSessionDto> UserSessions { get; set; }

        public UserDto()
        {
            Roles = new HashedSet<RoleDto>();
            UserSessions = new HashedSet<UserSessionDto>();
        }
    }
}
