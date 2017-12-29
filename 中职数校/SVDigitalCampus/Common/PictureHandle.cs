using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace SVDigitalCampus.Common
{
    public static class PictureHandle
    {

        #region 长传图片到图片库里
        public static int UploadImage(HttpPostedFile myFile, string picid,  out string filePath,out bool result, out string msg)
        {
                SPWeb spweb = SPContext.Current.Web;
            try
            {
                SPList imageList = spweb.Lists.TryGetList("图片库");
                string photoName1 = myFile.FileName; //获取初始文件名
                int i = photoName1.LastIndexOf("."); //取得文件名中最后一个"."的索引
                string newext = photoName1.Substring(i); //获取文件扩展名
                if (newext.ToLower() != ".gif" && newext.ToLower() != ".jpg" && newext.ToLower() != ".jpeg" && newext.ToLower() != ".bmp" && newext.ToLower() != ".png")
                {
                   msg="文件格式不正确!";
                   result = false;
                   filePath = null;
                   return 0;
                }
                DateTime now = DateTime.Now; //获取系统时间
                string photoName2 = now.Millisecond.ToString() + "_" + myFile.ContentLength.ToString() + newext; //重新为文件命名,时间毫秒部分+文件大小+扩展名
                Stream imageStream = myFile.InputStream;

                spweb.AllowUnsafeUpdates = true;
                string dirPath = spweb.Url + "/Picture/";
               
                string picUrl = dirPath+ photoName2 ;
                if (picid != null && picid.Trim() != "")
                {//判断图片库是否存在该图片
                    SPListItem item = imageList.GetItemById(int.Parse(picid));
                    if (item != null)
                    {

                        item.Delete();

                    }
                    //if (!string.IsNullOrEmpty(filePath))
                    //{ 
                    //    SPQuery query=new SPQuery();
                    //    query.Query=@"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>111</Value></Eq></Where>";
                    //   SPListItemCollection oldimgs= imageList.GetItems(query);
                    //   foreach (SPListItem oitem in oldimgs)
                    //   {
                    //       oitem.Delete();
                    //   }
                    //}
                }
                SPFolder folder = imageList.RootFolder;
                filePath = picUrl;
                SPFile imageFile = folder.Files.Add(picUrl, imageStream);
                imageFile.Update();
                spweb.AllowUnsafeUpdates = false;
                result = true;
                msg = "新增成功！";
                return imageFile.Item.ID;

            }
            catch (Exception e)
            {
                result = false;
                msg = "新增失败!";
                filePath = null;
                spweb.AllowUnsafeUpdates = false;
                return 0;
            }
        }
        #endregion
    }
}
