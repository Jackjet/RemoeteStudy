using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Common;
using System.Data;

namespace UserCenterSystem
{
    public partial class InterfaceConfigManager : BaseInfo
    {
        /// <summary>
        /// 超级管理员账号
        /// </summary>
        public string AdminName
        {
            get { return ViewState["AdminName"] == null ? string.Empty : ViewState["AdminName"].ToString(); }
            set { ViewState["AdminName"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员

            if (!IsPostBack)
            {
                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;

                if (teacher != null)
                {

                    if (teacher.YHZH.Trim() == AdminName)
                    {
                        Bind();

                    }
                    else
                    {
                        Response.Redirect("/UserMenuManage.aspx");// 转向用户页面

                    }
                }
                else
                {
                    Response.Redirect("/UserMenuManage.aspx");// 转向用户页面
                }


            }
        }
        protected void lvSystem_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            Bind();
        }
        public void Bind()
        {
            InterfaceConfigurationBLL IIBLL = new InterfaceConfigurationBLL();
            DataSet DS = IIBLL.SelectAllInfo();
            lvSystem.DataSource = DS;
            lvSystem.DataBind();
        }
        protected void lvSystem_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            string SystemID = "";
            string InterfaceID = "";
            string TableName = "";
            try
            {
                TextBox txtDataItems = e.Item.FindControl("txtDataItems") as TextBox;
                TextBox txtTableName = e.Item.FindControl("txtTableName") as TextBox;
                DropDownList ddlSystem = e.Item.FindControl("ddlSystem") as DropDownList;
                DropDownList ddlInformation = e.Item.FindControl("ddlInformation") as DropDownList;
                if (txtTableName != null && txtDataItems != null && ddlSystem != null && ddlInformation != null)
                {
                    SystemID = ddlSystem.SelectedValue;
                    InterfaceID = ddlInformation.SelectedValue;
                    TableName = txtTableName.Text.Trim();
                    InterfaceConfigurationBLL ICBLL = new InterfaceConfigurationBLL();
                    if (ICBLL.CheckISExist(InterfaceID, SystemID, TableName))
                    {
                        //系统信息已经存在
                        e.Cancel = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                        return;
                    }
                    InterfaceConfiguration IIF = new InterfaceConfiguration();
                    IIF.DataItems = txtDataItems.Text.Trim();
                    IIF.TableName = txtTableName.Text.Trim();
                    IIF.SystemID = Convert.ToInt16(SystemID);
                    IIF.InterfaceID = Convert.ToInt16(InterfaceID);

                    ICBLL.Insert(IIF);
                    Bind();
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('数据插入失败【" + ex.Message + "】')", true);
            }
        }
        protected void lvSystem_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvSystem.EditIndex = e.NewEditIndex;
            Bind();
            HiddenField hfSystem = lvSystem.EditItem.FindControl("hfSystem") as HiddenField;
            HiddenField hfInterface = lvSystem.EditItem.FindControl("hfInterface") as HiddenField;
            DropDownList ddlSystem = lvSystem.EditItem.FindControl("ddlSystem") as DropDownList;
            DropDownList ddlInformation = lvSystem.EditItem.FindControl("ddlInformation") as DropDownList;
            if (hfSystem != null && hfInterface != null && ddlSystem != null && ddlInformation != null)
            {
                BindDLL(ddlSystem, ddlInformation);
                ddlSystem.SelectedValue = hfSystem.Value;
                ddlInformation.SelectedValue = hfInterface.Value;
            }
        }
        protected void lvSystem_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string DataItems = "";
            string InterfaceID = "";
            string SystemID = "";
            string ID = "";
            string TableName = "";
            try
            {
                if (e.NewValues["DataItems"] != null && e.NewValues["TableName"] != null && e.NewValues["ID"] != null)
                {
                    DataItems = e.NewValues["DataItems"].ToString().Trim();
                    TableName = e.NewValues["TableName"].ToString().Trim();
                    ID = e.NewValues["ID"].ToString().Trim();

                    DropDownList ddlSystem = lvSystem.EditItem.FindControl("ddlSystem") as DropDownList;
                    DropDownList ddlInformation = lvSystem.EditItem.FindControl("ddlInformation") as DropDownList;
                    HiddenField hfSystem = lvSystem.EditItem.FindControl("hfSystem") as HiddenField;
                    HiddenField hfInterface = lvSystem.EditItem.FindControl("hfInterface") as HiddenField;
                    HiddenField HFTableName = lvSystem.EditItem.FindControl("HFTableName") as HiddenField;
                    if (ddlSystem != null && ddlInformation != null && hfSystem != null && hfInterface != null && HFTableName != null)
                    {
                        InterfaceID = ddlInformation.SelectedValue;
                        SystemID = ddlSystem.SelectedValue;
                        if (!string.IsNullOrWhiteSpace(DataItems) &&
                            !string.IsNullOrWhiteSpace(ID) &&
                            !string.IsNullOrWhiteSpace(InterfaceID) &&
                            !string.IsNullOrWhiteSpace(SystemID) &&
                            !string.IsNullOrWhiteSpace(TableName))
                        {
                            InterfaceConfigurationBLL ICBLL = new InterfaceConfigurationBLL();
                            if (!InterfaceID.Equals(hfInterface.Value))
                            {
                                if (ICBLL.CheckISExist(InterfaceID, SystemID, TableName))
                                {
                                    //系统信息已经存在
                                    e.Cancel = true;
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                                    return;
                                }
                            }
                            if (!SystemID.Equals(hfSystem.Value))
                            {
                                if (ICBLL.CheckISExist(InterfaceID, SystemID, TableName))
                                {
                                    //系统信息已经存在
                                    e.Cancel = true;
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                                    return;
                                }
                            }
                            if (!TableName.Equals(HFTableName.Value))
                            {
                                if (ICBLL.CheckISExist(InterfaceID, SystemID, TableName))
                                {
                                    //系统信息已经存在
                                    e.Cancel = true;
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                                    return;
                                }
                            }
                            InterfaceConfiguration SC = new InterfaceConfiguration();
                            SC.ID = Convert.ToInt16(ID);
                            SC.DataItems = DataItems;
                            SC.TableName = TableName;
                            SC.InterfaceID = Convert.ToInt16(InterfaceID);
                            SC.SystemID = Convert.ToInt16(SystemID);
                            ICBLL.Update(SC);
                            Reset();
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('数据更新失败【" + ex.Message + "】')", true);
            }
        }
        private void Reset()
        {
            lvSystem.EditIndex = -1;
            Bind();
        }
        protected void lvSystem_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            //取消编辑
            if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            {
                lvSystem.EditIndex = -1;
                Bind();
            }
            else if (e.CancelMode == ListViewCancelMode.CancelingInsert)
            {
                Bind();
                return;
            }
        }

        protected void lvSystem_PreRender(object sender, EventArgs e)
        {
            DropDownList ddlSystem = lvSystem.InsertItem.FindControl("ddlSystem") as DropDownList;
            DropDownList ddlInformation = lvSystem.InsertItem.FindControl("ddlInformation") as DropDownList;
            if (ddlSystem != null && ddlInformation != null)
            {
                BindDLL(ddlSystem, ddlInformation);
            }
        }
        /// <summary>
        /// 绑定系统和接口信息下拉列表
        /// </summary>
        /// <param name="ddlSystem"></param>
        /// <param name="ddlInformation"></param>
        private static void BindDLL(DropDownList ddlSystem, DropDownList ddlInformation)
        {
            SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
            ddlSystem.DataSource = SCBLL.SelectAll();
            ddlSystem.DataTextField = "Name";
            ddlSystem.DataValueField = "ID";
            ddlSystem.DataBind();

            InterfaceInformationBLL IIBLL = new InterfaceInformationBLL();
            ddlInformation.DataSource = IIBLL.SelectAll();
            ddlInformation.DataTextField = "Name";
            ddlInformation.DataValueField = "ID";
            ddlInformation.DataBind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSystem_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jkqxgl, ActionConstants.del);
                    InterfaceConfigurationBLL SCBLL = new InterfaceConfigurationBLL();
                    HiddenField HidNo = e.Item.FindControl("HidID") as HiddenField;
                    bool istrue = SCBLL.Del(Convert.ToInt32(HidNo.Value));
                    if (istrue)
                    {
                        alert("删除成功！");
                        Bind();
                    }
                    else
                    {
                        alert("删除失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

         
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
    }
}