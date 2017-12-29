using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuTypeEdit
{
    public partial class CO_wp_MenuTypeEditUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //获取id绑定数据
                string typeid = Request.QueryString["TypeID"];
                if (!string.IsNullOrEmpty(typeid))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品分类");
                    if (list != null)
                    {
                        int id = int.Parse(typeid);
                        SPListItem item = list.Items.GetItemById(id);//根据id查询
                        if (item != null)
                        {
                            this.txtType.Text = item["Title"].ToString();
                            this.TypeID.Value = typeid;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string typeid = this.TypeID.Value;
            if (!string.IsNullOrEmpty(typeid))
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {
                    int id = int.Parse(typeid);
                    SPListItem item = list.Items.GetItemById(id);//根据id查询
                    if (item != null)
                    {
                        item["Title"] = this.txtType.Text;
                        item.Update();//保存修改
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('修改成功！');close();", true);
                    }
                }
            }
        }
    }
}

