using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IExperienceRepository
    {
        object GetAllExperiences();
        List<hr_experience> GetExperienceByEmployee(int? employee_id);
        hr_experience GetExperienceByID(int experience_id);

        bool InsertExperience(hr_experience oExperience);
        bool UpdateExperience(hr_experience oExperience);
        bool DeleteExperience(int experience_id);
    }
}
