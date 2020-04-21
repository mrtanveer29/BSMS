using ERPApi.Models.IRepository;
using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private ERPEntities _entities;

        public BranchRepository()
        {
            this._entities = new ERPEntities();
        }

        //Get branch code by Asma
        public string GetBranchCodeByID(int branch_id)
        {
            try
            {
                string branchCode = _entities.branches.Where(b => b.branch_id == branch_id).FirstOrDefault().branch_code;
                return branchCode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object GetAllBranches(int company_id)
        {
            var branche = (from b in _entities.branches

                            join e in _entities.employees
                            on b.branch_incharge equals e.emp_id into bm
                            from mbm in bm.DefaultIfEmpty()
            
            
                            join com in _entities.companies
                            on b.company_id equals com.company_id


                             join a in _entities.addresses.Where(xb=>xb.source_type ==  "Branch")
                             on b.branch_id equals a.source_id into bbs
                             from xm in bbs.DefaultIfEmpty()

                             join c in _entities.cities
                               on xm.city_id equals c.city_id into cxm
                               from cc in cxm.DefaultIfEmpty()
    
                         
                             join co in _entities.countries
                             on xm.country_id equals co.country_id into cxmo
                             from cct in cxmo.DefaultIfEmpty()

                            where b.company_id == company_id
                           
                            select new
                            {
                                branch_id = b.branch_id,
                                branch_code = b.branch_code,
                                branch_name = b.branch_name,
                                branch_location = b.branch_location,
              
                                branch_incharge = mbm.emp_id ==null?0:mbm.emp_id,
                                branch_incharge_name = mbm.emp_firstname==""?"":mbm.emp_firstname,
                                company_id = com.company_id,
                                company_title = com.company_name,
                                 country_id = cct.country_id == null?0:cct.company_id,
                                  country_name = cct.country_name == null?"":cct.country_name,
                                city_id = cc.city_id==null?0:cc.city_id,
                                 city_name = cc.city_name==""?"":cc.city_name,
                                is_active = b.is_active
                            }).ToList();
            return branche;
        }

        public object GetBranchByID(int branch_id)
        {
            try
            {
                BranchModel comModel = new BranchModel();
                var branch = _entities.branches.SingleOrDefault(a => a.branch_id == branch_id);
                var address = _entities.addresses.FirstOrDefault(a => a.source_id == branch_id && a.source_type == "Branch");

                    comModel.branch_code = branch.branch_code;
                    comModel.branch_name = branch.branch_name;
                    comModel.branch_location = branch.branch_location;
                    comModel.branch_incharge = branch.branch_incharge;
                    comModel.company_id = branch.company_id;
                    comModel.is_active = branch.is_active;

                comModel.address_1 = address.address_1;
                comModel.address_2 = address.address_2;
                comModel.city_id = Convert.ToInt32(address.city_id);
                comModel.country_id = Convert.ToInt32(address.country_id);
                comModel.email = address.email;
                comModel.fax = address.fax;
                comModel.mobile = address.mobile;
                comModel.phone = address.phone;
                comModel.web = address.web;
                comModel.zip_code = address.zip_code;

                return comModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public bool CheckDuplicateForBranchName(int string branch_name)
        //{
        //    var checkBranchIsExists = _entities.branches.FirstOrDefault(b => b.branch_name == branch_name);
        //    return checkBranchIsExists == null ? false : true;
        //}

        #region Insert of Branch

        public bool InsertBranch(BranchModel oBranch)
        {
            try
            {
                branch Insert_branch = new branch
                {
                    branch_code = oBranch.branch_code,
                    branch_name = oBranch.branch_name,
                    branch_location = oBranch.branch_location,
                    branch_incharge = oBranch.branch_incharge,
                    company_id = oBranch.company_id,

                    is_active = oBranch.is_active
                };

                _entities.branches.Add(Insert_branch);
                _entities.SaveChanges();

                var last_insert_id = Insert_branch.branch_id;

                address addresses = new address

                {
                    source_id = last_insert_id,
                    source_type = "Branch",
                    address_type = "Main Address",
                    address_1 = oBranch.address_1,
                    address_2 = oBranch.address_2,
                    country_id = oBranch.country_id,
                    city_id = oBranch.city_id,
                    zip_code = oBranch.zip_code,
                    email = oBranch.email,
                    phone = oBranch.phone,
                    fax = oBranch.fax,
                    web = oBranch.web,
                    mobile = oBranch.mobile
                };
                _entities.addresses.Add(addresses);
                _entities.SaveChanges();

                //employee data save
                employee tempemEmployee = new employee
                {
                    emp_firstname = oBranch.emp_firstname,
                    emp_lastname = oBranch.emp_lastname,
                    company_id = oBranch.company_id,
                    branch_id = Insert_branch.branch_id

                };
                _entities.employees.Add(tempemEmployee);
                _entities.SaveChanges();

                //role save
                var role_id = 0;
            
                var checkRolename = _entities.roles.FirstOrDefault(o => o.role_name == "Company Admin");
                if (checkRolename == null)
                {
                    role tempRole = new role
                    {
                        role_name = "Company Admin",
                        company_id = oBranch.company_id,
                        is_active = true
                    };
                    _entities.roles.Add(tempRole);
                    _entities.SaveChanges();
                    role_id = _entities.roles.Max(o => o.role_id);
                }
                else
                {
                    role_id = checkRolename.role_id;
                }
                

                //employee user data save

                user adminuser = new user
                {
                    user_name = oBranch.user_name,
                    password = oBranch.password,
                    user_firstname = oBranch.emp_firstname,
                    user_lastname = oBranch.emp_lastname,
                    role_id = role_id,
                    company_id = oBranch.company_id,
                    employee_id = _entities.employees.Max(i => i.emp_id)
                };
                _entities.users.Add(adminuser);
                _entities.SaveChanges();

                //user permission set for the user

                var tempRoleID = _entities.roles.FirstOrDefault(o => o.role_name == "Company Admin").role_id;
                var tempRolePermission = _entities.user_permission.Where(u => u.user_role_id == tempRoleID).ToList();

                foreach (var itemUserpermission in tempRolePermission)
                {
                    user_permission permission = new user_permission
                    {
                        user_control_id = itemUserpermission.user_control_id,
                        user_role_id = _entities.roles.Max(p => p.role_id),
                        user_au_id = _entities.users.Max(u => u.user_id)
                    };
                    _entities.user_permission.Add(permission);
                    _entities.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Insert of Branch

        #region Update of Branch

        public bool UpdateBranch(BranchModel oBranch)
        {
            try
            {
                branch Branch = _entities.branches.Find(oBranch.branch_id);
                Branch.branch_code = oBranch.branch_code;
                Branch.branch_name = oBranch.branch_name;
                Branch.branch_location = oBranch.branch_location;
                Branch.branch_incharge = oBranch.branch_incharge;
                Branch.company_id = oBranch.company_id;
                Branch.is_active = oBranch.is_active;

                _entities.SaveChanges();

                address Addresses = _entities.addresses.FirstOrDefault(a => a.source_id == oBranch.branch_id && a.source_type == "Branch");

                Addresses.source_type = "Branch";
                Addresses.address_type = "Main Address";
                Addresses.address_1 = oBranch.address_1;
                Addresses.address_2 = oBranch.address_2;
                Addresses.country_id = oBranch.country_id;
                Addresses.city_id = oBranch.city_id;
                Addresses.zip_code = oBranch.zip_code;
                Addresses.email = oBranch.email;
                Addresses.phone = oBranch.phone;
                Addresses.fax = oBranch.fax;
                Addresses.web = oBranch.web;
                Addresses.mobile = oBranch.mobile;

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Update of Branch

        #region Delete of Branch

        public bool DeleteBranch(int branch_id)
        {
            try
            {
                branch oBranch = _entities.branches.FirstOrDefault(b => b.branch_id == branch_id);
                _entities.branches.Attach(oBranch);
                _entities.branches.Remove(oBranch);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Delete of Branch

        public bool CheckDuplicateForBranchName(int? company_id, string branch_name)
        {
            var checkBranchIsExists = _entities.branches.FirstOrDefault(b => b.company_id == company_id && b.branch_name == branch_name);
            return checkBranchIsExists == null ? false : true;
        }

        public bool CheckDuplicateByBranchCode(int? company_id, string branch_code)
        {
            var checkBranchIsExists = _entities.branches.FirstOrDefault(b => b.company_id == company_id && b.branch_code == branch_code);
            return checkBranchIsExists == null ? false : true;
        }

        public Object GetAllBranchesById(int companyid)
        {
            try
            {
                using (var db = new ERPEntities())
                {
                    return
                        db.branches.Where(i => i.company_id == companyid)
                            .Select(x => new { x.branch_name, x.branch_id })
                            .ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}