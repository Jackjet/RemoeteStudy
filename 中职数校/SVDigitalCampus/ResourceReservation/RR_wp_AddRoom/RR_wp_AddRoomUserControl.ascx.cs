using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_AddRoom
{
    public partial class RR_wp_AddRoomUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        SPWeb spweb = SPContext.Current.Web;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {



            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddData();
        }




        private void AddData()
        {
            string pageName = Request.QueryString["pagename"];
            string weekNums = Request.QueryString["weekNum"];
            string linum = Request.QueryString["linum"];
            string resid = Request.QueryString["resid"];
            
            string para = "weekNum=" + weekNums.SafeToString() + "&linum=" + linum.SafeToString() + "&resid=" + resid.SafeToString();
            
            try
            {
                Dictionary<int, string> dic = CreateWeekDic();
                int weekNum = Convert.ToInt32(Request.QueryString["weekNum"]);

                DateTime current = DateTime.Now.AddDays(weekNum * 7);
                string week = Request.QueryString["para"];
                DateTime mondy = current.AddDays(1 - Convert.ToInt32(current.DayOfWeek.ToString("d")));//本周周一
                string[] arr = week.Split(new char[] { 'B' }, StringSplitOptions.RemoveEmptyEntries);
                string timeinterval = string.Empty;
                string weeknum = string.Empty;
                foreach (string item in arr)
                {
                    timeinterval += item.Split('A')[0] + ",";
                    weeknum = item.Split('A')[1];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["linum"].SafeToString()))
                {
                    weeknum = (Convert.ToInt32(Request.QueryString["linum"]) + 1).SafeToString();
                    Hid_linum.Value = Request.QueryString["linum"];
                }
                //string resid = Request.QueryString["resid"];
                string resType = GetResouType(resid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPListItem item = list.AddItem();
                        item["Title"] = this.TB_Title.Text;
                        item["ResourcesID"] = resid;
                        item["ResourcesType"] = resType.Split('#')[0];
                        item["ResourcesTypeId"] = resType.Split('#')[1];
                        item["TimeInterval"] = timeinterval.TrimEnd(',');
                        item["WeekData"] = dic[Convert.ToInt32(weeknum)];
                        item["Data"] = mondy.AddDays(Convert.ToInt32(weeknum) - 1);
                        item["BelongSchool"] = this.TB_BelongSchool.Text;
                        item["ContactPhone"] = this.TB_ContactPhone.Text;
                        item["ClassCount"] = arr.Length;
                        item["AuditStatus"] = "待审批";
                        //item["AuditContent"] = "哈哈，小伙子你通过额";
                        item["Description"] = this.TB_Description.Text;

                        item.Update();
                        //script += "+" + "'?linum=" + Request.QueryString["linum"]+"'";
                        //**************************************
                        //校本课程
                        if (Request["Type"].SafeToString() == "cla")
                        {

                            SPWeb web = oSite.OpenWeb("CouseManage");
                            web.AllowUnsafeUpdates = true;
                            SPList CourseList = web.Lists.TryGetList("校本课程");
                            int id = Convert.ToInt32(Request["ClassID"]);
                            SPListItem Courseitem = CourseList.GetItemById(id);
                            Courseitem["Status"] = "5";
                            Courseitem["WeekName"] = dic[Convert.ToInt32(weeknum)];
                            Courseitem["AddressID"] = GetResouTitle(resid);
                            Courseitem["ReservationID"] = item.ID;
                            Courseitem.Update();
                            web.AllowUnsafeUpdates = false;
                            para += "&Type=cla";
                        }
                        //**************************************

                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "RR_wp_AddRoom.ascx_AddData");
            }
            //"window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/" + pageName + ".aspx?" + para + ";"
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/" + pageName + ".aspx?" + para + "';", true);
        }


        private Dictionary<int, string> CreateWeekDic()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "周一");
            dic.Add(2, "周二");
            dic.Add(3, "周三");
            dic.Add(4, "周四");
            dic.Add(5, "周五");
            dic.Add(6, "周六");
            dic.Add(7, "周日");

            return dic;
        }

        private string GetResouType(string resid)
        {
            string result = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPListItem item = list.GetItemById(Convert.ToInt32(resid));
                        result = item["ResourcesType"].SafeToString() + "#" + item["ResourcesTypeId"].SafeToString();


                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp__AddRoom.ascx_GetResouType");
            }
            return result;
        }
        private string GetResouTitle(string resid)
        {
            string result = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPListItem item = list.GetItemById(Convert.ToInt32(resid));
                        result = item.Title;


                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_AddRoom.ascx_GetResouTitle");
            }
            return result;
        }
    }
}
