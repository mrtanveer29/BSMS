using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private ERPEntities _entities;

        public RoleRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllRoles(int companyId)
        {
            var data = (from r in _entities.roles
                        join rt in _entities.role_type on r.role_type_id equals rt.role_type_id into RoleTypeTable from SubRole in RoleTypeTable.DefaultIfEmpty()
                             where r.company_id == companyId
                             select new
                             {
                                 role_id = r.role_id,
                                 role_name = r.role_name,
                                 role_type_name = SubRole.role_type_name,
                                 role_type_id=r.role_type_id,
                                is_active= r.is_active,
                                is_fixed= r.is_fixed

                             }).OrderByDescending(x=>x.role_id).ToList();
            return data;
        }

        public object GetRolenameByID(int emp_id)
        {
            //  string role_name = (from e in _entities.employees ...... }).FirstOrDefault().role_name;  it is for single value
            var role_name = (from e in _entities.employees
                             join r in _entities.roles on e.employee_role equals r.role_id
                             where e.emp_id == emp_id
                             select new
                             {
                                 role_id = r.role_id,
                                 role_name = r.role_name
                             }).FirstOrDefault();
            return role_name;
        }

        public role GetRoleByID(int role_id)
        {
            throw new NotImplementedException();
        }

        public role GetRoleByName(string role_name)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoleForDuplicateByname(string role_name,string company_id)
        {
            int com_id=Int32.Parse(company_id);
            var checkDuplicateRole = _entities.roles.FirstOrDefault(r => r.role_name == role_name && r.company_id == com_id);

            if (checkDuplicateRole == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InsertRole(role oRole)
        {
            try
            {
                string temp_source_type = "";
                if (oRole.role_type_id == 1)
                {
                    temp_source_type = "E";
                }else if (oRole.role_type_id == 2)
                {
                    temp_source_type = "S";
                }else if (oRole.role_type_id == 4)
                {
                    temp_source_type = "C";
                }else if (oRole.role_type_id == 5)
                {
                    temp_source_type = "R";
                }
                role insert_role = new role
                {
                    role_name = oRole.role_name,
                    is_active = oRole.is_active,
                    created_by = oRole.created_by,
                    created_date = oRole.created_date,
                    updated_by = oRole.updated_by,
                    updated_date = oRole.updated_date,
                    company_id = oRole.company_id,
                    is_fixed = oRole.is_fixed,
                    role_type_id = oRole.role_type_id,
                    source_type =  temp_source_type
                };
                _entities.roles.Add(insert_role);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool UpdateRole(role oRole)
        {
            try
            {
                string temp_source_type = "";
                if (oRole.role_type_id == 1)
                {
                    temp_source_type = "E";
                }
                else if (oRole.role_type_id == 2)
                {
                    temp_source_type = "S";
                }
                else if (oRole.role_type_id == 4)
                {
                    temp_source_type = "C";
                }
                else if (oRole.role_type_id == 5)
                {
                    temp_source_type = "R";
                }
                role ro = _entities.roles.Find(oRole.role_id);
                ro.role_name = oRole.role_name;
                ro.is_active = oRole.is_active;
                ro.updated_by = oRole.updated_by;
                ro.updated_date = oRole.updated_date;
               // ro.company_id = oRole.company_id;
                ro.is_fixed = oRole.is_fixed;
                ro.role_type_id = oRole.role_type_id;
                ro.source_type = temp_source_type;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRole(int role_id)
        {
            try
            {
                role oRole = _entities.roles.FirstOrDefault(r => r.role_id == role_id);
                _entities.roles.Attach(oRole);
                _entities.roles.Remove(oRole);

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object GetAllRolesByType()
        {
            var roles = (from r in _entities.roles
                         join rt in _entities.role_type
                             on r.role_type_id equals rt.role_type_id
                         //where r.role_type_id==1
                         select new
                         {
                             role_id = r.role_id,
                             role_name = r.role_name,
                             is_active = r.is_active,
                             updated_by = r.updated_by,
                             updated_date = r.updated_date,
                             company_id = r.company_id,
                             is_fixed = r.is_fixed,
                             role_type_id = rt.role_type_id,
                             role_type_name = rt.role_type_name
                         }).ToList();
            return roles;
        }

        public List<role> GetEmployeeRoleType()
        {
            List<role> roles = _entities.roles.Where(r => r.role_type_id == 1).OrderByDescending(r => r.role_id).ToList();
            return roles;
        }

        public List<role> GetsupllierRoleType()
        {
            List<role> roles = _entities.roles.Where(r => r.role_type_id == 2).OrderByDescending(r => r.role_id).ToList();
            return roles;
        }


        public List<role> GetEmployeeRoleTypeBySource(int companyId)
        {
            List<role> roles = _entities.roles.Where(r => r.source_type == "E" && r.company_id == companyId).OrderByDescending(r => r.role_id).ToList();
            return roles;
           
        }

        public List<role> GetAllRolesForEmpUser(int company_id)
        {
            List<role> roles = _entities.roles.Where(c => c.company_id == company_id).ToList();

            return roles;
        }



        public List<role> GetAssignedRoleForUser(int user_id)
        {
            string sql;
            List<role> lstAssignedRole = null;
            var tt = _entities.user_role_mapping.Count(a => a.user_id == user_id);
            if (tt == 0)
            {
                sql = "select * from role where role_id in (select role_id from users where user_id=" + user_id + ")";
                lstAssignedRole = _entities.Database.SqlQuery<role>(sql).ToList();
            }
            else
            {
                sql = "select * from role where role_id in (select role_id from user_role_mapping where user_id=" + user_id + ")";
                lstAssignedRole = _entities.Database.SqlQuery<role>(sql).ToList();
            }
            return lstAssignedRole;
        }
        public List<role> GetUnassignedRoleForUser(int user_id)
        {
            string sql;
            List<role> lstUnassignedRole = null;
            var tt = _entities.user_role_mapping.Count(a => a.user_id == user_id);
            if (tt == 0)
            {
                sql = "select * from role where role_id not in (select role_id from users where user_id=" + user_id + ")";
                lstUnassignedRole = _entities.Database.SqlQuery<role>(sql).ToList();
            }
            else
            {
                sql = "select * from role where role_id not in (select role_id from user_role_mapping where user_id=" + user_id + ")";
                lstUnassignedRole = _entities.Database.SqlQuery<role>(sql).ToList();
            }

            return lstUnassignedRole;
        }

        public object GetRoleByRoleId(int roleId)
        {
            return _entities.roles.Find(roleId);
        }
    }
}