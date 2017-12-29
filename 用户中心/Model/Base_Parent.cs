using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Base_Parent
    {
        /// <summary>
        /// 成员身份证件号
        /// </summary>
        public string CYSFZJH { get; set; }
        public string YHZH { get; set; }
        public string XXZZJGH { get; set; }
        public string YHZT { get; set; }
        /// <summary>
        /// 身份证件号-对应学生表外键
        /// </summary>
        public string SFZJH { get; set; }
        public string CYXM { get; set; }
        public string GXM { get; set; }
        public Nullable<System.DateTime> CSNY { get; set; }
        public string MZM { get; set; }
        public string GJDQM { get; set; }
        public string JKZKM { get; set; }
        public string CYGZDW { get; set; }
        public string CYEM { get; set; }
        public string ZYJSZWM { get; set; }
        public string ZWJBM { get; set; }
        public string DH { get; set; }
        public string DZXX { get; set; }
        public string SFJHR { get; set; }
        public string XBM { get; set; }
        public string XLM { get; set; }
        public string LXDZ { get; set; }
        public string SJHM { get; set; }
        /// <summary>
        /// 最近登陆时间
        /// </summary>
        public Nullable<System.DateTime> ZJDLSJ { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        public string DLIP { get; set; }
        /// <summary>
        /// 登陆标识码
        /// </summary>
        public string DLBSM { get; set; }
        public Nullable<System.DateTime> XGSJ { get; set; }
        public string BZ { get; set; }
    }
}
