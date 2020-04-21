using ERPApi.Models.IRepository;
using System;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private ERPEntities _entities;

        public DesignationRepository()
        {
            this._entities = new ERPEntities();
        }

        public object GetAllDesignations()
        {
            //Temporarily disable department join
            //Cause: S2S now have independent designation list
            var designations = (from des in _entities.designations
                                join dep in _entities.departments
                                on des.department_id equals dep.department_id
                                into DepTable from subDep in DepTable.DefaultIfEmpty()
                                select new
                                {
                                    designation_id = des.designation_id,
                                    designation_name = des.designation_name,
                                    designation_abbreviation = des.designation_abbreviation,
                                   department_id = des.department_id,
                                    created_by = des.created_by,
                                    created_date = des.created_date,
                                    updated_by = des.updated_by,
                                    updated_date = des.updated_date,
                                    company_id = des.company_id,
                                    is_active = des.is_active,
                                    department_name = subDep.department_name
                                }).OrderByDescending(des => des.designation_id).ToList();

            return designations;
        }

        public designation GetDesignationByID(int designation_id)
        {
            throw new NotImplementedException();
        }

        public designation GetDesignationByName(string designation_name)
        {
            throw new NotImplementedException();
        }

        public bool CheckDesignationForDuplicateByname(string designation_name)
        {
            var checkDuplicateDesignation = _entities.designations.FirstOrDefault(d => d.designation_name == designation_name);

            if (checkDuplicateDesignation == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InsertDesignation(designation oDesignation)
        {
            try
            {
                designation insert_designation = new designation
                {
                    designation_name = oDesignation.designation_name,
                    designation_abbreviation = oDesignation.designation_abbreviation,
                    department_id = oDesignation.department_id,
                    created_by = oDesignation.created_by,
                    created_date = oDesignation.created_date,
                    updated_by = oDesignation.updated_by,
                    updated_date = oDesignation.updated_date,
                    company_id = oDesignation.company_id,
                    is_active = oDesignation.is_active
                };
                _entities.designations.Add(insert_designation);
                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDesignation(designation oDesignation)
        {
            try
            {
                designation de = _entities.designations.Find(oDesignation.designation_id);
                de.designation_name = oDesignation.designation_name;
                de.designation_abbreviation = oDesignation.designation_abbreviation;
                de.department_id = oDesignation.department_id;
                de.updated_by = oDesignation.updated_by;
                de.updated_date = oDesignation.updated_date;
                de.company_id = oDesignation.company_id;
                de.is_active = oDesignation.is_active;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDesignation(int designation_id)
        {
            try
            {
                designation oDesignation = _entities.designations.FirstOrDefault(d => d.designation_id == designation_id);
                _entities.designations.Attach(oDesignation);
                _entities.designations.Remove(oDesignation);

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}