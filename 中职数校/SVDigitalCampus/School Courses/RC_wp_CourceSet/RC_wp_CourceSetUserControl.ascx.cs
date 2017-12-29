using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;

namespace SVDigitalCampus.School_Courses.RC_wp_CourceSet
{
    public partial class RC_wp_CourceSetUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        private void BindList()
        {
            try
            {

                DataTable CourceSet = new DataTable();
                CourceSet.Columns.Add("Title");
                CourceSet.Columns.Add("ID");
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("选课基础设置");
                if (list != null)
                {

                    foreach (SPListItem item in list.Items)
                    {
                        DataRow dr = CourceSet.NewRow();
                        dr["Title"] = item["Title"];
                        dr["ID"] = item["ID"];
                        CourceSet.Rows.Add(dr);
                    }
                }
                lvCourceset.DataSource = CourceSet;
                lvCourceset.DataBind();
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "RC_wp_CourceSetUserControl.BindList");
            }
        }
        protected void lvCourceset_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();//获取id
            if (e.CommandName.Equals("Del"))//删除
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("选课基础设置");
                SPListItem item = list.GetItemById(Convert.ToInt32(ID));
                item.Delete();
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('删除成功！');", true);
                BindList();

            }
        }

        protected void lvCourceset_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
           
        }

        protected void lvCourceset_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        private void Delete(string ID)
        {


        }
    }
}
