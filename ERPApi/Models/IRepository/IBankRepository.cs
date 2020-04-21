using System.Collections.Generic;

namespace ERPApi.Models.IRepository
{
    public interface IBankRepository
    {
        bank GetBankById(int bankId);
        List<bank> GetBankBySourceType(string sourceType);
        List<bank> GetBankBySourceTypeAndId(string sourceType, int sourceId);
    }
}