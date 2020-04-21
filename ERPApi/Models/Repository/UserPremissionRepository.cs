using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class UserPremissionRepository : IUserPermissionRepository
    {
        private ERPEntities _entities;

        public UserPremissionRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<user_permission> GetAllUserPermission()
        {
            List<user_permission> lstUserPermissions = _entities.user_permission.ToList();
            return lstUserPermissions;
        }

        public user_permission GetAllUserPermissionByRoleTypeId(int role_type_id)
        {
            throw new NotImplementedException();
        }

        public List<user_permission> GetAllUserPermissionByRoleId(int role_id, int user_au_id)
        {
            List<user_permission> lstuseUserPermissions =
                _entities.user_permission.Where(up => up.user_role_id == role_id && up.user_au_id==user_au_id).ToList();

            if (lstuseUserPermissions.Count==0)
            {
                lstuseUserPermissions =
                _entities.user_permission.Where(up => up.user_role_id == role_id).ToList();
            }
            return lstuseUserPermissions;
        }

        public user_permission GetAllUserPermissionByRoleTypeAndRoleId(int role_id, int role_type_id)
        {
            throw new NotImplementedException();
        }

        bool IUserPermissionRepository.InsertUserPermission(user_permission ouserUserPermission)
        {
            try
            {
                user_permission Inser_userPermission = new user_permission

                {
                    user_au_id = ouserUserPermission.user_au_id??0,
                    user_control_id = ouserUserPermission.user_control_id,
                    user_role_id = ouserUserPermission.user_role_id
                };
                _entities.user_permission.Add(Inser_userPermission);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool IUserPermissionRepository.UpdateUserPermission(user_permission ouserUserPermission)
        {
            try
            {
                user_permission userPermission = _entities.user_permission.Find(ouserUserPermission.user_permission_id);

                userPermission.user_au_id = ouserUserPermission.user_au_id;
                userPermission.user_control_id = ouserUserPermission.user_control_id;
                userPermission.user_permission_status = ouserUserPermission.user_permission_status;
                userPermission.user_role_id = ouserUserPermission.user_role_id;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool IUserPermissionRepository.DeleteUserPermission(int user_permission_id)
        {
            throw new NotImplementedException();
        }

        public List<control> GetAllControls()
        {
            List<control> lstControls = _entities.controls.Where(c => c.control_type_id == 1).ToList();
            return lstControls;
        }

        public bool DeleteUserPermissionByRole(int user_role_id, List<user_permission> userPermissions)
        {
            try
            {
                userPermissions.ForEach(us => _entities.user_permission.Remove(us));
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<user_permission> GetAllUserPermissionByUserId(int role_id)
        {
            List<user_permission> lstuseUserPermissions =
                _entities.user_permission.Where(up => up.user_role_id == role_id && up.user_au_id==0).ToList();
            return lstuseUserPermissions;
        }

        public bool DeleteUserPermissionByUser(int roleid, List<user_permission> userPermissions)
        {
            try
            {
                userPermissions.ForEach(us => _entities.user_permission.Remove(us));
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public object GetUserByRoleId()
        {

            var data = (from us in _entities.users
                select new
                {
                    user_id = us.user_id,
                    Role_id=us.role_id,
                    user_full_name = us.user_firstname + " " + us.user_lastname??""
                }).ToList();
            return data;
        }


        public List<user_permission> GetAllUserPermissionByRoleIdOnly(int p)
        {
            try
            {
                List<user_permission> lstuseUserPermissions =
                _entities.user_permission.Where(up => up.user_role_id == p && up.user_au_id==0 ).ToList();

                
                return lstuseUserPermissions;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}