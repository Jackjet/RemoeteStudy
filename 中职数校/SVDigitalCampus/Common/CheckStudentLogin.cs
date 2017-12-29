using Common.SchoolUser;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace SVDigitalCampus.Common
{
    public class CheckStudentLogin : IRequiresSessionState
    {
        public static void CkStudentLogin(string url) { 
              DataTable student = new DataTable();
              if (HttpContext.Current.Session["Student"] != null)
              {
                  student = HttpContext.Current.Session["Student"] as DataTable;
              }
              else {
              //    student = null; ;
              //}
              //if (student==null||student.Rows.Count == 0)
              //{
                  //HttpContext.Current.Response.Redirect(url);
                  //HttpContext.Current.Response.End();
                  //获取学生信息并保存在session中
                  UserPhoto user = new UserPhoto();
                  SPWeb web = SPContext.Current.Web;
                  string username = web.CurrentUser.LoginName.Substring(web.CurrentUser.LoginName.IndexOf("\\") + 1, web.CurrentUser.LoginName.Length - web.CurrentUser.LoginName.IndexOf("\\") - 1);
                  DataTable studentmeg = user.GetStudentByAccount(username);
                  if (studentmeg != null && studentmeg.Rows.Count > 0)
                  {
                      if (HttpContext.Current.Session["Student"] != null)
                      {
                          HttpContext.Current.Session["Student"] = studentmeg;
                      }
                      HttpContext.Current.Session.Add("Student", studentmeg);
                  }
              }
            
        }
    }
}
