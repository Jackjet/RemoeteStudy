using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class ConnectionManager
    {
        //private static string connectionString = ConfigurationManager.ConnectionStrings["YHSDVocationalEducationConnectionString"].ConnectionString;
        //图片文档库名称
        public static string ImgKuUrl = ConfigurationManager.AppSettings["ImgKuUrl"].ToString();
        //菜单图片存放的文件夹名称
        public static string MenuIcoName = ConfigurationManager.AppSettings["MenuIcoName"].ToString();
        //课程图片存放的文件夹名称
        public static string CurriculumName = ConfigurationManager.AppSettings["CurriculumName"].ToString();
        //作业描述存放的文件夹名称
        public static string WorkDescriptionImage = ConfigurationManager.AppSettings["WorkDescriptionImage"].ToString();
        //临时存放图片的文件夹名称
        public static string InterimImage = ConfigurationManager.AppSettings["InterimImage"].ToString();
        //人员头像存放的文件夹名称
        public static string UserPhoto = ConfigurationManager.AppSettings["UserPhoto"].ToString();
        //附件文档库名称
        public static string FuJianUrl = ConfigurationManager.AppSettings["FuJianUrl"].ToString();
        //学习心得存放的文件夹名称
        public static string StudyExperience = ConfigurationManager.AppSettings["StudyExperience"].ToString();
        //AD域名
        public static string ADName = ConfigurationManager.AppSettings["ADName"].ToString();
        //AD域完全地址
        public static string ADPath = ConfigurationManager.AppSettings["ADPath"].ToString();
        //AD域管理员账号
        public static string ADAdminUser = ConfigurationManager.AppSettings["ADAdminUser"].ToString();
        //AD域管理员密码
        public static string ADAdminPassword = ConfigurationManager.AppSettings["ADAdminPassword"].ToString();
        //AD域导入人员所在组织单位路径
        public static string ADOUUrl = ConfigurationManager.AppSettings["ADOUUrl"].ToString();
        //AD域导入初始密码
        public static string ADInitialpassword = ConfigurationManager.AppSettings["ADInitialpassword"].ToString();
        /// <summary>
        /// 获取资源分类在SharePoint中的文件夹名称，默认值为/ResourceStore
        /// </summary>
        public static string SPClassificationName
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["SPClassificationName"] != null)
                    return System.Web.Configuration.WebConfigurationManager.AppSettings["SPClassificationName"];
                return "/ResourceStore";
            }
        }
        /// <summary>
        /// 获取临时文件夹的路径，默认值为 /资源分类的路径/TempFolder
        /// </summary>
        public static string SPTempFolder
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["SPTempFolder"] != null)
                    return string.Format("{0}{1}", SPClassificationName, System.Web.Configuration.WebConfigurationManager.AppSettings["SPTempFolder"]);
                return string.Format("{0}/TempFolder", SPClassificationName);
            }
        }
        /// <summary>
        /// 无权限时跳转的页面,默认值:/_layouts/15/AccessDenied.aspx
        /// </summary>
        public static string NoPowerPageUrl
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["SPTempFolder"] != null)
                    return string.Format("{0}{1}", SPClassificationName, System.Web.Configuration.WebConfigurationManager.AppSettings["SPTempFolder"]);
                return string.Format("/_layouts/15/AccessDenied.aspx", SPClassificationName);
            }
        }
        /// <summary>
        /// 获取储存视频的本地路径，默认值为C:\\VideoUtil\\Temp\\
        /// </summary>
        public static string LocalSavePath
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["LocalSavePath"] != null)
                    return System.Web.Configuration.WebConfigurationManager.AppSettings["LocalSavePath"];
                return "C:\\VideoUtil\\Temp\\";
            }
        }

        /// <summary>
        /// 获取抓取工具的路径，默认值为C:\\VideoUtil\\ffmpeg.exe
        /// </summary>
        public static string FfmPegPath
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["FfmPegPath"] != null)
                    return System.Web.Configuration.WebConfigurationManager.AppSettings["FfmPegPath"];
                return "C:\\VideoUtil\\ffmpeg.exe";
            }
        }
        /// <summary>
        /// 获取截取图片的大小，默认值为260x160
        /// </summary>
        public static string FfmPegImgSize
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["FfmPegImgSize"] != null)
                    return System.Web.Configuration.WebConfigurationManager.AppSettings["FfmPegImgSize"];
                return "260x160";
            }
        }

        #region Three System ConStr
        /// <summary>
        /// 获取职教中心的连接字符串
        /// </summary>
        public static string SystemVocationalConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDVocationalEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDVocationalEducationConnectionString"].ConnectionString;
                return "Data Source=.;Initial Catalog=VocationalEducation;User ID=sa;password=COM.aec01!;";
            }
        }

        /// <summary>
        /// 获取学习平台的连接字符串
        /// </summary>
        public static string SystemStudentConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDStudentEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDStudentEducationConnectionString"].ConnectionString;
                throw new Exception("请设置学习平台的连接字符串!");
            }
        }
        /// <summary>
        /// 获取党员学习系统的连接字符串
        /// </summary>
        public static string SystemPartyMemberConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDPartyMemberEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDPartyMemberEducationConnectionString"].ConnectionString;
                throw new Exception("请设置党员学习的连接字符串!");
            }
        }
        /// <summary>
        /// 获取继续教育系统的连接字符串
        /// </summary>
        public static string SystemTeacherConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDContinuationEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDContinuationEducationConnectionString"].ConnectionString;
                throw new Exception("请设置继续教育的连接字符串!");
            }
        }
        #endregion

        public static SqlConnection GetConnection()
        {
            string url = CommonUtil.GetChildWebUrl();
            string title = CommonUtil.GetChildWebName();
            string conStr = string.Empty;

            if (title.Equals(PublicEnum.SystemStudentWebName) || url.Equals(PublicEnum.SystemStudentUrl))
                conStr = SystemStudentConStr;

            else if (title.Equals(PublicEnum.SystemTeacherWebName) || url.Equals(PublicEnum.SystemTeacherUrl))
                conStr = SystemTeacherConStr;

            else if (title.Equals(PublicEnum.SystemPartyMemberWebName) || url.Equals(PublicEnum.SystemPartyMemberUrl))
                conStr = SystemPartyMemberConStr;

            if (string.IsNullOrEmpty(conStr))
                conStr = SystemVocationalConStr;
                //throw new SystemException("无法获取系统信息!");

            return new SqlConnection(conStr);
        }
        public static SqlConnection GetConnection(string webUrl)
        {
            string conStr = string.Empty;

            if (webUrl.Equals(PublicEnum.SystemStudentUrl))
                conStr = SystemStudentConStr;

            else if (webUrl.Equals(PublicEnum.SystemTeacherUrl))
                conStr = SystemTeacherConStr;

            else if (webUrl.Equals(PublicEnum.SystemPartyMemberUrl))
                conStr = SystemPartyMemberConStr;

            if (string.IsNullOrEmpty(conStr))
                conStr = SystemVocationalConStr;
            //throw new SystemException("无法获取系统信息!");

            return new SqlConnection(conStr);
        }
        public static string GetConnectionStr()
        {
            String connStr = ConfigurationManager.ConnectionStrings["YHSDVocationalEducationConnectionString"].ConnectionString;
            return connStr;
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns></returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        public static object GetSingle(string SQLString,string conStr)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = GetConnection())
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
    }
}
