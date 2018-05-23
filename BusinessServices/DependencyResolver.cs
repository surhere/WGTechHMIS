using System.ComponentModel.Composition;
using DataModel;
using DataModel.UnitOfWork;
using Resolver;
using BusinessServices.Service.Interfaces;
using BusinessServices.Services;
using BusinessServices.MasterData.Interfaces;
using BusinessServices.MasterData;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserAuthenticationService, UserAuthenticationServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();
            registerComponent.RegisterType<IRoleService, RoleServices>();
            registerComponent.RegisterType<IPatientService, PatientServices>();
            registerComponent.RegisterType<IPatientAdmissionService, PatientAdmissionService>();
            registerComponent.RegisterType<IDepartmentTypeServices, DepartmentTypeServices>();

        }
    }
}
