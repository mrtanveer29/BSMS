using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IContactRepository
    {
        List<contact> GetAllContact();

        List<contact> GetAllContactBySource(int source_id, string source_type);

        bool InsertContact(contact oContact);

        bool UpdateContact(contact oContact);
    }
}