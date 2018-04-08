
using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessServices.Services
{
    public interface IUserServices
    {
        System.Guid Authenticate(string userName, string password);
        hmisUserBase ValidateUser(string userName, string password);

        IEnumerable<hmisUserBase> GetAllUsers();
        hmisUserBase GetUserById(Guid userId);       
        Guid CreateUser(hmisUserBase productEntity);
        bool UpdateUser(Guid userId, hmisUserBase userEntity);
        bool DeleteUser(Guid userId);
    }
}
