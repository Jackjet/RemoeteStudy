using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class DepartmentManageOrder : System.Web.UI.Page
    {
        public string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = string.IsNullOrWhiteSpace(Request.QueryString["id"]) ? "" : Request.QueryString["id"];
            if (!IsPostBack)
            {
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                DataTable isok = deptBll.GetDeptInfo(id);
                txtoredr.Text = isok.Rows[0]["OrderNum"].ToString();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtoredr.Text))
                {
                    alert("排序序号不能为空！"); return;
                }
                else
                {
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    bool isok = deptBll.updateSort(txtoredr.Text, id);
                    if (isok)
                        AlertAndRedirect("排序更新成功！！", "/DepartmentManage.aspx");
                    else
                        AlertAndRedirect("排序更新失败！！", "/DepartmentManage.aspx");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        public static void AlertAndRedirect(string message, string toURL)
        {
            try
            {
                string js = "<script language=javascript>alert('{0}');window.parent.location.replace('{1}')</script>";
                HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
            finally
            {
                HttpContext.Current.Response.End();
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>alert('" + strMessage + "');window.parent.location.reload(); </script>");
        }
    }
}