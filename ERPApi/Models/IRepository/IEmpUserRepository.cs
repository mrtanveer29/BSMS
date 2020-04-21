using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IEmpUserRepository
    {
       //object GetAllCreateUser(int company_id);
       int GetUserInformation(int emp_id);
       bool InsertEmpUser(user oEmpUser);

       int GetUserId(int emp_id);
       object GetEmployeeInformationByUserId(int user_id);

       bool DeleteExistiingSignature(int uId);

       bool UpdateEmpUser(user update_emp_user);

       bool delete(int userId);

       bool InserUserRoleMapping(user_role_mapping InsertUserRoleMapping);

       int lastUserId();
    }
}
