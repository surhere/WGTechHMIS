using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices.MasterData.Interfaces
{
    public interface IDepartmentTypeServices
    {
         string CreateDepartmentType(hmisDepartmentTypeMaster departmentTypeEntity);
         IEnumerable<hmisDepartmentTypeMaster> GetAllDepartmentTypes();
    }
}
