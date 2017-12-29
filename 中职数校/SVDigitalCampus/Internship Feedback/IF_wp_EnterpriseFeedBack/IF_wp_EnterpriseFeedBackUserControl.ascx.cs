using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
namespace SVDigitalCampus.Internship_Feedback.IF_wp_EnterpriseFeedBack
{
    public partial class IF_wp_EnterpriseFeedBackUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EnterId"] != null)
                {
                    BindListView(Convert.ToInt32(Label1.Text));
                }
                else
                {
                    Response.Write("<script>alert('登录超时或未登录！');window.location.href='" + SPContext.Current.Web.Url + "/SitePages/Login.aspx';</script>");
                }
            }
        }
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

        #region 数据绑定
        private void BindListView(int IsCompleate)
        {
            UserPhoto user = new UserPhoto();

            DataTable dtAll = new DataTable();
            string[] arrs = new string[] { "Title", "Sex", "EJob", "ID", "StuID", "Created", "EnterID", "IsCompleate" };
            foreach (string column in arrs)
            {
                dtAll.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string StuID = "";
                        SPList listEnter = oWeb.Lists.TryGetList("企业信息");
                        SPQuery qEn = new SPQuery();
                        qEn.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Session["EnterId"].ToString() + "</Value></Eq></Where>";
                        SPListItemCollection itemlist = listEnter.GetItems(qEn);
                        string EnterID = itemlist[0]["ID"].ToString();

                        SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><And><Eq><FieldRef Name='IsDel' /><Value Type='Text'>0</Value></Eq><Eq><FieldRef Name='IsCompleate' /><Value Type='Text'>" +
                            IsCompleate + "</Value></Eq></And><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + EnterID + "</Value></Eq></And></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems.Count > 0)
                        {                                //学生信息
                            //SPList termList1 = oWeb.Lists.TryGetList("学生信息表");

                            //DataTable dt2 = termList1.Items.GetDataTable();
                            DataTable dt2 = user.GetStudentInfoByWhere("", "", -1, -1, "");

                            if (txtStudent.Value != "")
                            {
                                DataTable dt5 = user.GetStudentInfoByWhere("", txtStudent.Value.Trim(), -1, -1, "");

                                //SPQuery studentq = new SPQuery();
                                //studentq.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + txtStudent.Value.Trim() + "</Value></Eq></Where>";
                                //SPListItemCollection co = termList1.GetItems(studentq);
                                if (dt5.Rows.Count > 0)
                                {
                                    if (dt5.Rows[0]["XM"].ToString() == txtStudent.Value.Trim())
                                    {
                                        StuID = dt5.Rows[0]["SFZJH"].ToString();
                                    }
                                }
                                else
                                {
                                    StuID = "0";
                                }
                            }

                            //反馈信息
                            DataTable dt1 = termItems.GetDataTable();
                            //当前登录的企业信息
                            SPList List3 = oWeb.Lists.TryGetList("企业岗位信息");
                            DataTable dt3 = List3.Items.GetDataTable();
                            DataRow[] drs = null;
                            if (StuID != "")
                            {
                                drs = dt1.Select("StuID =" + StuID);
                            }
                            else
                            {
                                drs = dt1.Select();
                            }
                            foreach (DataRow dr in drs)
                            {
                                DataRow drnew = dtAll.NewRow();
                                drnew["ID"] = dr["ID"];
                                drnew["StuID"] = dr["StuID"];
                                drnew["Created"] = dr["Created"];
                                drnew["EnterID"] = dr["EnterID"];
                                drnew["IsCompleate"] = dr["IsCompleate"];

                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    string stuid = dr["StuID"].ToString();
                                    string ID = dr2["SFZJH"].ToString();
                                    if (stuid == ID)
                                    {
                                        drnew["Title"] = dr2["XM"];
                                        drnew["Sex"] = dr2["XBM"];
                                    }
                                }
                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    string Job = dr["Job"].ToString();
                                    string jobid = dr3["ID"].ToString();
                                    if (Job == jobid)
                                    {
                                        drnew["EJob"] = dr3["Title"];
                                    }
                                }
                                dtAll.Rows.Add(drnew);
                            }

                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "StudentListUserControl.ascx_BindListView");
            }
            LV_TermList.DataSource = dtAll;
            LV_TermList.DataBind();
        }

        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "SName", "SSex", "Major", "EnterID", "Created", "ID", "IsfeedBack", "StuID" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView(1);
        }
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                string SName = ((Label)e.Item.FindControl("lbTitle")).Text;
                string Ssex = ((Label)e.Item.FindControl("lbSex")).Text;
                string Job = ((Label)e.Item.FindControl("lbJob")).Text;
                string EnterID = ((Label)e.Item.FindControl("lbEnterID")).Text;
                string EName = EnterName(EnterID);

                //Hashtable ht = new Hashtable();
                //ht.Add("Sname", SName);
                //ht.Add("Ssex", Ssex);
                //ht.Add("Professional", Job);
                //ht.Add("EnterName", EName);

                //string Stuid = ((Label)e.Item.FindControl("lbstuID")).Text;
                //lbenter.Text = EName;
                //lbStuID.Text = Stuid;
                //lbID.Text = ((Label)e.Item.FindControl("lbID")).Text;
                string id = ((Label)e.Item.FindControl("lbID")).Text;
                string url = SPContext.Current.Web.Url + "/SitePages/IF_wp_FeedBackModol.aspx?SName=" + HttpUtility.UrlEncode(SName) + "&Ssex=" + HttpUtility.UrlEncode(Ssex) + "&Job=" + HttpUtility.UrlEncode(Job) + "&EnterID=" + id + "&EnterName=" + HttpUtility.UrlEncode(EName);

                if (e.CommandName == "Edit")
                {
                    btOK.Visible = true;
                    // BindTitle(ht);
                    url += "&Type=1&StuID=" + ((Label)e.Item.FindControl("lbstuID")).Text + "&" + DateTime.Now;

                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript",
                        //"<script> popWin.showWin('600', '578', '填写反馈单', '" + url + "', 'no');</script>");
                        "<script> $.webox({height: 578,width: 600,bgvisibel: true,title: '填写反馈单',iframe: '" + url + "'});</script>");

                    //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), 'myscript", "<script>showDiv('Editdiv', 'Edit_head');</script>");
                }
                if (e.CommandName == "View")
                {
                    //SPWeb oWeb = SPContext.Current.Web;
                    //SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                    //SPQuery query = new SPQuery();
                    //query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + lbID.Text + "</Value></Eq></Where>";

                    //SPListItemCollection newItem = termList.GetItems(query);
                    //string title = newItem[0]["Title"].ToString();
                    //if (newItem != null)
                    //{
                    //    ht.Add("Content", newItem[0]["Content"].ToString());
                    //    ht.Add("Result", newItem[0]["Title"].ToString());
                    //    ht.Add("Identify", newItem[0]["Identify"].ToString());
                    //    BindTitle(ht);
                    //}
                    btOK.Visible = false;
                    //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>showDiv('Editdiv', 'Edit_head');</script>");
                    url += "&Type=2&" + DateTime.Now;
                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript",
                        //"<script> popWin.showWin('600', '578', '查看反馈单', '" + url + "', 'no');</script>");
                       "<script> $.webox({height: 578,width: 600,bgvisibel: true,title: '查看反馈单',iframe: '" + url + "'});</script>");

                }
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "StudentListUserControl.ascx_LV_TermList_ItemCommand");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        #region 根据ID返回企业名称
        private string EnterName(string EnterID)
        {
            string EnterName = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();

                        SPListItem newItem = termList.GetItemById(Convert.ToInt32(Convert.ToInt32(EnterID)));
                        if (newItem != null)
                        {
                            EnterName = newItem["Title"].ToString();
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseFeedBackUserControl.ascx_BindListView");
            }
            return EnterName;
        }
        #endregion
        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item is ListViewDataItem)
            {
                string IsfeedBack = ((Label)e.Item.FindControl("IsCompleate")).Text;
                if (IsfeedBack == "未反馈")
                {
                    ((LinkButton)e.Item.FindControl("lbEdit")).Visible = true;
                    ((LinkButton)e.Item.FindControl("lbView")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lbEdit")).Visible = false;
                    ((LinkButton)e.Item.FindControl("lbView")).Visible = true;
                }
            }
        }
        #endregion

        #region 查 改 增
        //查询
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindListView(Convert.ToInt32(Label1.Text));
        }

        //提交反馈
        protected void btOK_Click(object sender, EventArgs e)
        {
            UserPhoto user = new UserPhoto();

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string stuID = lbStuID.Text.ToString();
                        string ID = lbID.Text.ToString();
                        SPList BackList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPListItem newItem = BackList.GetItemById(Convert.ToInt32(ID));// BackList.Items.Add();
                        newItem["Title"] = Request["Result"].ToString();
                        newItem["Content"] = Request["Content"].ToString();
                        newItem["Identify"] = Request["Identify"].ToString();
                        newItem["IsCompleate"] = "1";
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('反馈表填写成功！');", true);

                        newItem.Update();
                        user.SetStudentFP_FK(1, 0, stuID);

                        //SPList StudentList = oWeb.Lists.TryGetList("学生信息表");

                        //SPListItem StudentItem = StudentList.Items.GetItemById(Convert.ToInt32(stuID));//lbStuID.Text));
                        //StudentItem["IsfeedBack"] = "1";
                        //StudentItem["IsAssign"] = "0";
                        //StudentItem.Update();

                        //SPList fenpeiList = oWeb.Lists.TryGetList("实习反馈结果表");
                        BindListView(0);
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "FeedPaperUserControl.ascx_BindListView");
            }
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        #endregion'

        #region 标签选择 绑定数据
        //未反馈标签
        protected void btN_Click(object sender, EventArgs e)
        {
            Label1.Text = "0";
            BindListView(0);
            btN.CssClass = "Enable";
            btY.CssClass = "Disable";
        }
        //已反馈标签vie
        protected void btY_Click(object sender, EventArgs e)
        {
            Label1.Text = "1";
            BindListView(1);
            btN.CssClass = "Disable";
            btY.CssClass = "Enable";
        }
        #endregion
    }
}
