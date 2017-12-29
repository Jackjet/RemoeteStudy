using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using Common.SchoolUserService;
using System.Text;
using System.Data;
namespace Sinp_TeacherWP.LAYOUTS.hander
{
    public partial class LibraryList : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        StringBuilder sbjson = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            sbjson = new StringBuilder();
            //string returnJson = "[" + Major()+BindtvNodes("0").TrimEnd(',') + "]";
            Major();
            Response.Write("[" + sbjson.ToString().TrimEnd(',') + "]");
            Response.End();
        }
        private void Major()
        {
            string gradid = Request["GradID"];

            UserPhoto user = new UserPhoto();
            try
            {
                //5195 6198
                DataTable dt = user.GetGradeAndSubjectBySchoolID(gradid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbjson.Append("{\"id\": " + dt.Rows[i]["NJ"] + ",\"type\":\"专业\",  \"SubjectID\": \"\",\"pId\": 0, \"name\":\"" + dt.Rows[i]["NJMC"] + "(专业)\"},");
                    string subjectList = dt.Rows[i]["XK"].ToString().TrimEnd(';');
                    string[] sj = subjectList.Split(';');
                    for (int j = 0; j < sj.Length; j++)
                    {
                        string subid = sj[j].Split(',')[0].ToString() + dt.Rows[i]["ID"].ToString();
                        sbjson.Append("{\"id\": " + subid + ",\"type\":\"学科\", \"SubjectID\": " + subid + ",\"pId\": " + dt.Rows[i]["NJ"] + ", \"name\":\"" + sj[j].Split(',')[1] + "(学科)\"},");
                        BindtvNodes(sj[j].Split(',')[0].ToString() + dt.Rows[i]["ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Major");
            }
        }

        /// <summary>
        /// 文件夹根节点
        /// </summary>
        /// <param name="pid"></param>
        private void BindtvNodes(string pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        UserPhoto user = new UserPhoto();

                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();

                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></Query><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string name = item["Title"].ToString();
                                sbjson.Append("{\"id\": " + item.ID + ",\"type\":\"目录\", \"SubjectID\": " + item["SubJectID"] + ", \"pId\": " + pid + ", \"name\":\"" + name + "(目录)\"},");

                                BindtvNodes(item.ID.ToString());
                            }

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TreeNodes.BindtvNodes");
            }
            //return sbjson.ToString();
        }
    }
}
