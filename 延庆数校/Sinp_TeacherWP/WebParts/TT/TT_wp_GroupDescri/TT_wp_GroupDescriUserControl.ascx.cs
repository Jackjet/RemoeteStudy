using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TT.TT_wp_GroupDescri
{
    public partial class TT_wp_GroupDescriUserControl : UserControl
    {
        public TT_wp_GroupDescri AllTrain { get; set; }
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        List<string> ls = new List<string>();
                        SPGroupCollection groups = user.Groups;
                        foreach (SPGroup item in groups)
                        {
                            if (AllTrain.AllTrainGroup.Contains(item.Name))
                            {
                                ls.Add(item.Name);
                            }
                        }
                        if(ls.Count>0)
                        {
                            SPList listGroup = oWeb.Lists.TryGetList("研修组");
                            SPQuery listQuery = new SPQuery();
                            listQuery.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Title"), CAML.Value(ls[0])));
                            SPListItemCollection items = listGroup.GetItems(listQuery);

                            if (items != null && items.Count > 0)
                            {
                                this.Lit_Description.Text = items[0]["GroupDescription"].SafeToString();
                            }
                            
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_TempListView_PreRender");
            }
        }
    }
}
