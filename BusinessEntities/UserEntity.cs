using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<hmisRoleBase> Roles { get; set; }
        public List<hmisPermisionBase> Permissions { get; set; }
        public UserEntity()
        {
            this.Roles = new List<hmisRoleBase>();
            this.Permissions = new List<hmisPermisionBase>();
        }
    }
}
