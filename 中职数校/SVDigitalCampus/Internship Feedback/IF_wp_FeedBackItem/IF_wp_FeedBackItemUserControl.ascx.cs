using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackItem
{
    public partial class IF_wp_FeedBackItemUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string rootUrl = SPContext.Current.Web.Url;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListView();
            }
        }
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Code", "Title", "Type", "Result", "Sorts", "ID" });
                        SPList termList = oWeb.Lists.TryGetList("企业反馈表");
                        SPQuery query = new SPQuery();

                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Code"] = item["Code"];
                                dr["Title"] = item["Title"];
                                dr["Type"] = item["Type"];
                                dr["Result"] = item["Result"];
                                dr["Sorts"] = item["Sorts"];
                                dr["ID"] = item["ID"];

                                dt.Rows.Add(dr);
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackItemUserControl.ascx_BindListView");
            }

        }       
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                EditID.Text = ID.ToString();
                SPWeb web = SPContext.Current.Web;
                SPList termList = web.Lists.TryGetList("企业反馈表");


                SPListItem newItem = termList.GetItemById(Convert.ToInt32(ID));
                if (newItem != null)
                {
                    lbCode.Value = newItem["Code"].ToString();
                    lbNewName.Value = newItem["Title"].ToString();

                    Type.SelectedValue = newItem["Type"].ToString();
                    Sorts.Value = newItem["Sorts"].ToString();
                    if (newItem["Result"] != null)
                    {
                        Result.Value = newItem["Result"].ToString();
                    }
                }
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>showDiv('Editdiv', 'Edit_head');</script>");
            }
        }
        //修改
        protected void Edit_Click(object sender, EventArgs e)
        {
            if (Sorts.Value.Trim().Length > 0 && lbNewName.Value.Trim().Length > 0)
            {

                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("企业反馈表");
                string ID = EditID.Text;

                SPListItem newItem = list.Items.GetItemById(Convert.ToInt32(ID));
                newItem["Title"] = lbNewName.Value;
                newItem["Result"] = Result.Value;
                newItem["Type"] = Type.SelectedValue;
                newItem["Sorts"] = Sorts.Value;

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改成功！');", true);

                newItem.Update();
                BindListView();
            }
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        /*
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildDataTable();
                        SPList termList = oWeb.Lists.TryGetList("企业反馈表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<OrderBy><FieldRef Name='Sorts' Ascending='True' /></OrderBy>";
                        SPListItemCollection termItems = termList.GetItems(query);

                        if (termItems != null)
                        {
                            int j = 0;
                            int i = 1;

                            if (i == 1)
                            {
                                string type = termItems[j]["Type"].ToString();
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";

                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString());

                                j++;
                                FeedBack.Rows[i].Cells[2].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[3].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString());

                                j++;
                                FeedBack.Rows[i].Cells[4].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[5].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString());

                                j++;
                                i++;
                            }
                            if (i > 1 && i < 5)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString());
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString());//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString());//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                            }
                            if (i > 4 && i < 7)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString();
                                i++;
                                FeedBack.Rows[i].Cells[0].InnerHtml = "<textarea type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value='' cols='20' rows='2'  style=\"width:510px; height:180px;\"/></textarea>";
                            }


                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackItemUserControl.ascx_BindListView");
            }
            this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>showDiv('Viewdiv', 'View_head');</script>");
        }
        private string result(SPListItem item, int width, string type)
        {
            if (type == "输入")
            {
                return "<input type=\"text\" id=\"" + item["Code"].ToString() + "\" name=\"" + item["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:" + width + "px;\"/>";
            }
            else
            {
                string returnResult = "";
                string result = item["Result"].ToString();
                string[] str = result.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    returnResult += "<input name=\"" + item["Code"].ToString() + "\" type=\"radio\"/>" + str[i] + "";
                }
                return returnResult;
            }
        }
        private void AttrTr(int i, int j, SPListItemCollection termItems)
        {
            FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
            FeedBack.Rows[i].Cells[1].InnerHtml = "<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:100px;\"/>";

        }
        */
    }
}
