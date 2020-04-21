using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
    public interface IPasswordRepository
    {
        object ChangePassword(string userName,string oldPass, string newPass, string rePass);
    }
}
