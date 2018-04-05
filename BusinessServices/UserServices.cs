using DataModel.UnitOfWork;
using System;
using BusinessEntities;
namespace BusinessServices.Services
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public System.Guid Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.user_name == userName && u.password == password);
            if (user != null && user.SID != System.Guid.Empty)
            {
                return user.SID;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public hmisUserBase ValidateUser(string userName, string password)
        {
            hmisUserBase UserData = new hmisUserBase();
            var user = _unitOfWork.UserRepository.Get(u => u.user_name == userName && u.password == password);
            if (user != null && user.SID != System.Guid.Empty)
            {
                UserData.last_name = user.last_name;
                UserData.first_name = user.first_name;
                UserData.SID = user.SID;
            }
            return UserData;
        }
    }
}
