using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_ReserveSource
{
    public partial class RR_wp_ReserveSourceUserControl : UserControl
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
        public RR_wp_ReserveSource ReserveSource { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["Type"] = "车辆";
                ViewState["WeekNum"] = 0;
                Hid_WeekNum.Value = "0";
                Hid_CurrentWeekCount.Value = "0";
                this.Pan_Classroom.Visible = false;
                BindPersonalListView();
                BindClassRoomListView();
                BindAsserts();
                if (!string.IsNullOrEmpty(Request.QueryString["weekNum"]) && !string.IsNullOrEmpty(Request.QueryString["linum"]))
                {
                    Hid_WeekNum.Value = Request.QueryString["weekNum"];
                    Hid_linum.Value = Request.QueryString["linum"];
                    BindPublicSouce(Convert.ToInt32(Request.QueryString["weekNum"]) * 7 + Convert.ToInt32(Request.QueryString["linum"]));
                    BindData(Convert.ToInt32(Request.QueryString["weekNum"]));
                }
                else
                {
                    BindPublicSouce(0);
                    BindData(0);
                }
                
                
                
            }
            

        }

        private void BindAsserts()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Holder", "BelongSchool", "Department", "Status0", "Created" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                                CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value("资产管理"))
                            );
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(item["Title"].SafeToString(), 12) : item["Title"].SafeToString();
                            dr["Holder"] = item["Holder"].SafeToString();
                            dr["BelongSchool"] = item["BelongSchool"].SafeToString();
                            dr["Department"] = item["Department"].SafeToString();
                            dr["Status0"] = item["Status0"].SafeToString();
                            dr["Created"] = item["Created"].SafeToDataTime();
                            dt.Rows.Add(dr);
                        }
                        LV_Assert.DataSource = dt;
                        LV_Assert.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ReserveSource.ascx_BindAssert");
            }
        }

        /// <summary>
        /// 绑定顶部的日期
        /// </summary>
        private void BindData(int weeknum)
        {
            DateTime current = DateTime.Now.AddDays(weeknum*7);
            
            DateTime mondy = current.AddDays(1 - Convert.ToInt32(current.DayOfWeek.ToString("d")));
            
            this.LB_Mon.Text = GetDate(mondy)+"周一";
            this.LB_Tue.Text = GetDate(mondy.AddDays(1))+ "周二";
            this.LB_Wed.Text = GetDate(mondy.AddDays(2))+"周三";
            this.LB_Thu.Text = GetDate(mondy.AddDays(3)) + "周四";
            this.LB_Fri.Text = GetDate(mondy.AddDays(4)) + "周五";
            this.LB_Sat.Text = GetDate(mondy.AddDays(5)) + "周六";
            this.LB_Sun.Text = GetDate(mondy.AddDays(6)) + "周日";
        }

        private string GetDate(DateTime dt)
        {
            return dt.Month + "-" + dt.Day;
        }


        private Dictionary<string, string> GetColByWeek()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("一", "0");
            dic.Add("二", "1");
            dic.Add("三", "2");
            dic.Add("四", "3");
            dic.Add("五", "4");
            dic.Add("六", "5");
            dic.Add("日", "6");

            return dic;
        }
        /// <summary>
        /// 绑定专业教室
        /// </summary>
        private void BindPersonalListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Address", "Area", "OpenTime", "CloseTime", "LimitCount", "imgSource" };
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
                            if(attachments.Count>0)
                            {
                                dr["imgSource"] = attachments.UrlPrefix.Replace(oSite.Url, ServerUrl) + attachments[0].ToString();
                            }
                            else
                            {
                                dr["imgSource"] = "/_layouts/15/SVDigitalCampus/Image/zs28.jpg";
                            }
                            
                            dt.Rows.Add(dr);
                        }
                        LV_Room.DataSource = dt;
                        LV_Room.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ReserveSource.ascx_BindPersonalListView");
            }
        }

        private void BindClassRoomListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Place", "Area", "OpenTime", "CloseTime", "LimitCount", "imgSource" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value("会议室")),
                                CAML.Eq(CAML.FieldRef("Status"), CAML.Value("启用"))
                            )
                            
                            
                            );
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(item["Title"].SafeToString(), 12) : item["Title"].SafeToString();
                            dr["Place"] = item["Place"].SafeToString();
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

                            dt.Rows.Add(dr);
                        }
                        LV_Classroom.DataSource = dt;
                        LV_Classroom.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ReserveSource.ascx_BindClassRoomListView");
            }
        }

        /// <summary>
        /// 绑定公共资源
        /// </summary>
        private void BindPublicSouce(int dataNum)
        {
            DataTable dataTable = GetReserve(dataNum);

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Time1", "Time2", "Time3", "Time4", "BgColor1", "BgColor2", "BgColor3", "BgColor4", "imgSource", "Place", "Area", "LimitCount", "isselect" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        //DateTime current = DateTime.Now.AddDays(dataNum);

                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value(ViewState["Type"].SafeToString())),
                                CAML.Eq(CAML.FieldRef("Status"), CAML.Value("启用"))
                            )
                                
                            );
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString();
                            dr["Place"] = item["Place"].SafeToString();
                            dr["Area"] = item["Area"].SafeToString();
                            dr["LimitCount"] = item["LimitCount"].SafeToString();
                            dr["isselect"] = "false";
                            DataRow[] dataRows = dataTable.Select("ID='" + item["ID"].SafeToString()+"'");
                            //DataRow[] drs =             dt.Select("Id='" + categoryID + "'");
                            foreach (DataRow datadr in dataRows)
                            {
                                foreach (string str in datadr["TimeInterval"].SafeToString().Split(','))
                                {
                                    dr["Time" + str] = "已预约";
                                    dr["BgColor" + str] = "#fccb7c";
                                    dr["isselect"] = "true";
                                }
                                
                            }
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments.Count > 0)
                            {
                                dr["imgSource"] = attachments.UrlPrefix.Replace(oSite.Url, ServerUrl) + attachments[0].ToString();
                            }
                            else
                            {
                                dr["imgSource"] = "/_layouts/15/SVDigitalCampus/Image/zs28.jpg";
                            }

                            dt.Rows.Add(dr);
                        }
                        LV_Car.DataSource = dt;
                        LV_Car.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ReserveSource.ascx_BindPublicSouce");
            }
        }

        /// <summary>
        /// 获取所有的预定信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetReserve(int dataNum)
        {

            //第一个string表示资源的Id，第二个表示第几个时间段
            
            string[] arrs = new string[] { "ID", "TimeInterval" };
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        DateTime current = DateTime.Now;

                        DateTime mondy = current.AddDays(1 - Convert.ToInt32(current.DayOfWeek.ToString("d")));
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ResourcesType"), CAML.Value(ViewState["Type"].SafeToString())),
                                CAML.Eq(CAML.FieldRef("Data"), CAML.Value("DateTime", mondy.AddDays(dataNum).SafeToDataTime()))
                            )
                            );

                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item["ResourcesID"].SafeToString();
                            dr["TimeInterval"] = item["TimeInterval"].SafeToString();

                            
                            dt.Rows.Add(dr);
                        }
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ReserveSource_GetReserve.ascx绑定数据");
            }

            return dt;
        }

        protected void LB_Mon_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            int dataNum = Convert.ToInt32(lb.CommandName);
            int weekNum = Convert.ToInt32(Hid_WeekNum.Value)*7;
            BindPublicSouce(dataNum+weekNum);
            Hid_linum.Value = dataNum.SafeToString();
            Hid_opera.Value = "1";
        }


        protected void TB_NextWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = Convert.ToInt32(ViewState["WeekNum"]) + 1;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindData(Convert.ToInt32(Hid_WeekNum.Value));
            BindPublicSouce(Convert.ToInt32(Hid_WeekNum.Value)*7);
        }

        protected void TB_PreWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = Convert.ToInt32(ViewState["WeekNum"]) - 1;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindData(Convert.ToInt32(Hid_WeekNum.Value));
            BindPublicSouce(Convert.ToInt32(Hid_WeekNum.Value)*7);
        }

        protected void TB_CurrentWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = 0;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindData(Convert.ToInt32(Hid_WeekNum.Value));
            BindPublicSouce(Convert.ToInt32(Hid_WeekNum.Value)*7);
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            //DateTime searchTime = Convert.ToDateTime(this.TB_Data.Text.Trim());
            //ViewState["SearchData"] = searchTime;
        }

        protected void LV_Car_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Car.SetPageProperties(DP_Car.StartRowIndex, e.MaximumRows, false);
            BindPublicSouce(Convert.ToInt32(Hid_WeekNum.Value) * 7+Convert.ToInt32(Hid_linum.Value));
            BindData(Convert.ToInt32(Hid_WeekNum.Value));
        }

        protected void DDL_Resource_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = DDL_Resource.SelectedItem.Text;
            //ViewState["Type"] = type;
            //BindData(Convert.ToInt32(Hid_WeekNum.Value));
            //BindPublicSouce(Convert.ToInt32(Hid_WeekNum.Value) * 7);
            if (type=="车辆")
            {
                this.Pan_Car1.Visible = true;
                this.Pan_Car2.Visible = true;
                this.Pan_Classroom.Visible = false;
            }
            else
            {
                this.Pan_Car1.Visible = false;
                this.Pan_Car2.Visible = false;
                this.Pan_Classroom.Visible = true;
            }
            

            BindClassRoomListView();

        }

        protected void LV_Room_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Room.SetPageProperties(DP_Room.StartRowIndex, e.MaximumRows, false);
            BindPersonalListView();
        }

        protected void LV_Classroom_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Classroom.SetPageProperties(DP_Classroom.StartRowIndex, e.MaximumRows, false);
            BindClassRoomListView();
        }

        protected void LV_Assert_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Asserts.SetPageProperties(DP_Asserts.StartRowIndex, e.MaximumRows, false);
            BindAsserts();
        }

        protected void LV_Assert_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Assert_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    HiddenField hf = e.Item.FindControl("DetailID") as HiddenField;
            //    LinkButton btn_edit = e.Item.FindControl("LB_Edit") as LinkButton;
            //    LinkButton btn_del = e.Item.FindControl("LB_Del") as LinkButton;
            //    SPList list = SPContext.Current.Web.Lists.TryGetList("专业教室资源表");
            //    SPListItem item = list.GetItemById(int.Parse(hf!=null?hf.Value:"0"));
            //    if (btn_edit != null && btn_del!=null)
            //    {
            //        if (item != null)
            //        {
            //            if (item["Status0"].SafeToString() == "审核通过")
            //            {
            //                btn_edit.Enabled = false;
            //                btn_del.Enabled = false;
            //            }
            //        }
            //    }
            //}
        }
    }
}
