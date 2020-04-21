using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPApi.Models;
using ERPApi.Models.StronglyType;

namespace ERPApi.Models.IRepository
{
   public interface IRoleWisePageMappingRepository
    {

        object GetAllbuttonsByPageid(int page_id);
        object GetAllPages();
        object GetAllButtons();
        List<sts_role_pagewise_button> GetAllPagesByRole(int? role_id);
        sts_role_pagewise_button GetroleWiseByID(int page_id);

        bool InsertRoleWisePage(RoleWisePageMappingModel oRoleWisePageMappingModel);
        bool DeleteRoleWisePage(int page_id);
        bool UpdateRoleNPagewiseButton(sts_role_pagewise_button oRoleWisePage);
        //bool InsertRoleNPageWiseButton(sts_role_pagewise_button oRoleWisePage);
        //bool checkpagexist(int? role_id, int page);
        //object GetAllRoleWisePageMapping();
      // bool CheckDuplicateCountry(string CountryName);

    }
}
