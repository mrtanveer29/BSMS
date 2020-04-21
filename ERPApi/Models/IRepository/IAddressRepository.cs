using ERPApi.Models.StronglyType;
using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IAddressRepository
    {
        List<address> GetAllAddress();

        List<address> GetAllAddressBySource(int source_id, string source_type);

        List<address> GetAllAddressBySourceAddressType(int source_id, string source_type, string address_type);

        address GetAddressById(int address_id);

        bool InsertAddress(address oAddress);

        bool UpdateAddress(address oAddress);

        
    }
}