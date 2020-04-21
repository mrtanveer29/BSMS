using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPApi.Models.Repository
{
    public class EducationRepository:IEducationRepository
    {
         private ERPEntities _entities;

       public EducationRepository() {

           this._entities = new ERPEntities();
       
       }
        public object GetAllEducation()
        {
            List<hr_education> educations = _entities.hr_education.ToList();
            return educations;
        }

        public hr_education GetEducationByID(int education_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertEducation(hr_education oEducation)
        {
            try
            {
                hr_education Insert_education = new hr_education
                {
                    employee_id = oEducation.employee_id,
                    degree_name = oEducation.degree_name,
                    institute = oEducation.institute,
                    passing_year = oEducation.passing_year,
                    result = oEducation.result

                };

                _entities.hr_education.Add(Insert_education);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEducation(hr_education oEducation)
        {
            try
            {
                hr_education Education = _entities.hr_education.Find(oEducation.education_id);
                Education.employee_id = oEducation.employee_id;
                Education.degree_name = oEducation.degree_name;
                Education.institute = oEducation.institute;
                Education.passing_year = oEducation.passing_year;
                Education.result = oEducation.result;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEducation(int education_id)
        {
            try
            {
                hr_education oEdcation = _entities.hr_education.FirstOrDefault(e => e.education_id == education_id);
                _entities.hr_education.Attach(oEdcation);
                _entities.hr_education.Remove(oEdcation);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }

        public List<hr_education> GetEducationByEmployee(int? employee_id)
        {
            List<hr_education> educations = _entities.hr_education.Where(ed => ed.employee_id == employee_id).ToList();
            return educations;
        }
    }
}