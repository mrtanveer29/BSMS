using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IRoleTypeRepository
    {
        List<role_type> GetAllRolesOnly();

        object GetAllRoleType();

        role_type GetRoleTypeById(int role_type_id);

        role_type GetRoleTypeByName(string role_type_name);

        bool CheckRoleForDuplicateByname(string role_type_name);

        bool InsertRoleType(role_type oRoleType);

        bool UpdateRoleType(role_type oRoleType);

        bool DeleteRoleType(int role_Type_id);
    }
}