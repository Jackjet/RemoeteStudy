using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Common;

namespace UserCenterSystem
{
    public partial class SystemConfigManager : BaseInfo
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

        #region 分页
        protected void lvSystem_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            Bind();
        }
        #endregion
        #region  加载数据
        public void Bind()
        {
            SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
            List<SystemConfiguration> list = SCBLL.SelectAll();
            lvSystem.DataSource = list;
            lvSystem.DataBind();
        }
        #endregion

        protected void lvSystem_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                TextBox txtName = e.Item.FindControl("txtName") as TextBox;
                TextBox txtManager = e.Item.FindControl("txtManager") as TextBox;
                if (txtName != null && txtManager != null)
                {
                    SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
                    if (SCBLL.CheckISExistByName(txtName.Text.Trim()))
                    {
                        //系统信息已经存在
                        e.Cancel = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('系统信息已经存在')", true);
                        return;
                    }

                    if (SCBLL.CheckISExistByManager(txtManager.Text.Trim()))
                    {
                        //管理员信息已经存在
                        e.Cancel = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('管理员已经存在')", true);
                        return;
                    }

                    SystemConfiguration SC = new SystemConfiguration();
                    SC.Name = txtName.Text.Trim();
                    SC.Manager = txtManager.Text.Trim();
                    SCBLL.Insert(SC);
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
        }

        protected void lvSystem_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string Manager = "";
            string Name = "";
            string ID = "";
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jkxxgl, ActionConstants.xg);
                if (e.NewValues["Name"] != null && e.NewValues["Manager"] != null && e.NewValues["ID"] != null)
                {
                    Name = e.NewValues["Name"].ToString().Trim();
                    Manager = e.NewValues["Manager"].ToString().Trim();
                    HiddenField hfname = lvSystem.EditItem.FindControl("hfname") as HiddenField;
                    HiddenField hfManager = lvSystem.EditItem.FindControl("hfManager") as HiddenField;
                    if (hfManager != null && hfname != null)
                    {
                        ID = e.NewValues["ID"].ToString().Trim();
                        if (!string.IsNullOrWhiteSpace(Name) &&
                            !string.IsNullOrWhiteSpace(ID) &&
                            !string.IsNullOrWhiteSpace(Manager))
                        {
                            SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
                            //如果新值没有变化则取消更新
                            if (Manager.Equals(hfManager.Value) && Name.Equals(hfname.Value))
                            {
                                e.Cancel = true;
                                Reset();
                            }
                            else
                            {
                                if (!Name.Equals(hfname.Value))
                                {
                                    if (SCBLL.CheckISExistByName(Name))
                                    {
                                        //系统信息已经存在
                                        e.Cancel = true;
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('系统信息已经存在')", true);
                                        return;
                                    }
                                }
                                if (!Manager.Equals(hfManager.Value))
                                {
                                    if (SCBLL.CheckISExistByManager(Manager))
                                    {
                                        //管理员信息已经存在
                                        e.Cancel = true;
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('管理员已经存在')", true);
                                        return;
                                    }
                                }
                                SystemConfiguration SC = new SystemConfiguration();
                                SC.ID = Convert.ToInt16(ID);
                                SC.Name = Name;
                                SC.Manager = Manager;
                                SCBLL.Update(SC);
                                Reset();
                            }
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

        protected void lvSystem_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xtzhgl, ActionConstants.del);
                SystemConfigurationBLL SCBLL = new SystemConfigurationBLL();
                HiddenField HidNo = e.Item.FindControl("HidID") as HiddenField;
                //查询是否在使用
                if (SCBLL.IsExistsbySystemID(HidNo.Value))
                {
                    alert("该用户已关联接口，不允许直接删除");
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

        //protected void lvSystem_ItemCanceling(object sender, ListViewCancelEventArgs e)
        //{
        //    //取消编辑
        //    if (e.CancelMode == ListViewCancelMode.CancelingEdit)
        //    {
        //        lvSystem.EditIndex = -1;
        //        Bind();
        //    }
        //    else if (e.CancelMode == ListViewCancelMode.CancelingInsert)
        //    {
        //        Bind();
        //        return;
        //    }
        //}
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
    }
}