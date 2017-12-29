using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;

namespace SVDigitalCampus.School_Courses.RC_wp_CourseHome
{
    public partial class RC_wp_CourseHomeUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Cache.Get("CourceSet") == null)
                {
                    CatchCource();
                }
            }
        }
        private void CatchCource()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("CourceSet") != null)
                        {
                            Cache.Remove("CourceSet");
                        }
                        SPList termList = oWeb.Lists.TryGetList("选课基础设置");
                        SPQuery query = new SPQuery();
                        //query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        SPListItemCollection items = termList.Items;
                        if (items != null)
                        {
                            string Cource = "";
                            foreach (SPItem item in items)
                            {
                                Cource += item["Title"] + ";";
                            }
                            if (Cource.Length>0)
                            {
                                Cource.TrimEnd(';');
                            }
                            Cache.Insert("CourceSet", Cource);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_CourseHomeUserControl.CatchCource");
            }
        }
    }
}
