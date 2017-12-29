using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Model;

namespace UserCenterSystem
{
    public partial class InterfaceConfigManagerAdd : System.Web.UI.Page
    {
        public string id = "";
        InterfaceConfigurationBLL SCBLL = new InterfaceConfigurationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            id = string.IsNullOrWhiteSpace(Request.QueryString["id"]) ? "" : Request.QueryString["id"];
            if (!IsPostBack)
            {
                init();
                if (id != "")
                { 
                    //读数据库，给控件赋值 
                    DataTable list = SCBLL.GetTableNameByIDToTable(id); 
                    //给下拉框赋值并选中
                    txtSysteName.Items[txtSysteName.SelectedIndex].Selected = false;
                    txtSysteName.Items.FindByValue(list.Rows[0]["SystemID"].ToString()).Selected = true;
                    //给下拉框赋值并选中 ;
                    txtInterfaceName.Items[txtInterfaceName.SelectedIndex].Selected = false;
                    txtInterfaceName.Items.FindByValue(list.Rows[0]["InterfaceID"].ToString()).Selected = true;

                    txtretItem.Text = list.Rows[0]["DataItems"].ToString();
                    txttableData.Text = list.Rows[0]["TableName"].ToString();
                }
                else
                { 
                    txtSysteName.SelectedIndex = 0;
                    txtInterfaceName.SelectedIndex = 0;
                    txtretItem.Text = "";
                    txttableData.Text = "";
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
               
                if (txtSysteName.SelectedValue == "0")
                {
                    alert("请选择系统名称！");
                }
                else if (txtInterfaceName.SelectedValue == "0")
                {
                    alert("请选择接口名称！");
                }
                else if (string.IsNullOrWhiteSpace(txtretItem.Text.Trim()))
                {
                    alert("返回数据项不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txttableData.Text.Trim()))
                {
                    alert("数据表不能为空！");
                }
                else if (!SCBLL.CheckISExistbyinfo(txtInterfaceName.Text.Trim(), txtretItem.Text.Trim(), txttableData.Text.Trim(), id))
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jkqxgl, ActionConstants.xg);
                    InterfaceConfiguration  SC = new InterfaceConfiguration();
                    SC.InterfaceID = Convert.ToInt32(txtInterfaceName.SelectedValue);
                    SC.DataItems = txtretItem.Text.Trim() ;
                    SC.SystemID = Convert.ToInt32(txtSysteName.SelectedValue);
                    SC.TableName = txttableData.Text.Trim();
                    SC.ID = Convert.ToInt32(id);
                    bool istrue = SCBLL.Update(SC);
                    if (istrue)
                    {
                        alert("修改成功！");
                    }
                }
                else
                    alert("数据已存在！");
            }
            else //添加
            {
            
                if (txtSysteName.SelectedValue=="0")
                {
                    alert("请选择系统名称！");
                }
                else if (txtInterfaceName.SelectedValue == "0")
                {
                    alert("请选择接口名称！");
                }
                else if (string.IsNullOrWhiteSpace(txtretItem.Text.Trim()))
                {
                    alert("返回数据项不能为空！");
                }
                else if (string.IsNullOrWhiteSpace(txttableData.Text.Trim()))
                {
                    alert("数据表不能为空！");
                }
                else if (!SCBLL.CheckISExistbyinfo(txtInterfaceName.Text.Trim(), txtretItem.Text.Trim(), txttableData.Text.Trim()))
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jkqxgl, ActionConstants.add);
                    InterfaceConfiguration SC = new InterfaceConfiguration();
                    SC.InterfaceID = Convert.ToInt32(txtInterfaceName.SelectedValue);
                    SC.DataItems = txtretItem.Text.Trim();
                    SC.SystemID = Convert.ToInt32(txtSysteName.SelectedValue);
                    SC.TableName = txttableData.Text.Trim();
                    bool istrue = SCBLL.Insert(SC);
                    if (istrue)
                        alert("添加成功！");
                }
                else
                    alert("数据已存在！");
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
            txtSysteName.DataSource = SCBLL.SelectAll();
            txtSysteName.DataTextField = "Name";
            txtSysteName.DataValueField = "ID";
            txtSysteName.DataBind();
            ListItem li = new ListItem("--请选择--", "0");
            txtSysteName.Items.Insert(0, li);
            txtSysteName.Items.FindByValue("0").Selected = true;


            InterfaceInformationBLL IIBLL = new InterfaceInformationBLL();
            txtInterfaceName.DataSource = IIBLL.SelectAll();
            txtInterfaceName.DataTextField = "Name";
            txtInterfaceName.DataValueField = "ID";
            txtInterfaceName.DataBind();
            ListItem li1 = new ListItem("--请选择--", "0");
            txtInterfaceName.Items.Insert(0, li1);
            txtInterfaceName.Items.FindByValue("0").Selected = true;
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>alert('" + strMessage + "');window.parent.location.reload(); </script>");
        }
    }
}