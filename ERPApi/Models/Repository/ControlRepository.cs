using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class ControlRepository : IControlRepository
    {
        private ERPEntities _entities;

        public ControlRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllControlForPermission()
        {
            var controls = (from e in _entities.controls
                            join ct in _entities.control_type
                            on e.control_type_id equals ct.control_type_id
                            join e1 in _entities.controls
                            on e.control_parent_id equals e1.control_id
                            select new
                            {
                                control_id = e.control_id,
                                control_name = e.control_name,
                                control_parent_id = e1.control_id,
                                control_parent_name = e1.control_name,
                                control_type_id = ct.control_type_id,
                                control_type_name = ct.control_type_name,
                                control_sort = e.control_sort,
                                control_alias = e.control_alias,
                                control_controller = e.control_controller,
                                control_action = e.control_action,
                                company_id = e.company_id,
                                icon = e.icon
                            }
                           ).ToList();

            return controls;
        }

        public List<control> GetControlById(int control_id)
        {
            //throw new NotImplementedException();
            List<control> contol = _entities.controls.Where(c => c.control_id == control_id).ToList();
            return contol;
        }

        public control GetControlByName(string control_name)
        {
            throw new NotImplementedException();
        }

        public control GetControlByParentId(int control_parent_id)
        {
            throw new NotImplementedException();
        }

        public control GetControlByTypeId(int control_type_id)
        {
            throw new NotImplementedException();
        }

        public control GetControlBySort(int control_sort)
        {
            throw new NotImplementedException();
        }

        public control GetControlByAlias(string control_alias)
        {
            throw new NotImplementedException();
        }

        public control GetControlByController(string control_controller)
        {
            throw new NotImplementedException();
        }

        public control GetControlByAction(string control_action)
        {
            throw new NotImplementedException();
        }

        public bool CheckControlForDuplicateByName(string control_name)
        {
            var checkControlIsExist = _entities.controls.FirstOrDefault(co => co.control_name == control_name);
            return checkControlIsExist == null ? false : true;
        }

        public bool InsertControl(control ocontrol)
        {
            try
            {
                control Insert_control = new control();

                Insert_control = new control
                {
                    control_name = ocontrol.control_name,
                    control_parent_id = ocontrol.control_parent_id,
                    control_type_id = ocontrol.control_type_id,
                    control_sort = ocontrol.control_sort,
                    control_alias = ocontrol.control_alias,
                    control_controller = ocontrol.control_controller,
                    control_action = ocontrol.control_action,
                    created_by = ocontrol.created_by,
                    created_date = ocontrol.created_date,
                    updated_by = ocontrol.updated_by,
                    updated_date = ocontrol.updated_date,
                    company_id = ocontrol.company_id,
                    is_active = ocontrol.is_active,
                    Level = ocontrol.Level,
                    icon = ocontrol.icon
                };
                var exists_control = _entities.controls.Where(con => con.control_name == ocontrol.control_name).FirstOrDefault();
                _entities.controls.Add(Insert_control);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateControl(control ocontrol)
        {
            try
            {
                control con = _entities.controls.Find(ocontrol.control_id);

                con.control_name = ocontrol.control_name;
                con.control_parent_id = ocontrol.control_parent_id;
                con.control_type_id = ocontrol.control_type_id;
                con.control_sort = ocontrol.control_sort;
                con.control_alias = ocontrol.control_alias;
                con.control_controller = ocontrol.control_controller;
                con.control_action = ocontrol.control_action;
                con.updated_by = ocontrol.updated_by;
                con.updated_date = ocontrol.updated_date;
                con.company_id = ocontrol.company_id;
                con.is_active = ocontrol.is_active;
                con.Level = ocontrol.Level;
                con.icon = ocontrol.icon;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteControl(int control_id)
        {
            try
            {
                var deleted_control = _entities.controls.FirstOrDefault(c => c.control_id == control_id);
                _entities.controls.Attach(deleted_control);
                _entities.controls.Remove(deleted_control);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object GetAllControlsOnly()
        {
            var c =
                _entities.Database.SqlQuery<control>(
                    "select * from Control where control_Type_id in (select control_type_id from control_type where control_type_name='Form') and control_parent_id=0");

            return c;
        }

        public object GetAllControls()
        {
            var controls = (from e in _entities.controls
                            join ct in _entities.control_type
                            on e.control_type_id equals ct.control_type_id
                            join e1 in _entities.controls
                            on e.control_parent_id equals e1.control_id
                            select new
                            {
                                control_id = e.control_id,
                                control_name = e.control_name,
                                control_parent_id = e.control_parent_id,
                                control_parent_name = e1.control_name,
                                control_type_id = ct.control_type_id,
                                control_type_name = ct.control_type_name,
                                control_sort = e.control_sort,
                                control_alias = e.control_alias,
                                control_controller = e.control_controller,
                                control_action = e.control_action,
                                company_id = e.company_id,
                                icon = e.icon
                            }
                           ).ToList();

            return controls;
        }

        List<control> IControlRepository.GetAllControlForPermission()
        {
            List<control> lstControls = _entities.controls.ToList();
            return lstControls;
        }
    }
}