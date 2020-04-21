using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class ControlTypeRepository : IControlTypeRepository
    {
        private ERPEntities _entities;

        public ControlTypeRepository()
        {
            this._entities = new ERPEntities();
        }

        public IList<control_type> GetAllControlTypes()
        {
            List<control_type> control_types = _entities.control_type.ToList();
            return control_types;
        }

        public control_type GetControlTypeByID(int control_type_id)
        {
            throw new NotImplementedException();
        }

        public control_type GetControlTypeByName(string control_type_name)
        {
            throw new NotImplementedException();
        }

        public control_type GetControlTypeByIsActive(bool is_active)
        {
            throw new NotImplementedException();
        }

        public bool InsertControleType(control_type ocontrol_type)
        {
            try
            {
                string created_date = DateTime.Now.ToLongTimeString();
                control_type insert_control_type = new control_type
                {
                    control_type_name = ocontrol_type.control_type_name,
                    created_by = ocontrol_type.created_by,
                    created_date = ocontrol_type.created_date,
                    updated_by = ocontrol_type.updated_by,
                    updated_date = ocontrol_type.updated_date,
                    company_id = ocontrol_type.company_id,
                    is_active = ocontrol_type.is_active
                };
                _entities.control_type.Add(insert_control_type);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            throw new NotImplementedException();
        }

        public bool UpdateControlType(control_type ocontrol_type)
        {
            try
            {
                control_type update_control_type = _entities.control_type.Find(ocontrol_type.control_type_id);
                update_control_type.control_type_name = ocontrol_type.control_type_name;
                update_control_type.updated_by = ocontrol_type.updated_by;
                update_control_type.updated_date = ocontrol_type.updated_date;
                update_control_type.company_id = ocontrol_type.company_id;
                update_control_type.is_active = ocontrol_type.is_active;

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckControlTypeForDuplicateByName(string control_type_name)
        {
            var checkControlTypeIsExists = _entities.control_type.FirstOrDefault(c => c.control_type_name == control_type_name);
            return checkControlTypeIsExists == null ? false : true;
        }

        public bool DeleteControlType(int control_type_id)
        {
            try
            {
                var delete_control_type = _entities.control_type.FirstOrDefault(ct => ct.control_type_id == control_type_id);
                _entities.control_type.Attach(delete_control_type);
                _entities.control_type.Remove(delete_control_type);
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