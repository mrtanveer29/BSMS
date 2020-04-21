using ERPApi.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPApi.Models.Repository
{
    public class EmployeeJobLocationRepository : IEmployeeJobLocationRepository
    {
        private ERPEntities _entities;

        public EmployeeJobLocationRepository()
        {
            this._entities = new ERPEntities();
        }

        public List<job_location> GetAllJobLocation()
        {
            List<job_location> joblocation = _entities.job_location.ToList();
            return joblocation;
        }

        public subsection GetJobLocationByID(int job_location_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertJobLocation(job_location oJobLocation)
        {
            try
            {
                job_location Insert_job_location = new job_location
                {
                    job_location_title = oJobLocation.job_location_title
                };

                _entities.job_location.Add(Insert_job_location);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateJobLocation(job_location oJobLocation)
        {
            try
            {
                job_location JobLocation = _entities.job_location.Find(oJobLocation.job_location_id);
                JobLocation.job_location_title = oJobLocation.job_location_title;

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteJobLocation(int job_location_id)
        {
            try
            {
                job_location oJobLocation = _entities.job_location.FirstOrDefault(jl => jl.job_location_id == job_location_id);
                _entities.job_location.Attach(oJobLocation);
                _entities.job_location.Remove(oJobLocation);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckDuplicateJobLocation(string job_location_title)
        {
            var checkJobLocationIsExists = _entities.job_location.FirstOrDefault(jl => jl.job_location_title == job_location_title);
            return checkJobLocationIsExists == null ? false : true;
        }
    }
}