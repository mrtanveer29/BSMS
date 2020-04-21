using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IContactPersonRepository
    {
        List<contact> GetAllRBOContactPerson();

        List<contact> GetRBOContactPersonByRBOId(int rbo_id);

        contact GetRBOContactPersonByID(int contact_id);

        bool InsertRBOContactPerson(contact oRBOContactPerson);

        bool DeleteRBOContactPerson(int contact_id);

        bool UpdateRBOContactPerson(contact oRBOContactPerson);

        bool CheckDuplicateRBOContactPerson(contact oRBOContactPerson, string action);
    }
}