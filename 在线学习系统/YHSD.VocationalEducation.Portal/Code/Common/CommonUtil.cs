using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.SharePoint;
using System.IO;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using Centerland.ADUtility;
using System.DirectoryServices;


namespace YHSD.VocationalEducation.Portal.Code.Common
{

    public class CommonUtil
    {

        LogCom com = new LogCom();
        /// <summary>
        /// 替换换行符
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static String changeRN(String strInput)
        {
            String strOutput = "";
            if (strInput != null && strInput != "")
                strOutput = strInput.Replace("\r\n", "<br>");
            return strOutput;
        }
        public static String GetCurrentTimeMillis()
        {
            DateTime now = System.DateTime.Now;
            return String.Format("{0}{1}{2}{3}{4}{5}{6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
            //Convert.ToString(System.DateTime.Now.Millisecond)
        }
        /// <summary>
        /// 转换时间格式为年月日 格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static String getDate(Object datetime)
        {
            String rtn = "";
            if (datetime != null && !String.IsNullOrEmpty(datetime.ToString()))
            {
                rtn = DateTime.Parse(datetime.ToString()).ToString("yyyy-MM-dd");
            }
            return rtn;
        }
        public static String getDate(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 时间转换为yyyy-MM-dd HH:mm:ss格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string getDetailDate(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string getDetailDate(object datetime)
        {
            DateTime time;
            if (datetime != null && DateTime.TryParse(datetime.ToString(), out time))
            {
                return getDetailDate(time);
            }
            return string.Empty;

        }

        /// <summary>   
        /// Json序列化   
        /// </summary>   
        /// <typeparam name="T">泛型</typeparam>   
        /// <param name="t">泛型</param>   
        /// <returns>序列化</returns>   
        public static string Serialize(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string str)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(str);
        }
        public static object DeSerialize(string str, Type type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize(str, type);
        }
        /// <summary>
        /// 转换时间格式为年月日 时分 格式
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static String getDate2(Object datetime)
        {
            String rtn = "";
            if (datetime != null && !String.IsNullOrEmpty(datetime.ToString()))
            {
                rtn = DateTime.Parse(datetime.ToString()).ToString("yyyy-MM-dd HH:mm");
            }
            return rtn;
        }

        public static String changeGUIDToSql(String strGUID)
        {
            string[] strArr = strGUID.Split(',');

            string strTemp = "";

            foreach (string strID in strArr)
            {
                strTemp = strTemp + "'" + strID + "',";
            }

            return strTemp.TrimEnd(',');
        }
        /// <summary>
        /// 不能转换字符串则设置为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(object str)
        {
            string st = "";
            try
            {
                st = str.ToString();
            }
            catch (Exception)
            {

                st = "";
            }
            return st;
        }
        /// <summary>
        /// 上传附件到指定文档库中的指定文件夹,并且删除相同的附件
        /// </summary>
        /// <param name="TableName">文件夹名称</param>
        /// <param name="filepath">文档库路径</param>
        /// <param name="WenJianName">附件名</param>
        /// <param name="b">附件字节</param>
        /// <param name="DeleteFileURL">要删除的附件</param>
        /// <returns></returns>
        public string CreatetFuJianName(string TableName, string filepath, string WenJianName, byte[] b, string DeleteFileURL)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {
                    bool flag = true;
                    MyWeb.AllowUnsafeUpdates = true;
                    SPDocumentLibrary DocLibrary = (SPDocumentLibrary)MyWeb.GetList(SPContext.Current.Web.Url + filepath);
                    SPFolderCollection folders = DocLibrary.RootFolder.SubFolders;
                    foreach (SPFolder subFolder in folders)//获取文件夹中的文件夹
                    {

                        if (subFolder.Name == TableName)
                        {
                            flag = false;
                            if (!string.IsNullOrEmpty(DeleteFileURL))
                            {
                                //如果之前有图片,将之前的图片删除
                                subFolder.Files.Delete(DeleteFileURL);
                            }

                            //将最新的图片放到文档库中
                            subFolder.Files.Add(WenJianName, b);
                            return GetChildWebUrl() + "/" + subFolder.Url + "/" + WenJianName;
                        }
                    }
                    if (flag == true)
                    {
                        SPFolder subFolder = folders.Add(TableName);
                        //将最新的图片放到文档库中
                        subFolder.Files.Add(WenJianName, b);
                        return GetChildWebUrl() + "/" + subFolder.Url + "/" + WenJianName;
                    }
                }
            }
            return "";

        }
        /// <summary>
        /// 上传附件到指定文档库中的指定文件夹,并且删除相同的附件
        /// </summary>
        /// <param name="TableName">文件夹名称</param>
        /// <param name="filepath">文档库路径</param>
        /// <param name="WenJianName">附件名</param>
        /// <param name="b">附近字节</param>
        /// <param name="DeleteFileURL">要删除的附件</param>
        /// <returns></returns>
        public string CreatetFuJianName(string TableName, string filepath, string WenJianName, Stream b, string DeleteFileURL)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {
                    bool flag = true;
                    MyWeb.AllowUnsafeUpdates = true;
                    SPDocumentLibrary DocLibrary = (SPDocumentLibrary)MyWeb.GetList(SPContext.Current.Web.Url + filepath);
                    SPFolderCollection folders = DocLibrary.RootFolder.SubFolders;
                    foreach (SPFolder subFolder in folders)//获取文件夹中的文件夹
                    {

                        if (subFolder.Name == TableName)
                        {
                            flag = false;
                            if (!string.IsNullOrEmpty(DeleteFileURL))
                            {
                                //如果之前有图片,将之前的图片删除
                                subFolder.Files.Delete(DeleteFileURL);
                            }

                            //将最新的图片放到文档库中
                            subFolder.Files.Add(WenJianName, b);
                            return GetChildWebUrl() + "/" + subFolder.Url + "/" + WenJianName;
                        }
                    }
                    if (flag == true)
                    {
                        SPFolder subFolder = folders.Add(TableName);
                        //将最新的图片放到文档库中
                        subFolder.Files.Add(WenJianName, b);
                        return GetChildWebUrl() + "/" + subFolder.Url + "/" + WenJianName;
                    }

                }



            }
            return "";

        }
        /// <summary>
        /// 创建指定文档库中指定文件夹
        /// </summary>
        /// <param name="TableName">文件夹名称</param>
        /// <param name="filepath">文档库路径</param>
        public void CreatetTableName(string TableName, string filepath)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {
                    bool flag = false;
                    MyWeb.AllowUnsafeUpdates = true;
                    SPDocumentLibrary DocLibrary = (SPDocumentLibrary)MyWeb.GetList(SPContext.Current.Web.Url + filepath);
                    SPFolderCollection folders = DocLibrary.RootFolder.SubFolders;
                    foreach (SPFolder subFolder in folders)//获取文件夹中的文件夹
                    {
                        if (subFolder.Name == TableName)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        folders.Add(TableName);
                    }
                }



            }

        }
        /// <summary>
        /// 删除指定地址附件
        /// </summary>
        /// <param name="DeleteFileURL">附件URL</param>

        public static void DeleteFuJian(string DeleteFileURL)
        {
            if (!string.IsNullOrEmpty(DeleteFileURL))
            {
                using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
                {

                    using (SPWeb MyWeb = MySite.OpenWeb())
                    {
                        try
                        {
                            MyWeb.AllowUnsafeUpdates = true;
                            MyWeb.GetFile(DeleteFileURL).Delete();
                        }
                        catch (Exception e)
                        {

                        }

                    }



                }
            }
        }
        /// <summary>
        /// 删除指定地址文件夹
        /// </summary>
        /// <param name="DeleteFileURL">文件夹名称</param>
        public void DeleteFileName(string DeleteFileURL)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {

                    try
                    {
                        MyWeb.AllowUnsafeUpdates = true;
                        MyWeb.GetFolder(DeleteFileURL).Delete();
                    }
                    catch (Exception e)
                    {

                    }
                }



            }
        }
        /// <summary>
        /// 移动文件夹到另外一个文件夹
        /// </summary>
        /// <param name="MoveFileUrl">文档路径</param>
        /// <param name="NewFileUrl">移动后的文件夹路径</param>
        public static void MoveFile(string MoveFileUrl, string NewFileUrl)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {


                    MyWeb.AllowUnsafeUpdates = true;
                    MyWeb.GetFolder(MoveFileUrl).MoveTo(NewFileUrl);

                }
            }
        }
        /// <summary>
        /// 移动附件到另外一个文件夹
        /// </summary>
        /// <param name="MoveFileUrl">文档路径</param>
        /// <param name="NewFileUrl">移动后的文件夹路径</param>
        public static void MoveFuJian(string MoveFileUrl, string NewFileUrl)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {


                    MyWeb.AllowUnsafeUpdates = true;
                    MyWeb.GetFile(MoveFileUrl).MoveTo(NewFileUrl, true);

                }
            }
        }
        /// <summary>
        /// 创建指定文档库路径文件夹
        /// </summary>
        /// <param name="DeleteFileURL">文档库路径</param>
        /// <param name="FileName">文件夹名称</param>
        public bool CreateFileName(string DeleteFileURL, string FileName)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {

                    try
                    {
                        MyWeb.GetFolder(DeleteFileURL).SubFolders.Add(FileName);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 创建指定文档库路径文件夹
        /// </summary>
        /// <param name="createPath">文档库路径</param>
        /// <param name="folderName">文件夹名称</param>
        public static bool CreateFolderByPath(string createPath, string folderName)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {

                    try
                    {
                        MyWeb.AllowUnsafeUpdates = true;
                        MyWeb.GetFolder(createPath).SubFolders.Add(folderName);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }




            }
        }
        /// <summary>
        /// 上传指定路径附件到文档库
        /// </summary>
        /// <param name="createFilePath">文档库路径</param>
        /// <param name="fileName">附件名称</param>
        /// <param name="fileStream">附件文件流</param>
        /// <returns></returns>
        public static bool CreateFileByPath(string createFilePath, string fileName, Stream fileStream)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {
                    try
                    {
                        MyWeb.AllowUnsafeUpdates = true;
                        MyWeb.GetFolder(createFilePath).Files.Add(fileName, fileStream);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
        }
        public static string CreateFileByPath(string createFilePath, string fileName, byte[] bytes)
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb MyWeb = MySite.OpenWeb())
                {
                    try
                    {
                        MyWeb.AllowUnsafeUpdates = true;
                        SPFolder folder = MyWeb.GetFolder(createFilePath);
                        folder.Files.Add(fileName, bytes);
                        return GetChildWebUrl() + "/" + folder.Url + "/" + fileName;
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }
        }
        public static string GetSiteUrl()
        {
            using (SPSite MySite = new SPSite(SPContext.Current.Web.Url))
            {
                return MySite.Url;
            }
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        public static string CatchImg(string vFileName, string ffmpegPath, string catchImgSize, int catchTime)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:<add key="ffmpeg" value="E:\ffmpeg\ffmpeg.exe" />

            if (!System.IO.File.Exists(ffmpegPath))
            {
                return "";
            }

            //获得图片相对路径/最后存储到数据库的路径,如:/Web/FlvFile/User1/00001.jpg
            string flv_img = System.IO.Path.ChangeExtension(vFileName, ".jpg");

            //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="240x180" />

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpegPath);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过
            startInfo.Arguments = " -i " + vFileName + " -ss " + catchTime + " -y -f image2 -t 0.001 -s " + catchImgSize + " " + flv_img;

            try
            {
                System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                return "";
            }

            ///注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长;
            ///这儿需要延时后再检测,我服务器延时8秒,即如果超过8秒图片仍不存在,认为截图失败;
            ///此处略去延时代码.如有那位知道如何捕捉ffmpeg.exe截图失败消息,请告知,先谢过!
            System.Threading.Thread.Sleep(2500);
            if (System.IO.File.Exists(flv_img))
            {
                return flv_img;
            }

            return "";
        }
        /// <summary>
        /// 获取当前登录用户名(不带域名)
        /// </summary>
        /// <returns></returns>
        public static string GetSPUser()
        {
            return SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1].ToString();
        }

        /// <summary>
        /// 获取当前登录人姓名
        /// </summary>
        /// <returns></returns>
        public static string GetSPADUserName()
        {
            return SPContext.Current.Web.CurrentUser.Name.ToString();
        }
        /// <summary>
        /// 获取当前登录人用户表对象
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetSPADUserID()
        {
            return new UserInfoManager().GetCode(GetSPUser());
        }

        /// <summary>
        /// 获取Img的路径
        /// </summary>
        /// <param name="htmlText">Html字符串文本</param>
        /// <returns>以数组形式返回图片路径</returns>
        public static string[] GetHtmlImageUrlList(string htmlText)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            //新建一个matches的MatchCollection对象 保存 匹配对象个数(img标签)
            MatchCollection matches = regImg.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            //遍历所有的img标签对象
            foreach (Match match in matches)
            {
                if (match.Groups["imgUrl"].Value.IndexOf(ConnectionManager.ImgKuUrl) != -1)
                {
                    //获取路径图片文档库中的图片,并保存到数组中
                    sUrlList[i++] = match.Groups["imgUrl"].Value;
                }
            }
            return sUrlList;
        }

        public static string GetErrorStr(Exception ex)
        {
            string stackTrace = ex.StackTrace;
            string errMsg;
            if (ex == null)
            {
                errMsg = "发生了未知异常!";
            }
            if (ex.InnerException != null)
                errMsg = ex.InnerException.Message;
            else
                errMsg = ex.Message;
            return CommonUtil.Serialize(new { Success = false, Business = true, Msg = errMsg, StackTrace = stackTrace });
        }

        public static void UndefinedCMDException(string CMD)
        {
            throw new Exception(string.Format("未定义的操作命令:{0}", CMD));
        }

        public static bool IsTeacher(UserInfo ui)
        {
            if (ui == null || !PublicEnum.PositionTeacher.Equals(ui.Role))
                return false;
            return true;
        }
        public static bool IsClassTutor(UserInfo ui)
        {
            if (ui == null || !PublicEnum.PositionClassTutor.Equals(ui.Role))
                return false;
            return true;
        }
        public static string IsClassTutor()
        {
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (!PublicEnum.PositionClassTutor.Equals(ui.Role))
                return "false";
            return "true";
        }
        public static bool IsTeacher()
        {
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (!PublicEnum.PositionTeacher.Equals(ui.Role))
                return false;
            return true;
        }
        public static bool IsStudent()
        {
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (!PublicEnum.PositionStudent.Equals(ui.Role))
                return false;
            return true;
        }
        public static bool IsStudent(UserInfo ui)
        {
            if (ui == null || !PublicEnum.PositionStudent.Equals(ui.Role))
                return false;
            return true;
        }
        public static bool IsAdministrator(UserInfo ui)
        {
            if (ui == null || !PublicEnum.Positionadministrator.Equals(ui.Role))
                return false;
            return true;
        }
        public static bool IsAdministrator()
        {
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (!PublicEnum.Positionadministrator.Equals(ui.Role))
                return false;
            return true;
        }

        /// <summary>
        /// 学习中心数据库中是否存在指定用户
        /// </summary>
        /// <returns></returns>
        public static bool ExitStudentSystemPower(string code)
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", code);
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemStudentConStr));
            if (num > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 学习中心数据库中是否存在当前登陆人
        /// </summary>
        /// <returns></returns>
        public static bool ExitStudentSystemPower()
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", GetSPUser());
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemStudentConStr));
            if (num > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 继续教育数据库中是否存在指定用户
        /// </summary>
        /// <returns></returns>
        public static bool ExitTeacherSystemPower(string code)
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", code);
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemTeacherConStr));
            if (num > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 继续教育数据库中是否存在当前登陆人
        /// </summary>
        /// <returns></returns>
        public static bool ExitTeacherSystemPower()
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", GetSPUser());
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemTeacherConStr));
            if (num > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 党员学习数据库中是否存在当前登陆人
        /// </summary>
        /// <returns></returns>
        public static bool ExitPartyMemberSystemPower(string code)
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", code);
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemPartyMemberConStr));
            if (num > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 党员学习数据库中是否存在当前登陆人
        /// </summary>
        /// <returns></returns>
        public static bool ExitPartyMemberSystemPower()
        {
            string sql = string.Format("SELECT COUNT(*) FROM UserInfo WHERE IsDelete=0 AND Code='{0}'", GetSPUser());
            int num = Convert.ToInt32(ConnectionManager.GetSingle(sql, ConnectionManager.SystemPartyMemberConStr));
            if (num > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 获取子站点URL,如 /SystemTeacher
        /// </summary>
        /// <returns></returns>
        public static string GetChildWebUrl()
        {
            string childWebUrl = SPContext.Current.Web.ServerRelativeUrl;
            if (childWebUrl.Equals("/") && HttpContext.Current.Request.RawUrl.Length > 1 && HttpContext.Current.Request.RawUrl.IndexOf('/', 1) != -1)
            {
                return HttpContext.Current.Request.RawUrl.Substring(0, HttpContext.Current.Request.RawUrl.IndexOf('/', 1));
            }
            return childWebUrl;
        }
        /// <summary>
        /// 获取子站点名称,如 继续教育
        /// </summary>
        /// <returns></returns>
        public static string GetChildWebName()
        {
            return SPContext.Current.Web.Title;
        }
        public static string SynchronousADUser(UserInfo user)
        {
            LogCom com = new LogCom();
            try
            {
                ADUtils _utils = new ADUtils(ConnectionManager.ADPath, ConnectionManager.ADAdminUser, ConnectionManager.ADAdminPassword);
                if (!_utils.CheckUserLogin(ConnectionManager.ADAdminUser, ConnectionManager.ADAdminPassword))
                {
                    return "False";
                }
                UserNode ad = new UserNode();
                ad.Type = NodeType.User;
                //显示名
                ad.Properties.Add(new ADNodeProperties(EProperties.DisplayName, user.Name));
                //移动电话
                ad.Properties.Add(new ADNodeProperties(EProperties.MobilePhone, user.Mobile));
                //办公电话
                ad.Properties.Add(new ADNodeProperties(EProperties.OfficePhone, user.Telephone));
                //邮箱改为员工序号+域名
                ad.Properties.Add(new ADNodeProperties(EProperties.Mail, user.Email));
                ad.LogonName = user.Code;
                ad.ParentOUPath = ConnectionManager.ADOUUrl;

                //此行代码报错
                DirectoryEntry domain = _utils.GetADUserOfLogonName2(ad.LogonName);
                if (domain == null) //如果当前对象未存在,则进行增加操作
                {
                    ad.AccountNeverExpires = true;
                    ad.AccountState = AccountState.Enable;
                    ad.Password = ConnectionManager.ADInitialpassword;
                    string jieguo = "";
                    if (new ValidUser().impersonateValidUser(ConnectionManager.ADAdminUser, ConnectionManager.ADName, ConnectionManager.ADAdminPassword))
                    {
                        jieguo = _utils.AddADStr(ad);
                        new ValidUser().undoImpersonation();

                    }
                    return jieguo;
                }
                else
                {
                    return "False";
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "UserInfoAdd_BTSave_SynchronousADUser");
            }
            return "False";
        }
    }
}
