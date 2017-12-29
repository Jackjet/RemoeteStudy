using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SE.SE_wp_SelectQuestion
{
    public partial class SE_wp_SelectQuestionUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string _type { get { return Request.QueryString["type"]; } }
        private SPList CurrentList { get {
            if (this._type != "2") return ListHelp.GetCureenWebList("选择题", false);
            else return ListHelp.GetCureenWebList("问答题", false); 
        } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this._type))
                {
                    BindQuestionData();
                }
            }
        }

        private void BindQuestionData()
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "ID", "Group", "Title", "Content" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            string where = string.Empty;
            if (this._type != "2")
            {
                string Type = string.Empty;
                if (this._type == "0")  Type="单选题";
                else if (this._type == "1") Type = "多选题";
                where = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + Type + @"</Value></Eq></Where>";
            }
            SPQuery query = new SPQuery { Query =where+ "<OrderBy><FieldRef Name='Modified' Ascending='False'/></OrderBy>" };
            SPListItemCollection items = CurrentList.GetItems(query);
            foreach (SPListItem item in items)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.ID;
                dr["Title"] = item.Title.Length > 30 ? item.Title.Substring(0, 30) + "..." : item.Title;
                dr["Group"] = item["Group"];
                if (this._type != "2")
                {
                    string choice = "A:" + item["AnswerA"] + "  B:" + item["AnswerA"] + "  C:" + item["AnswerC"] + "  D:" + item["AnswerD"];
                    dr["Content"] = choice.Length > 40 ? choice.Substring(0, 40) + "..." : choice;
                }
                dt.Rows.Add(dr);
            }
            lvQuestion.DataSource = dt;
            lvQuestion.DataBind();
        }
    }
}
