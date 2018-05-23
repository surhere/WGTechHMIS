using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.MasterData.Interfaces
{
    public interface IDepartmentServices
    {
        string CreateDepartment(hmisDepartmentMaster departmentEntity);
        IEnumerable<hmisDepartmentMaster> GetAllDepartments();
    }
}
