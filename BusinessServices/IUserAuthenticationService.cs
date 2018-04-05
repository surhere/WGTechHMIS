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
        hmisUserBase GetUserById(int productId);

        IEnumerable<hmisUserBase> GetAllUsers();
    }
}
