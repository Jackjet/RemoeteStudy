using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Configuration;
namespace Common
{
    public static class UCSKey
    {

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public const string SESSION_LoginInfo = "UInfo";
       
    //  public string AdminName = ConfigurationManager.ConnectionStrings["AdminName"].ToString();
      ///// <summary>
      /////  
      ///// </summary>
      //public const string AdminName
      //{
      //    get
      //    {
      //        return ConfigurationManager.ConnectionStrings["AdminName"].ToString();// AdminName;
      //    }
      //    set
      //    {
      //        AdminName = value;
             
      //    }
      //}
        /// <summary>
        /// 教师学历类型
        /// </summary>
        public enum XXLX
        {
            /// <summary>
            /// 原学历
            /// </summary>
            YXL = 1,
            /// <summary>
            /// 现学历
            /// </summary>
            XXL = 2,
            /// <summary>
            /// 现进修学历
            /// </summary>
            XJXXL = 3,
            /// <summary>
            /// 参加研究生课程班学习
            /// </summary>
            YJSKC = 4
        }
        // public const string Admin_Super = "yqadmin";//超级管理员
       // public const string Admin_Super ="vmmadmin";// AdminName;//超级管理员
        public const string Root_Text = "远程教学平台";//组织机构根节点Text值
        public const string Root_Value = "0";//组织机构根节点Value值
        public const string CreatePWD = "aaa@123";//账号管理-生成账号功能的默认密码
        public static string DatabaseName = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseName"].ToString();//数据库名

        //public static string SFZJH_Prefix = System.Configuration.ConfigurationManager.ConnectionStrings["SFZJH_Prefix"].ToString();//自动生成身份证号前缀

        /// <summary>
        /// 教师Excel表 工作表名称
        /// </summary>
        public const string TeacherSheetName = "教师信息";

    }
}
