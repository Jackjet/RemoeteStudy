using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class Libarary : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #region 知识库
        public void BindKnowledgeLibList()
        {
            KnowledgeLib lib = new KnowledgeLib();
            KnowledgeLibManager libManager = new KnowledgeLibManager();
            if (!string.IsNullOrEmpty(this.TB_Question.Text))
            {
                lib.Question = this.TB_Question.Text;
            }
            List<KnowledgeLib> list = libManager.FindKnowledgeLibSearch(lib);
            this.AspNetPagerKnowledgeLib.PageSize = 10;
            this.AspNetPagerKnowledgeLib.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPagerKnowledgeLib.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPagerKnowledgeLib.PageSize;//分页数据源的每页记录数
            Rep_KnowledgeLib.DataSource = list;
            Rep_KnowledgeLib.DataBind();
        }
        protected void AspNetPagerKnowledgeLib_PageChanged(object sender, EventArgs e)
        {
            BindKnowledgeLibList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindKnowledgeLibList();
        }
        #endregion      
    }
}
