using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using BLL;
using Model;
using System.Data;
using Common;
using System.Xml;
using System.IO;
using System.Text;
using ADManager.UserBLL;
using System.Text.RegularExpressions;

namespace ADManager
{
    /// <summary>
    /// CertificationService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://Certification.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class CertificationService : System.Web.Services.WebService
    {
        ValidateRegex validateReg = new ValidateRegex();
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="password"></param>
        /// <param name="UserManager">过滤返回参数系统账号</param>
        /// <returns></returns>
        [WebMethod(Description = "根据用户账号、IP和时间生成加密令牌，返回XmlDocument格式Token串")]
        public XmlDocument GeneratingToken_New(string LoginName, string password, string ip)
        {
            LogMethod("【记录信息】：生成token的信息", LoginName, password, ip, "无错误，此条非错误信息", "");//记录ip
            string HashToken = "", Type = "", IdCard = "", ErrorMessage = "1000";
            ADHelp ad = new ADHelp();
            Base_TeacherBLL tb = new Base_TeacherBLL();
            try
            {
                if (!string.IsNullOrEmpty(LoginName) && !string.IsNullOrEmpty(password))
                {
                    if (!ad.IsUserValid(LoginName, password))   //登陆失败
                    {
                        ErrorMessage = "3001";//用户名或密码不正确
                        HashToken = "";
                        //记录日志 
                        LogMethod("生成token", LoginName, password, ip, "用户名或密码不正确", "");
                    }
                    else //登陆成功
                    {
                        if (ValidateRegex.ValidateIP(ip))//验证ip
                        {
                            DateTime Time = DateTime.Now;
                            string StrToken = LoginName.Trim() + "sbtq" + ip.Trim() + Time.ToString();
                            HashToken = ValidateRegex.MD5(StrToken).ToLower();
                            if (!string.IsNullOrEmpty(HashToken) && HashToken.Length >= 30)
                                HashToken = HashToken.Remove(30);
                            //根据登录名查找用户信息
                            DataSet ds = tb.GetIDCardByZH(LoginName);
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Type"] != null)
                                    {
                                        Type = ds.Tables[0].Rows[0]["Type"].ToString();
                                        if (!string.IsNullOrWhiteSpace(Type))
                                        {
                                            if (ds.Tables[0].Rows[0]["SFZJH"] != null)
                                            {
                                                IdCard = ds.Tables[0].Rows[0]["SFZJH"].ToString();
                                                if (!string.IsNullOrWhiteSpace(IdCard))
                                                {
                                                    if (Type == "教师")
                                                    {
                                                        Base_Teacher Tea = new Base_Teacher();
                                                        Tea.ZJDLSJ = Time;
                                                        Tea.DLIP = ip;
                                                        Tea.DLBSM = HashToken;
                                                        Tea.SFZJH = IdCard;
                                                        tb.UpdateLoginInfo(Tea);

                                                    }
                                                    else if (Type == "学生")
                                                    {
                                                        Base_StudentBLL stubll = new Base_StudentBLL();
                                                        Base_Student stu = new Base_Student();
                                                        stu.ZJDLSJ = Time;
                                                        stu.DLIP = ip;
                                                        stu.DLBSM = HashToken;
                                                        stu.SFZJH = IdCard;
                                                        stubll.UpdateLoginInfo(stu);
                                                    }
                                                    else if (Type == "家长")
                                                    {
                                                        Base_ParentBLL parbll = new Base_ParentBLL();
                                                        Base_Parent Par = new Base_Parent();
                                                        Par.ZJDLSJ = Time;
                                                        Par.DLIP = ip;
                                                        Par.DLBSM = HashToken;
                                                        Par.SFZJH = IdCard;
                                                        parbll.UpdateLoginInfo(Par);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorMessage = "3003";
                            HashToken = "";
                            LogMethod("生成token", LoginName, password, ip, "IP地址获取错误", "");
                        }
                    }
                }
                else
                {
                    ErrorMessage = "3002";
                    HashToken = "";
                    LogMethod("生成token", LoginName, password, ip, "用户名和密码不能为空", "");
                }
                return CreateXmlInfo(ErrorMessage, "<Token>" + HashToken + "</Token>");
            }
            catch (Exception ex)
            {
                ErrorMessage = "4000";
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                LogMethod("生成token", LoginName, password, ip, ex.Message, ex.StackTrace);
                return CreateXmlInfo(ErrorMessage, "<Token></Token>");
            }
        }
        [WebMethod(Description = "根据用户账号、IP和时间生成加密令牌，返回XmlDocument格式Token串")]
        public XmlDocument GeneratingToken(string LoginName, string password, string UserManager)
        {
            string ErrorMessage = "";
            DataTable DtUserInfo = DataTableInitialization();
            string XmlTeacher = "";

            try
            {
                string IP = "";
                if (!string.IsNullOrEmpty(LoginName) && !string.IsNullOrEmpty(password))
                {
                    try
                    {
                        ADHelp ad = new ADHelp();
                        if (!ad.IsUserValid(LoginName, password))   //登陆失败
                        {
                            ErrorMessage = "用户名或密码不正确";
                            DtUserInfo.Rows[0]["ErrorMessage"] = ErrorMessage;
                        }
                        else //登陆成功
                        {
                            //获取用户IP
                            if (Context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy 
                            {
                                IP = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 
                            }
                            else// not using proxy or can't get the Client IP 
                            {
                                IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 
                            }
                            if (!string.IsNullOrEmpty(IP))
                            {
                                DateTime Time = DateTime.Now;
                                string StrToken = LoginName.Trim() + "sbtq" + IP.Trim() + Time.ToString();
                                string HashToken = FormsAuthentication.HashPasswordForStoringInConfigFile(StrToken, "MD5").ToLower();
                                string IdCard = "";
                                string Type = "";
                                if (!string.IsNullOrEmpty(HashToken) && HashToken.Length >= 30)
                                {
                                    HashToken = HashToken.Remove(30);
                                }
                                Base_TeacherBLL tb = new Base_TeacherBLL();
                                //根据登录名查找用户信息
                                DataSet ds = tb.GetIDCardByZH(LoginName);
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["Type"] != null)
                                        {
                                            Type = ds.Tables[0].Rows[0]["Type"].ToString();
                                            if (!string.IsNullOrWhiteSpace(Type))
                                            {
                                                if (ds.Tables[0].Rows[0]["SFZJH"] != null)
                                                {
                                                    IdCard = ds.Tables[0].Rows[0]["SFZJH"].ToString();
                                                    if (!string.IsNullOrWhiteSpace(IdCard))
                                                    {
                                                        DataSet dsUserInfo = tb.GetUserInfoBLL(LoginName);//获取用户信息
                                                        if (dsUserInfo != null && ds.Tables.Count > 0)
                                                        {
                                                            DtUserInfo.Rows[0]["YHZH"] = dsUserInfo.Tables[0].Rows[0]["YHZH"];
                                                            DtUserInfo.Rows[0]["XM"] = dsUserInfo.Tables[0].Rows[0]["XM"];
                                                            DtUserInfo.Rows[0]["JGMC"] = "";
                                                            DtUserInfo.Rows[0]["LXDH"] = "";
                                                            DtUserInfo.Rows[0]["DZXX"] = "";
                                                            DtUserInfo.Rows[0]["XBM"] = "";
                                                        }

                                                        if (Type == "教师")
                                                        {
                                                            DataSet dsTeacherInfo = tb.GetTeacherInfoBLL(LoginName);//获取用户信息
                                                            if (dsUserInfo != null && ds.Tables.Count > 0)
                                                            {
                                                                DtUserInfo.Rows[0]["JGMC"] = dsTeacherInfo.Tables[0].Rows[0]["JGMC"];
                                                                DtUserInfo.Rows[0]["LXDH"] = dsTeacherInfo.Tables[0].Rows[0]["LXDH"];
                                                                DtUserInfo.Rows[0]["DZXX"] = dsTeacherInfo.Tables[0].Rows[0]["DZXX"];
                                                                DtUserInfo.Rows[0]["XBM"] = dsTeacherInfo.Tables[0].Rows[0]["XBM"];
                                                            }


                                                            Base_Teacher Tea = new Base_Teacher();
                                                            Tea.ZJDLSJ = Time;
                                                            Tea.DLIP = IP;
                                                            Tea.DLBSM = HashToken;
                                                            Tea.SFZJH = IdCard;
                                                            tb.UpdateLoginInfo(Tea);

                                                            if (UserManager == "")
                                                            { XmlTeacher = GetTeachersInfo(LoginName); }
                                                            else
                                                            { XmlTeacher = GetTeachersInfo(UserManager); }
                                                        }
                                                        if (Type == "学生")
                                                        {
                                                            Base_StudentBLL stubll = new Base_StudentBLL();
                                                            Base_Student stu = new Base_Student();
                                                            stu.ZJDLSJ = Time;
                                                            stu.DLIP = IP;
                                                            stu.DLBSM = HashToken;
                                                            stu.SFZJH = IdCard;
                                                            stubll.UpdateLoginInfo(stu);
                                                        }
                                                        if (Type == "家长")
                                                        {
                                                            Base_ParentBLL parbll = new Base_ParentBLL();
                                                            Base_Parent Par = new Base_Parent();
                                                            Par.ZJDLSJ = Time;
                                                            Par.DLIP = IP;
                                                            Par.DLBSM = HashToken;
                                                            Par.SFZJH = IdCard;
                                                            parbll.UpdateLoginInfo(Par);
                                                        }
                                                        DtUserInfo.Rows[0]["Token"] = HashToken;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ErrorMessage = "IP地址获取错误";
                                DtUserInfo.Rows[0]["ErrorMessage"] = ErrorMessage;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                        ErrorMessage = "账号验证异常请联系管理员";
                        DtUserInfo.Rows[0]["ErrorMessage"] = ErrorMessage;
                    }
                }
                else
                {
                    ErrorMessage = "参数不符合要求";
                    DtUserInfo.Rows[0]["ErrorMessage"] = ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                ErrorMessage = "令牌生成异常请联系管理员";
                DtUserInfo.Rows[0]["ErrorMessage"] = ErrorMessage;
            }
            XmlDocument XmlDoc = new XmlDocument();

            string UserInfoXML = @"<ALL>
                                    <UserInfoXml>
                                          <ErrorMessage>" + DtUserInfo.Rows[0]["ErrorMessage"].ToString() + @"</ErrorMessage>
                                          <YHZH>" + DtUserInfo.Rows[0]["YHZH"].ToString() + @"</YHZH>
                                          <XM>" + DtUserInfo.Rows[0]["XM"].ToString() + @"</XM>
                                          <JGMC>" + DtUserInfo.Rows[0]["JGMC"].ToString() + @"</JGMC>
                                          <LXDH>" + DtUserInfo.Rows[0]["LXDH"].ToString() + @"</LXDH>
                                          <DZXX>" + DtUserInfo.Rows[0]["DZXX"].ToString() + @"</DZXX>
                                          <XBM>" + DtUserInfo.Rows[0]["XBM"].ToString() + @"</XBM>
                                          <Token>" + DtUserInfo.Rows[0]["Token"].ToString() + @"</Token>
                                     </UserInfoXml>" + XmlTeacher + "</ALL>";
            XmlDoc.LoadXml(UserInfoXML);
            return XmlDoc;
        }

        [WebMethod(Description = "验证令牌，传入令牌值，验证通过后返回用户信息，返回XmlDocument格式")]
        public XmlDocument AuthenticationToken_New(string Token, string UserManager, string IP)
        {
            string XmlTeacher = "", ErrorMessage = "1000", strIP = "";
            Base_StudentBLL stubll = new Base_StudentBLL();
            Base_TeacherBLL tb = new Base_TeacherBLL();
            Base_ParentBLL parbll = new Base_ParentBLL();
            DateTime ZJDLSJ;
            try
            {
                //IP = "192.168.1.1"; 
                if (ValidateRegex.ValidateIP(IP))//验证ip  
                {
                    if (validateReg.ValidateLength(Token.Trim(), 30))
                    {
                        //超时时间间隔,超过20分钟，阻止进入其他系统（类似session过期）
                        int timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.ConnectionStrings["Timeout"].ToString());
                        TimeSpan Timeout = new TimeSpan(0, timeout, 0);
                        DataSet ds = tb.GetTeacherInfoByTokenBLL(Token.Trim());
                        if (ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["DLIP"] != null)
                            {
                                strIP = ds.Tables[0].Rows[0]["DLIP"].ToString();
                                //判断IP是否相同
                                if (strIP.Equals(IP))
                                {
                                    if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["ZJDLSJ"].ToString()) && ds.Tables[0].Rows[0]["ZJDLSJ"] != DBNull.Value)
                                    {
                                        if (DateTime.TryParse(ds.Tables[0].Rows[0]["ZJDLSJ"].ToString(), out ZJDLSJ))
                                        {
                                            if (ZJDLSJ != DateTime.MinValue)
                                            {
                                                //判断登录时间是否超时
                                                if ((DateTime.Now - ZJDLSJ) < Timeout)
                                                {
                                                    //根据系统账号去查询返回配置信息
                                                    if (UserManager == "")
                                                        XmlTeacher = GetTeachersInfo("DefaultUser", ds.Tables[0].Rows[0]["XM"].ToString());
                                                    else
                                                        XmlTeacher = GetTeachersInfo(UserManager, ds.Tables[0].Rows[0]["XM"].ToString());
                                                }
                                                else
                                                {
                                                    ErrorMessage = "3009";
                                                    XmlTeacher = "";
                                                    ErrorMessage = "登陆超时请重新登陆！";
                                                    LogMethod("验证token", Token, IP, "登陆超时请重新登陆！", "");
                                                }
                                            }
                                        }
                                    }
                                    //最近登陆时间为空
                                    else
                                    {
                                        ErrorMessage = "3008";
                                        XmlTeacher = "";
                                        Common.LogCommon.writeLogWebService("上一次登陆时间获取失败，ToKen:" + Token + "，时间：" + DateTime.Now.ToString(), "CertificationService.asmx");
                                        LogMethod("验证token", Token, IP, "上一次登陆时间获取失败请联系管理员", "");
                                    }
                                }
                                //IP不一致
                                else
                                {
                                    ErrorMessage = "3004";
                                    XmlTeacher = "";
                                    LogMethod("验证token", Token, IP, "IP地址不一致", "");
                                }
                            }
                            //DLIP为空
                            else
                            {
                                ErrorMessage = "3005";
                                XmlTeacher = "";
                                LogMethod("验证token", Token, IP, "上一次登陆IP地址获取失败请联系管理员", "");
                                Common.LogCommon.writeLogWebService("上一次登陆IP获取失败，ToKen:" + Token + "，时间：" + DateTime.Now.ToString(), "CertificationService.asmx");
                            }
                        }
                        else
                        {
                            ErrorMessage = "3007";
                            XmlTeacher = "";
                            LogMethod("验证token", Token, IP, "Token值不存在", "");
                        }
                    }
                    //正则验证未通过
                    else
                    {
                        ErrorMessage = "3006";
                        XmlTeacher = "";
                        LogMethod("验证token", Token, IP, "Token值格式有误", "");
                    }
                }
                else
                {
                    ErrorMessage = "3003";
                    XmlTeacher = "";
                    LogMethod("验证token", Token, IP, "IP格式不正确", "");
                }
                return CreateXmlInfo(ErrorMessage, XmlTeacher);
            }
            catch (Exception ex)
            {
                ErrorMessage = "4000";
                XmlTeacher = "";
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                LogMethod("验证token", Token, IP, ex.Message, ex.StackTrace);
                return CreateXmlInfo(ErrorMessage, XmlTeacher);
            }
        }
        [WebMethod(Description = "验证令牌，传入令牌值，验证通过后返回用户信息，返回XmlDocument格式")]
        public XmlDocument AuthenticationToken(string Token, string UserManager)
        {
            string XmlTeacher = "";
            string UserInfoXML = "";
            XmlDocument xd = new XmlDocument();
            Base_StudentBLL stubll = new Base_StudentBLL();
            Base_TeacherBLL tb = new Base_TeacherBLL();
            Base_ParentBLL parbll = new Base_ParentBLL();
            //登陆IP
            string strIP = "";
            //最近登陆IP
            DateTime ZJDLSJ;
            //用户登陆IP
            string IP = "";
            DataTable DtUserInfo = DataTableInitialization();
            try
            {

                if (Context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy 
                {
                    IP = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 
                }
                else// not using proxy or can't get the Client IP 
                {
                    IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 
                }
                if (string.IsNullOrEmpty(IP))
                {
                    Common.LogCommon.writeLogWebService("IP地址获取不到", "CertificationService.asmx");
                    DtUserInfo.Rows[0]["ErrorMessage"] = "IP地址获取不到";
                    DtUserInfo.Rows[0]["ValidationResults"] = "false";
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                DtUserInfo.Rows[0]["ErrorMessage"] = ex.Message;
                DtUserInfo.Rows[0]["ValidationResults"] = "false";
            }

            try
            {
                if (validateReg.ValidateLength(Token, 30))
                {
                    //超时时间间隔
                    TimeSpan Timeout = new TimeSpan(0, 20, 0);
                    //DataSet ds = teabll.GetIPByToken(Token);
                    DataSet ds = tb.GetTeacherInfoByTokenBLL(Token);
                    if (ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["DLIP"] != null)
                        {
                            strIP = ds.Tables[0].Rows[0]["DLIP"].ToString();
                            //判断IP是否相同
                            if (strIP.Equals(IP))
                            {
                                if (ds.Tables[0].Rows[0]["ZJDLSJ"] != null)
                                {
                                    if (DateTime.TryParse(ds.Tables[0].Rows[0]["ZJDLSJ"].ToString(), out ZJDLSJ))
                                    {
                                        if (ZJDLSJ != DateTime.MinValue)
                                        {
                                            //判断登录时间是否超时
                                            if ((DateTime.Now - ZJDLSJ) < Timeout)
                                            {
                                                DtUserInfo.Rows[0]["YHZH"] = ds.Tables[0].Rows[0]["YHZH"].ToString();
                                                DtUserInfo.Rows[0]["XM"] = ds.Tables[0].Rows[0]["XM"].ToString();
                                                DtUserInfo.Rows[0]["JGMC"] = ds.Tables[0].Rows[0]["JGMC"];
                                                DtUserInfo.Rows[0]["LXDH"] = ds.Tables[0].Rows[0]["LXDH"];
                                                DtUserInfo.Rows[0]["DZXX"] = ds.Tables[0].Rows[0]["DZXX"];
                                                DtUserInfo.Rows[0]["XBM"] = ds.Tables[0].Rows[0]["XBM"];

                                                DtUserInfo.Rows[0]["ValidationResults"] = "true";
                                                if (UserManager == "")
                                                { XmlTeacher = GetTeachersInfo(DtUserInfo.Rows[0]["YHZH"].ToString()); }
                                                else
                                                { XmlTeacher = GetTeachersInfo(UserManager); }

                                                UserInfoXML = @"<ALL>
                                                                    <UserInfoXml>
                                                                          <ErrorMessage>" + DtUserInfo.Rows[0]["ErrorMessage"].ToString() + @"</ErrorMessage>
                                                                          <YHZH>" + DtUserInfo.Rows[0]["YHZH"].ToString() + @"</YHZH>
                                                                          <XM>" + DtUserInfo.Rows[0]["XM"].ToString() + @"</XM>
                                                                          <JGMC>" + DtUserInfo.Rows[0]["JGMC"].ToString() + @"</JGMC>
                                                                          <LXDH>" + DtUserInfo.Rows[0]["LXDH"].ToString() + @"</LXDH>
                                                                          <DZXX>" + DtUserInfo.Rows[0]["DZXX"].ToString() + @"</DZXX>
                                                                          <XBM>" + DtUserInfo.Rows[0]["XBM"].ToString() + @"</XBM>
                                                                          <Token>" + DtUserInfo.Rows[0]["Token"].ToString() + @"</Token>
                                                                     </UserInfoXml>" + XmlTeacher + "</ALL>";

                                                xd.LoadXml(UserInfoXML);
                                                return xd;
                                            }
                                            else
                                            {
                                                DtUserInfo.Rows[0]["ValidationResults"] = "false";
                                                DtUserInfo.Rows[0]["ErrorMessage"] = "超时、请重新登陆";

                                            }
                                        }
                                    }
                                }
                                //最近登陆时间为空
                                else
                                {
                                    DtUserInfo.Rows[0]["ValidationResults"] = "false";
                                    DtUserInfo.Rows[0]["ErrorMessage"] = "上一次登陆时间获取失败请联系管理员";
                                    Common.LogCommon.writeLogWebService("上一次登陆时间获取失败，ToKen:" + Token + "，时间：" + DateTime.Now.ToString(), "CertificationService.asmx");
                                }
                            }
                            //IP不一致
                            else
                            {
                                DtUserInfo.Rows[0]["ValidationResults"] = "false";
                                DtUserInfo.Rows[0]["ErrorMessage"] = "IP地址不一致";
                            }
                        }
                        //DLIP为空
                        else
                        {
                            DtUserInfo.Rows[0]["ValidationResults"] = "false";
                            DtUserInfo.Rows[0]["ErrorMessage"] = "上一次登陆IP地址获取失败请联系管理员";
                            Common.LogCommon.writeLogWebService("上一次登陆IP获取失败，ToKen:" + Token + "，时间：" + DateTime.Now.ToString(), "CertificationService.asmx");
                        }

                    }
                    else
                    {
                        DtUserInfo.Rows[0]["ValidationResults"] = "false";
                        DtUserInfo.Rows[0]["ErrorMessage"] = "Token值不对请联系管理员";
                    }
                }
                //正则验证未通过
                else
                {

                    DtUserInfo.Rows[0]["ValidationResults"] = "false";
                    DtUserInfo.Rows[0]["ErrorMessage"] = "Token值错误";
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                DtUserInfo.Rows[0]["ValidationResults"] = "false";
                DtUserInfo.Rows[0]["ErrorMessage"] = ex.Message;

            }

            XmlTeacher = GetTeachersInfo(DtUserInfo.Rows[0]["YHZH"].ToString());

            UserInfoXML = @"<ALL>
                                <UserInfoXml>
                                      <ErrorMessage>" + DtUserInfo.Rows[0]["ErrorMessage"].ToString() + @"</ErrorMessage>
                                      <YHZH>" + DtUserInfo.Rows[0]["YHZH"].ToString() + @"</YHZH>
                                      <XM>" + DtUserInfo.Rows[0]["XM"].ToString() + @"</XM>
                                      <JGMC>" + DtUserInfo.Rows[0]["JGMC"].ToString() + @"</JGMC>
                                      <LXDH>" + DtUserInfo.Rows[0]["LXDH"].ToString() + @"</LXDH>
                                      <DZXX>" + DtUserInfo.Rows[0]["DZXX"].ToString() + @"</DZXX>
                                      <XBM>" + DtUserInfo.Rows[0]["XBM"].ToString() + @"</XBM>
                                      <Token>" + DtUserInfo.Rows[0]["Token"].ToString() + @"</Token>
                                 </UserInfoXml>" + XmlTeacher + "</ALL>";
            xd.LoadXml(UserInfoXML);
            return xd;
        }

        /// <summary>
        /// 返回xml格式数据
        /// </summary>
        /// <param name="Error">错误代码</param>
        /// <param name="Valnode">返回值</param>
        /// <returns></returns>
        public XmlDocument CreateXmlInfo(string Error, string Valnode)
        {
            XmlDocument XmlDoc = new XmlDocument();
            string UserInfoXML = @"<ALL>
                    <ErrorMessage>" + Error + "</ErrorMessage>" + Valnode + "</ALL>";
            XmlDoc.LoadXml(UserInfoXML);
            return XmlDoc;
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        private static DataTable DataTableInitialization()
        {
            DataTable DtUserInfo = new DataTable();
            DtUserInfo.Columns.Add("ErrorMessage");
            DtUserInfo.Columns.Add("YHZH");
            DtUserInfo.Columns.Add("XM");
            DtUserInfo.Columns.Add("JGMC");
            DtUserInfo.Columns.Add("LXDH");
            DtUserInfo.Columns.Add("DZXX");
            DtUserInfo.Columns.Add("XBM");
            DtUserInfo.Columns.Add("Token");
            DtUserInfo.Columns.Add("ValidationResults");
            DataRow dr = DtUserInfo.NewRow();
            dr["ErrorMessage"] = "";
            dr["YHZH"] = "";
            dr["XM"] = "";
            dr["JGMC"] = "";
            dr["LXDH"] = "";
            dr["DZXX"] = "";
            dr["XBM"] = "";
            dr["Token"] = "";
            dr["ValidationResults"] = "";
            DtUserInfo.Rows.Add(dr);
            DtUserInfo.TableName = "UserInfo";
            return DtUserInfo;
        }
        /// <summary>
        /// 获取教师信息
        /// </summary>
        private string GetTeachersInfo(string LoginName)
        {
            string strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = GetCommonInfo(LoginName, null, "", "GetTeachersInfo", "Base_Teacher", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return DataTableToXml(dt);
        }
        private string GetTeachersInfo(string LoginName, string xm)
        {
            string strResult = string.Empty;
            DataTable dt = new DataTable();
            UserInfoBLL userInfoBll = new UserInfoBLL();
            try
            {
                DataTable table = userInfoBll.IsConfig(LoginName);
                if (table.Rows.Count > 0)
                    dt = GetCommonInfo(LoginName, null, "", table.Rows[0]["InterfaceName"].ToString(), table.Rows[0]["TableName"].ToString(), xm, out strResult);
                else
                    dt = GetCommonInfo(LoginName, null, "", "GetTeachersInfo", "Base_Teacher", xm, out strResult);
                if (dt.Rows.Count > 0)
                    return DataTableToXml(dt);
                else
                    return strResult;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return strResult;
            }
        }
        /// <summary>
        /// 根据登录名、返回数据列集合、学校组织机构号查询教师信息
        /// </summary>
        /// <param name="LoginName">登录名</param>
        /// <param name="ColumnTitle">数据列集合</param>
        /// <param name="SchoolCode">学校组织机构号</param>
        /// <param name="strFunName">方法名称</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="strResult">提示信息</param>
        private DataTable GetCommonInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, string strFunName, string strTableName, string xm, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            dt.TableName = "请求表";
            try
            {
                ValidateRegex validate = new ValidateRegex();
                string strValidats = string.Empty;
                if (ColumnTitle != null)
                {
                    foreach (string str in ColumnTitle)
                    {
                        strValidats += str;
                        strValidats += ",";
                    }
                    string strLast = ",";
                    if (strValidats.Contains(strLast))
                        strValidats = strValidats.Substring(0, strValidats.Length - strLast.Length);//去除最后一个英文逗号
                }

                //验证传入参数是否有SQL注入，如果没有，执行数据库查询操作；否则，不执行数据库查询操作。
                if (validate.ValidateSQL(LoginName))
                    strResult = "参数“登录名”无效";
                else if (validate.ValidateSQL(strValidats))
                    strResult = "参数“数据列集合”无效";
                else if (validate.ValidateSQL(SchoolCode))
                    strResult = "参数“学校组织机构号”无效";
                else if (validate.ValidateSQL(strTableName))
                    strResult = "参数“表名称”无效";
                else
                {
                    UserInfoBLL userInfoBll = new UserInfoBLL();
                    if (userInfoBll.IsConfig(LoginName, strFunName, strTableName))
                    {
                        strValidats = userInfoBll.GetDataItems(LoginName, strFunName, strTableName);
                        return userInfoBll.GetUserInfoByXXDM(strValidats, SchoolCode, strTableName, xm);
                    }
                    else
                        strResult = "没有读取此接口的数据权限";
                }
                return null;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            }
        }
        private DataTable GetCommonInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, string strFunName, string strTableName, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            dt.TableName = "请求表";
            try
            {
                ValidateRegex validate = new ValidateRegex();
                string strValidats = string.Empty;
                if (ColumnTitle != null)
                {
                    foreach (string str in ColumnTitle)
                    {
                        strValidats += str;
                        strValidats += ",";
                    }
                    string strLast = ",";
                    if (strValidats.Contains(strLast))
                        strValidats = strValidats.Substring(0, strValidats.Length - strLast.Length);//去除最后一个英文逗号
                }
                //验证传入参数是否有SQL注入，如果没有，执行数据库查询操作；否则，不执行数据库查询操作。
                if (validate.ValidateSQL(LoginName))
                    strResult = "参数“登录名”无效";
                else if (validate.ValidateSQL(strValidats))
                    strResult = "参数“数据列集合”无效";
                else if (validate.ValidateSQL(SchoolCode))
                    strResult = "参数“学校组织机构号”无效";
                else if (validate.ValidateSQL(strTableName))
                    strResult = "参数“表名称”无效";
                else
                {
                    UserInfoBLL userInfoBll = new UserInfoBLL();
                    if (userInfoBll.IsConfig(LoginName, strFunName, strTableName))
                    {
                        strValidats = userInfoBll.GetDataItems(LoginName, strFunName, strTableName);
                        return userInfoBll.GetUserInfoByXXDM(strValidats, SchoolCode, strTableName);
                    }
                    else
                        strResult = "没有读取此接口的数据权限";
                }
                return null;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            }
        }
        #region 【DataTable】 转换 【XML】
        /// <summary>
        /// 【DataTable】 转换 【XML】
        /// </summary>
        public string DataTableToXml(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder strXml = new StringBuilder();
                    strXml.AppendLine("<XmlTable>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strXml.AppendLine("<rows>");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            strXml.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j] + "</" + dt.Columns[j].ColumnName + ">");
                        }
                        strXml.AppendLine("</rows>");
                    }
                    strXml.AppendLine("</XmlTable>");
                    return strXml.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return "";
            }
        }
        /// <summary>
        /// 【DataTable】 转换 【XML】
        /// </summary>
        private string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                if (xmlDS.Rows.Count > 0)
                {

                    stream = new MemoryStream();
                    writer = new XmlTextWriter(stream, Encoding.Default);
                    xmlDS.WriteXml(writer);
                    int count = (int)stream.Length;
                    byte[] arr = new byte[count];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(arr, 0, count);
                    UTF8Encoding utf = new UTF8Encoding();
                    return utf.GetString(arr).Trim();
                }
                else
                    return "";
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        } 
        #endregion
        #region 记录错误日志
        /// <summary>
        /// 记录错误日志
        /// </summary> 
        private static void LogMethod(string Name, string LoginName, string password, string ip, string ex, string exstc)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：" + Name);
                sb.AppendLine("参数：LoginName：" + LoginName + ",password：" + password + ", ip：" + ip);
                sb.AppendLine("错误信息：" + ex);
                LogCommon.writeLogWebService(sb.ToString(), exstc);
            }
            catch (Exception ex2)
            {
                Common.LogCommon.writeLogWebService(ex2.Message, ex2.StackTrace);
            }
        }
        private static void LogMethod(string Name, string token, string ip, string ex, string exstc)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：" + Name);
                sb.AppendLine("参数：token：" + token + ",ip：" + ip);
                sb.AppendLine("错误信息：" + ex);
                LogCommon.writeLogWebService(sb.ToString(), exstc);
            }
            catch (Exception ex2)
            {
                Common.LogCommon.writeLogWebService(ex2.Message, ex2.StackTrace);
            }
        }
        #endregion
    }
}

