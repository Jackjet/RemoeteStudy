using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;

using YHSD.VocationalEducation.Portal.Code;
using Common;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class TrainRoomInfo : UserControl
    {
        SPWeb web = SPContext.Current.Web;
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["IsSearch"] = false;
                BindTrainRoom();
            }
        }

        private void BindTrainRoom()
        {

            DataTable dt = BuildDataTable();
            SPList list = web.Lists.TryGetList("实训室表");
            SPQuery query = AppendQuery();
            SPListItemCollection items = list.GetItems(query);
            foreach (SPListItem item in items)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.ID;
                dr["Title"] = item.Title.SafeToString();
                dr["Place"] = item["Place"].SafeToString();
                dr["Area"] = item["Area"].SafeToString();
                dr["IsUse"] = item["IsUse"].SafeToString();
                dr["IsCanUse"] = item["IsCanUse"].SafeToDataTime();
                //dr["ExamineResult"] = item["ExamineResult"].SafeToString();
                dt.Rows.Add(dr);
            }
            this.LV_TrainRoom.DataSource = dt;
            this.LV_TrainRoom.DataBind();
        }


        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value("0"));
            bool flag = Convert.ToBoolean(ViewState["IsSearch"]);
            if (DDL_ApplyType.SelectedItem.Text != "不限" && flag)
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(DDL_ApplyType.SelectedItem.Text))), strQuery);
            }
            if (DDL_ExamineResult.SelectedItem.Text != "不限" && flag)
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(DDL_ExamineResult.SelectedItem.Text))), strQuery);
            }
            strQuery += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            query.Query = CAML.Where(strQuery);
            return query;
        }



        public static DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title","Place", "Area", "IsUse", "IsCanUse" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = true;
            BindTrainRoom();
        }

        protected void LV_Examine_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("申请审批");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                        }
                        if (e.CommandName.Equals("Appoint"))
                        {
                            item["ExamineId"]=CreateAppoint(item.Title,item.ID);
                            item.Update();
                        }
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "LV_Examine_ItemCommand——TrainRoom");
            }
            BindTrainRoom();
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作成功');", true);
        }

        protected void LV_Examine_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_TrainRoom.SetPageProperties(DP_TrainRoom.StartRowIndex, e.MaximumRows, false);
            BindTrainRoom();
        }

        private int CreateAppoint(string tit,int itemid)
        {
            SPWeb web = SPContext.Current.Web;
            web.AllowUnsafeUpdates = true;
            SPList list = web.Lists.TryGetList("申请审批");
            SPListItem item = list.AddItem();
            item["ExamineType"] = "实训室预约";
            item["Title"] = "预约"+tit;
            item["RelationList"] = "实训室表";
            item["ApplyTime"] = DateTime.Now.SafeToDataTime();
            item["RelationId"] = itemid;
            item["ApplyUser"] = SPContext.Current.Web.CurrentUser.Name;
            item.Update();
            web.AllowUnsafeUpdates = false;
            return item.ID;
        }

        protected void LV_TrainRoom_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
    }
}
