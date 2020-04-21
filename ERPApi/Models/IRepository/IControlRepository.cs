using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IControlRepository
    {
        object GetAllControlsOnly();

        object GetAllControls();

        List<control> GetAllControlForPermission();

        List<control> GetControlById(int control_id);

        control GetControlByName(string control_name);

        control GetControlByParentId(int control_parent_id);

        control GetControlByTypeId(int control_type_id);

        control GetControlBySort(int control_sort);

        control GetControlByAlias(string control_alias);

        control GetControlByController(string control_controller);

        control GetControlByAction(string control_action);

        bool CheckControlForDuplicateByName(string control_name);

        bool InsertControl(control ocontrol);

        bool UpdateControl(control ocontrol);

        bool DeleteControl(int control_id);
    }
}