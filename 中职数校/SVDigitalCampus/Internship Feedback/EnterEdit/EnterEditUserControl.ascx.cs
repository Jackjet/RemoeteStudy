using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.EnterEdit
{
    public partial class EnterEditUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind(Request["EnterID"].ToString());
            }
        }
        private void Bind(string EnterID)
        {
            try
            {
                if (EnterID.Trim().Length > 0)
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("企业信息");
                    SPQuery query = new SPQuery();

                    query.Query = @" <Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + EnterID + "</Value></Eq></Where>";
                    SPListItemCollection items = list.GetItems(query);
                    txtName1.Value = items[0]["Title"].ToString();
                    string RelationName = items[0]["RelationName"] == null ? "" : items[0]["RelationName"].ToString();
                    RelationName1.Value = RelationName;
                    string tel = items[0]["RelationPhone"] == null ? "" : items[0]["RelationPhone"].ToString();
                    txtPhone1.Value = tel;
                    string Email = items[0]["Email"] == null ? "" : items[0]["Email"].ToString();
                    tbEmail1.Value = Email;
                    UserPwd1.Value = items[0]["UserPwd"].ToString();
                    UserID1.Value = items[0]["UserID"].ToString();
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterEditUserControl.EnterEditUserControl");
            }
        }
        protected void btEdit_Click(object sender, EventArgs e)
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("企业信息");
                string ID = isEdit.Text;
                SPListItem newItem = list.Items.GetItemById(Convert.ToInt32(Request["EnterID"].ToString()));

                newItem["Title"] = txtName1.Value;
                newItem["RelationName"] = RelationName1.Value;

                newItem["RelationPhone"] = txtPhone1.Value;
                newItem["Email"] = tbEmail1.Value;
                newItem["UserID"] = UserID1.Value.Trim();
                newItem["UserPwd"] = UserPwd1.Value.Trim();

                newItem.Update();

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/Enterprise.aspx';", true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterEditUserControl.btEdit_Click");
            }
        }
        private void Clear()
        {
            txtName1.Value = "";
            RelationName1.Value = "";
            txtPhone1.Value = "";

            UserID1.Value = "";
            UserPwd1.Value = "";
        }

    }
}
