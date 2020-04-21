using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPApi.Models.IRepository;

namespace ERPApi.Models.Repository
{
    public class PasswordRepository : IPasswordRepository
    {
        private ERPEntities _entities;

        public PasswordRepository()
        {
            _entities=new ERPEntities();
        }

        public object ChangePassword(string user_name, string oldPass, string newPass, string rePass)
        {
            user user;
            if (newPass == rePass)
            {

                user = _entities.users.FirstOrDefault(x => x.user_name == user_name);
                if (user.password == oldPass)
                {
                    user.password = newPass;
                    _entities.SaveChanges();
                    return "Password Changed";
                }
                else
                {
                    return "Password Not Correct";
                }
            }
            else
            {
               return "New Password and confirm Password value not matched";
            }

            return user;
        }
    }
}