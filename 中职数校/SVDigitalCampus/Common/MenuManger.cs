using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SVDigitalCampus.Common
{
    public static class MenuManger
    {
        public static DataTable GetMenuList(bool isUsed)
        {
            DataTable MenuDb = new DataTable();
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("菜品");
            SPList typelist = web.Lists.TryGetList("菜品分类");
            if (list != null)
            {
                MenuDb.Columns.Add("ID");
                MenuDb.Columns.Add("Title");
                MenuDb.Columns.Add("Type");
                MenuDb.Columns.Add("TypeID");
                MenuDb.Columns.Add("Picture");
                MenuDb.Columns.Add("Price");
                MenuDb.Columns.Add("Hot");
                MenuDb.Columns.Add("Status");
                MenuDb.Columns.Add("Description");
                SPQuery query = new SPQuery();
                if (isUsed) { query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                else
                {
                    query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                }
                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    DataRow newrow = MenuDb.NewRow();
                    DataRow dr = MenuDb.NewRow();
                    dr["ID"] = item["ID"].safeToString();
                    dr["Title"] = item["Title"].safeToString();
                    //查询分类名称
                    if (typelist != null)
                    {
                        foreach (SPListItem type in typelist.Items)
                        {
                            if (type["ID"].ToString().Equals(item["Type"].ToString()))
                            {

                                dr["Type"] = type["Title"].safeToString();
                                break;
                            }
                        }
                    }
                    dr["TypeID"] = item["Type"].safeToString();
                    SPList imageList = web.Lists.TryGetList("图片库");
                    if (item["Picture"] != null)
                    {

                        dr["Picture"] = web.Url + "/" + imageList.Items.GetItemById(int.Parse(item["Picture"].safeToString())).Url;

                    }
                    dr["Hot"] = item["Hot"].safeToString() == "0" ? "无" : item["Hot"].safeToString() == "1" ? "微辣" : "辛辣";
                    dr["Price"] = item["Price"].safeToString();
                    dr["Description"] = item["Description"].safeToString();
                    dr["Status"] = item["Status"].safeToString() == "1" ? "启用" : "禁用";
                    MenuDb.Rows.Add(dr);
                }
            }
            return MenuDb;
        }
        /// <summary>
        /// 根据菜品id获取菜品
        /// </summary>
        /// <param name="menuid">菜品id</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetMenuByID(int menuid)
        {
            DataTable MenuDb = new DataTable();
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("菜品");
            SPList typelist = web.Lists.TryGetList("菜品分类");
            if (list != null)
            {
                MenuDb.Columns.Add("ID");
                MenuDb.Columns.Add("Title");
                MenuDb.Columns.Add("Type");
                MenuDb.Columns.Add("TypeID");
                //MenuDb.Columns.Add("Picture");
                MenuDb.Columns.Add("Price");
                MenuDb.Columns.Add("Hot");
                MenuDb.Columns.Add("Description");
                MenuDb.Columns.Add("Status");
                if (menuid != 0)
                {
                    SPListItem item = list.GetItemById(menuid);
                    DataRow newrow = MenuDb.NewRow();
                    DataRow dr = MenuDb.NewRow();
                    dr["ID"] = item["ID"].safeToString();
                    dr["Title"] = item["Title"].safeToString();
                    //查询分类名称
                    if (typelist != null)
                    {
                        foreach (SPListItem type in typelist.Items)
                        {
                            if (type["ID"].ToString().Equals(item["Type"].safeToString()))
                            {

                                dr["Type"] = type["Title"].safeToString();
                                break;
                            }
                        }
                    }
                    dr["TypeID"] = item["Type"].safeToString();
                    //SPList imageList = web.Lists.TryGetList("图片库");
                    //if (item["Picture"] != null)
                    //{

                    //    dr["Picture"] = web.Url + "/" + imageList.Items.GetItemById(int.Parse(item["Picture"].ToString())).Url;

                    //}
                    dr["Hot"] = item["Hot"].safeToString() == "0" ? "无" : item["Hot"].safeToString() == "1" ? "微辣" : "辛辣";
                    dr["Price"] = item["Price"].safeToString();
                    dr["Description"] = item["Description"].safeToString();
                    dr["Status"] = item["Status"].safeToString() == "1" ? "启用" : "禁用";
                    MenuDb.Rows.Add(dr);
                }
            }
            return MenuDb;
        }
    }
}
