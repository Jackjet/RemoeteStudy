首先，找到109行的代码
1
 var swf = './expressInstall.swf';
修改为您的地址
1
2
//修改您的flash地址
var swf = './Scripts/webuploader-0.1.5/examples/image-upload/expressInstall.swf';
其次，找到151行，在实例化的时候修改用于上传flash的地址：
1
  swf: '../../dist/Uploader.swf',
修改为
1
swf: './Scripts/webuploader-0.1.5/dist/Uploader.swf',
第三，找到154行，将图片上传地址修改为.net的一般处理程序的请求地址
1
server: '../../server/fileupload.php',
修改为您的一般处理程序地址
1
server: './server/fileupload.ashx',
第四，找到260行修改预览的服务器代码地址(我没有写不影响文件上传)
1
$.ajax('../../server/preview.php', {
修改为您的一般处理程序的预览地址
1
 $.ajax('./server/preview.ashx', {
好了，到这里我们将upload.js修改完成了。
下面就是写了处理程序了，在项目中创建一个server文件夹并添加以下两个文件fileupload.ashx和preview.ashx。
下面介绍fileupload.ashx的实现方法

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
 
namespace Blog.server
{
    /// <summary>
    /// Summary description for fileupload
    /// </summary>
    public class fileupload : IHttpHandler
    {
 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //指定字符集
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            SaveFile();
        }
        /// <summary>
        /// 文件保存操作
        /// </summary>
        /// <param name="basePath"></param>
        private void SaveFile(string basePath = "~/Upload/Images/")
        {
            var name = "<a href='https://www.baidu.com/s?wd=string.Empty&tn=44039180_cpr&fenlei=mv6quAkxTZn0IZRqIHckPjm4nH00T1dBPjN-nhcvPvcYuAfdnhmY0ZwV5Hcvrjm3rH6sPfKWUMw85HfYnjn4nH6sgvPsT6K1TL0qnfK1TL0z5HD0IgF_5y9YIZ0lQzqlpA-bmyt8mh7GuZR8mvqVQL7dugPYpyq8Q1cznjn4nWRzrHRLPWTvnWR4nj6' target='_blank' class='baidu-highlight'>string.Empty</a>";
            basePath = (basePath.IndexOf("~") > -1) ? System.Web.HttpContext.Current.Server.MapPath(basePath) :
            basePath;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //如果目录不存在，则创建目录
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
 
            var suffix = files[0].ContentType.Split('/');
            //获取文件格式
            var _suffix = suffix[1].Equals("jpeg",StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];
            //如果不修改文件名，则创建随机文件名
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            //文件保存
            var full = basePath + name;
            files[0].SaveAs(full);
            var _result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + name + "\"}";
            System.Web.HttpContext.Current.Response.Write(_result);
        }
 
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}