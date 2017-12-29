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
    public partial class InterfaceManagerAdd : System.Web.UI.Page
    {
        public string id = "";
        InterfaceInformationBLL SCBLL = new InterfaceInformationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = string.IsNullOrWhiteSpace(Request.QueryString["id"]) ? "" : Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (id != "")
                {
                    //读数据库，给控件赋值 
                    InterfaceInformation list = SCBLL.SelectINFO(Convert.ToInt32(id));
                    txtInterfaceName.Text = list.Name;
                    txtInterfaceDescribe.Text = list.Information;
                    //ddlservicepage.SelectedValue = list.ServiceName;
                }
                else
                {
                    txtInterfaceName.Text = "";
                    txtInterfaceDescribe.Text = "";
                }
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>alert('" + strMessage + "');window.parent.location.reload(); </script>");
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
           
                if (string.IsNullOrWhiteSpace(txtInterfaceName.Text.Trim()))
                {
                    alert("接口名称不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txtInterfaceDescribe.Text.Trim()))
                {
                    alert("接口描述不能为空！");
                }
                else if (!SCBLL.CheckISExistbyinfo(txtInterfaceName.Text.Trim(), id))
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jkxxgl, ActionConstants.xg);
                    InterfaceInformation SC = new InterfaceInformation();
                    SC.Name = txtInterfaceName.Text.Trim();
                    SC.Information = txtInterfaceDescribe.Text.Trim();
                    SC.ServiceName = "UserInfo.asmx";
                    SC.ID = Convert.ToInt32(id);
                    bool istrue = SCBLL.Update(SC);
                    if (istrue)
                    {
                        alert("修改成功！");
                    }
                }
                else
                    alert("接口[" + txtInterfaceName.Text + "]已存在！");
            }
            else //添加
            {
            
                if (string.IsNullOrWhiteSpace(txtInterfaceName.Text.Trim()))
                {
                    alert("接口名称不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txtInterfaceDescribe.Text.Trim()))
                {
                    alert("接口描述不能为空！");
                }
                else if (!SCBLL.CheckISExistbyinfo(txtInterfaceName.Text.Trim()))
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jkxxgl, ActionConstants.add);
                    InterfaceInformation SC = new InterfaceInformation();
                    SC.Name = txtInterfaceName.Text.Trim();
                    SC.Information = txtInterfaceDescribe.Text.Trim();
                    SC.ServiceName = "UserInfo.asmx";
                    bool istrue = SCBLL.Insert(SC);
                    if (istrue)
                        alert("添加成功！");
                }
                else
                    alert("接口[" + txtInterfaceName.Text + "]已存在！");
            }
        }
    }
}