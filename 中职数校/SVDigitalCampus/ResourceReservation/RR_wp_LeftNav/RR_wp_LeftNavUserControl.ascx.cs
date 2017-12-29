using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_LeftNav
{
    public partial class RR_wp_LeftNavUserControl : UserControl
    {
        SPWeb web = SPContext.Current.Web;
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

            //string controlName = Request["__EVENTTARGET"];
            //string eventArgument = Request["__EVENTARGUMENT"];
            //if (!string.IsNullOrEmpty(controlName) && !string.IsNullOrEmpty(eventArgument))
            //{
            //    if (controlName.Equals("LoadData"))
            //    {
            //        ViewState["LoadData"] = eventArgument;
            //        BindListView();
            //    }
            //}
            

            //if (!IsPostBack)
            //{
            //    ViewState["IsSearch"] = false;
            //    BindListView();
            //}
            
        }

        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "ResourcesType", "Created", "qStatus", "jStatus" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = AppendQuery();
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(item["Title"].SafeToString(), 12) : item["Title"].SafeToString();
                            dr["ResourcesType"] = item["ResourcesType"].SafeToString();
                            dr["Created"] = item["Created"].SafeToDataTime();
                            //dr["Status"] = item["Status"].SafeToString();
                            dr["qStatus"] = item["Status"].ToString() == "启用" ? "Enable" : "Disable";
                            dr["jStatus"] = item["Status"].ToString() == "启用" ? "Disable" : "Enable";
                            dt.Rows.Add(dr);
                        }
                        //LV_Base.DataSource = dt;
                        //LV_Base.DataBind();
                        //if(dt.Rows.Count<=DP_Base.PageSize)
                        //{
                        //    DP_Base.Visible = false;
                        //}
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_LeftNav.ascx_BindListView");
            }
        }


        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value("0"));

            //if (!TB_Search.Text.Trim().Equals(string.Empty) && Convert.ToBoolean(ViewState["IsSearch"]))
            //{
            //    strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(TB_Search.Text.Trim()))), strQuery);
            //}
            //if (ViewState["LoadData"]!=null)
            //{
            //    strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ResourcesTypeId"), CAML.Value(ViewState["LoadData"].SafeToString()))), strQuery);
            //}
            strQuery += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            query.Query = CAML.Where(strQuery);
            return query;
        }

        protected void LV_Base_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //DP_Base.SetPageProperties(DP_Base.StartRowIndex, e.MaximumRows, false);
            //BindListView();
        }

        protected void LV_Base_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string result = "alert('操作成功')";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int tempid = Convert.ToInt32(e.CommandArgument.ToString());
                        
                        web.AllowUnsafeUpdates = true;
                        SPListItem item = web.Lists.TryGetList("专业教室资源表").GetItemById(tempid);
                        if (e.CommandName == "del")
                        {

                            item.Delete();
                            BindListView();
                        }
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                result = "alert('操作失败')";
                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_TempListView_PreRender");
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), result, true);
        }

        protected void LB_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = true;
            BindListView();
        }

        protected void LB_Enable_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            string operate = lb.CommandName;
            
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int itemid = Convert.ToInt32(lb.CommandArgument);


                        SPListItem item = web.Lists.TryGetList("专业教室资源表").GetItemById(itemid);
                        if (operate == "Enable")
                        {
                            item["Status"] = "禁用";
                            
                        }
                        else
                        {
                            item["Status"] = "启用";
                        }
                        item.Update();
                        BindListView();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                
                com.writeLogMessage(ex.Message, "RR_wp_LeftNavigation");
            }
        }
    }
}
