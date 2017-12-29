using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDigitalCampus.Common
{
    public static class MealTypeJudge
    {

        /// <summary>
        /// 获取当前餐类型
        /// </summary>
        /// <returns></returns>
        public static string GetMealType()
        {
            SPWeb sweb = SPContext.Current.Web;
            SPList list = sweb.Lists.TryGetList("时间截止配置");
            if (list != null)
            {


                string moringdate = string.Empty;
                string lunchdate = string.Empty;
                string dinnerdate = string.Empty;
                foreach (SPListItem item in list.Items)
                {
                    if (item["Type"].ToString().Equals("1"))
                    {
                        moringdate = item["EndTime"].ToString();

                    }
                    else if (item["Type"].ToString().Equals("2"))
                    {
                        lunchdate = item["EndTime"].ToString();
                    }
                    else if (item["Type"].ToString().Equals("3"))
                    {
                        dinnerdate = item["EndTime"].ToString();
                    }
                }
                try
                {
                    string mealdate = "2";
                    #region 判断当前时间段的餐类型

                    //三餐下单截止时间都不为空
                    if (!string.IsNullOrEmpty(moringdate) && !string.IsNullOrEmpty(lunchdate) && !string.IsNullOrEmpty(dinnerdate))
                    {
                        mealdate = DateTime.Now.Hour < int.Parse(moringdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(moringdate.Substring(3, 2)) ? "1" : ((DateTime.Now.Hour < int.Parse(lunchdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(lunchdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(lunchdate.Substring(3, 2)))) && ((DateTime.Now.Hour >= int.Parse(moringdate.Substring(0, 2)) || (DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute > int.Parse(moringdate.Substring(3, 2))))) ? "2" : "3";

                    }
                    //早中两餐下单截止时间都不为空
                    else if (!string.IsNullOrEmpty(moringdate) && !string.IsNullOrEmpty(lunchdate) && string.IsNullOrEmpty(dinnerdate))
                    {
                        if (DateTime.Now.Hour < int.Parse(dinnerdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(dinnerdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(dinnerdate.Substring(3, 2))) { mealdate = "3"; };


                    }//中截止时间不为空
                    else if (string.IsNullOrEmpty(moringdate) && !string.IsNullOrEmpty(lunchdate) && string.IsNullOrEmpty(dinnerdate))
                    {
                        if (DateTime.Now.Hour < int.Parse(lunchdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(lunchdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(lunchdate.Substring(3, 2))) { mealdate = "2"; };


                    }//早截止时间不为空
                    else if (!string.IsNullOrEmpty(moringdate) && string.IsNullOrEmpty(lunchdate) && string.IsNullOrEmpty(dinnerdate))
                    {
                        mealdate = DateTime.Now.Hour < int.Parse(moringdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(moringdate.Substring(3, 2)) ? "1" : "2";

                    }//早晚截止时间不为空
                    else if (!string.IsNullOrEmpty(moringdate) && string.IsNullOrEmpty(lunchdate) && !string.IsNullOrEmpty(dinnerdate))
                    {
                        mealdate = DateTime.Now.Hour < int.Parse(moringdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(moringdate.Substring(3, 2)) ? "1" : ((DateTime.Now.Hour < int.Parse(dinnerdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(dinnerdate.Substring(0, 2))) && (DateTime.Now.Hour >= int.Parse(moringdate.Substring(0, 2)) || (DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute > int.Parse(moringdate.Substring(3, 2))))) ? "3" : "2";
                    }//中晚截止时间不为空
                    else if (string.IsNullOrEmpty(moringdate) && !string.IsNullOrEmpty(lunchdate) && !string.IsNullOrEmpty(dinnerdate))
                    {
                        mealdate = ((DateTime.Now.Hour < int.Parse(lunchdate.Substring(0, 2)) || DateTime.Now.Hour == int.Parse(lunchdate.Substring(0, 2)) && DateTime.Now.Minute < int.Parse(lunchdate.Substring(3, 2)))) && ((DateTime.Now.Hour >= int.Parse(moringdate.Substring(0, 2)) || (DateTime.Now.Hour == int.Parse(moringdate.Substring(0, 2)) && DateTime.Now.Minute > int.Parse(moringdate.Substring(3, 2))))) ? "2" : "3";

                    }
                    #endregion
                    return mealdate;

                }
                catch
                {
                }
            }
            return "";
        }
        //获取三餐显示类型
        public static string GetMealTypeShow(string Typeid)
        {
            if (!string.IsNullOrEmpty(Typeid))
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("三餐");
                if (list != null)
                {
                    try
                    {
                        SPListItem item = list.Items.GetItemById(int.Parse(Typeid));
                        return item["Title"].ToString();
                    }
                    catch
                    {
                        return null;
                    }

                }
            }
            return null;
        }
        /// <summary>
        /// 计算可下单剩余时间
        /// </summary>
        /// <returns></returns>
        public static string WorkSPTime(string mealtype,DateTime date)
        {
            //计算下单剩余可修改时间
            SPWeb sweb = SPContext.Current.Web;
            SPList list = sweb.Lists.TryGetList("时间截止配置");
            if (list != null)
            {


                string moringdate = string.Empty;
                string lunchdate = string.Empty;
                string dinnerdate = string.Empty;
                foreach (SPListItem item in list.Items)
                {
                    if (item["Type"].ToString().Equals("1"))
                    {
                        moringdate = item["EndTime"].ToString();

                    }
                    else if (item["Type"].ToString().Equals("2"))
                    {
                        lunchdate = item["EndTime"].ToString();
                    }
                    else if (item["Type"].ToString().Equals("3"))
                    {
                        dinnerdate = item["EndTime"].ToString();
                    }
                }

                //string mealdate = MealTypeJudge.GetMealType();
                DateTime time = new DateTime();
                switch (mealtype)
                {
                    case "1":
                        if (moringdate.Split(':').Length > 0)
                        {

                            time = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", date.Date) + " " + moringdate + ":00");
                        }
                        else
                        {
                            return "0小时0分0秒";
                        }
                        break;
                    case "2":
                        if (lunchdate.Split(':').Length > 0)
                        {
                            time = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", date.Date) + " " + lunchdate + ":00");
                        }
                        else
                        {
                            return "0小时0分0秒";
                        }
                        break;
                    case "3":
                        if (dinnerdate.Split(':').Length > 0)
                        {
                            time = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", date.Date) + " " + dinnerdate + ":00");
                        }
                        else
                        {
                            return "0小时0分0秒";
                        }
                        break;
                }

                return DateDiff(DateTime.Now, time);
            } return "0小时0分0秒";
        }
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="DateTime1">当前时间</param>
        /// <param name="DateTime2">最晚时间</param>
        /// <returns>几小时几分几秒</returns>
        private static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            if (DateTime1.CompareTo(DateTime2) < 0)
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
            }
            else { dateDiff = "0小时0分0秒"; }
            return dateDiff;
        }
        #region 获取当前周的指定星期几日期
        /// <summary>
        /// 获取当前周的指定星期几日期
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <param name="i">星期几</param>
        /// <returns></returns>
        public static string GetThisWeekday(DateTime date, DayOfWeek day)
        {
            DateTime weekDate = System.DateTime.Now;
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date;
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(+1);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(+2);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(+3);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+4);

                            break;
                    }
                    break;
                case System.DayOfWeek.Tuesday:
                    switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(-1);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date;
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(+1);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(+2);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+3);

                            break;
                    }
                    break;
                case System.DayOfWeek.Wednesday: switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(-2);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(-1);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date;
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(+1);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+2);

                            break;
                    }
                    break;
                case System.DayOfWeek.Thursday: switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(-3);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(-2);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(-1);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date;
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+1);

                            break;
                    }
                    break;
                case System.DayOfWeek.Friday: switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(-4);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(-3);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(-2);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(-1);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date;

                            break;
                    }
                    break;
                case System.DayOfWeek.Saturday: switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(+2);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(+3);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(+4);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(+5);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+6);

                            break;
                    }
                    break;
                case System.DayOfWeek.Sunday: switch (day)
                    {
                        case DayOfWeek.Monday:
                            weekDate = date.AddDays(+1);
                            break;
                        case DayOfWeek.Tuesday:
                            weekDate = date.AddDays(+2);
                            break;
                        case DayOfWeek.Wednesday:
                            weekDate = date.AddDays(+3);
                            break;
                        case DayOfWeek.Thursday:
                            weekDate = date.AddDays(+4);
                            break;
                        case DayOfWeek.Friday:
                            weekDate = date.AddDays(+5);

                            break;
                    } break;
            }
            return weekDate.ToString("yyyy-MM-dd");
        }
        #endregion

    }
}
