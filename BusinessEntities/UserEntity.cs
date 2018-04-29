using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
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
