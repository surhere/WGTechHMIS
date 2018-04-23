using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessServices.Service.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAuthenticationService
    {
        BusinessEntities.hmisUserBase GetUserById(int productId);

        IEnumerable<BusinessEntities.hmisUserBase> GetAllUsers();
    }
}
