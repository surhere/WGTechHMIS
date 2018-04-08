using DataModel.UnitOfWork;
using System;
using BusinessEntities;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using DataModel;
using System.Transactions;

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

        /// <summary>
        /// Get All users.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.hmisUserBase> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                Mapper.CreateMap<hmis_user_base, hmisUserBase>();
                var userModel = Mapper.Map<List<hmis_user_base>, List<hmisUserBase>>(users);
                return userModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a user in DBV
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public Guid CreateUser(BusinessEntities.hmisUserBase userEntity)
        {
            using (var scope = new TransactionScope())
            {
                var userHMIS = new hmis_user_base
                {
                    SID = Guid.NewGuid(),
                    user_name = userEntity.user_name,
                    password = userEntity.password,
                    first_name = userEntity.first_name,
                    last_name = userEntity.last_name,
                    createdby = "Test Data",
                    created_date = DateTime.Now,
                    modifiedby = "Test Data",
                    modified_date = DateTime.Now
          

                };
                _unitOfWork.UserRepository.Insert(userHMIS);
                _unitOfWork.Save();
                scope.Complete();
                return userHMIS.SID;
            }
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="usertId"></param>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public bool UpdateUser(Guid userId, BusinessEntities.hmisUserBase userEntity)
        {
            var success = false;
            if (userEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var user = _unitOfWork.UserRepository.GetByID(userId);
                    if (user != null)
                    {
                        user.SID = userEntity.SID;
                        _unitOfWork.UserRepository.Update(user);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(Guid userId)
        {
            var success = false;
            if (userId != null && userId!=Guid.Empty)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.UserRepository.GetByID(userId);
                    if (product != null)
                    {

                        _unitOfWork.UserRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Fetches product details by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BusinessEntities.hmisUserBase GetUserById(Guid userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user != null)
            {
                Mapper.CreateMap<hmis_user_base, hmisUserBase>();
                var userModel = Mapper.Map<hmis_user_base, hmisUserBase>(user);
                return userModel;
            }
            return null;
        }
    }
}
