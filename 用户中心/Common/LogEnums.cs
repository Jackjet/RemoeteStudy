using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 系统所有模块
    /// </summary>
    public class LogConstants
    {
        /// <summary>
        /// 登录模块
        /// </summary>
        public static String login = "用户登录模块";
        /// <summary>
        /// 教师信息管理
        /// </summary>
        public static String jsxxgl = "教师信息管理";
        /// <summary>
        /// 学生信息管理
        /// </summary>
        public static String xsxxgl   = "学生信息管理";
        /// <summary>
        /// 账号信息管理
        /// </summary>
        public static String zzxxgl = "账号信息管理";
        /// <summary>
        /// 家长信息管理
        /// </summary>
        public static String jzxxgl = "家长信息管理";
        /// <summary>
        /// 组织机构管理
        /// </summary>
        public static String zzjggl = "组织机构管理";
        /// <summary>
        /// 年级管理
        /// </summary>
        public static String njgl = "年级管理";
        /// <summary>
        /// 班级管理
        /// </summary>
        public static String bjgl = "班级管理";
        /// <summary>
        /// 学科信息
        /// </summary>
        public static String xkxx = "学科信息";
        /// <summary>
        /// 学科管理
        /// </summary>
        public static String xkgl = "学科管理";
        /// <summary>
        /// 角色管理
        /// </summary>
        public static String jsgl = "角色管理";
        /// <summary>
        /// 教研组管理
        /// </summary>
        public static String jyzgl = "教研组管理";
        /// <summary>
        /// 系统账号管理
        /// </summary>
        public static String xtzhgl = "系统账号管理";
        /// <summary>
        /// 接口信息管理
        /// </summary>
        public static String jkxxgl = "接口信息管理";
        /// <summary>
        /// 接口权限管理
        /// </summary>
        public static String jkqxgl = "接口权限管理";
        /// <summary>
        /// 学年学期管理
        /// </summary>
        public static String xnxqgl = "学年学期管理";
        /// <summary>
        /// 修改密码管理
        /// </summary>
        public static String xgmmgl = "修改密码管理";
        /// <summary>
        /// 操作日志管理
        /// </summary>
        public static String czrzgl = "操作日志管理";
        /// <summary>
        /// 专业管理
        /// </summary>
        public static String zygl = "专业管理"; 
    }
    /// <summary>
    /// 操作
    /// </summary>
    public class ActionConstants
    {
        /// <summary>
        /// 登录操作
        /// </summary>
        public static String Actionlogin = "登录操作";
        /// <summary>
        /// 导入操作
        /// </summary>
        public static String dr = "教师导入操作";
        public static String exceldc = "Excel导出操作";

        public static String add = "添加操作";
        public static string Search = "查询操作";
        public static string xg = "修改操作";
        public static string del = "删除操作";


        public static string jy = "禁用操作";
        public static string qy = "启用操作";
        public static string czmm = "重置密码操作";
        public static string jb = "解绑操作";

        public static string xsup = "学生升级操作";
        public static string xsdown = "学生降级操作";
        public static string excelfb = "Excel分班操作";
        public static string xzmb = "下载模板操作";
        public static string xsesceldr = "学生Excel导入操作";

        public static string tjxxxx = "添加学校信息操作";
        public static string xgxxxx = "修改学校信息操作";


        public static string glysz = "管理员设置操作";

        public static string pltj = "批量添加操作";

        public static string tjrydjyz = "添加人员到教研组操作";

        public static string xgpwd = "修改密码";
    }
}
