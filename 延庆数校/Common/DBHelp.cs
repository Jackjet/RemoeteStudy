using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;


namespace Common
{
   public  class DBHelp
    {

        //添加
        //1，成功，0失败
        //
          //查询  
        //<table="表明">
        //<strwhere="查询语句"> <Eq></Eq>""  全部  为 *
        //<rowsname="返回table的字段名">
        //<itemname="需要绑定的sp列表中的字段值">

       public DataTable Query(string table, string strwhere, string[] rowsname, string[] itemname, string orderby)
        {
            if (orderby == "")
            {
                orderby = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
            }
            DataTable newTable = new DataTable();
            try
            { }
            catch
            {

            }
            
                for (int i = 0; i < rowsname.Length; i++)
                {
                    newTable.Columns.Add(rowsname[i]);
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList(table);
                SPQuery query = new SPQuery();
                if (strwhere == "*")
                { strwhere = ""; }
                query.Query = @" <Where>" + strwhere + "</Where>"+orderby;
                SPListItemCollection items = list.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow newRows = newTable.NewRow();
                    for (int j = 0; j < itemname.Length; j++)
                    {
                        newRows[rowsname[j]] = item[itemname[j]]==null?"无": item[itemname[j]].ToString();
                    }
                    newTable.Rows.Add(newRows);
                }
          
            return newTable;

        }

     
       public void Delete(string table, int id)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList(table);
            list.GetItemById(id).Delete();
        }
        //根据id 返回字段值
        // 表明，id，返回字段
       public string QueryString(string tb, string id, string ziduan)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList(tb);
            SPQuery query = new SPQuery();
            query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value> </Eq></Where>";
            SPListItemCollection items = list.GetItems(query);

            if (items.Count > 0)
            {
                return items[0][ziduan]==null?"null":items[0][ziduan].ToString();
            }
            else
            {
                return "无";
            }
        }
        //修改
       public int Update(string table, string id, string[] con, string[] itemname)
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList(table);
                SPListItem upitem = list.GetItemById(Convert.ToInt32(id));
                for (int i = 0; i < con.Length; i++)
                { upitem[itemname[i]] = con[i]; }
                upitem.Update();
                return 1;
            }
            catch {
                return 0;
            }
            
        }
       
        //添加
        //1，成功，0失败
        //
       public int Insert(string table, string[] colname, string[] content)
        {
            try
            { }
            catch { return 0; }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList(table);
                SPListItem addItem = list.Items.Add();
                for (int i = 0; i < colname.Length; i++)
                {
                    addItem[colname[i]] = content[i];
                }
                addItem.Update();
                return 1;
  
        }
       
       //假删除
       public void UpdateDel(string table, int id)
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList(table);
                SPListItem upitem = list.GetItemById(Convert.ToInt32(id));
                upitem["Del"] = 0; 
                upitem.Update();
               
            }
            catch
            {
            }          
        }

       //public void Alert(string msg)
       //{
       //    string errScript = "<script language='javascript'> alert ('" + msg + "') </script>";
       //    Page.ClientScript.RegisterStartupScript(this.GetType(), "errScript", errScript);
       //}
       public string GetSPDateTime(string time)
       {
           return SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(time));
       }
       /// <summary>
       /// 查重
       /// </summary>
       /// <
       public bool ISRepeat(string table,string where)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList(table);
            SPQuery query = new SPQuery();
            query.Query = @" <Where>" + where + "</Where>" ;
            SPListItemCollection items = list.GetItems(query);
            if (items.Count > 0)
            {
                return true;
            }
            else {

                return false;
            }
       }
       //查重  Title
       public bool ISRepeatTitle(string table, string con)
       {
           SPWeb web = SPContext.Current.Web;
           SPList list = web.Lists.TryGetList(table);
           SPQuery query = new SPQuery();
           query.Query = @" <Where> <Eq><FieldRef Name='Title' /><Value Type='Text'>"+con+"</Value></Eq></Where>";
           SPListItemCollection items = list.GetItems(query);
           if (items.Count > 0)
           {
               return true;
           }
           else
           {

               return false;
           }
       }
       
    }

}
