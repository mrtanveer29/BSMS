using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IEmployeeJobLocationRepository
    {
        List<job_location> GetAllJobLocation();

        subsection GetJobLocationByID(int job_location_id);

        bool InsertJobLocation(job_location oJobLocation);

        bool UpdateJobLocation(job_location oJobLocation);

        bool DeleteJobLocation(int job_location_id);

        bool CheckDuplicateJobLocation(string job_location_title);
    }
}