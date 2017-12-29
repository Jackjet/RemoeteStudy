using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.DirectoryServices;
using Common;
using System.Text.RegularExpressions;

using System.Text;

namespace ADManager
{
    /// <summary>
    /// ADWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://ADManager.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ADWebService : System.Web.Services.WebService
    {
        ValidateRegex validateReg = new ValidateRegex();
        [WebMethod(Description = "创建家长账户：sAMAccountName（zhangsan）、displayName（张三）、IDCard（身份证）、Password(密码要求：1.至少有六个字符长2.包含以下四类字符中的三类字符:英文大写字母(A 到 Z)，英文小写字母(a 到 z)，10 个基本数字(0 到 9)，非字母字符(例如 !、$、#、%))")]
        private string CreateParents(string sAMAccountName, string displayName, string Password, string IDCard, string OUType)
        {
            try
            {
                ADHelp ad = new ADHelp(OUType);
                if (!string.IsNullOrEmpty(sAMAccountName) && !string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(IDCard) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(OUType))
                {
                    if (validateReg.ValidateUseName(sAMAccountName))
                    {
                        if (validateReg.ValidatePassWord(Password))
                        {
                            if (validateReg.ValidateSFZ(IDCard))
                            {
                                //创建域控家长账户
                                ad.CreateParents(sAMAccountName, displayName, Password, IDCard);
                                return "";
                            }
                            else
                                return "身份证不符合命名要求！";
                        }
                        else
                            return "密码不符合命名要求！";
                    }
                    else
                        return "用户名不符合命名要求！";
                }
                else
                    return "参数不能为空！";
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：CreateParents");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName：" + displayName + ",Password：" + Password + ",IDCard：" + IDCard + ",OUType：" + OUType);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "创建教师账户：sAMAccountName（zhangsan）、displayName（张三）、IDCard（身份证）、Password(密码要求：1.至少有六个字符长2.包含以下四类字符中的三类字符:英文大写字母(A 到 Z)，英文小写字母(a 到 z)，10 个基本数字(0 到 9)，非字母字符(例如 !、$、#、%))")]
        public string CreateTeacher(string sAMAccountName, string displayName, string Password, string IDCard, string OUType)
        {
            try
            {
                ADHelp ad = new ADHelp(OUType);
                if (!string.IsNullOrEmpty(sAMAccountName) && !string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(IDCard) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(OUType))
                {
                    if (validateReg.ValidateUseName(sAMAccountName))
                    {
                        if (validateReg.ValidatePassWord(Password))
                        {
                            if (validateReg.ValidateSFZ(IDCard))
                            {
                                //创建域控教师账户
                                ad.CreateTeacher(sAMAccountName, displayName, Password, IDCard);
                                return "";
                            }
                            else
                                return "身份证不符合命名要求！";
                        }
                        else
                            return "密码不符合命名要求！";
                    }
                    else
                        return "用户名不符合命名要求！";
                }
                else
                    return "参数不能为空！";
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：CreateTeacher");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName：" + displayName + ",Password：" + Password + ",IDCard：" + IDCard + ",OUType：" + OUType);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }

        }

        [WebMethod(Description = "创建教师账户：sAMAccountName（zhangsan）、displayName（张三）、IDCard（身份证）、Password(密码要求：1.至少有六个字符长2.包含以下四类字符中的三类字符:英文大写字母(A 到 Z)，英文小写字母(a 到 z)，10 个基本数字(0 到 9)，非字母字符(例如 !、$、#、%))")]
        public string CreateTeacherTEST(string sAMAccountName, string displayName, string Password, string IDCard, string OUType)
        {
            try
            {
                ADHelp ad = new ADHelp(OUType);
                if (!string.IsNullOrEmpty(sAMAccountName) && !string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(IDCard) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(OUType))
                {
                    if (validateReg.ValidateUseName(sAMAccountName))
                    {
                        if (validateReg.ValidateSFZ(IDCard))
                        {
                            //创建域控教师账户
                            ad.CreateTeacher(sAMAccountName, displayName, Password, IDCard);
                            return "";
                        }
                        else
                            return "身份证不符合命名要求！";
                    }
                    else
                        return "用户名不符合命名要求！";
                }
                else
                    return "参数不能为空！";
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：CreateTeacherTEST");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName：" + displayName + ",Password：" + Password + ",IDCard：" + IDCard + ",OUType：" + OUType);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }


        [WebMethod(Description = "创建学生账户：sAMAccountName（zhangsan）、displayName（张三）、IDCard（身份证）、Password(密码要求：1.至少有六个字符长2.包含以下四类字符中的三类字符:英文大写字母(A 到 Z)，英文小写字母(a 到 z)，10 个基本数字(0 到 9)，非字母字符(例如 !、$、#、%))")]
        public string CreateStudent(string sAMAccountName, string displayName, string Password, string IDCard, string OUType)
        {
            try
            {
                ADHelp ad = new ADHelp(OUType);
                if (!string.IsNullOrEmpty(sAMAccountName) && !string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(IDCard) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(OUType))
                {
                    if (validateReg.ValidateUseName(sAMAccountName))
                    {
                        if (validateReg.ValidatePassWord(Password))
                        {
                            if (validateReg.ValidateSFZ(IDCard))
                            {
                                //创建域控学生账户
                                ad.CreateStudent(sAMAccountName, displayName, Password, IDCard);
                                return "";
                            }
                            else
                                return "身份证不符合命名要求！";
                        }
                        else
                            return "密码不符合命名要求！";
                    }
                    else
                        return "用户名不符合命名要求！";
                }
                else
                    return "参数不能为空！";
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：CreateStudent");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName：" + displayName + ",Password：" + Password + ",IDCard：" + IDCard + ",OUType：" + OUType);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "根据用户名删除域账户")]
        public bool DeleteUser(string sUserName)
        {
            ADHelp ad = new ADHelp();
            try
            {
                if (!string.IsNullOrEmpty(sUserName))
                {
                    //根据中文名查询域控账户是否存在
                    return ad.DeleteUser(sUserName);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：DeleteUser");
                sb.AppendLine("参数：sUserName：" + sUserName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "根据用户名删除域账户")]
        public bool DeleteUser2(string sUserName)
        {
            ADHelp ad = new ADHelp();
            try
            {
                if (!string.IsNullOrEmpty(sUserName))
                {
                    //根据中文名查询域控账户是否存在
                    return ad.DeleteUser2(sUserName);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：DeleteUser2");
                sb.AppendLine("参数：sUserName：" + sUserName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }
        [WebMethod(Description = "检查中文名是否存在")]
        public bool GetUserByCN(string displayName)
        {
            ADHelp ad = new ADHelp();
            try
            {
                if (!string.IsNullOrEmpty(displayName))
                {
                    //根据中文名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserByCN(displayName);
                    if (de == null)
                    {
                        return false;
                    } return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：GetUserByCN");
                sb.AppendLine("参数：displayName：" + displayName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }




        [WebMethod(Description = "检查身份证是否存在")]
        public bool GetUserByIDCard(string IDCard)
        {
            ADHelp ad = new ADHelp();
            try
            {
                if (!string.IsNullOrEmpty(IDCard))
                {
                    //根据中文名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserByIDCard(IDCard);
                    if (de == null)
                    {
                        return false;
                    } return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                // return false;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：GetUserByIDCard");
                sb.AppendLine("参数：displayName：" + IDCard);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }
        [WebMethod(Description = "检查身份证是否存在")]
        public string GetUserByIDCard2(string IDCard)
        {
            ADHelp ad = new ADHelp();
            try
            {
                if (!string.IsNullOrEmpty(IDCard))
                {
                    //根据中文名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserByIDCard(IDCard);
                    if (de == null)
                    {
                        return de.Properties["sAMAccountName"].Value.ToString();
                    }
                    return de.Properties["sAMAccountName"].Value.ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：GetUserByIDCard2");
                sb.AppendLine("参数：displayName：" + IDCard);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "检查用户名是否存在")]
        public bool GetUserBysAMAccountName(string sAMAccountName)
        {
            try
            {
                if (!string.IsNullOrEmpty(sAMAccountName))
                {
                    ADHelp ad = new ADHelp();
                    //根据用户名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserBysAMAccountName(sAMAccountName);
                    if (de == null)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：GetUserBysAMAccountName");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "检查用户名,查询用户组")]
        public string GetUserTypeBysAMAccountName(string sAMAccountName, string SchoolName)
        {
            try
            {
                string Result = "";
                if (!string.IsNullOrEmpty(sAMAccountName))
                {
                    ADHelp ad = new ADHelp(SchoolName);
                    //根据用户名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserBysAMAccountName(sAMAccountName);
                    if (de != null)
                    {
                        string A = de.Path;
                        if (A.Contains("老师"))
                        {
                            Result = "老师";
                        }
                        if (A.Contains("学生"))
                        {
                            Result = "学生";
                        }
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：GetUserTypeBysAMAccountName");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",SchoolName：" + SchoolName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "根据用户名和密码验证用户是否存在")]
        public bool IsUserValid(string sAMAccountName, string Password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(sAMAccountName) && !string.IsNullOrWhiteSpace(Password))
                {
                    ADHelp ad = new ADHelp();
                    return ad.IsUserValid(sAMAccountName, Password);
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：IsUserValid");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",Password：" + Password);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        [WebMethod(Description = "是否禁用域账户 , sAMAccountName：账户名 , IsEnable 是否启用")]
        public string IsEnableUser(string sAMAccountName, bool IsEnable)
        {
            try
            {
                if (!string.IsNullOrEmpty(sAMAccountName))
                {
                    ADHelp ad = new ADHelp();
                    //根据用户名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserBysAMAccountName(sAMAccountName);
                    if (de != null)
                    {
                        if (IsEnable)
                        {
                            ad.EnableUserAccount(de);
                        }
                        else
                        {
                            ad.DisableUserAccount(de);
                        }
                        return "操作成功";
                    }
                    return "域控中不存在【" + sAMAccountName + "】账户";
                }
                else
                {
                    return "参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：IsEnableUser");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",IsEnable：" + IsEnable);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPwd">老密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        [WebMethod(Description = "修改用户密码 , userName：账户名 , oldPwd 老密码 ，newPwd  新密码")]
        public string SetUserPassword(string userName, string oldPwd, string newPwd)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(oldPwd))
                {
                    ADHelp ad = new ADHelp();
                    string msg = string.Empty;
                    if (ad.IsUserValid(userName, oldPwd))
                    {
                        DirectoryEntry de = ad.GetUserBysAMAccountName(userName);
                        ad.ChangePassword(de, newPwd, out msg);
                        return "";
                    }
                    else
                    {
                        return "该用户信息不正确!";
                    }
                }
                else
                {
                    return "用户名或旧密码不能为空!";
                }

            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：SetUserPassword");
                sb.AppendLine("参数：userName：" + userName + ",oldPwd：" + oldPwd + ",newPwd：" + newPwd);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>9
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        [WebMethod(Description = "重置密码，UserName：帐户名")]
        public string ManagerResetPassWord(string UserName)
        {
            try
            {
                string strPatern = @"^([1-9]\d*)$";
                string newPwd = string.Empty;

                Regex reg = new Regex(strPatern);
                Random rd = new Random();
                string[] str = new string[3];
                str[0] = "abcdefghijklmnopqrstuvwxyz";
                str[1] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                str[2] = "0123456789";

                //for (int j = 0; j < 3; j++)
                //{
                //    for (int i = 0; i < 3; i++)
                //    {
                // if (i == 0 && reg.IsMatch(str[j][rd.Next(str.Length)].ToString()))
                // {
                //    i--;
                // }
                //else
                //{
                //        newPwd += str[j][rd.Next(str[j].Length)];
                //}
                //    }
                //}
                newPwd = System.Configuration.ConfigurationManager.ConnectionStrings["ResetPassword"].ToString();
                ADHelp ad = new ADHelp();
                string msg = string.Empty;
                DirectoryEntry de = ad.GetUserBysAMAccountName(UserName);
                ad.ChangePassword(de, newPwd, out msg);


                return newPwd;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：ManagerResetPassWord");
                sb.AppendLine("参数：UserName：" + UserName);
                sb.AppendLine("错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                return "";
            }

        }
        [WebMethod(Description = "查看域中指定的目录是否存在")]
        public bool GetDirectoryEntryOfGroup(string groupName)
        {
            try
            {
                ADHelp ad = new ADHelp();
                DirectoryEntry de = ad.IsExistDEGroup(groupName);
                if (de != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADWebService.asmx，方法名：GetDirectoryEntryOfGroup" + DateTime.Now + "--" + ex.Message, ex.StackTrace);
                return false;
            }
        }
        [WebMethod(Description = "修改OU名称")]
        public bool RenameOU(string old, string newOUName)
        {
            try
            {
                ADHelp ad = new ADHelp();
                return ad.RenameOU(old, newOUName);
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADWebService.asmx，方法名：RenameOU" + DateTime.Now + "--" + ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        [WebMethod(Description = "移动OU组 , originalSchoolName：原有学校 , nodeName:原有节点名称 , nowSchoolName：目标学校")] 
        public bool MoveOU(string originalSchoolName, string nodeName, string nowSchoolName)
        {
            try
            {
                ADHelp ad = new ADHelp();
                return ad.MoveTo(originalSchoolName, nodeName, nowSchoolName);
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADWebService.asmx，方法名：MoveOU" + DateTime.Now + "--" + ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
