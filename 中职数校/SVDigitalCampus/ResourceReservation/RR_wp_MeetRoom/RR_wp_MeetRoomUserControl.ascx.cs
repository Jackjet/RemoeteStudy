using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_MeetRoom
{
    public partial class RR_wp_MeetRoomUserControl : UserControl
    {

        SPWeb web = SPContext.Current.Web;
        LogCommon com = new LogCommon();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string resid = Request.QueryString["resid"];
                if (!string.IsNullOrEmpty(resid))
                {
                    ViewState["resid"] = resid;
                    this.Hid_ResId.Value = resid;
                    BindResouce(resid);
                    BindSed();
                }

                ViewState["WeekNum"] = 0;
                Hid_WeekNum.Value = "0";

            }
            else
            {
                
            }
        }


        private void BindResouce(string resid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPListItem item = list.GetItemById(Convert.ToInt32(resid));
                        this.Lit_Title.Text = item.Title;
                        this.Lit_Address.Text = item["Address"].safeToString();
                        this.Lit_Area.Text = item["Area"].safeToString();
                        this.Lit_OpenTime.Text = item["OpenTime"].SafeToString();
                        this.Lit_CloseTime.Text = item["CloseTime"].safeToString();
                        this.Lit_LimitCount.Text = item["LimitCount"].SafeToString();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments.Count > 0)
                        {
                            this.Img_ImgSource.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ServerUrl) + attachments[0].ToString();
                        }
                        else
                        {
                            this.Img_ImgSource.ImageUrl = "/_layouts/15/SVDigitalCampus/Image/zs28.jpg";
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_MeetRoom.ascx_BindResouce");
            }
        }


        private void AddData()
        {
            Dictionary<int, string> dic = CreateWeekDic();
            DateTime current = DateTime.Now;
            string week = this.Hid_AppointData.Value;
            DateTime mondy = current.AddDays(1 - Convert.ToInt32(current.DayOfWeek.ToString("d")));//本周周一

            string[] arr = week.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            string timeinterval = string.Empty;
            string weeknum = string.Empty;
            foreach (string item in arr)
            {
                timeinterval += item.Split(',')[0] + ",";
                weeknum = item.Split(',')[1];
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPListItem item = list.AddItem();
                        item["Title"] = "上数学课";
                        item["ResourcesID"] = 1;
                        item["TimeInterval"] = timeinterval.TrimEnd(','); ;
                        item["WeekData"] = dic[Convert.ToInt32(weeknum)];
                        item["Data"] = mondy.AddDays(Convert.ToInt32(weeknum) - 1);
                        item["BelongSchool"] = "哈哈大学";
                        item["ContactPhone"] = "18201238071";
                        item["ClassCount"] = arr.Length;
                        item["AuditStatus"] = "待审批";
                        item["AuditContent"] = "哈哈，小伙子你通过额";
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_MeetRoom.ascx_AddData");
            }
            this.Hid_AppointData.Value = "";
        }

        private string GetDayOfWeek(DateTime dt)
        {
            string result = string.Empty;
            //dt.
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    result = "周一";
                    break;
                case DayOfWeek.Tuesday:
                    result = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    result = "周三";
                    break;
                case DayOfWeek.Thursday:
                    result = "周四";
                    break;
                case DayOfWeek.Friday:
                    result = "周五";
                    break;
                case DayOfWeek.Saturday:
                    result = "周六";
                    break;
                case DayOfWeek.Sunday:
                    result = "周日";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 生成Tabel表格的HTML代码
        /// </summary>
        /// <param name="items"></param>
        private void CreateTabel(SPListItemCollection items)
        {
            Dictionary<int, string> dic = CreateDic();
            StringBuilder htmlsb = new StringBuilder();
            for (int i = 1; i <= 4; i++)
            {
                htmlsb.Append("<tr>");
                for (int j = 1; j <= 8; j++)
                {
                    if (j == 1)
                    {
                        htmlsb.Append("<th>" + dic[i] + "</th>");
                    }
                    else
                    {
                        htmlsb.Append("<td rowindex='" + i + "' colindex='" + (j - 1) + "'></td>");
                    }
                }
                htmlsb.Append("</tr>");
            }
            string result = htmlsb.ToString();
            Dictionary<string, string> col = GetColByWeek();
            foreach (SPListItem item in items)
            {
                string weekData = item["WeekData"].safeToString();//得到周几

                string timeInterval = item["TimeInterval"].safeToString();//得到时间段
                string[] timearr = item["TimeInterval"].safeToString().Split(',');
                for (int m = 0; m < timearr.Length; m++)
                {
                    if (m == 0)
                    {
                        result = result.Replace("<td rowindex='" + timearr[m] + "' colindex='" + col[weekData] + "'>", "<td style='background-color:#FCCB7C;vertical-align:middle;' isselect='true' rowspan='" + item["ClassCount"].SafeToString() + "' rowindex='" + timearr[m] + "' colindex='" + col[weekData] + "'>" + item.Title);
                    }
                    else
                    {
                        result = result.Replace("<td rowindex='" + timearr[m] + "' colindex='" + col[weekData] + "'>", "");
                    }
                }

            }
            this.Lit_result.Text = result;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void BindSed()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string resid = ViewState["resid"].SafeToString();
                        DateTime current = DateTime.Now.AddDays(Convert.ToInt32(ViewState["WeekNum"]) * 7);
                        if (ViewState["SearchData"] != null)
                        {
                            current = Convert.ToDateTime(ViewState["SearchData"]);
                        }
                        DateTime monday = current.AddDays(1 - Convert.ToInt32(current.DayOfWeek.ToString("d")));//本周周一
                        if (!string.IsNullOrEmpty(Request.QueryString["weekNum"]))
                        {
                            monday = monday.AddDays(7 * Convert.ToInt32(Request.QueryString["weekNum"]));
                        }
                        this.Lit_Mon.Text = monday.Month + "-" + monday.Day;
                        this.Lit_Tue.Text = monday.AddDays(1).Month + "-" + monday.AddDays(1).Day;
                        this.Lit_Wed.Text = monday.AddDays(2).Month + "-" + monday.AddDays(2).Day;
                        this.Lit_Thu.Text = monday.AddDays(3).Month + "-" + monday.AddDays(3).Day;
                        this.Lit_Fri.Text = monday.AddDays(4).Month + "-" + monday.AddDays(4).Day;
                        this.Lit_Sat.Text = monday.AddDays(5).Month + "-" + monday.AddDays(5).Day;
                        this.Lit_Sun.Text = monday.AddDays(6).Month + "-" + monday.AddDays(6).Day;
                        DateTime sunday = monday.AddDays(6);//本周周日
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.And(
                                    CAML.Geq(CAML.FieldRef("Data"), CAML.Value("DateTime", monday.SafeToDataTime())),
                                    CAML.Leq(CAML.FieldRef("Data"), CAML.Value("DateTime", sunday.SafeToDataTime()))
                                ),
                                CAML.Eq(CAML.FieldRef("ResourcesID"), CAML.Value(resid))
                            )
                            );
                        SPListItemCollection items = list.GetItems(query);
                        CreateTabel(items);
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_MeetRoom.ascx_BindSed");
            }
        }


        

        private Dictionary<int, string> CreateDic()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "第一节");
            dic.Add(2, "第二节");
            dic.Add(3, "第三节");
            dic.Add(4, "第四节");
            dic.Add(5, "第五节");
            dic.Add(6, "第六节");
            dic.Add(7, "第七节");
            dic.Add(8, "第八节");
            dic.Add(9, "第九节");
            dic.Add(10, "第十节");
            dic.Add(11, "第十一节");
            dic.Add(12, "第十二节");
            dic.Add(13, "第十三节");

            return dic;
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

        private Dictionary<string, string> GetColByWeek()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("周一", "1");
            dic.Add("周二", "2");
            dic.Add("周三", "3");
            dic.Add("周四", "4");
            dic.Add("周五", "5");
            dic.Add("周六", "6");
            dic.Add("周日", "7");

            return dic;
        }

        protected void TB_NextWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = Convert.ToInt32(ViewState["WeekNum"]) + 1;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindSed();
        }

        protected void TB_PreWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = Convert.ToInt32(ViewState["WeekNum"]) - 1;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindSed();
        }

        protected void TB_CurrentWeek_Click(object sender, EventArgs e)
        {
            ViewState["WeekNum"] = 0;
            Hid_WeekNum.Value = ViewState["WeekNum"].safeToString();
            BindSed();
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            //DateTime searchTime = Convert.ToDateTime(this.TB_Data.Text.Trim());
            //ViewState["SearchData"] = searchTime;
        }

    }
}
