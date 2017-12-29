using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Web;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumInfoList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        public void BindList()
        {
            CurriculumInfo cus = new CurriculumInfo();
            CurriculumInfoManager cusManager = new CurriculumInfoManager();
            if (!string.IsNullOrEmpty(this.HidResourceNameID.Value))
            {
                //cus.ResourceID = HidResourceNameID.Value;
            }
            if (!string.IsNullOrEmpty(CurriculumName.Text))
            {
                //cus.Title = CurriculumName.Text;
            }
            cus.CreaterUserID = CommonUtil.GetSPADUserID().Id;
            List<CurriculumInfo> list = cusManager.FindCurriculumSeache(cus);
            List<CurriculumInfo> list1 = new List<CurriculumInfo>();
            if (!string.IsNullOrEmpty(CurriculumName.Text) && !string.IsNullOrEmpty(this.HidResourceNameID.Value)) 
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Title == CurriculumName.Text && list[i].ResourceID == this.HidResourceNameID.Value)
                    {
                        list1.Add(list[i]);
                    }
                }  
            }
            else if (!string.IsNullOrEmpty(CurriculumName.Text))
            {
                for (int i = 0; i < list.Count;i++ )
                {
                    if (list[i].Title == CurriculumName.Text) 
                    {
                        list1.Add(list[i]);
                    }
                }   
         
            }
            else if (!string.IsNullOrEmpty(this.HidResourceNameID.Value))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ResourceID == this.HidResourceNameID.Value)
                    {
                        list1.Add(list[i]);
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.HidResourceNameID.Value) || !string.IsNullOrEmpty(CurriculumName.Text))
            {
                list = list1;
            }
            ViewState["DataSou"] = list;
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
            this.HidResourceNameID.Value = "";
            CurriculumName.Text = "";
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }

        protected void Btn_ExportClass_Click(object sender, EventArgs e)
        {
            StringBuilder sbtext = new StringBuilder();
            if (ViewState["DataSou"]!=null)
            {
                List<CurriculumInfo> list=ViewState["DataSou"] as List<CurriculumInfo>;
                foreach (CurriculumInfo item in list)
                {
                    sbtext.Append("<tr>");
                    sbtext.Append("<td>" + item.Title + "</td>");
                    sbtext.Append("<td>" + item.ResourceName + "</td>");
                    sbtext.Append("<td>" + item.IsOpenCourses + "</td>");
                    sbtext.Append("</tr>");
                }
            }
            string result = staticstr.Replace("@ReplaceContent", sbtext.ToString());
            ExportToHtml(result);
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('导出成功');", true);
            //OfficeExportManage(this.Page,)
        }

        private void ExportToHtml(string result)
        {
            try
            {
                //FileStream fs = new FileStream(CreateFolderFirst(), FileMode.OpenOrCreate, FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("UTF-8"));//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                //sw.WriteLine(result);
                //sw.Close();
                //using (FileStream fsWrite = File.OpenWrite(@"C:\\静态课程\导出静态课程.html"))
                //{
                //    //1.把字符串转换为byte[]
                //    byte[] buffer = Encoding.UTF8.GetBytes(result);

                //    //写入数据
                //    fsWrite.Write(buffer, 0, buffer.Length);
                //}
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Response.ContentType = "application/ms-text";
                Response.AppendHeader("Content-Disposition", "attachment;filename="
                                      + HttpUtility.UrlEncode("StaticPage", System.Text.Encoding.UTF8).ToString() //该段需加，否则会出现中文乱码
                                      + ".html");
                Response.Write(result);
                Response.End();  



                //Response.AddHeader("Content-Disposition", "attachment; filename=导出静态课程.html");
                //Response.ContentType = "text/plain";
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //Response.Write(result.ToString());
                //Response.End();

                   
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public string CreateFolderFirst()
        {
            
            string folderPath = "C:\\静态课程";

            string filePath = @"C:\\静态课程\导出静态课程.html";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                //给文件夹Everyone赋完全控制权限
                DirectorySecurity folderSec = new DirectorySecurity();
                folderSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                System.IO.Directory.SetAccessControl(folderPath, folderSec);
                CreateFile(filePath);

            }
            else
            {
                CreateFile(filePath);
            }
            return filePath;
        }

        public void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    //给Xml文件EveryOne赋完全控制权限
                    DirectorySecurity fSec = new DirectorySecurity();
                    fSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    System.IO.Directory.SetAccessControl(filePath, fSec);
                }

            }
        }
        
        


        public string staticstr = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><title>Document</title><style type=\"text/css\">*{ margin:0; padding:0;}td{height:35px;}table{border-collapse:collapse;border-spacing:0;}ol,ul,li,dl,dd,dt {	list-style: none; text-decoration:none; }address,caption,cite,code,dfn,em,strong,th,var{font-weight:normal;font-style:normal;}caption,th{text-align:left;}em,i,small,caption,th{font-weight:normal;font-style:normal;font-size:100%;}h1,h2,h3,h4,h5,h6,em,strong,b{font-weight:normal;font-size:100%;}q:before,q:after{content:'';}body,html{height:100%;}.clear { display:block !important; clear:both !important; float:none !important; margin:0 !important; padding:0 !important; height:0; line-height:0; font-size:0; overflow:hidden;}.clearfix{*zoom:1;}.clearfix:after{content:\".\"; display:block;clear:both;visibility:hidden;line-height:0;height:0;}.spacer{clear:both; font-size:0; height:0; line-height:0;}tr.trth { background:#48b700; color:#fff;}tr.Single { background:#fafafa;}tr.Double {background:#ebf5ff; }.Display_form table.D_form { width:100%; text-align:center; font-size:14px; border:1px solid #48b700;}.Display_form table.D_form th { border:1px solid #fff; border-bottom:none; color:#fff; font-size:14px; line-height:34px; }.Display_form table.D_form tr { border:1px solid #48b700; color:#666; line-height:28px; }.Display_form table.D_form td { border:1px solid #48b700;color:#666; line-height:28px;}.Display_form table.D_form th::first-line {border-left: 1px solid #48b700 !important;}.Display_form table.D_form th::after {border-right: 1px solid #48b700 !important;}.sousuo{height: 53px;line-height: 53px;}.sousuo span{margin: 0 0 0 15px;display: inline-block;}.sousuo span select,.sousuo span input{width: 110px;height: 29px;line-height: 29px;padding: 0 0 0 18px;border-radius: 3px;border: solid 1px #48b700;}.sousuo span input{width: 142px;padding: 0 0 0 0;padding: 0 7px 0 10px;}.sousuo span input:focus{outline: none;border: solid 1px #ff0000;}.sousuo span.ss input{width: 78px;text-align: center;background: #48b700;color: #fff;}.tj_form{width: 100%;background: #fff;border: solid 1px #48b700;}.tj_form th{line-height: 34px;padding: 0 5px;text-align: center;background: #48b700;border-left: solid 1px #fff;color: #fff;}.tj_form tr{border-bottom: solid 1px #48b700;.tj_form td{padding: 0 5px;line-height: 34px;text-align: center;border-left: solid 1px #48b700;color: #666;}.tj_form th:first-child{border-left: none;}</style></head><body><div style=\"width:90%;margin:auto;\"><dl class=\"tj_table\"><dt class=\"name\">课程导出:</dt><dd><table class=\"tj_form\"><tr><th>课程名称</th><th>课程类型</th><th>是否公开</th></tr>@ReplaceContent</table></dd></dl></div></div></body></html>";
    }
}
