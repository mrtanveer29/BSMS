using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

//Farzana
namespace ERPApi.Models.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private ERPEntities _entities;

        public ExperienceRepository()
        {

            this._entities = new ERPEntities();

        }
        public object GetAllExperiences()
        {
            var experiences = _entities.hr_experience.ToList();
            return experiences;
        }

        public hr_experience GetExperienceByID(int experience_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertExperience(hr_experience oExperience)
        {
            try
            {
                hr_experience Insert_experience = new hr_experience
                {
                    employee_id = oExperience.employee_id,
                    company = oExperience.company,
                    job_title = oExperience.job_title,
                    from_date = oExperience.from_date,
                    to_date = oExperience.to_date,
                    responsibilities = oExperience.responsibilities

                };

                _entities.hr_experience.Add(Insert_experience);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateExperience(hr_experience oExperience)
        {
            try
            {
               hr_experience Experience = _entities.hr_experience.Find(oExperience.experience_id);
                Experience.employee_id = oExperience.employee_id;
                Experience.company = oExperience.company;
                Experience.job_title = oExperience.job_title;
                Experience.from_date = oExperience.from_date;
                Experience.to_date = oExperience.to_date;
                Experience.responsibilities = oExperience.responsibilities;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteExperience(int experience_id)
        {
            try
            {
                hr_experience oExperience = _entities.hr_experience.FirstOrDefault(ex => ex.experience_id == experience_id);
                _entities.hr_experience.Attach(oExperience);
                _entities.hr_experience.Remove(oExperience);
                _entities.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
            }
        }


        public List<hr_experience> GetExperienceByEmployee(int? employee_id)
        {
            List<hr_experience> experiences = _entities.hr_experience.Where(ex => ex.employee_id == employee_id).ToList();
            return experiences;
        }
        
    }
}