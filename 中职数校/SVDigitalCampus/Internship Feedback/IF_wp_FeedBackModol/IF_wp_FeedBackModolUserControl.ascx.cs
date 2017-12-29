using Common;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using Common.SchoolUser;
namespace SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackModol
{
    public partial class IF_wp_FeedBackModolUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Type"] == "0")
                {
                    btOK.Visible = false;
                    reset.Visible = false;
                    print.Visible = false;
                }
                else if (Request["Type"] == "1")
                {
                    btOK.Visible = true;
                    reset.Visible = true;
                    print.Visible = false;
                }
                else if (Request["Type"] == "2")
                {
                    btOK.Visible = false;
                    reset.Visible = false;
                    print.Visible = true;
                }
                BindTitle(getTable());
            }
        }
        public Hashtable getTable()
        {
            SPWeb oWeb = SPContext.Current.Web;
            Hashtable ht = new Hashtable();
            string Sname = HttpUtility.UrlDecode(Request["SName"]);
            string Job = HttpUtility.UrlDecode(Request["Job"]);
            string EnterName = HttpUtility.UrlDecode(Request["EnterName"]);

            string Ssex = HttpUtility.UrlDecode(Request["Ssex"]);
            lben.Text = EnterName;
            ht.Add("Sname", Sname);
            ht.Add("Ssex", Request["Ssex"]);
            ht.Add("Professional", Job);
            ht.Add("EnterName", EnterName);
            SPList resultList = oWeb.Lists.TryGetList("实习反馈结果表");
            SPQuery ResultQuery = new SPQuery();
            ResultQuery.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + Request["EnterID"] + "</Value></Eq></Where>";

            SPListItemCollection newItem = resultList.GetItems(ResultQuery);
            if (newItem.Count > 0)
            {
                if (newItem[0].Title != null)
                {
                    string title = newItem[0]["Title"] == null ? "" : newItem[0]["Title"].ToString();
                    string Content = newItem[0]["Content"] == null ? "" : newItem[0]["Content"].ToString();
                    string Identify = newItem[0]["Identify"] == null ? "" : newItem[0]["Identify"].ToString();

                    ht.Add("Content", Content);
                    ht.Add("Result", title);
                    ht.Add("Identify", Identify);
                }
            }
            return ht;
        }
        protected void BindPaper()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        Hashtable ht = getTable();

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

                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[2].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[3].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[4].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[5].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                i++;
                            }
                            if (i > 1 && i < 5)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                            }
                            if (i > 4 && i < 7)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j]["Title"].ToString();
                                i++;
                                string value = (string)ht[termItems[j]["Code"].ToString()];

                                FeedBack.Rows[i].Cells[0].InnerHtml = "<textarea type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value='' cols='20' rows='2'  style=\"width:510px; height:180px;\"/>" + value + "</textarea>";
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
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Code", "Title", "Type", "Result", "Sorts", "ID" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        /*
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
        #region 动态拼反馈表
        //动态绑定反馈表
        private void BindTitle(Hashtable ht)
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
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[2].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[3].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[4].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[5].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                i++;
                            }
                            if (i > 1 && i < 5)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);//"<input type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\" runat=\"server\" value=''  style=\"width:441px;\"/>";
                                i++;
                                j++;
                            }
                            if (i > 4 && i < 7)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString();
                                i++;
                                string value = (string)ht[termItems[j]["Code"].ToString()];

                                //FeedBack.Rows[i].Cells[0].InnerHtml = "<textarea type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\"  runat=\"server\" value='" + (string)ht[termItems[j]["Code"].ToString()] + "' cols='20' rows='2'  style=\"width:505px; height:180px;\"/>" + (string)ht[termItems[j]["Code"].ToString()] + "</textarea>";
                                FeedBack.Rows[i].Cells[0].InnerHtml = "<textarea type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\"  runat=\"server\" value='' cols='20' rows='2'  style=\"width:513px; height:180px;\"/>" + value + "</textarea>";

                            }


                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ItemListUserControl.ascx_BindListView");
            }
        }
        /// <summary>
        /// 根据参数拼html
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="width">控件width</param>
        /// <param name="type">控件类型（输入，选择）</param>
        /// <param name="ht">table实例对象</param>
        /// <returns></returns>
        private string result(SPListItem item, int width, string type, Hashtable ht)
        {
            if (type == "输入")
            {
                return "<input type=\"text\" id=\"" + item["Code"].ToString() + "\" name=\"" + item["Code"].ToString() + "\" runat=\"server\" value='" + (string)ht[item["Code"].ToString()] + "'  style=\"width:" + width + "px;\"/>";
            }
            else
            {
                string returnResult = "";
                string result = item["Result"].ToString();
                string[] str = result.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string re = (string)ht[item["Code"].ToString()];
                    string value = item["Code"].ToString();
                    if (str[i] == re)
                    {
                        returnResult += "<input name=\"" + value + "\" checked=\"checked\"  value=\"" + str[i] + "\" type=\"radio\"/>" + str[i] + "";
                    }
                    else
                    {
                        returnResult += "<input name=\"" + value + "\"  value=\"" + str[i] + "\" type=\"radio\"/>" + str[i] + "";
                    }
                }
                return returnResult;
            }
        }
        #endregion

        protected void btOK_Click(object sender, EventArgs e)
        {
            UserPhoto user = new UserPhoto();

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string stuID = Request["StuID"].ToString();
                        string ID = Request["EnterID"].ToString();
                        SPList BackList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPListItem newItem = BackList.GetItemById(Convert.ToInt32(ID));// BackList.Items.Add();
                        newItem["Title"] = Request["Result"].ToString();
                        newItem["Content"] = Request["Content"].ToString();
                        newItem["Identify"] = Request["Identify"].ToString();
                        newItem["IsCompleate"] = "1";
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('反馈表填写成功！');", true);

                        newItem.Update();
                        user.SetStudentFP_FK(1, 0, stuID);


                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('提交成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/FeedBack.aspx';", true);
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "FeedPaperUserControl.ascx_BindListView");
            }
        }
    }
}
