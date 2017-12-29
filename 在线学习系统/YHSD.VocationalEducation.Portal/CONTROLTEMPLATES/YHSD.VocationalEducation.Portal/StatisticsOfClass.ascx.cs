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
    public partial class StatisticsOfClass : UserControl
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
                string fileName = "班级成绩统计";
                string paperName = txtPaperName.Value;
                string className = txtClassName.Value;
                List<PaperScore> psLs = new ExamResultManager().FindClassPaperScore(new PaperScore() { PaperName = paperName, Class = className }, -1, 0);
                DataTable dt = new DataTable();
                dt.Columns.Add("试卷名称");
                dt.Columns.Add("班级姓名");
                dt.Columns.Add("最高分");
                dt.Columns.Add("平均分");
                dt.Columns.Add("通过率");
                foreach (var item in psLs)
                {
                    dt.Rows.Add(item.PaperName, item.Class, item.MaxScore, item.AvgScore, item.PassPercent);
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
