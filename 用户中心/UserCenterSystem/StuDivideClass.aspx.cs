using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using Model;
using Common;

namespace UserCenterSystem
{
    public partial class StuDivideClass : BaseInfo
    {
        Base_StudentBLL stuBll = new Base_StudentBLL();

        string StrUserid = "";  //用户主键
        string strRxxzzjgh = "";//学校
        string strRnj = "";//年级编号
        string strRbjbh = "";//班级编号
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    //string AA = Session["strTreeNode"].ToString();
                    //string bb = Request.QueryString["XXZZJGH"];
                    //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;


                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    DataTable deptList = deptBll.SelectXJByLoginName(teacher);

                    dp_DepartMent.DataTextField = "JGMC";
                    dp_DepartMent.DataValueField = "XXZZJGH";
                    dp_DepartMent.DataSource = deptList;
                    dp_DepartMent.DataBind();

                    if (dp_DepartMent.Items.FindByValue(Session["strTreeNode"].ToString()) != null)
                    {
                        dp_DepartMent.Items.FindByValue(Session["strTreeNode"].ToString()).Selected = true;
                        BindDrop();

                    }
                    else
                    {
                        dp_DepartMent.Items.Insert(0, new ListItem("--学校--", ""));
                    }
                    if (Session["strDivideStu"] != null)
                    {
                        StrUserid = Session["strDivideStu"].ToString(); //Request.QueryString["uid"];
                        hiddenSfzjhm.Value = StrUserid;
                    }

                    //strRxxzzjgh = Request.QueryString["xxzzjgh"];
                    //hiddenschool.Value = strRxxzzjgh;
                    //strRnj = Request.QueryString["nj"];
                    //hiddenGrade.Value = strRnj;
                    //strRbjbh = Request.QueryString["bjbh"];
                    //hiddenClass.Value = strRbjbh;
                }
                catch (Exception ex)
                {
                    DAL.LogHelper.WriteLogError(ex.ToString());
                }
            }

        }

        protected void btnDivide_Click(object sender, EventArgs e)
        {

            try
            {
                string strxxzzjgh = this.dp_DepartMent.SelectedItem.Value;
                string strnj = this.dp_Grades.SelectedItem.Value;
                string strbh = this.dp_Class.SelectedItem.Value;
                if (string.IsNullOrEmpty(StrUserid))
                {
                    StrUserid = hiddenSfzjhm.Value;
                }

                if (!string.IsNullOrEmpty(strxxzzjgh) && !string.IsNullOrEmpty(strnj) && !string.IsNullOrEmpty(strbh))
                {

                    int intDis = stuBll.DivideClassMore(strxxzzjgh, strnj, strbh, StrUserid);
                    if (intDis > 0)
                    {
                        Session["strTreeNode"] = strbh;
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');parent.window.location=\"StudentList.aspx?bjbh=" + strbh+ "\";</script>");

                    }
                    else
                    {
                        Session["strTreeNode"] = strbh;
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员!');parent.window.location='StudentList.aspx?bjbh=" + strbh + "';</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }

        /// <summary>
        /// 【Change】    【学校】
        /// </summary>
        protected void dp_DepartMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDrop();
        }

        /// <summary>
        /// 【Change】 【年级】
        /// </summary>
        protected void dp_GradesIndexChanged(object sender, EventArgs e)
        {
            this.dp_Class.Items.Clear();

            string strDepart = this.dp_DepartMent.SelectedItem.Value;
            string strNj = dp_Grades.SelectedItem.Value;
            if (!string.IsNullOrEmpty(strNj) && !string.IsNullOrEmpty(strDepart))
            {
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
                DataTable dtClass = stuBll.GetClassNameByGradeID(strDepart, strNj);
                foreach (DataRow dr in dtClass.Rows)
                {
                    this.dp_Class.Items.Add(new ListItem(dr["bj"].ToString(), dr["bjbh"].ToString()));
                }

            }
            else
            {
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
            }
        }

        public void BindDrop()
        {
            this.dp_Grades.Items.Clear();

            string strDepart = this.dp_DepartMent.SelectedItem.Value;
            if (!string.IsNullOrEmpty(strDepart))
            {
                this.dp_Grades.Items.Add(new ListItem("-年级-", ""));
                DataTable dtGrade = stuBll.GetGradeNameByDepartID(strDepart);
                foreach (DataRow dr in dtGrade.Rows)
                {
                    this.dp_Grades.Items.Add(new ListItem(dr["njmc"].ToString(), dr["nj"].ToString()));
                }
                this.dp_Class.Items.Clear();
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));

            }
            else
            {
                this.dp_Grades.Items.Add(new ListItem("-年级-", ""));
                this.dp_Class.Items.Clear();
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
            }
        }


    }
}