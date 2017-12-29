using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SVDigitalCampus.Common
{
   public  class UpdateImages
    {

        #region 长传图片文件
       public static string UploadImage(HttpContext context, string fileNamePath)
        {
            try
            {
                if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Images")) == false)//如果不存在就创建文件夹
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images"));
                }
                if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/" + DateTime.Today.Year + DateTime.Today.Month)) == false)//如果不存在就创建文件夹
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + DateTime.Today.Year + DateTime.Today.Month));
                }
                string serverPath = HttpContext.Current.Server.MapPath("~/Images/" + DateTime.Today.Year + DateTime.Today.Month + "/");

                string toFilePath = serverPath;

                //获取要保存的文件信息
                FileInfo file = new FileInfo(fileNamePath);
                //获得文件扩展名
                string fileNameImg = file.Extension;

                //验证合法的文件
                if (CheckFileImg(fileNameImg))
                {
                    //生成将要保存的随机文件名
                    string fileName = GetFileName() + fileNameImg;

                    //获得要保存的文件路径
                    string serverFileName = toFilePath + fileName;
                    //物理完整路径                    
                    string toFileFullPath = serverFileName; //HttpContext.Current.Server.MapPath(toFilePath);

                    //将要保存的完整文件名                
                    string toFile = toFileFullPath;//+ fileName;

                    ///创建WebClient实例       
                    WebClient myWebClient = new WebClient();
                    //设定windows网络安全认证   方法1
                    myWebClient.Credentials = CredentialCache.DefaultCredentials;
                    ////设定windows网络安全认证   方法2
                    
                     context.Request.Files[0].SaveAs(toFile);

                     return "/Images/" + DateTime.Today.Year + DateTime.Today.Month + "/" + fileName;
                }
                else
                {
                    throw new Exception("文件格式非法，请上传图片格式的文件。");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region 验证获取文件
        /// <summary>
        /// 检查是否为合法的上传图片
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
       private static bool CheckFileImg(string _FileImg)
        {
            string[] allowImg = new string[] { ".jpg", ".jpeg", "gif", ".png" };
            for (int i = 0; i < allowImg.Length; i++)
            {
                if (allowImg[i] == _FileImg) { return true; }
            }
            return false;

        }

        #endregion
       private static string GetFileName()
        {
            Random rd = new Random();
            StringBuilder serial = new StringBuilder();
            serial.Append(DateTime.Now.ToString("yyyyMMddHHmmssff"));
            serial.Append(rd.Next(0, 999999).ToString());
            return serial.ToString();

        }

    }
}
