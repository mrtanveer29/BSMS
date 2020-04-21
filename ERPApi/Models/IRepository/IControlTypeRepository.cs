using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IControlTypeRepository
    {
        IList<control_type> GetAllControlTypes();

        control_type GetControlTypeByID(int control_type_id);

        control_type GetControlTypeByName(string control_type_name);

        control_type GetControlTypeByIsActive(bool is_active);

        //control_type GetControlTypeByCreatedDate(string created_date);
        //control_type GetControlTypeByCreatedBy(int created_by);
        //control_type GetControlTypeByHistoricalChange(string historical_change);

        bool CheckControlTypeForDuplicateByName(string control_type_name);

        bool InsertControleType(control_type ocontrol_type);

        bool UpdateControlType(control_type ocontrol_type);

        bool DeleteControlType(int control_type_id);
    }
}