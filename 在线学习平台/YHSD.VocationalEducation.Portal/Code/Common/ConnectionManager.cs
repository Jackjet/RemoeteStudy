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
        //ͼƬ�ĵ�������
        public static string ImgKuUrl = ConfigurationManager.AppSettings["ImgKuUrl"].ToString();
        //�˵�ͼƬ��ŵ��ļ�������
        public static string MenuIcoName = ConfigurationManager.AppSettings["MenuIcoName"].ToString();
        //�γ�ͼƬ��ŵ��ļ�������
        public static string CurriculumName = ConfigurationManager.AppSettings["CurriculumName"].ToString();
        //��ҵ������ŵ��ļ�������
        public static string WorkDescriptionImage = ConfigurationManager.AppSettings["WorkDescriptionImage"].ToString();
        //��ʱ���ͼƬ���ļ�������
        public static string InterimImage = ConfigurationManager.AppSettings["InterimImage"].ToString();
        //��Աͷ���ŵ��ļ�������
        public static string UserPhoto = ConfigurationManager.AppSettings["UserPhoto"].ToString();
        //�����ĵ�������
        public static string FuJianUrl = ConfigurationManager.AppSettings["FuJianUrl"].ToString();
        //ѧϰ�ĵô�ŵ��ļ�������
        public static string StudyExperience = ConfigurationManager.AppSettings["StudyExperience"].ToString();
        //AD����
        public static string ADName = ConfigurationManager.AppSettings["ADName"].ToString();
        //AD����ȫ��ַ
        public static string ADPath = ConfigurationManager.AppSettings["ADPath"].ToString();
        //AD�����Ա�˺�
        public static string ADAdminUser = ConfigurationManager.AppSettings["ADAdminUser"].ToString();
        //AD�����Ա����
        public static string ADAdminPassword = ConfigurationManager.AppSettings["ADAdminPassword"].ToString();
        //AD������Ա������֯��λ·��
        public static string ADOUUrl = ConfigurationManager.AppSettings["ADOUUrl"].ToString();
        //AD�����ʼ����
        public static string ADInitialpassword = ConfigurationManager.AppSettings["ADInitialpassword"].ToString();
        /// <summary>
        /// ��ȡ��Դ������SharePoint�е��ļ������ƣ�Ĭ��ֵΪ/ResourceStore
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
        /// ��ȡ��ʱ�ļ��е�·����Ĭ��ֵΪ /��Դ�����·��/TempFolder
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
        /// ��Ȩ��ʱ��ת��ҳ��,Ĭ��ֵ:/_layouts/15/AccessDenied.aspx
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
        /// ��ȡ������Ƶ�ı���·����Ĭ��ֵΪC:\\VideoUtil\\Temp\\
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
        /// ��ȡץȡ���ߵ�·����Ĭ��ֵΪC:\\VideoUtil\\ffmpeg.exe
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
        /// ��ȡ��ȡͼƬ�Ĵ�С��Ĭ��ֵΪ260x160
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
        /// ��ȡְ�����ĵ������ַ���
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
        /// ��ȡѧϰƽ̨�������ַ���
        /// </summary>
        public static string SystemStudentConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDStudentEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDStudentEducationConnectionString"].ConnectionString;
                throw new Exception("������ѧϰƽ̨�������ַ���!");
            }
        }
        /// <summary>
        /// ��ȡ��Աѧϰϵͳ�������ַ���
        /// </summary>
        public static string SystemPartyMemberConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDPartyMemberEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDPartyMemberEducationConnectionString"].ConnectionString;
                throw new Exception("�����õ�Աѧϰ�������ַ���!");
            }
        }
        /// <summary>
        /// ��ȡ��������ϵͳ�������ַ���
        /// </summary>
        public static string SystemTeacherConStr
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDContinuationEducationConnectionString"] != null)
                    return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YHSDContinuationEducationConnectionString"].ConnectionString;
                throw new Exception("�����ü��������������ַ���!");
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
                //throw new SystemException("�޷���ȡϵͳ��Ϣ!");

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
            //throw new SystemException("�޷���ȡϵͳ��Ϣ!");

            return new SqlConnection(conStr);
        }
        public static string GetConnectionStr()
        {
            String connStr = ConfigurationManager.ConnectionStrings["YHSDVocationalEducationConnectionString"].ConnectionString;
            return connStr;
        }
        /// <summary>
        /// ִ��һ�������ѯ�����䣬���ز�ѯ���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
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
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����
        /// </summary>
        /// <param name="SQLString">�����ѯ������</param>
        /// <returns>��ѯ�����object��</returns>
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
        /// ִ�в�ѯ��䣬����DataSet
        /// </summary>
        /// <param name="SQLString">��ѯ���</param>
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
        /// ִ��SQL��䣬����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>Ӱ��ļ�¼��</returns>
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
