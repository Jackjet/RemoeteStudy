using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Task_base.TB_wp_QuestionTypeManager
{
    public partial class TB_wp_QuestionTypeManagerUserControl : UserControl
    {
        //public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        //public string SietUrl = appsetting.SiteUrl;
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();//绑定数据
            }
        }
        /// <summary>
        /// 查询数据绑定
        /// </summary>
        private void BindList()
        {
            try
            {

                DataTable typedb = ExamQTManager.GetExamQTList(false);
                lvEQType.DataSource = typedb;
                lvEQType.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_QuestionTypeManager_试题类型管理获取绑定数据");
            }
        }
        /*
        /// <summary>
        /// 行命令事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvEQType_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string itemid = e.CommandArgument.ToString();//获取id
            if (e.CommandName.Equals("del"))//删除
            {
                Delete(itemid);
            }
            else if (e.CommandName.Equals("Edit"))//编辑
            {
                lvEQType.EditIndex = e.Item.DataItemIndex;
                BindList();
                //Response.Redirect(Request.Url.AbsolutePath);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        private void Delete(string ID)
        {
            SPSite site = SPContext.Current.Site;
            SPWeb web = site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试题类型");
            try
            {

                if (list != null)
                {
                    int typeid = int.Parse(ID);
                    //从客观题中查询是否存在
                    DataTable oeqdt = ExamQManager.GetExamObjQList(false, null);
                    bool ishava = false;
                    if (oeqdt != null && oeqdt.Rows.Count > 0)
                    {
                        //循环判断该类型是否存在试题
                        foreach (DataRow item in oeqdt.Rows)
                        {
                            if (item["TypeID"].ToString().Equals(typeid.ToString()))
                            {
                                ishava = true;
                            }
                        }

                    }
                    //从主观题中查询是否存在
                    DataTable seqdt = ExamQManager.GetExamObjQList(false, null);
                    if (seqdt != null && seqdt.Rows.Count > 0)
                    {
                        //循环判断该类型是否存在试题
                        foreach (DataRow item in seqdt.Rows)
                        {
                            if (item["TypeID"].ToString().Equals(typeid.ToString()))
                            {
                                ishava = true;
                            }
                        }

                    }
                    if (!ishava)//不存在删除
                    {
                        list.Items.GetItemById(typeid).Delete();//删除
                        BindList();
                        Response.Redirect(Request.Url.AbsolutePath);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('删除失败，该类型存在试题！');", true);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_QuestionTypeManager_试题类型管理的删除试题类型");
            }

        }

        protected void lvEQType_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            //lvEQType.EditIndex = e.NewEditIndex;
            //BindList();
            //Response.Redirect(Request.Url.AbsolutePath);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvEQType_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                //找到参数控件
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                DropDownList ddlMarkType = (DropDownList)e.Item.FindControl("ddlMarkType");
                DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");
                if (txtTitle == null || string.IsNullOrEmpty(txtTitle.Text))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('必填项不能为空！');", true);
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("试题类型");
                if (list != null)
                {
                    //判断是否存在改分类
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                    + txtTitle.Text.Trim() + "</Value></Eq></Where>";
                    SPListItemCollection typelist = list.GetItems(query);
                    if (typelist != null && typelist.Count > 0)
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增失败，该类型已存在！');", true);
                        return;
                    }
                    else
                    {
                        SPListItem item = list.AddItem();
                        item["Title"] = txtTitle.Text;
                        if (ddlMarkType != null && !string.IsNullOrEmpty(ddlMarkType.SelectedValue))
                        {
                            item["QType"] = ddlMarkType.SelectedValue;
                        }
                        if (ddlStatus != null && !string.IsNullOrEmpty(ddlStatus.SelectedValue))
                        {
                            item["Status"] = ddlStatus.SelectedValue;
                        }
                        item.Update();
                        DataBind();
                        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增成功！');", true);

                        Response.Redirect(Request.Url.AbsolutePath);
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_QuestionTypeManager_试题类型管理新增试题类型");
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvEQType_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            //取消编辑
            if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            {
                //e.Cancel = true;
                lvEQType.EditIndex = -1;
                BindList();
                Response.Redirect(Request.Url.AbsolutePath);
            }
            else if (e.CancelMode == ListViewCancelMode.CancelingInsert)
            {
                BindList();
                Response.Redirect(Request.Url.AbsolutePath);
                return;
            }
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lvEQType_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {

                int KeyId = Convert.ToInt32(lvEQType.DataKeys[e.ItemIndex].Value);
                TextBox txtTitle = (TextBox)lvEQType.Items[e.ItemIndex].FindControl("txtTitle");
                DropDownList ddlMarkType = (DropDownList)lvEQType.Items[e.ItemIndex].FindControl("ddlMarkType");
                DropDownList ddlStatus = (DropDownList)lvEQType.Items[e.ItemIndex].FindControl("ddlStatus");
                if (txtTitle == null || string.IsNullOrEmpty(txtTitle.Text))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('必填项不能为空！');", true);
                }
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb("Examination");
                SPList list = web.Lists.TryGetList("试题类型");
                if (list != null)
                {

                    SPListItem item = list.GetItemById(KeyId);
                    item["Title"] = txtTitle.Text;
                    if (ddlMarkType != null && !string.IsNullOrEmpty(ddlMarkType.SelectedValue))
                    {
                        item["QType"] = ddlMarkType.SelectedValue;
                    }
                    if (ddlStatus != null && !string.IsNullOrEmpty(ddlStatus.SelectedValue))
                    {
                        item["Status"] = ddlStatus.SelectedValue;
                    }
                    item.Update();
                    lvEQType.EditIndex = -1;
                    DataBind();
                    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('修改成功！');", true);

                    Response.Redirect(Request.Url.AbsolutePath);

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_QuestionTypeManager_试题类型管理修改试题类型");
            }
        }


        protected void lvEQType_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item != null)
            {

                DropDownList uddlMarkType = e.Item.FindControl("ddlMarkType") as DropDownList;
                if (uddlMarkType != null)
                {
                    //选中当前类型
                    HiddenField MarkType = e.Item.FindControl("MarkType") as HiddenField;
                    uddlMarkType.SelectedValue = MarkType.Value;
                }
                DropDownList ddlTemplate = e.Item.FindControl("ddlTemplate") as DropDownList;
                if (ddlTemplate != null)
                {
                    //选中当前模板类型
                    HiddenField Template = e.Item.FindControl("Template") as HiddenField;
                    ddlTemplate.SelectedValue = Template.Value;
                }

                DropDownList ddlStatus = e.Item.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    //选中当前状态
                    HiddenField Status = e.Item.FindControl("Status") as HiddenField;
                    ddlStatus.SelectedValue = Status.Value;
                }
            }
        }*/
    }
}
