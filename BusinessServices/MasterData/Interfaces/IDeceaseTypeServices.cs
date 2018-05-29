using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.MasterData.Interfaces
{
    public interface IDeceaseTypeServices
    {
        string CreateDeceaseType(hmisDeceaseTypeMaster departmentEntity);
        IEnumerable<hmisDeceaseTypeMaster> GetAllDeceaseTypes();
    }
}
