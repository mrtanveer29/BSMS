using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IEducationRepository
    {
        object GetAllEducation();
        List<hr_education> GetEducationByEmployee(int? employee_id);
        hr_education GetEducationByID(int education_id);

        bool InsertEducation(hr_education oEducation);
        bool UpdateEducation(hr_education oEducation);
        bool DeleteEducation(int education_id);
    }
}
