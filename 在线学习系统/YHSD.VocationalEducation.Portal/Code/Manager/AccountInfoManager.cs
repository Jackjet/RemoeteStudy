using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class AccountInfoManager
    {
        public List<AccountInfo> FindAccountSearch(AccountInfo entity)
        {
            return new AccountInfoDao().FindAccountSearch(entity);
        }
    }
}
