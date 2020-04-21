using System.Security.Cryptography.X509Certificates;

using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private ERPEntities _entities;

        public LoginRepository()
        {
            this._entities = new ERPEntities();
        }

        public object LoginInformation(string user_name, string password)
        {
            try
            {
               var super_user= _entities.super_user.FirstOrDefault(x => x.user_name == user_name && x.password == password);
               if (super_user != null)
                {
                    LoginModel login = new LoginModel();
                    login.user_id = super_user.user_id;
                    login.user_name = super_user.user_name;
                    login.password = super_user.password;
                    login.role_id = 0;
                    login.role_type_id = super_user.role_type_id;
                    login.employee_id = 0;
                    login.company_id = 0;
                    login.branch_id = 0;

                    login.employee_email = super_user.user_email;
                    login.employee_name = super_user.user_name;
                    login.user_role_name = "Super User";
                    login.customer_id = 0;
                    return login;
                }

        
                var userCheckExists =
                    _entities.users.FirstOrDefault(x => x.user_name == user_name && x.password == password);
                if (userCheckExists != null)
                {
                    //return user_check_exists;

                    LoginModel login = new LoginModel();

                    login.user_id = userCheckExists.user_id;
                    login.user_name = userCheckExists.user_name;
                    login.password = userCheckExists.password;
                    login.role_id = userCheckExists.role_id;
                    login.role_type_id = userCheckExists.role_type_id;
                    login.employee_id = userCheckExists.employee_id;
                    login.company_id = userCheckExists.company_id;
                    login.branch_id = _entities.employees.Where(i => i.emp_id == userCheckExists.employee_id).Select(b => b.branch_id).SingleOrDefault();
                    
                    login.employee_email = _entities.employees.Where(a => a.emp_id == userCheckExists.employee_id).Select(a => a.employee_email).SingleOrDefault();
                    login.employee_name = _entities.employees.Where(b => b.emp_id == userCheckExists.employee_id).Select(b => (string.IsNullOrEmpty(b.emp_firstname) ? " " : b.emp_firstname) + " " + (string.IsNullOrEmpty(b.emp_lastname) ? "" : b.emp_lastname)).SingleOrDefault();
                    login.user_role_name = _entities.roles.Where(r => r.role_id == userCheckExists.role_id).Select(c => c.role_name).SingleOrDefault();
                    login.customer_id = userCheckExists.customer_id;
    
                    return login;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object UserLogin(string userName, string password)
        {
            var desc= _entities.designations.FirstOrDefault(x => x.designation_name == "Counter Man").designation_id;
            var loginInfo = (from us in _entities.users.Where(x => x.user_name == userName && x.password == password)
                join emp in _entities.employees.Where(x => x.designation_id == desc) on us.employee_id equals emp.emp_id
                join com in _entities.companies on emp.company_id equals com.company_id
                //join cem in _entities.counter_employee_mapping on emp.emp_id equals cem.counter_man_id
                select new
                {
                    us.user_name,
                    us.password,
                    emp.emp_firstname,
                    emp.emp_lastname,
                   // cem.route_id,
                    //cem.area_id,
                    //cem.direction,
                    emp.company_id,
                    com.company_code,
                    com.company_name,
                    emp.designation_id,
                    
                }).FirstOrDefault();

            if (loginInfo == null)
            {
                return new
                {
                    login = false,
                    output_message = "Username or Password in incorrect",
                    

                };
            }
            else
            {
                return new
                {
                    login = true,
                    output_message = "Login Successful",
                    user_data = loginInfo

                };
            }

        }
    }

  
}