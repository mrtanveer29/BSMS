using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace ERPApi.Models.Repository
{
    public class EmpUserRepository : IEmpUserRepository
    {
        private ERPEntities _entities;

        public EmpUserRepository()
        {

            this._entities = new ERPEntities();

        }
        public int GetUserId(int emp_id)
        {
            int userID = 0;
            var UserInfo = (from u in _entities.users
                            where u.employee_id == emp_id
                            select u).FirstOrDefault();
            if (UserInfo !=null)
            {
                userID = UserInfo.user_id;
            }
            return userID;

        }
        public bool InsertEmpUser(user oEmpUser)
        {
            try
            {
                var employeeInfo = _entities.employees.Where(e => e.emp_id == oEmpUser.employee_id).FirstOrDefault();

                user Insert_emp_user = new user
                {

                    user_id = oEmpUser.user_id,
                    user_name = oEmpUser.user_name,
                    password = oEmpUser.password,
                    role_id = oEmpUser.role_id,
                    employee_id = oEmpUser.employee_id,
                    role_type_id = oEmpUser.role_type_id,
                    company_id = oEmpUser.company_id,
                    customer_id = oEmpUser.customer_id,
                    user_firstname = employeeInfo.emp_firstname,
                    user_lastname = employeeInfo.emp_lastname
                };

                _entities.users.Add(Insert_emp_user);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetUserInformation(int emp_id)
        {
            List<user> UserInfo = (from u in _entities.users
                                   where u.employee_id == emp_id
                                   select u).ToList();
            if (UserInfo.Count() > 0)
            {
                //If there is data
                return 1;
            }
            else
            {
                //If there is no data
                return 0;
            }

        }



        public object GetEmployeeInformationByUserId(int user_id)
        {
            try
            {
                var data = _entities.users.FirstOrDefault(u => u.user_id == user_id);
                return data;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public bool DeleteExistiingSignature(int userId)
        {
            try
            {
                var user = _entities.users.SingleOrDefault(a => a.user_id == userId);

                if (user.signature != "")
                {
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Signature/"), user.signature);
                    File.Delete(fileSavePath);
                }
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateEmpUser(user oEmpUser)
        {
            try
            {
                user update = _entities.users.Find(oEmpUser.user_id);
                employee empInfo = _entities.employees.Find(oEmpUser.employee_id);

                if (update==null)
                {
                    user insertuser = new user
                    {
                        user_name = oEmpUser.user_name,
                        password = oEmpUser.password,
                        user_firstname = empInfo.emp_firstname??"",
                        user_lastname = empInfo.emp_lastname??"",
                        company_id = oEmpUser.company_id,
                        signature = oEmpUser.signature,
                        role_type_id = oEmpUser.role_type_id,
                        role_id = oEmpUser.role_id,
                        employee_id = oEmpUser.employee_id
                        

                    };
                    _entities.users.Add(insertuser);
                    _entities.SaveChanges();

                }
                else
                {
                    update.user_name = oEmpUser.user_name;
                    update.password = oEmpUser.password;
                    update.role_id = oEmpUser.role_id;
                    update.user_firstname = empInfo.emp_firstname??"";
                    update.user_lastname = empInfo.emp_lastname??"";
                    update.employee_id = oEmpUser.employee_id;
                    update.role_type_id = oEmpUser.role_type_id;
                    update.company_id = oEmpUser.company_id;
                    update.customer_id = oEmpUser.customer_id;
                    update.signature = oEmpUser.signature;

                    _entities.SaveChanges();
                }
                


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool delete(int userId)
        {
            try
            {
                var tt = _entities.user_role_mapping.Where(a => a.user_id == userId).ToList();
                int s = 0;
                if (tt.Count == 0)
                {
                    return true;
                }
                else
                {
                    foreach (var userRoleMapping in tt)
                    {
                        _entities.user_role_mapping.Remove(userRoleMapping);
                        s = _entities.SaveChanges();
                    }

                    if (s > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int lastUserId()
        {
            int userId = _entities.users.OrderByDescending(u => u.user_id).FirstOrDefault().user_id;
            return userId;
        }
        public bool InserUserRoleMapping(user_role_mapping oUserRoleMapping)
        {
            try
            {
                int userId = int.Parse(oUserRoleMapping.user_id.ToString());


                user_role_mapping inserUserRoleMapping = new user_role_mapping
                {

                    user_id = oUserRoleMapping.user_id,
                    role_id = oUserRoleMapping.role_id,

                };

                _entities.user_role_mapping.Add(inserUserRoleMapping);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}