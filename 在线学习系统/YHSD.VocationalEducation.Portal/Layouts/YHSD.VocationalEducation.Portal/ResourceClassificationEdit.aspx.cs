using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ResourceClassificationEdit : LayoutsPageBase
    {
        private string LevStr = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");
        ResourceClassificationManager manager = new ResourceClassificationManager();
        private string SltId
        {
            get
            {
                return !string.IsNullOrEmpty(Request["Id"]) ? Request["Id"] : string.Empty;
            }
            set
            {
                SltId = value;
            }
        }
        private string PId
        {
            get
            {
                return !string.IsNullOrEmpty(Request["PId"]) ? Request["PId"] : "0";
            }
            set
            {
                PId = value;
            }
        }
        /// <summary>
        /// 操作类型
        /// 1 新建顶级分类
        /// 2 新建子级分类
        /// 3 编辑分类
        /// </summary>
        private string CmdType
        {
            get
            {
                return string.IsNullOrEmpty(Request["type"]) ? Request["type"] : "1";
            }
            set
            {
                SltId = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowData();
                ddlParent.SelectedValue = PId;
                if (CmdType.Equals("3") && !string.IsNullOrEmpty(SltId))
                {
                    ResourceClassification SltEntity = manager.Get(SltId);
                    txtRName.Text = SltEntity.Name;
                }
            }
        }
        void ShowData()
        {
            List<ResourceClassification> ls = manager.Find(null,-1,0);
            BindDropDownlist(ls);
        }

        #region 绑定下拉框
        private void BindDropDownlist(List<ResourceClassification> ls)
        {
            ListItem root = new ListItem("顶级分类", "0");
            ddlParent.Items.Add(root);
            foreach (var item in ls)
            {
                if (item.Pid.Equals("0"))
                {
                    ListItem li = new ListItem(FormatDisName(item.Name, 1), item.ID);
                    ddlParent.Items.Add(li);
                    EachItem(li, ls, 1);
                }
            }
        }
        private void EachItem(ListItem li, List<ResourceClassification> ls, int grade)
        {
            grade += 1;
            foreach (var item in ls)
            {
                if (item.Pid.Equals(li.Value))
                {
                    ListItem childLi = new ListItem(FormatDisName(item.Name, grade), item.ID);
                    ddlParent.Items.Add(childLi);
                    EachItem(childLi, ls, grade);
                }
            }
        }
        private string FormatDisName(string oldVal, int grade)
        {
            StringBuilder sbs = new StringBuilder();
            for (int i = 0; i < grade; i++)
            {
                sbs.Append(LevStr);
            }
            sbs.Append(oldVal);
            return sbs.ToString();
        }
        #endregion

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int grade = (ddlParent.SelectedItem.Text.LastIndexOf(LevStr) + 1) / LevStr.Length + 1;
            string path = string.Format("{0}{1}", ConnectionManager.SPClassificationName, hdPath.Value);
            switch (CmdType)
            {
                case "1":
                case "2":
                    ResourceClassification entity1 = new ResourceClassification()
                    {
                        Name = txtRName.Text,
                        Pid = ddlParent.SelectedValue,
                        Grade = grade.ToString()
                    };
                    AddNode(entity1);
                    CommonUtil.CreateFolderByPath(path, entity1.Name);
                    break;
                case "3":
                    if (string.IsNullOrEmpty(SltId))
                    {
                        ResourceClassification entity2 = new ResourceClassification()
                        {
                            ID = SltId,
                            Name = txtRName.Text,
                            Pid = ddlParent.SelectedValue,
                            Grade = grade.ToString()
                        };
                        EditNode(entity2);
                    }
                    break;
                default:
                    break;
            }
        }
        private void AddNode(ResourceClassification entity)
        {
            try
            {
                manager.Add(entity);
                this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "window.frameElement.commonModalDialogClose(1,'success');", true);
            }
            catch (Exception ex)
            {
                ShowMsg(string.Format("操作失败,{0}", ex.Message));
            }
        }
        private void EditNode(ResourceClassification entity)
        {
            try
            {
                manager.Update(entity);
                this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "window.frameElement.commonModalDialogClose(1,'success');", true);
            }
            catch (Exception ex)
            {
                ShowMsg(string.Format("操作失败,{0}", ex.Message));
            }
        }
        private void ShowMsg(string msg)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "AddMess", "alert('" + msg + "');", true);
        }
    }
}
