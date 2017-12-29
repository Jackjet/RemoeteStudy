using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityDetailShow
{
    public partial class SA_wp_ActivityDetailShowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindActivityDetail(itemId);
                }
            }
        }
        private void BindActivityDetail(string actid)
        {
            try
            {
                int itemId = Convert.ToInt32(actid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.Lit_Title.Text = item.Title;
                        this.Lit_Date.Text = item["BeginDate"].SafeToDataTime() + "-" + item["EndDate"].SafeToDataTime();
                        this.Lit_Address.Text = item["Address"].SafeToString();
                        SPQuery memQuery = new SPQuery() { Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(item.ID))) };
                        if (item["Type"].SafeToString() == "招新")
                        {
                            this.Lit_Count.Text = oWeb.Lists.TryGetList("招新申请").GetItems(memQuery).Count.ToString();
                        }
                        else
                        {
                            SPListItemCollection proItems = oWeb.Lists.TryGetList("活动项目").GetItems(memQuery);
                            List<string> memNames = new List<string>(); //参加该活动的人
                            foreach (SPListItem proitem in proItems)
                            {
                                string[] proMems = proitem["JoinMembers"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 1; i < proMems.Length; i = i + 2)
                                {
                                    if (!memNames.Contains(proMems[i]))
                                    {
                                        memNames.Add(proMems[i]);
                                    }
                                }
                            }
                            this.Lit_Count.Text = memNames.Count.ToString();
                        } 
                        this.P_Introduction.InnerHtml = item["Introduction"].SafeToString().SafeLengthToString(248);
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.Img_Activity.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.Img_Activity.ImageUrl = @"/_layouts/15/Stu_images/zs28.jpg";
                        }
                        BindActivityProjects(actid, item["Type"].SafeToString());
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityDetailShow_BindActivityDetail");
            }
        }

        private void BindActivityProjects(string actid,string type)
        {
            try
            {
                int itemId = Convert.ToInt32(actid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "JoinMembers","hasRegist"};
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPUser curUser = SPContext.Current.Web.CurrentUser;
                        int registCount = 0;
                        if (type == "招新")
                        {
                            SPList reclist = oWeb.Lists.TryGetList("招新申请");                           
                            SPListItemCollection recItems = reclist.GetItems(new SPQuery()
                            {
                                Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(itemId)))
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                            });
                            DataRow recRow = dt.NewRow();
                            recRow["ID"] = itemId;
                            recRow["hasRegist"] = "no";  
                            List<string> recNames = new List<string>(); //招新申请学生                
                            foreach (SPListItem recitem in recItems)
                            {
                                string applyStr = recitem["Author"].SafeToString();                               
                                int userId = Convert.ToInt32(applyStr.Substring(0, applyStr.IndexOf(";#")));
                                string userName = applyStr.SafeLookUpToString();
                                if (!recNames.Contains(userName))
                                {
                                    if (userId.ToString() == curUser.ID.ToString())
                                    {
                                        recRow["hasRegist"] = "yes";
                                        registCount++;
                                    }
                                    recNames.Add(userName);
                                }
                            }
                            recRow["JoinMembers"] = recNames.Count == 0 ? "暂无" : string.Join("  ", recNames.ToArray());
                            recRow["Title"] = "招新" + " ( " + recNames.Count + "个人 )";
                            dt.Rows.Add(recRow);
                        }
                        else
                        {
                            SPList prolist = oWeb.Lists.TryGetList("活动项目");                            
                            SPListItemCollection proitems = prolist.GetItems(new SPQuery()
                            {
                                Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(actid)))
                            });
                            foreach (SPListItem proitem in proitems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = proitem.ID.SafeToString();
                                dr["Title"] = proitem.Title.SafeToString();
                                dr["hasRegist"] = "no";                                
                                string[] members = proitem["JoinMembers"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                List<string> memNames = new List<string>(); //参加该项目的人
                                for (int i = 1; i < members.Length; i = i + 2)
                                {
                                    if (members[i - 1].ToString() == curUser.ID.ToString())
                                    {
                                        dr["hasRegist"] = "yes";
                                        registCount++;
                                    }
                                    memNames.Add(members[i]);
                                }
                                string inNamesStr = string.Join("  ", memNames.ToArray());
                                dr["JoinMembers"] = memNames.Count == 0 ? "暂无" : "<span style='color:#28a907'>( " + memNames.Count + "个人 )</span> " + inNamesStr;                               
                                dt.Rows.Add(dr);
                            }
                        }                        
                        LV_ProjectList.DataSource = dt;
                        LV_ProjectList.DataBind();
                        this.HF_RegistCount.Value = registCount.ToString();
                        foreach (ListViewItem viewitem in LV_ProjectList.Items)
                        {
                            HiddenField hf_hasRegist = viewitem.FindControl("HF_hasRegist") as HiddenField;
                            LinkButton lbJoin = viewitem.FindControl("JoinPro") as LinkButton;

                            if (hf_hasRegist.Value == "yes")
                            {
                                lbJoin.Enabled = false;
                                lbJoin.Text = "已报名";
                                lbJoin.BackColor = System.Drawing.Color.LightGray;// "#fafafa";

                            }
                            else
                            {
                                lbJoin.Enabled = true;
                            }                            
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityDetailShow_BindActivityProjects");
            }
        }

        protected void LV_ProjectList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = "alert('报名成功');";
            try
            {
                int actid = Convert.ToInt32(ViewState["itemid"].ToString());
                int proId = Convert.ToInt32(e.CommandArgument.SafeToString());
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (e.CommandName.Equals("Join"))
                        {
                            SPListItem actItem = oWeb.Lists.TryGetList("活动信息").GetItemById(actid);
                            if (Convert.ToDateTime(actItem["BeginDate"])> DateTime.Today)
                            {
                                script = "alert('报名还未开始');";
                                return;
                            }
                            if (Convert.ToDateTime(actItem["EndDate"]) < DateTime.Today)
                            {
                                script = "alert('报名已经结束');";
                                return;
                            }  
                            if (actItem["Type"].SafeToString()!= "招新"&&Convert.ToInt32(this.HF_RegistCount.Value) == Convert.ToInt32(actItem["MaxCount"].SafeToString()))
                            {
                                script="alert('最多报名" + this.HF_RegistCount.Value + "个项目');";
                                return;
                            }
                            SPUser curUser = SPContext.Current.Web.CurrentUser;
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, curUser.ID, curUser.Name);
                            if (actItem["Type"].SafeToString() == "招新")
                            {
                                SPList memList = oWeb.Lists.TryGetList("部门成员");
                                SPListItemCollection memitems = memList.GetItems(new SPQuery()
                                {
                                    Query = CAML.Where(CAML.And(
                                                      CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", curUser.Name)),
                                                      CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(actItem["DepartmentID"].SafeToString()))))
                                });
                                if (memitems.Count == 0) 
                                {
                                    script = "openPage('报名加入', '/SitePages/RecruitApply.aspx?itemid=" + actid + "&departid=" + actItem["DepartmentID"].SafeToString() + "&flag=0', '650', '460');";
                                    return;
                                }
                                else
                                {
                                    string departName = oWeb.Lists.TryGetList("学生会组织机构").GetItemById(Convert.ToInt32(actItem["DepartmentID"].SafeToString())).Title;
                                    script = "alert('你已经是" + departName + "的成员了！');";
                                    return;
                                }
                            }
                            else
                            {
                                #region 活动项目
                                SPList list = oWeb.Lists.TryGetList("活动项目");
                                SPListItem item = list.GetItemById(proId);
                                string member = item["JoinMembers"].SafeToString();                               
                                string joinStr = curUser.ID + ";#" + curUser.Name;
                                if (!string.IsNullOrEmpty(member))
                                {
                                    string[] members = member.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                    bool hasRegist = false;
                                    for (int i = 0; i < members.Length - 1; i = i + 2)
                                    {
                                        if (curUser.ID.ToString() == members[i].ToString())
                                        {
                                            hasRegist = true;
                                            break;
                                        }
                                    }
                                    member = hasRegist ? member : member + ";#";
                                    joinStr = hasRegist ? string.Empty : curUser.ID + ";#" + curUser.Name;
                                }
                                item["JoinMembers"] = member + joinStr;
                                item.Update();
                                #endregion
                            }
                            BindActivityDetail(ViewState["itemid"].ToString());
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('报名失败，请重试...')";
                com.writeLogMessage(ex.Message, "SA_wp_ActivityDetailShow_LV_ProjectList_ItemCommand");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        protected void LV_ProjectList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
    }
}
