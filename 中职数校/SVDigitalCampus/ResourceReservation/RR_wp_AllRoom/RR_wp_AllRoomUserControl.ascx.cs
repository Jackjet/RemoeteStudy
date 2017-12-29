using Common;
using Microsoft.SharePoint;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_AllRoom
{
    public partial class RR_wp_AllRoomUserControl : UserControl
    {

        LogCommon com = new LogCommon();

        
        public string ServerUrl
        {
            get
            {
                if (ViewState["ServerUrl"] == null)
                {
                    ViewState["ServerUrl"] = ConfigurationManager.AppSettings["ServerUrl"];
                }
                return ViewState["ServerUrl"].SafeToString();
            }
            set
            {
                ViewState["ServerUrl"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                BindPersonalListView();
            }
        }



        private void BindPersonalListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Address", "Area", "OpenTime", "CloseTime", "LimitCount", "imgSource", "Url" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value("专业教室")),
                                CAML.Eq(CAML.FieldRef("Status"), CAML.Value("启用"))
                            )
                            );
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(item["Title"].SafeToString(), 12) : item["Title"].SafeToString();
                            dr["Address"] = item["Address"].SafeToString();
                            dr["Area"] = item["Area"].SafeToString();
                            dr["OpenTime"] = item["OpenTime"].SafeToString();
                            dr["CloseTime"] = item["CloseTime"].SafeToString();
                            dr["LimitCount"] = item["LimitCount"].SafeToString();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments.Count > 0)
                            {
                                dr["imgSource"] = attachments.UrlPrefix.Replace(oSite.Url, ServerUrl) + attachments[0].ToString();
                            }
                            else
                            {
                                dr["imgSource"] = "/_layouts/15/SVDigitalCampus/Image/zs28.jpg";
                            }
                            dr["Url"] = ConfigurationManager.AppSettings["ServerUrl"] + "/ResourceReservation/SitePages/ReserveRoom.aspx?resid="
                                + item.ID + "&WeekID=" + Request.QueryString["WeekID"] + "&Type=" + Request.QueryString["Type"] + "&ClassID=" + Request.QueryString["ClassID"];       
                            dt.Rows.Add(dr);
                        }
                        LV_Room.DataSource = dt;
                        LV_Room.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_AllRoom.ascx_BindPersonalListView");
            }
        }


        protected void LV_Room_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Room.SetPageProperties(DP_Room.StartRowIndex, e.MaximumRows, false);
            BindPersonalListView();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Site.Url + "/CouseManage/SitePages/CaurseManage.aspx");
        }

    }
}
