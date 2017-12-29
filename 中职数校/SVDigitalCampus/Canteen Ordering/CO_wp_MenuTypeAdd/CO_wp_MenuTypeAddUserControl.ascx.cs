using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuTypeAdd
{
    public partial class CO_wp_MenuTypeAddUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtType.Text))
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {
                    //新增
                    SPListItem item = list.Items.Add();
                    item["Title"] = this.txtType.Text;
                    item.Update();
                    if (item.ID > 0) {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增成功！');close();", true);
                    }
                }
            }
        }
    }
}

