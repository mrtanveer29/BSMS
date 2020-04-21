using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public  interface IAreaRepository
    {
       object GetAllAreas(int companyId);
       object Post(area value);
       object Put(area value);
       object Delete(area value);
       object GetAllAreasByCityId(int cityId, int companyId);
       object GetAllAreasByRoute(int routeId);
    }
}
