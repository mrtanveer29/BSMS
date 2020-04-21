using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
    interface ILoginRepository
    {
        object LoginInformation(string userName, string password);
        object UserLogin(string userName, string password);
    }
}
