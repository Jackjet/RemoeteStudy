using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class StatisticsOfQuestion : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string beforeSubmitJS = "\nvar exportRequested = false; \n";
            beforeSubmitJS += "var beforeFormSubmitFunction = theForm.onsubmit;\n";
            beforeSubmitJS += "theForm.onsubmit = function(){ \n";
            beforeSubmitJS += "var returnVal = beforeFormSubmitFunction(); \n";
            beforeSubmitJS += "if(exportRequested && returnVal) {_spFormOnSubmitCalled=false; exportRequested=false;} \n";
            beforeSubmitJS += "return returnVal; \n";
            beforeSubmitJS += "}; \n";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alterFormSubmitEvent", beforeSubmitJS, true);
            btnExport.Attributes["onclick"] = "javascript:exportRequested=true;";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "错题统计";
                string title = txtTitle.Value;
                string createUser = txtCreateUser.Value;
                string questionType = hfQuestionType.Value;
                List<PaperScore> psLs = new ExamResultManager().FindStatisticsQuestion(new PaperScore() { Title = title, CreateUser = createUser, QuestionType = questionType }, -1, 0);
                DataTable dt = new DataTable();
                dt.Columns.Add("问题名称");
                dt.Columns.Add("类型");
                dt.Columns.Add("错误率");
                dt.Columns.Add("出题次数");
                dt.Columns.Add("出题人");
                dt.Columns.Add("出题时间");
                foreach (var item in psLs)
                {
                    dt.Rows.Add(item.Title, item.QuestionType, item.ErrorPercent, item.ShowCount, item.CreateUser, item.CreateTime);
                }
                new ExportUtil().ExportDataTableToExcel(dt, fileName);//byte short int long float double string boolean
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrInfo", "<script>console.error('" + ex.Message + "')</script>", true);
            }
        }
    }
}
