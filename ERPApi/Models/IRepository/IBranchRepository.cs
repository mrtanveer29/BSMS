using ERPApi.Models.StronglyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPApi.Models.IRepository
{
   public interface IBranchRepository
    {
       object GetAllBranches(int company_id);
       object GetBranchByID(int branch_id);

       bool CheckDuplicateForBranchName(int? company_id,string branch_name);

       bool InsertBranch(BranchModel oBranch);
       bool UpdateBranch(BranchModel oBranch);
       bool DeleteBranch(int branch_id);
       Object GetAllBranchesById(int companyid);
       bool CheckDuplicateByBranchCode(int? company_id,string branch_code);

       string GetBranchCodeByID(int branch_id);

    }
}
