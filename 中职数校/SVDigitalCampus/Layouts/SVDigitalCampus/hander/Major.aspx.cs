using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using Common;
using Common.SchoolUser;
using System.Text;
namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class Major : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Func"] != null)
            {
                string func = Request["Func"];
                switch (func)
                {
                    case "GetMajor":
                        GetMajor();
                        break;
                    case "GetStudysection":
                        GetStudysection();
                        break;
                    case "GetSubJect":
                        Response.Write(GetSubJect());
                        break;
                    default:
                        break;
                }

            }
            Response.End();
        }
        #region 专业学科
        private void GetMajor()
        {
            try
            {
                UserPhoto user = new UserPhoto();

                DataTable Majordt = user.GetGradeAndSubjectBySchoolID();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Majordt.Rows.Count; i++)
                {
                    if (Majordt.Rows[i]["NJ"].safeToString() == Request.Form["MID"].safeToString())
                    {
                        sb.Append("<option value='" + Majordt.Rows[i]["NJ"] + "'  selected=\"selected\">" + Majordt.Rows[i]["NJMC"] + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + Majordt.Rows[i]["NJ"] + "'>" + Majordt.Rows[i]["NJMC"] + "</option>");
                    }
                }
                if (sb != null)
                {
                    Context.Response.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "Major.GetMajor()");

            }
        }
        private string GetSubJect()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                string subjectList = "";
                DataTable dts = new DataTable();
                dts.Columns.Add("ID");
                dts.Columns.Add("Title");
                UserPhoto user = new UserPhoto();

                DataTable Majordt = user.GetGradeAndSubjectBySchoolID();
                foreach (DataRow dr in Majordt.Rows)
                {
                    if (dr["NJ"].ToString() == Request["Majorid"].safeToString())
                    {
                        subjectList = dr["XK"].ToString().TrimEnd(';');
                        string[] sj = subjectList.Split(';');
                        for (int i = 0; i < sj.Length; i++)
                        {
                            DataRow tr = dts.NewRow();
                            tr["ID"] = sj[i].Split(',')[0].ToString() + dr["ID"].ToString();
                            tr["Title"] = sj[i].Split(',')[1];
                            dts.Rows.Add(tr);
                        }
                    }
                }

                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    if (Majordt.Rows[i]["ID"].safeToString() == Request.Form["SID"].safeToString())
                    {
                        sb.Append("<option value='" + dts.Rows[i]["ID"] + "' selected=\"selected\">" + dts.Rows[i]["Title"] + "</option>");

                    }
                    else
                    {
                        sb.Append("<option value='" + dts.Rows[i]["ID"] + "'>" + dts.Rows[i]["Title"] + "</option>");
                    }
                }

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Major.GetMajor()");
            }
            return sb.ToString();
        }
        #endregion
        #region 学期学年
        private void GetStudysection()
        {
            try
            {
                UserPhoto user = new UserPhoto();

                DataTable Studysection = user.GetStudysection().Tables[0];
                StringBuilder sb = new StringBuilder();
                int Year = DateTime.Now.Year;
                for (int i = 0; i < Studysection.Rows.Count; i++)
                {
                    int StudyYear = Convert.ToInt32(Studysection.Rows[i]["Academic"]);
                    if (StudyYear >= Year)
                    {
                        if (Studysection.Rows[i]["StudysectionID"].safeToString() == Request.Form["BID"].safeToString())
                        {
                            sb.Append("<option value='" + Studysection.Rows[i]["StudysectionID"] + "' selected=\"selected\">" + Studysection.Rows[i]["Academic"] + "-" + Studysection.Rows[i]["Semester"] + "</option>");
                        }
                        else if (Studysection.Rows[i]["StudysectionID"].safeToString() == Request.Form["EID"].safeToString())
                        {
                            sb.Append("<option value='" + Studysection.Rows[i]["StudysectionID"] + "' selected=\"selected\">" + Studysection.Rows[i]["Academic"] + "-" + Studysection.Rows[i]["Semester"] + "</option>");

                        }
                        else
                        {
                            sb.Append("<option value='" + Studysection.Rows[i]["StudysectionID"] + "'>" + Studysection.Rows[i]["Academic"] + "-" + Studysection.Rows[i]["Semester"] + "</option>");
                        }
                    }
                }
                if (sb != null)
                {
                    Context.Response.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "Major.GetMajor()");

            }
        }
        #endregion
    }
}
