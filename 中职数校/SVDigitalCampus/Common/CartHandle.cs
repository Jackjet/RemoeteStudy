using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common;

namespace SVDigitalCampus.Common
{
    public class CartHandle
    {

        public static DataTable GetCart(string type, string user, DateTime date)
        {
            DataTable Carttable = new DataTable();
            //获取购物车原数据
            if (HttpContext.Current.Request.Cookies["Cart"] != null)
            {
                Carttable.Clear();
                Carttable.Columns.Add("ID");
                Carttable.Columns.Add("Menu");
                Carttable.Columns.Add("Hot");
                Carttable.Columns.Add("Price");
                Carttable.Columns.Add("Number");
                Carttable.Columns.Add("TypeID");
                Carttable.Columns.Add("Type");
                Carttable.Columns.Add("Date");
                Carttable.Columns.Add("Total");
                HttpCookie cart = HttpContext.Current.Request.Cookies["Cart"];
                SPWeb web = SPContext.Current.Web;
                SPList typelist = web.Lists.TryGetList("菜品分类");
                SPList list = web.Lists.TryGetList("菜品");
                if (list != null && list.ItemCount>0)
                {
                    for (int i = 0; i < cart.Values.Count; i++)
                    {
                        string[] cartitemvalues = cart.Values[i].ToString().Split(',');
                        //判断是否当前用户指定日期的购物车数据
                        if (type.Equals(cartitemvalues[1].ToString()) && cartitemvalues[3].ToString().Equals(user) && cartitemvalues[2].ToString().Equals(date.ToString()))
                        {
                            string status = GetStatus(date, type, cart.Values.AllKeys[i].ToString().Split('-')[0]);
                            if (status.Equals("已确认"))
                            {
                                SPListItem menuitem = list.GetItemById(int.Parse(cart.Values.AllKeys[i].ToString().Split('-')[0]));
                                if (menuitem["Status"].ToString().Equals("1"))
                                {
                                    DataRow dr = Carttable.NewRow();
                                    //绑定值  
                                    dr["ID"] = cart.Values.AllKeys[i].Split('-')[0];
                                    dr["Menu"] = menuitem["Title"].ToString();
                                    dr["Hot"] = menuitem["Hot"].ToString() == "1" ? "icohot1" : menuitem["Hot"].ToString() == "2" ? "icohot2" : "";
                                    dr["Price"] = menuitem["Price"].ToString();
                                    dr["Number"] = cartitemvalues[0].ToString();
                                    dr["TypeID"] = menuitem["Type"].ToString();
                                    //查询分类
                                    if (typelist!=null)
                                    {
                                        SPListItem menutypeitem = typelist.GetItemById(int.Parse(menuitem["Type"].ToString()));
                                        if (menutypeitem != null)
                                        dr["Type"] = menutypeitem["Title"].ToString();
                                    }
                                    dr["Date"] = cartitemvalues[2].ToString();
                                    if (menuitem["Price"] != null && menuitem["Price"].ToString() != "" && cartitemvalues[0] != null && cartitemvalues[0].ToString() != "") { 
                                    dr["Total"] = decimal.Parse( menuitem["Price"].ToString()) * Convert.ToInt64( cartitemvalues[0].ToString());}
                                    else{dr["Total"] ="0";}
                                    Carttable.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }

            }
            return Carttable;
        }
        private static string GetStatus(DateTime date, string type, string menuid)
        {
            SPWeb web = SPContext.Current.Web;
            SPQuery query = new SPQuery();
            query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
+ SPUtility.CreateISO8601DateTimeFromSystemDateTime(date) +
"</Value></Eq></Where>";
            SPList list = web.Lists.TryGetList("菜单");
            string status = "已确认";
            if (list != null)
            {
                SPListItemCollection weekmenulist = list.GetItems(query);
                foreach (SPListItem weekmenu in weekmenulist)
                {
                    string weekmenustr = "";
                    switch (type)
                    {
                        case "1":
                            weekmenustr = weekmenu["MorningMenus"] == null ? "" : weekmenu["MorningMenus"].ToString();
                            break;
                        case "2":
                            weekmenustr = weekmenu["LunchMenus"] == null ? "" : weekmenu["LunchMenus"].ToString();
                            break;
                        case "3":
                            weekmenustr = weekmenu["DinnerMenus"] == null ? "" : weekmenu["DinnerMenus"].ToString();
                            break;
                    }
                    //循环遍历该菜品id
                    string[] MenuIDs = weekmenustr.Split(',');
                    bool result = false;
                    foreach (string ID in MenuIDs)
                    {
                        if (ID.Equals(menuid))
                        {
                            result = true;
                            break;
                        }
                    }
                    if (!result)
                    {
                        status = "已取消";
                        break;
                    }
                }
            }
            return status;
        }
        #region 修改购物车EditShoppingCar
        /// <summary>
        /// 修改购物车EditShoppingCar
        /// </summary>
        /// <param name="type">三餐类型（1、2、3）</param>
        /// <param name="user">用户</param>
        /// <param name="date">日期</param>
        /// <param name="isChange">是否修改不新增</param>
        /// <param name="id">菜品ID</param>
        /// <param name="num">数量 如果存在菜品 负数是减少 
        /// 正数是增加 如果不存在 直接增加</param>
        /// <param name="expires">cookies保存的天数</param>
        /// <remarks>这里的方法就是把在原有的Cookie基础上判断是否有
        /// 这个产品 如果有 修改原有产品数量</remarks>
        public static void EditShoppingCar(string type, string user, DateTime date, bool isChange, string id, string num, int expires)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["Cart"] != null)
            {
                System.Web.HttpCookie cookie;

                cookie = HttpContext.Current.Request.Cookies["Cart"];

                if (cookie[id + "-" + type] == null)
                {
                    cookie.Values.Add((id + "-" + type), num + "," + type + "," + date + "," + user);

                }
                else
                {
                    //if (cookie.Values[id.ToString()].ToString().Split(',')[0].safeToString().Equals(type))
                    //{
                        //判断是否加减
                        if (isChange)
                        {
                            int num1 = int.Parse(cookie.Values[id.ToString() + "-" + type].ToString().Split(',')[0].safeToString()) + int.Parse(num);
                            if (num1 > 0)
                            {
                                num = num1.ToString();
                            }
                        }
                        cookie.Values[id.ToString() + "-" + type] = num + "," + type + "," + date + "," + user;
                    //}
                    //else {
                    //    cookie.Values.Add(id, num + "," + type + "," + date + "," + user);
                    //}
                    //}
                    //else
                    //{
                    //    cookie.Values[id.ToString()] = menu + "," + price + "," + "0";
                    //}
                }
                if (expires != 0)
                {
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(expires, 0, 0, 20);
                    cookie.Expires = dt.Add(ts);
                }
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }
            else
            {
                System.Web.HttpCookie newcookie = new HttpCookie("Cart");
                if (expires != 0)
                {
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(expires, 0, 0, 20);
                    newcookie.Expires = dt.Add(ts);
                }
                newcookie.Values.Add(id+ "-" + type, num + "," + type + "," + date + "," + user);
                System.Web.HttpContext.Current.Response.AppendCookie(newcookie);
            }
        }
        #endregion
        /// <summary>
        /// 删除购物车
        /// </summary>
        public static bool RemoveCart(string type, string user,DateTime date)
        {
            DataTable Cartdb = GetCart(type, user, date);
            if (Cartdb != null && Cartdb.Rows.Count > 0)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Cart"];
                foreach (DataRow item in Cartdb.Rows)
                {
                    cookie.Values.Remove(item["id"].ToString() + "-" + type);
                    if (cookie.Values.Count == 0)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                    }
                }
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                return true;
            }
            return false;
        }
    }
}
