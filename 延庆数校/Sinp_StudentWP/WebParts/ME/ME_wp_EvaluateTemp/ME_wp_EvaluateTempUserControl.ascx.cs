using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp
{
    public partial class ME_wp_EvaluateTempUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("考评模板", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTypeAndList();
        }
        private void BindTypeAndList()
        {
            try
            {
                if (!IsPostBack)
                {
                    #region 学期
                    SPList xqList= ListHelp.GetCureenWebList("学期信息", true);
                    SPListItemCollection xqItems = xqList.GetItems();
                    for (int i = 0; i < xqItems.Count;i++)
                    {
                        string staDate = xqItems[i]["StartDate"].SafeToDataTime();
                        string endDate = xqItems[i]["EndDate"].SafeToDataTime();
                        this.DDL_LearnYear.Items.Add(new ListItem(xqItems[i].Title, staDate + "/" + endDate));
                        if (DateTime.Today >= Convert.ToDateTime(staDate) && DateTime.Today <= Convert.ToDateTime(endDate))
                        {
                            this.DDL_LearnYear.SelectedIndex = i;
                        }
                    }
                    #endregion
                    #region 适用对象
                    this.DDL_Type.Items.Add(new ListItem("不限", "不限"));
                    SPField fieldPrizeGrade = CurrentList.Fields.GetField("Type");
                    SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                    foreach (string type in choicePrizeGrade.Choices)
                    {
                        this.DDL_Type.Items.Add(new ListItem(type, type));
                    }       
                    #endregion
                }
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Title", "Content", "Type", "Status", "Date","Enable","Show" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                string[] date = this.DDL_LearnYear.SelectedValue.Split('/');
                DateTime start = Convert.ToDateTime(date[0]);
                DateTime end = Convert.ToDateTime(date[1]);
                string where = @"<And>
                                                <Gt>
                                                <FieldRef Name='Modified' />
                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(start) + @"</Value>
                                                </Gt>
                                                <Lt>
                                                <FieldRef Name='Modified' />
                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(end) + @"</Value>
                                                </Lt>
                                            </And>";
                if (this.DDL_Type.SelectedValue != "不限")
                {
                    where = @"<And><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + "</Value></Eq>" + where + "</And>";
                }
                SPQuery query = new SPQuery { Query = "<Where>" + where + "</Where><OrderBy><FieldRef Name='Modified' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = CurrentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Title"] = item.Title;
                    string content=item["Content"].SafeToString();
                    dr["Content"] = content.Length>28?content.Substring(0,28)+"...":content;
                    string status=item["Status"].SafeToString();
                    dr["Status"] = status;
                    dr["Type"] = item["Type"];
                    dr["Date"] = item["Modified"].SafeToDataTime();
                    if (status == "归档")
                    {
                        dr["Show"] = "none";
                    }
                    else
                    { 
                        dr["Show"] = "block";
                        if (status == "禁用")
                            dr["Enable"] = "启用";
                        else
                            dr["Enable"] = "禁用";
                    }
                    dt.Rows.Add(dr);
                }
                lvEvaluateTemp.DataSource = dt;
                lvEvaluateTemp.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTempUserControl_BindTypeAndList");
            }
        } 
        protected void Edit_Click(object sender, EventArgs e)
        {
            string stritem = this.ItemId.Value;
            if (!string.IsNullOrEmpty(stritem))
            {
                string[] arr = stritem.Split('_');
                int itemId = Convert.ToInt32(arr[1]);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("考评模板");
                        SPListItem item = list.GetItemById(itemId);
                        item["Status"] = arr[0];
                        item.Update();
                    }
                }, true);
                BindTypeAndList();
            }
        }
        protected void DDL_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTypeAndList();
        }

        protected void DDL_LearnYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DDL_Type.SelectedValue = "不限";
            BindTypeAndList();
        }
    }
}
