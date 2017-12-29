using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class KnowledgeLibAdd : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemid = HttpContext.Current.Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    BindKnowledgeById(itemid);
                }                
            }
        }
        public void BindKnowledgeById(string itemid)
        {
            KnowledgeLib entity =new KnowledgeLibManager().GetKnowledgeById(itemid);
            this.TB_Question.Text = entity.Question;
            this.TB_Answer.Text = entity.Answer;
          }
        protected void Btn_KnowledgeLibSave_Click(object sender, EventArgs e)
        {
            try
            {
                KnowledgeLib lib = new KnowledgeLib();
                string itemid=HttpContext.Current.Request.QueryString["itemid"];                
                if (!string.IsNullOrEmpty(this.TB_Question.Text))
                {
                    lib.Question = TB_Question.Text;
                }
                if (!string.IsNullOrEmpty(this.TB_Answer.Text))
                {
                    lib.Answer = this.TB_Answer.Text;
                }            
                if (string.IsNullOrEmpty(itemid))
                {
                    lib.Id = Guid.NewGuid().ToString();
                    lib.CreateUser = CommonUtil.GetSPADUserID().Id;
                    lib.CreateTime = DateTime.Now.ToString();
                    lib.IsDelete = "0";
                    new KnowledgeLibManager().Add(lib);
                }
                else
                {
                    lib.Id = itemid;
                    new KnowledgeLibManager().Update(lib);
                }             
                                                 
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('操作成功!');</script>");
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "KnowledgeLibAdd_Btn_KnowledgeLibSave_Click");
            }
        }
    }
}
