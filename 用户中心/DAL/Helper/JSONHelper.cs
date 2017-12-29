
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System;

namespace DAL
{
    public static class JSONHelper
    {
        /// <summary>
        /// 对象转为JSON格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(T t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }

        /// <summary>
        /// JSON格式转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T FromJson<T>(string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
        }


        /// <summary>
        /// 将db reader转换为Hashtable列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IList<Hashtable> DbReaderToHash(IDataReader reader)
        {
            using (reader)
            {
                var list = new List<Hashtable>();
                while (reader.Read())
                {
                    var item = new Hashtable();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        var value = reader[i];
                        item[name] = value;
                    }
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// DataTable通过Dictionary转Json
        /// </summary>
        /// <param name="dtb"></param>
        /// <returns></returns>
        public static string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dtb.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dtb.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            return jss.Serialize(dic);
        }
        /// <summary>   
        ///     将JSON解析成DataSet（只限标准的JSON数据）   
        ///     例如：Json＝{t1:[{name:'数据name',type:'数据type'}]} 或 Json＝{t1:[{name:'数据name',type:'数据type'}],t2:[{id:'数据id',gx:'数据gx',val:'数据val'}]}   
        /// </summary>   
        /// <param name="json">Json字符串</param>   
        /// <returns>DataSet</returns>   
        public static DataSet JsonToDataSet(string json)
        {
            try
            {
                var ds = new DataSet();
                var jss = new JavaScriptSerializer();
                object obj = jss.DeserializeObject(json);
                var datajson = (Dictionary<string, object>)obj;
                foreach (var item in datajson)
                {
                    var dt = new DataTable(item.Key);
                    var rows = (object[])item.Value;
                    foreach (object row in rows)
                    {
                        var val = (Dictionary<string, object>)row;
                        DataRow dr = dt.NewRow();
                        foreach (var sss in val)
                        {
                            if (!dt.Columns.Contains(sss.Key))
                            {
                                dt.Columns.Add(sss.Key);
                                dr[sss.Key] = sss.Value;
                            }
                            else
                                dr[sss.Key] = sss.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>   
        ///     将DataSet转化成JSON数据   
        /// </summary>   
        /// <param name="ds"></param>   
        /// <returns></returns>   
        public static string DataSetToJson(DataSet ds)
        {
            string json;
            try
            {
                if (ds.Tables.Count == 0)
                    throw new Exception("DataSet中Tables为0");
                json = "{";
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    json += ds.Tables[i].TableName  + ":[";
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        json += "{";
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            json += ds.Tables[i].Columns[k].ColumnName + ":'" + ds.Tables[i].Rows[j][k] + "'";
                            if (k != ds.Tables[i].Columns.Count - 1)
                                json += ",";
                        }
                        json += "}";
                        if (j != ds.Tables[i].Rows.Count - 1)
                            json += ",";
                    }
                    json += "]";
                    if (i != ds.Tables.Count - 1)
                        json += ",";
                }
                json += "}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return json;
        }

        /// <summary>
        /// 将DataSet转化成JSON数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string Ds2Json(DataSet ds)
        {
            string jsonString = "{";
            foreach (DataTable table in ds.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + Dtb2Json(table) + ",";
            }

            jsonString = jsonString.TrimEnd(',');

            return jsonString + "}";
        }


        /// <summary>
        /// Json通过Dictionary转DataTable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable Json2Dtb(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = jss.Deserialize<ArrayList>(json);
            DataTable dtb = new DataTable();

            if (dic.Count > 0)
            {
                foreach (Dictionary<string, object> drow in dic)
                {
                    if (dtb.Columns.Count == 0)
                    {
                        foreach (string key in drow.Keys)
                        {
                            //if (drow[key].GetType() != null)
                            //    dtb.Columns.Add(key, drow[key].GetType());
                            //else
                            dtb.Columns.Add(key);
                        }
                    }

                    DataRow row = dtb.NewRow();
                    foreach (string key in drow.Keys)
                    {

                        row[key] = drow[key];
                    }
                    dtb.Rows.Add(row);
                }
            }
            return dtb;
        }



    }
}
