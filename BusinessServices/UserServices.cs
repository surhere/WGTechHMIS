using DataModel.UnitOfWork;
using System;
using BusinessEntities;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using DataModel;
using System.Transactions;
using System.Text;
using System.Security.Cryptography;
using Utilities;

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
            var user = _unitOfWork.UserRepository.Get(u => u.user_name == userName && u.password == EncryptText("wgt_hmis", password));
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
            var user = _unitOfWork.UserRepository.Get(u => u.user_name == userName && u.password == EncryptText("wgt_hmis", password));
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
                // Mapper.CreateMap<hmis_user_base, hmisUserBase>();
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DataModel.hmis_user_base, BusinessEntities.hmisUserBase>();

                });
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
                    password = EncryptText("wgt_hmis",userEntity.password),
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
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = AESManaged.AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AESManaged.AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesDecrypted);
            return result;
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
        public UserEntity GetUserById(Guid userId)
        {
           // UserEntity responseUser = new UserEntity();
            var user = _unitOfWork.UserRepository.GetByID(userId);            

            if (user != null)
            {
                // Mapper.CreateMap<hmis_user_base, hmisUserBase>();
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<hmis_user_base, hmisUserBase>();

                });
                var userModel = Mapper.Map<hmis_user_base, hmisUserBase>(user);

                var responseUser = new UserEntity
                {
                    UserId = user.SID,
                    UserName = user.user_name,
                    Password = user.password,
                    FirstName = user.first_name,
                    LastName = user.last_name                  
                };

                //responseUser.UserName = user.user_name;
                //responseUser.FirstName = user.first_name;
                //responseUser.LastName = user.last_name;

                foreach (hmis_link_user_roles uroles in userModel.hmis_link_user_roles)
                {
                    var userRoles = uroles.hsmis_role_base;
                    hmisRoleBase roles = new hmisRoleBase();                    
                    //
                    roles.role_name = userRoles.role_name;
                    roles.role_id = userRoles.role_id;
                    responseUser.Roles.Add(roles);
                    foreach(hmis_link_role_persmissions accessPermission in userRoles.hmis_link_role_persmissions)
                    {
                        hmisPermisionBase access = new hmisPermisionBase();
                        //
                        var rolePermission = accessPermission.hmis_permission_base;
                        access.access_area = rolePermission.access_area;
                        access.can_create = rolePermission.can_create;
                        access.can_read = rolePermission.can_read;
                        access.can_delete = rolePermission.can_delete;
                        responseUser.Permissions.Add(access);
                    }
                }
                return responseUser;
            }
            return null;
        }
    }
}
