using BusinessEntities;
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
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<UserRoles> GetUserRoles(Guid UsersID)
        {
            List<UserRoles> userRoles = null;
            _unitOfWork.vwRoleRepository.GetAll();

           
            return userRoles;
        }
    }

    
}
