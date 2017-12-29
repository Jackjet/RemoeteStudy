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
    public partial class RoleManageList : BaseInfo
    {
        protected string RoleId = "";
        string RoleName = "";


        string SQLstring = "";
        string strxxzzjgh = "";

        private static string strLoginName;
        void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;


                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    DataTable deptList = deptBll.SelectXJByLoginName(teacher);

                    dp_DepartMent.DataTextField = "JGMC";
                    dp_DepartMent.DataValueField = "XXZZJGH";
                    dp_DepartMent.DataSource = deptList;
                    dp_DepartMent.DataBind();
                    dp_DepartMent.Items.Insert(0, new ListItem("--学校--", ""));
                    strxxzzjgh = Request.QueryString["unitsid"];
                    if (!string.IsNullOrEmpty(strxxzzjgh))
                    {
                        hiddenStrxxzzjgh.Value = strxxzzjgh;
                    }
                    BindListView();
                }
                catch (Exception ex)
                {
                    DAL.LogHelper.WriteLogError(ex.ToString());
                }
            }
        }

        public void BindListView()//绑定数据
        {

            SQLstring = @"  select RoleId,RoleName,CONVERT(varchar(10), CreateTime, 23) CreateTime,Remark ,UnitsId,jgjc
                              from Base_Role r
                              left join 
                              Base_Department d
                              on r.UnitsId=d.xxzzjgh where 1=1  ";

            if (!string.IsNullOrEmpty(tb_RealName.Text.Trim()))
            {
                SQLstring += " and RoleName like  '%" + tb_RealName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(dp_DepartMent.SelectedItem.Value))
            {
                SQLstring += " and UnitsId= '" + dp_DepartMent.SelectedItem.Value + "'";
            }
            else
            {
                if (!string.IsNullOrEmpty(strxxzzjgh))
                {
                    SQLstring += " and UnitsId= '" + strxxzzjgh + "'";
                }
            }
            SQLstring += "  order by createTime desc";
            DataTable dtuser = new DataTable();
            dtuser = DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring).Tables[0];
            this.lvRole.DataSource = dtuser;
            this.lvRole.DataBind();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_AddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RoleEdit.aspx");
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_Search_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.jsgl, ActionConstants.Search);
            BindListView();
        }

        protected void lvRole_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this.DPRole.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void lvRole_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            HiddenField hiddenRoleid = e.Item.FindControl("hiddenRoleid") as HiddenField;
            if (e.CommandName == "Edit")
            {
                string aspString = "/RoleEdit.aspx?PamramRoleId=" + hiddenRoleid.Value;
                Response.Redirect(aspString);
            }
            if (e.CommandName == "Del")
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jsgl, ActionConstants.del);
                string sql = "delete from Base_Role where RoleId ='" + hiddenRoleid.Value + "'";
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, sql);//删除记录
                BindListView();
            }
        }
    }
}