using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
using Common;
namespace SVDigitalCampus.School_Courses.RC_wp_SelNav
{
    public partial class RC_wp_SelNavUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
            }
        }
        private void BindMajor()
        {
            UserPhoto user = new UserPhoto();
            try
            {
                DataTable dt = user.GetGradeAndSubjectBySchoolID();
                dpMajor.DataSource = dt;
                dpMajor.DataBind();
                dpMajor.Items.Insert(0, new ListItem("请选择专业", "-1"));
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_SelNav.BindMajor");
            }
        }

        private void BindSubject(string MajorID)
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("ID");
            dts.Columns.Add("Title");

            UserPhoto user = new UserPhoto();
            try
            {
                string subjectList = "";
                DataTable dt = user.GetGradeAndSubjectBySchoolID();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["NJ"].ToString() == MajorID)
                    {
                        subjectList = dr["XK"].ToString().TrimEnd(';');
                        string[] sj = subjectList.Split(';');
                        for (int i = 0; i < sj.Length; i++)
                        {
                            DataRow tr = dts.NewRow();
                            tr["ID"] = dr["NJ"].ToString() + sj[i].Split(',')[0].ToString();
                            tr["Title"] = sj[i].Split(',')[1];
                            dts.Rows.Add(tr);
                        }
                    }
                }
                dpSubJect.DataSource = dts;
                dpSubJect.DataBind();
                dpSubJect.Items.Insert(0, new ListItem("请选择学科", "-1"));

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_SelNav.BindSubject");
            }
        }

        protected void dpMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubject(dpMajor.SelectedValue);
        }
    }
}
