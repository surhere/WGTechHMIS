using System.ComponentModel.Composition;
using System.Data.Entity;
using DataModel.UnitOfWork;
using Resolver;
using System.ComponentModel;

namespace DataModel
{
    [Export(typeof(Resolver.IComponent))]
    public class DependencyResolver : Resolver.IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
