using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class RequestEntity
    {
        /// <summary>
        /// 第一条记录
        /// </summary>
        public int FirstResult
        {
            get
            {
                if (PageIndex == -1)
                    return -1;
                return PageSize * (PageIndex - 1);
            }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Condition { get; set; }
        /// <summary>
        /// 查询条件的实体
        /// </summary>
        public object ConditionModel { get; set; }
    }
}
