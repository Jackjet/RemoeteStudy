using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Executive_Office.EO_wp_BulletinDetail
{
    public partial class EO_wp_BulletinDetailUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int BulletinID = int.Parse(Request["BulletinID"]);
                    BindBulletin(BulletinID);
                }
                catch
                {

                }
            }
        }

        private void BindBulletin(int bulletinid)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("通知公告");
            try
            {


                if (list != null)
                {
                    SPListItem item = list.Items.GetItemById(bulletinid);
                    this.lblTitle.Text = item["Title"].safeToString();

                    this.lblType.Text = getType(int.Parse(item["Type"].safeToString()));

                    this.lblOrder.Text = item["Order"].safeToString();
                    this.lblContent.Text = item["Content"].safeToString();
                    //this.lblKeyword.Text = item["Keyword"].safeToString();
                    this.lblRemark.Text = item["Remark"].safeToString();
                    string cbrowsestr = "";
                    foreach (string citem in item["Cbrowse"].safeToString().Split(';'))
                    {
                        if (citem.IndexOf('#') >= 0)
                        {
                            cbrowsestr += citem.Remove(0, citem.IndexOf('#') + 1)+";";
                        }
                    }
                    Cbrowse.Text = cbrowsestr;
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "通知公告详细数据绑定");
            }
        }


        private string getType(int typeid)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("公告分类");
            try
            {


                if (list != null)
                {
                    SPListItem item = list.Items.GetItemById(typeid);
                    if (item != null)
                    {
                        return item["Title"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "通知公告详细页面新闻公告类别绑定");
            }
            return null;
        }

    }
}

