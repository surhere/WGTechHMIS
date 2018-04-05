using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServices.Services;
using BusinessServices.Service.Interfaces;
using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel;
using AutoMapper;

namespace BusinessServices.Services
{
   public class UserAuthenticationServices : IUserAuthenticationService
    {
        
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserAuthenticationServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public BusinessEntities.hmisUserBase GetUserById (int productId)
        {
            // line login logic he to fectch user details for DB
            System.Diagnostics.Debugger.Break();
            var user = _unitOfWork.UserRepository.GetByID(productId);
            if (user != null)
            {
                Mapper.CreateMap<hmis_user_base, hmisUserBase >();
                var productModel = Mapper.Map<hmis_user_base, hmisUserBase>(user);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessEntities.hmisUserBase> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            if (users.Any())
            {
                Mapper.CreateMap<hmis_user_base, hmisUserBase>();
                var productsModel = Mapper.Map<List<hmis_user_base>, List<hmisUserBase>>(users);
                return productsModel;
            }
            return null;
        }
    }
}
