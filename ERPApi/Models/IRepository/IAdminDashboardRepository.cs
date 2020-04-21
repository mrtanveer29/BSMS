using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
    public interface IAdminDashboardRepository
    {
        object GetAllAdminDashboardData(int companyId);
    }
}
