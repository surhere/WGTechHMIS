
using BusinessEntities;

namespace BusinessServices.Services
{
    public interface IUserServices
    {
        System.Guid Authenticate(string userName, string password);
        hmisUserBase ValidateUser(string userName, string password);
    }
}
