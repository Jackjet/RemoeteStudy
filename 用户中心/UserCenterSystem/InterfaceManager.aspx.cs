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
    public partial class InterfaceManager : BaseInfo
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
                        Response.Redirect("/UserMenuManage.aspx");
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
            InterfaceInformationBLL IIBLL = new InterfaceInformationBLL();
            List<InterfaceInformation> list = IIBLL.SelectAll();
            lvSystem.DataSource = list;
            lvSystem.DataBind();
        }
        protected void lvSystem_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                TextBox txtName = e.Item.FindControl("txtName") as TextBox;
                TextBox txtInformation = e.Item.FindControl("txtInformation") as TextBox;
                DropDownList ddlService = e.Item.FindControl("ddlService") as DropDownList;
                //DropDownList ddlTableName = e.Item.FindControl("ddlTableName") as DropDownList;
                if (txtName != null && txtInformation != null && ddlService != null)
                {
                    InterfaceInformationBLL IIBLL = new InterfaceInformationBLL();
                    if (IIBLL.CheckISExist(txtName.Text.Trim(), ddlService.SelectedValue))
                    {
                        //系统信息已经存在
                        e.Cancel = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                        return;
                    }
                    InterfaceInformation IIF = new InterfaceInformation();
                    IIF.Name = txtName.Text.Trim();
                    IIF.Information = txtInformation.Text.Trim();
                    IIF.ServiceName = ddlService.SelectedValue;
                    //IIF.TableName = ddlTableName.SelectedValue;
                    IIBLL.Insert(IIF);
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
            HiddenField hfServiceName = lvSystem.EditItem.FindControl("hfService") as HiddenField;
            //HiddenField hfTableName = lvSystem.EditItem.FindControl("hfTableName") as HiddenField;
            DropDownList ddlServiceName = lvSystem.EditItem.FindControl("ddlService") as DropDownList;
            //DropDownList ddlTableName = lvSystem.EditItem.FindControl("ddlTableName") as DropDownList;
            if (hfServiceName != null && ddlServiceName != null)
            {
                ddlServiceName.SelectedValue = hfServiceName.Value;
                //BindDDL(ddlTableName);
                //ddlTableName.SelectedValue = hfTableName.Value;
            }
        }
        protected void lvSystem_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string Name = "";
            string Information = "";
            string ID = "";
            string ServiceName = "";
            string TableName = "";
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jkxxgl, ActionConstants.xg);
                if (e.NewValues["Name"] != null && e.NewValues["Information"] != null && e.NewValues["ID"] != null)
                {
                    Name = e.NewValues["Name"].ToString().Trim();
                    Information = e.NewValues["Information"].ToString().Trim();
                    ID = e.NewValues["ID"].ToString().Trim();

                    DropDownList ddlService = lvSystem.EditItem.FindControl("ddlService") as DropDownList;
                    //DropDownList ddlTableName = lvSystem.EditItem.FindControl("ddlTableName") as DropDownList;
                    HiddenField hfname = lvSystem.EditItem.FindControl("hfname") as HiddenField;
                    HiddenField hfService = lvSystem.EditItem.FindControl("hfService") as HiddenField;
                    //HiddenField hfTableName = lvSystem.EditItem.FindControl("hfTableName") as HiddenField;
                    if (hfService != null && hfname != null && ddlService != null)
                    {
                        ServiceName = ddlService.SelectedValue;
                        //TableName = ddlTableName.SelectedValue;
                        if (!string.IsNullOrWhiteSpace(Name) &&
                            !string.IsNullOrWhiteSpace(ID) &&
                            !string.IsNullOrWhiteSpace(ServiceName) &&
                            !string.IsNullOrWhiteSpace(Information))
                        {
                            InterfaceInformationBLL SCBLL = new InterfaceInformationBLL();
                            if (!Name.Equals(hfname.Value))
                            {
                                if (SCBLL.CheckISExist(Name, ServiceName))
                                {
                                    //系统信息已经存在
                                    e.Cancel = true;
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                                    return;
                                }
                            }
                            if (!ServiceName.Equals(hfService.Value))
                            {
                                if (SCBLL.CheckISExist(Name, ServiceName))
                                {
                                    //系统信息已经存在
                                    e.Cancel = true;
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('信息已经存在')", true);
                                    return;
                                }
                            }
                            InterfaceInformation SC = new InterfaceInformation();
                            SC.ID = Convert.ToInt16(ID);
                            SC.Name = Name;
                            SC.Information = Information;
                            SC.ServiceName = ServiceName;
                            //SC.TableName = TableName;
                            SCBLL.Update(SC);
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
            DropDownList ddlTableName = lvSystem.InsertItem.FindControl("ddlTableName") as DropDownList;
            if (ddlTableName != null)
            {
                BindDDL(ddlTableName);
            }
        }

        private static void BindDDL(DropDownList ddlTableName)
        {
            InterfaceInformationBLL IIBLL = new InterfaceInformationBLL();
            DataSet DS = IIBLL.GetTableName();
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                ddlTableName.DataSource = DS;
                ddlTableName.DataTextField = "name";
                ddlTableName.DataValueField = "name";
                ddlTableName.DataBind();
            }
        }

        protected void lvSystem_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jkxxgl, ActionConstants.del);
                InterfaceInformationBLL SCBLL = new InterfaceInformationBLL();
                HiddenField HidNo = e.Item.FindControl("HidID") as HiddenField;
                //查询是否在使用
                if (SCBLL.IsExistsbyInterfaceID(HidNo.Value))
                {
                    alert("该接口已和用户绑定，不允许直接删除");
                }
                else
                {
                    bool istrue = SCBLL.Del(Convert.ToInt32(HidNo.Value));
                    if (istrue)
                    {
                        alert("删除成功！");
                        Bind();
                    }
                }
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
    }
}