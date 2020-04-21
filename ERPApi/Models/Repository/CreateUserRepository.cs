using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class CreateUserRepository : ICreateUserRepository
    {
        private ERPEntities _entities;

        public CreateUserRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllCreateUser()
        {
//            var users = (from emp in _entities.employees
//                         join ejd in _entities.emp_job_details
//                             on emp.emp_id equals ejd.emp_id
//                         join u in _entities.users
//                         on emp.emp_id equals u.employee_id
//
//                         select new EmployeeModel
//                         {
//                             emp_code = ejd.emp_code,
//                             emp_firstname = emp.emp_firstname,
//                             emp_lastname = emp.emp_lastname,
//                             emp_info = ejd.emp_code + ";" + emp.emp_firstname + " " + emp.emp_lastname,
//                             employee_id = emp.emp_id,
//                             user_id = u.user_id,
//                             user_name = u.user_name
//                         }).ToList();
            return 0;
        }

        public object GetEmployeeByCreateUser(int user_id)
        {
            throw new NotImplementedException();
        }

        public object GetEmployees()
        {
            throw new NotImplementedException();
        }

        public user GetCreateUserByID(int user_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertCreateUser(user oCreateUser)
        {
            try
            {
                user Insert_user = new user
                {
                    user_name = oCreateUser.user_name,
                    password = oCreateUser.password,
                    role_id = oCreateUser.role_id,
                    employee_id = oCreateUser.employee_id,
                    role_type_id = oCreateUser.role_type_id,
                    company_id = oCreateUser.company_id,
                    confirm_password = oCreateUser.confirm_password
                };

                _entities.users.Add(Insert_user);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCreateUser(user oCreateUser)
        {
            try
            {
                user User = _entities.users.Find(oCreateUser.user_id);
                User.user_name = oCreateUser.user_name;
                User.password = oCreateUser.password;
                User.role_id = oCreateUser.role_id;
                User.employee_id = oCreateUser.employee_id;
                User.role_type_id = oCreateUser.role_type_id;
                User.company_id = oCreateUser.company_id;
                User.confirm_password = oCreateUser.confirm_password;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCreateUser(int user_id)
        {
            try
            {
                user oUser = _entities.users.FirstOrDefault(u => u.user_id == user_id);
                _entities.users.Attach(oUser);
                _entities.users.Remove(oUser);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

//        public object GetEmployeeByCreateUser(int user_id)
//        {
//            var users = (from emp in _entities.employees
//                         join ejd in _entities.emp_job_details
//                             on emp.emp_id equals ejd.emp_id
//                         join u in _entities.users
//                         on emp.emp_id equals u.employee_id
//
//                         where u.user_id == user_id
//
//                         select new EmployeeModel
//                         {
//                             emp_code = ejd.emp_code,
//                             emp_firstname = emp.emp_firstname,
//                             emp_lastname = emp.emp_lastname,
//                             emp_info = ejd.emp_code + ";" + emp.emp_firstname + " " + emp.emp_lastname,
//                             employee_id = emp.emp_id,
//                             user_id = u.user_id,
//                             user_name = u.user_name,
//                             password = u.password,
//                             confirm_password = u.confirm_password
//                         }).ToList();
//            return users;
//        }

//        public object GetEmployees()
//        {
//            object AllEmployees = (from emp in _entities.employees
//                                   join ejd in _entities.emp_job_details
//                                   on emp.emp_id equals ejd.emp_id
//                                   select new EmployeeModel
//                                   {
//                                       emp_code = ejd.emp_code,
//                                       emp_info = ejd.emp_code + "," + emp.emp_firstname + " " + emp.emp_lastname,
//                                       employee_id = emp.emp_id
//                                   }).ToList();
//            return AllEmployees;
//        }
    }
}