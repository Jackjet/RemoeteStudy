using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using BLL;
using Model;
using Common;

namespace UserCenterSystem
{
    public partial class RoleEdit : BaseInfo
    {
        static string RoleId = "";
        string RoleName = "";

        DateTime CreateTime;
        string Remark = "";
        string SQLstring = "";



        protected void Page_Load(object sender, EventArgs e)
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

                    RoleId = Request.QueryString["PamramRoleId"];
                    DisplayDetails();
                }
                catch (Exception ex)
                {
                    DAL.LogHelper.WriteLogError(ex.ToString());
                }

            }
        }
        public void DisplayDetails()
        {
            SQLstring = @"select RoleId,RoleName,CreateTime,Remark,UnitsId,jgjc
                         from Base_Role R
                         left join
                          Base_Department d
                          on r.UnitsId=d.xxzzjgh where RoleId='" + RoleId + @"' order by CreateTime desc";
            SqlDataReader dr = DAL.SqlHelper.ExecuteReader(CommandType.Text, SQLstring);
            if (dr.Read())//将数据显示到文本框中
            {
                this.tb_RoleName.Text = dr["RoleName"].ToString();
                this.tb_Remark.Text = dr["Remark"].ToString();
                this.dp_DepartMent.SelectedIndex = this.dp_DepartMent.Items.IndexOf(this.dp_DepartMent.Items.FindByValue(dr["UnitsId"].ToString()));
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //todo 此处写 编辑保存的code
            if (this.tb_RoleName.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('角色名称不能为空！'); </script>");

                return;
            }
            if (dp_DepartMent.SelectedItem.Value == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择学校！'); </script>");

                return;
            }
            string strXXZZJGH = dp_DepartMent.SelectedItem.Value;
            RoleName = this.tb_RoleName.Text;
            CreateTime = DateTime.Now;//获取当前时间
            Remark = this.tb_Remark.Text;
            if (RoleId == null || RoleId == "")//判断为增加用户操作
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jsgl, ActionConstants.add);

                this.tb_RoleName.Enabled = true;
                SQLstring = "select RoleName from Base_Role where RoleName=" + "'" + RoleName + "'and unitsid='" + strXXZZJGH + "'";
                SqlDataReader dr = DAL.SqlHelper.ExecuteReader(CommandType.Text, SQLstring);
                if (dr.Read())    //进行用户判断
                {
                    if (this.tb_RoleName.Text == dr["RoleName"].ToString())
                    { //数据库中已有该用户
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('已有该用户名！'); </script>");

                        return;
                    }
                }
                RoleId = Guid.NewGuid().ToString();

                SQLstring = "insert into Base_Role (RoleId,RoleName,UnitsId,CreateTime,Remark) values ('" + RoleId + "','" + RoleName + "','" + strXXZZJGH + "','" + CreateTime + "','" + Remark + "')";
                int i = DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstring);//返回受影响的行数
                if (i > 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功！');window.location='RoleManageList.aspx?unitsid=" + strXXZZJGH + "';</script>");

                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='RoleManageList.aspx?unitsid=" + strXXZZJGH + "';</script>");
                    return;
                }
            }
            else
            { //判断为编辑操作
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jsgl, ActionConstants.xg);
                SQLstring = "update Base_Role set RoleName='" + RoleName + "',Remark='" + Remark + "' ,Unitsid='" + strXXZZJGH + "' where RoleId='" + RoleId + "'";
                int i = DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, SQLstring);//返回受影响的行数
                if (i > 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功！');window.location='RoleManageList.aspx?unitsid=" + strXXZZJGH + "';</script>");
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='RoleManageList.aspx?unitsid=" + strXXZZJGH + "';</script>");
                    return;
                }

            }
        }


    }
}