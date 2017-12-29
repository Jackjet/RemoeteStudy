using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class SystemConfigMangerAdd : System.Web.UI.Page
    {
        public string id = "";
        SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

            id = string.IsNullOrWhiteSpace(Request.QueryString["id"]) ? "" : Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (id != "")
                {
                    //读数据库，给控件赋值
                    SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
                    DataTable list = SCBLL.SelectById(id);
                    txtSysName.Text = list.Rows[0]["Name"].ToString();
                    txtMangerName.Text = list.Rows[0]["Manager"].ToString();
                }
                else
                {
                    txtSysName.Text = "";
                    txtMangerName.Text = "";
                }
            }
        }
        /// <summary>
        /// 保存/修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (id != "")//修改
            {
          
                if (string.IsNullOrWhiteSpace(txtSysName.Text.Trim()))
                {
                    alert("系统名称不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txtMangerName.Text.Trim()))
                {
                    alert("管理员不能为空！");
                }
                else if (!SCBLL.CheckISExistByManager(txtMangerName.Text.Trim(), id))
                {
                    SystemConfiguration SC = new SystemConfiguration();
                    SC.Name = txtSysName.Text.Trim();
                    SC.Manager = txtMangerName.Text.Trim();
                    SC.ID = Convert.ToInt32(id);
                    bool istrue = SCBLL.Update(SC);
                    if (istrue)
                    {
                        //记入操作日志
                        Base_LogBLL.WriteLog(LogConstants.xtzhgl, ActionConstants.xg);
                        alert("修改成功！");

                        // Response.Redirect("SystemConfigManager.aspx", true);
                    }
                }
                else
                    alert("用户[" + txtMangerName.Text + "]已存在！");
            }
            else //添加
            {
           
                if (string.IsNullOrWhiteSpace(txtSysName.Text.Trim()))
                {
                    alert("系统名称不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txtMangerName.Text.Trim()))
                {
                    alert("管理员不能为空！");
                }
                else if (!SCBLL.CheckISExistByManager(txtMangerName.Text.Trim()))
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.xtzhgl, ActionConstants.add);
                    SystemConfiguration SC = new SystemConfiguration();
                    SC.Name = txtSysName.Text.Trim();
                    SC.Manager = txtMangerName.Text.Trim();
                    bool istrue = SCBLL.Insert(SC);
                    if (istrue)
                        alert("添加成功！");
                }
                else
                    alert("用户[" + txtMangerName.Text + "]已存在！");
            }
        }
        protected void alert(string strMessage)
        {
            //parent.wbox.close();
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>alert('" + strMessage + "');window.parent.location.reload(); </script>"); 
        }
    }
}