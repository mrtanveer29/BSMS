using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IUserPermissionRepository
    {
        List<user_permission> GetAllUserPermission();

        user_permission GetAllUserPermissionByRoleTypeId(int role_type_id);

        List<user_permission> GetAllUserPermissionByRoleId(int role_id,int user_au_id);

        List<user_permission> GetAllUserPermissionByUserId(int role_id);

        user_permission GetAllUserPermissionByRoleTypeAndRoleId(int role_id, int role_type_id);

        bool InsertUserPermission(user_permission ouserUserPermission);

        bool UpdateUserPermission(user_permission ouserUserPermission);

        bool DeleteUserPermission(int user_permission_id);

        bool DeleteUserPermissionByRole(int user_role_id, List<user_permission> userPermissions);

        bool DeleteUserPermissionByUser(int roleid, List<user_permission> userPermissions);

        List<control> GetAllControls();

        object GetUserByRoleId();

        List<user_permission> GetAllUserPermissionByRoleIdOnly(int p);
    }
}