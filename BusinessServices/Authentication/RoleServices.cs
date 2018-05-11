using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    /// <summary>
    /// Offers services for role specific operations
    /// </summary>
    /// 
   
    public class RoleServices :IRoleService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public RoleServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Public method to get user roles for specific user appended to user role list.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>List of Roles</returns>
        public List<string> GetUserRoles(Guid UsersID)
        {
            List<string> userRoles = null;
            var roles =( from m in _unitOfWork.vwRoleRepository.GetMany(u => u.user_id == UsersID)
                       select m).ToList();
           
            return userRoles;
        }
    }

    
}
