using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 组织机构字段
    /// </summary>
    public class Base_Department
    {
        /// <summary>
        /// 学校组织机构号
        /// </summary>
        public int XXZZJGH { get; set; }
        /// <summary>
        /// 隶属机构号
        /// </summary>
        public string LSJGH { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string JGMC { get; set; }
        /// <summary>
        ///机构简称
        /// </summary>
        public string JGJC { get; set; }
        /// <summary>
        /// 负责人证件号
        /// </summary>
        public string FZRZJH { get; set; }
        /// <summary>
        /// 是否是校级
        /// </summary>
        public string SFSXJ { get; set; }
        /// <summary>
        /// 学校组织机构码
        /// </summary>
        public string ZZJGM { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime XGSJ { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 校长姓名
        /// </summary>
        public string XZXM { get; set; }
        /// <summary>
        /// 组织机构排序
        /// </summary>
        public string OrderNum { get; set; }
    }
}

