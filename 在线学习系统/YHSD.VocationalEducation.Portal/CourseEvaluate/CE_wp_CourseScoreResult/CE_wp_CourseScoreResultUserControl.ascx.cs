using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.CourseEvaluate.CE_wp_CourseScoreResult
{
    public partial class CE_wp_CourseScoreResultUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("已学课程", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {
            try
            {
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "Title", "Score" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                CurriculumInfo Curriculum = new CurriculumInfo();
                Curriculum.CreaterTime = "Desc";
                Curriculum.UserID = CommonUtil.GetSPADUserID().Id;
                List<CurriculumInfo> ListCurr = new CurriculumInfoManager().Find(Curriculum);
                if (ListCurr.Count > 0)
                {
                    foreach (CurriculumInfo ci in ListCurr)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Title"] = ci.Title;
                        SPListItem item = this.GetItemByCTitle(ci.Title);
                        if (item != null)
                        {
                            string star = string.Empty;
                            int score = Convert.ToInt32(item["Score"]);
                            for (int i = 0; i < score; i++)
                            {
                                star += "★";
                            }
                            dr["Score"] = star;
                        }
                        else
                        {
                            dr["Score"] = "--";
                        }
                        dt.Rows.Add(dr);
                    }
                }
                //SPListItemCollection items = CurrentList.GetItems();
                //foreach (SPListItem item in items)
                //{
                //    int score = Convert.ToInt32(item["Score"]);

                //    dt.Rows.Add(item.ID, item.Title, score);
                //}
                lvCourseScore.DataSource = dt;
                lvCourseScore.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseScore_BindList");
            }
        }
        private SPListItem GetItemByCTitle(string courseTitle)
        {
            SPListItem item = null;
            SPQuery query = new SPQuery
            {
                Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + courseTitle + @"</Value></Eq></Where>
   <OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>"
            };
            SPListItemCollection items = CurrentList.GetItems(query);
            if (items.Count > 0)
            {
                item = items[0];
            }
            return item;
        }
    }
}
