using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_Examine
{
    public partial class RR_wp_ExamineUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["ResourcesType"] == "" | Request["ResourcesType"] == null)
                {
                    BindExamine("公共资源");
                }
                else
                {
                    BindExamine(Request["ResourcesType"].safeToString());
                    if (LinkButton1.Text == Request["ResourcesType"].safeToString())
                    {
                        LinkButton1.CssClass = "Enable";
                        //LinkButton2.CssClass = "";
                        LinkButton3.CssClass = "";
                    }
                    //if (LinkButton2.Text == Request["ResourcesType"].safeToString())
                    //{
                    //    LinkButton1.CssClass = "";
                    //    LinkButton2.CssClass = "Enable";
                    //    LinkButton3.CssClass = "";

                    //}
                    if (LinkButton3.Text == Request["ResourcesType"].safeToString())
                    {
                        LinkButton1.CssClass = "";
                        //LinkButton2.CssClass = "Enable";
                        LinkButton3.CssClass = "Enable";
                    }
                }
            }
        }

        private void BindExamine(string Type)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "ResourcesType", "ResourcesTypeId", "TimeInterval", "WeekData", "BelongSchool", "ContactPhone", "AuditStatus", "Data", };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPQuery query = new SPQuery();
                        string strQuery = CAML.Eq(CAML.FieldRef("AuditStatus"), CAML.Value("待审批"));
                        if (Type == "公共资源")
                        {
                            strQuery = "<And>" + strQuery + "<Or><Eq><FieldRef Name=\"ResourcesType\" /><Value Type=\"Text\">车辆</Value></Eq><Eq><FieldRef Name=\"ResourcesType\" /><Value Type=\"Text\">会议室</Value></Eq></Or></And>";
                        }
                        else
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value(Type))), strQuery);
                        }
                        query.Query = "<Where>" + strQuery + "</Where>";
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString();
                            dr["ResourcesType"] = item["ResourcesType"].SafeToString();
                            dr["ResourcesTypeId"] = item["ResourcesTypeId"].SafeToString();
                            dr["TimeInterval"] = item["TimeInterval"].SafeToString();
                            dr["WeekData"] = item["Title"].SafeToString();
                            dr["BelongSchool"] = item["BelongSchool"].SafeToString();
                            dr["ContactPhone"] = item["ContactPhone"].SafeToString();
                            dr["AuditStatus"] = item["AuditStatus"].SafeToString();
                            dr["Data"] = item["Data"].SafeToDataTime();
                            dt.Rows.Add(dr);
                        }
                        LV_PublishSouce.DataSource = dt;
                        LV_PublishSouce.DataBind();
                        //LV_OffRoom.DataSource = dt;
                        //LV_OffRoom.DataBind();
                        //LV_ClassRoom.DataSource = dt;
                        //LV_ClassRoom.DataBind();
                        if (dt.Rows.Count < DP_PublishSouce.PageSize)
                        {
                            this.DP_PublishSouce.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_Examine.ascx_BindExamine");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            BindExamine("公共资源");
            LinkButton1.CssClass = "Enable";
            //LinkButton2.CssClass = "";
            LinkButton3.CssClass = "";
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            BindExamine("资产管理");
            LinkButton1.CssClass = "";
            //LinkButton2.CssClass = "Enable";
            LinkButton3.CssClass = "";

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            BindExamine("专业教室");
            LinkButton1.CssClass = "";
            //LinkButton2.CssClass = "";
            LinkButton3.CssClass = "Enable";

        }

        protected void LV_PublishSouce_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Exit")
            {
                string url = "/SitePages/ExamineItem.aspx?itemid=&ResourcesType=" + e.CommandArgument;
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> editpdetail(this,'审核资源','" + url + "','800','500');</script>");

            }
        }
    }
}
