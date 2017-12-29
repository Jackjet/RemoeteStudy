using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{

    public partial class Enterprise : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (Request["Func"] != null)
            {
                string func = Request["Func"];
                switch (func)
                {
                    case "EnterName":
                        Response.Write(EnterPrise(Request.Form["Title"]));
                        break;
                    case "UserID":
                        Response.Write(User(Request.Form["Title"]));
                        break;
                    case "upload":
                        Response.Write(upload(Request.Form["file"]));
                        break;
                        
                    default:
                        break;
                }

            }
            Response.End();
        }
        private int upload(string file)
        {
            return Request.Files.Count;
        }
        private int EnterPrise(string EnterName)
        {
            int result = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + EnterName + "</Value></Eq></Where>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            result = 1;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_BindListView");
            }
            return result;
        }
        private int User(string UserName)
        {
            int result = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='UserID' /><Value Type='Text'>" + UserName + "</Value></Eq></Where>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            result = 1;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_BindListView");
            }
            return result;
        }

    }
}
