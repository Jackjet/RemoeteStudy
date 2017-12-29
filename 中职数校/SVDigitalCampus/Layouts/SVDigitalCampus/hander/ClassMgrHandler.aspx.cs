using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Common;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class ClassMgrHandler : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
            {
                ////ClassInfoManager manager = new ClassInfoManager();
                RequestEntity re = GetEntity(Request);
                switch (Request.Form["CMD"])
                {
                    //case "GetData":
                    //    List<ClassInfo> lss = manager.Find((ClassInfo)re.ConditionModel, re.FirstResult, re.PageSize);
                    //    Response.Write(CommonUtil.Serialize(lss));
                    //    break;
                    case "FullTab"://查询数据，并且会返回总记录数
                        SPWeb web = SPContext.Current.Web;
                        int firstNum = re.FirstResult;
                        DataTable dt = BuildDataTable();

                        SPListItemCollection spc = web.Lists.TryGetList("校本资源库").GetItems();
                        for (int i = 0; i < spc.Count; i++)
                        {
                            if (i > firstNum-1 && i < firstNum + re.PageSize)
                            {
                                GreatDt(dt, spc[i]);
                            }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string result = SerializeDataTable(dt);
                        Response.Write(js.Serialize(new { Data = result, PageCount = spc.Count }));
                        break;
                    default:
                        break;
                }
            }
        }
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Name", "Type", "Size", "Image", "Created", "Modified", "ID", "Url", "Status", "Skip", "Message" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        private void GreatDt(DataTable dt, SPItem item)
        {
            bool flag = false;
            string Author = item["Author"].ToString();
            string currentName = SPContext.Current.Web.CurrentUser.Name;
            //if (item["Status"].ToString() == "1")
            //{
            //    flag = true;
            //}
            //else if (IsTrue() || Author.IndexOf(currentName) > 0)
            //{
            //    flag = true;
            //}
            //else
            //    flag = false;
            //if (flag)
            //{

                DataRow dr = dt.NewRow();
                dr["Name"] = item["BaseName"];
                string Type = item["ContentType"].ToString();
                dr["Type"] = Type;
                string ImageUrl = "";
                string DocIcon = "";
                if (item["DocIcon"] != null)
                {
                    DocIcon = item["DocIcon"].ToString();
                }
                if (Type == "文件夹")
                {
                    dr["Image"] = "/_layouts/15/images/folder.gif?rev=23";
                }
                else
                {
                    if (DocIcon == "html")
                    {
                        DocIcon = "htm";
                    }
                    ImageUrl = "/_layouts/15/images/ic" + DocIcon + ".gif";//ictxt.gif";
                    dr["Image"] = ImageUrl;
                }
                string size = item["文件大小"].ToString();
                if (size == "")
                {
                    dr["Size"] = "--";
                }
                else
                {
                    if (int.Parse(size) < 1024 * 1024)
                    {
                        dr["Size"] = int.Parse(size) / 1024 + "KB";
                    }
                    if (int.Parse(size) > 1024 * 1024)
                    {
                        dr["Size"] = int.Parse(size) / 1024 / 1024 + "M";
                    }

                }
                dr["Created"] = item["Created"];
                dr["Modified"] = item["Modified"];

                dr["ID"] = item["ID"];
                dr["Url"] = item["ServerUrl"];
                dr["Status"] = item["Status"];
                dr["Skip"] = item["Skip"];
                dr["Message"] = item["Message"];
                dt.Rows.Add(dr);
            //}
        }
        public static RequestEntity GetEntity(System.Web.HttpRequest request)
        {
            RequestEntity re = new RequestEntity();
            re.PageSize = 0;
            re.PageIndex = 0;
            if (!string.IsNullOrEmpty(request.Form["PageSize"]))
            {
                re.PageSize = Convert.ToInt32(request.Form["PageSize"]);
            }
            if (!string.IsNullOrEmpty(request.Form["PageIndex"]))
            {
                re.PageIndex = Convert.ToInt32(request.Form["PageIndex"]);
            }
            //if (!string.IsNullOrEmpty(request.Form["Condition"]))
            //{
            //    re.Condition = request.Form["Condition"];
            //}
            //if (!string.IsNullOrEmpty(request.Form["ConditionModel"]))
            //{
            //    re.ConditionModel = CommonUtil.DeSerialize<T>(request.Form["ConditionModel"]);
            //}
            return re;
        }
        public string SerializeDataTable(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return serializer.Serialize(list);//调用Serializer方法 
        }

    }
}
