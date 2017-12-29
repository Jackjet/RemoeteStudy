using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text.RegularExpressions;
using DAL;
using BLL;
using Model;
using Common;


namespace UserCenterSystem
{
    public partial class ParentEdit : BaseInfo
    {
        Base_ParentBLL parBLL = new Base_ParentBLL();
        static string UserId = "";


        private static string strLoginName;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)//首次加载时
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


                    UserId = Request.QueryString["pid"];
                    DisplayDetails();//对用户信息进行加载
                }
                catch (Exception ex)
                {
                    DAL.LogHelper.WriteLogError(ex.ToString());
                }

            }


        }
        public void DisplayDetails()
        {

            DataTable dtpar = parBLL.GetSingleParent(UserId);
            foreach (DataRow dr in dtpar.Rows)
            {

                this.tb_RealName.Text = dr["cyxm"].ToString();
                dp_gxm.SelectedIndex = this.dp_gxm.Items.IndexOf(this.dp_gxm.Items.FindByValue(dr["gxm"].ToString()));
                dp_sfsjhr.SelectedIndex = this.dp_sfsjhr.Items.IndexOf(this.dp_sfsjhr.Items.FindByValue(dr["sfjhr"].ToString()));
                dp_DepartMent.SelectedIndex = this.dp_DepartMent.Items.IndexOf(this.dp_DepartMent.Items.FindByValue(dr["XXZZJGH"].ToString()));

                rblXB.SelectedValue = dr["xbm"].ToString();
                //this.dp_Sex.SelectedIndex = this.dp_Sex.Items.IndexOf(this.dp_Sex.Items.FindByValue(dr["xbm"].ToString()));


                //this.dp_Nation.SelectedIndex = this.dp_Nation.Items.IndexOf(this.dp_Nation.Items.FindByValue(dr["mzm"].ToString()));
                txtMzm.Text = dr["mzm"].ToString();
                this.dp_DepartMent.SelectedIndex = this.dp_DepartMent.Items.IndexOf(this.dp_DepartMent.Items.FindByValue(dr["xxzzjgh"].ToString()));

                string aa = dr["csny"].ToString();
                //出生日期
                if (dr["csny"].ToString() != "")
                {
                    this.tb_Birthday.Text = Convert.ToDateTime(dr["csny"]).ToString("yyyy-MM-dd");
                }

                this.tb_ContactTel.Text = dr["dh"].ToString();
                txtsjhm.Text = dr["sjhm"].ToString();
                txtlxdz.Text = dr["lxdz"].ToString();
                this.dp_HealthCondition.SelectedIndex = this.dp_HealthCondition.Items.IndexOf(this.dp_HealthCondition.Items.FindByValue(dr["jkzkm"].ToString()));
                //工作单位
                tb_WorkUnit.Text = dr["cygzdw"].ToString();
                txtdzyx.Text = dr["dzxx"].ToString();
                dp_xlm.SelectedIndex = this.dp_xlm.Items.IndexOf(this.dp_xlm.Items.FindByValue(dr["xlm"].ToString()));
                txtbz.Text = dr["bz"].ToString();


            }



        }
        /// <summary>
        /// 用户中心--编辑保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Base_ParentBLL parBll = new Base_ParentBLL();
                Base_Parent par = new Base_Parent();
                if (tb_RealName.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('姓名不能为空！'); </script>");

                    return;
                }
                if (this.dp_DepartMent.SelectedItem.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择子女所在学校！'); </script>");

                    return;
                }
                if (this.dp_gxm.SelectedItem.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择关系！'); </script>");

                    return;
                } if (this.dp_sfsjhr.SelectedItem.Value == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择是否是监护人！'); </script>");

                    return;
                }

                //if (dp_Sex.SelectedItem.Value == "")
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请选择性别！'); </script>");

                //    return;
                //}


                if (txtMzm.Text.Trim() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('请填写民族！'); </script>");
                    return;
                }

                par.CYXM = this.tb_RealName.Text;
                par.GXM = dp_gxm.SelectedItem.Value;
                par.SFJHR = dp_sfsjhr.SelectedItem.Value;
                par.XBM = rblXB.SelectedValue;// this.dp_Sex.SelectedItem.Value;
                par.MZM = this.txtMzm.Text;
                //出生日期
                par.CSNY = DAL.ConvertHelper.DateTime(this.tb_Birthday.Text).DateTimeResult; ;
                par.DH = this.tb_ContactTel.Text;
                par.SJHM = txtsjhm.Text;
                par.LXDZ = txtlxdz.Text;
                par.JKZKM = this.dp_HealthCondition.SelectedItem.Value;
                //工作单位
                par.CYGZDW = tb_WorkUnit.Text;
                par.DZXX = txtdzyx.Text;
                par.XLM = dp_xlm.SelectedItem.Value;
                par.BZ = txtbz.Text;
                par.YHZT = "0";//正常用户
                par.XXZZJGH = dp_DepartMent.SelectedItem.Value;

                int intResult = 0;
                if (!string.IsNullOrEmpty(UserId))
                {
                    par.CYSFZJH = UserId; //主键
                    //修改
                    intResult = parBll.UpdateParent(par);
                }

                if (intResult > 0)
                { 
                    AlertAndRedirect("修改成功！", "ParentList.aspx?xxzzjgh=" + par.XXZZJGH + "");
                }
                else
                { 
                    AlertAndRedirect("抱歉，系统出现问题，请联系管理员！", "ParentList.aspx");
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }

        }
        /// <summary>
        /// 弹出信息,并跳转指定页面。
        /// </summary>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            HttpContext.Current.Response.End();
        }
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value);
        }
    }
}