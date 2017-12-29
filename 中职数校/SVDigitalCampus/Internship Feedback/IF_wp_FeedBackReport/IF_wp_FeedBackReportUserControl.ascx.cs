using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
namespace SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackReport
{

    public partial class IF_wp_FeedBackReportUserControl : UserControl
    {
        public int FeededNum = 0;
        public int FeedingNum = 0;
        public int endNum = 0;
        public string Enter = "[";
        public string EnterY = "[";
        public string EnterN = "[";
        public string rootUrl = SPContext.Current.Web.Url;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserPhoto user = new UserPhoto();

                FeededNum = user.GetStudentInfoByWhere("", "", 1, -1,"").Rows.Count;// Inited(1, "IsAssign");
                FeedingNum = user.GetStudentInfoByWhere("", "", 0, -1, "").Rows.Count;// Inited(0, "IsAssign");
                endNum = user.GetStudentInfoByWhere("", "", -1, 1, "").Rows.Count;// Inited(1, "IsfeedBack");
                EnterpriseData();
            }
        }
        private int Inited(int feedStatus, string Type)
        {
            int result = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("学生信息表");
                        SPQuery query = new SPQuery();

                        query.Query = @"<Where><Eq><FieldRef Name='" + Type + "' /><Value Type='Text'>" + feedStatus + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            result = termItems.Count;
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private void EnterpriseData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPQuery query = new SPQuery();
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            for (int i = 0; i < termItems.Count; i++)
                            {
                                string EnterTitle = EnterName(termItems[i]["EnterID"].ToString());
                                if (Enter.IndexOf(EnterTitle) < 0 && i < termItems.Count - 1)
                                {
                                    Enter +="'"+ EnterTitle + "',";
                                    EnterY += result(termItems[i]["EnterID"].ToString(), "1") + ",";
                                    EnterN += result(termItems[i]["EnterID"].ToString(), "0") + ",";
                                }

                            }
                            Enter += "]";
                            EnterY += "]";
                            EnterN += "]";
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {

            }
        }
        private string EnterName(string EnterID)
        {
            string returnR = EnterID;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + EnterID + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            returnR = termItems[0].Title;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {

            }
            return returnR;
        }
        private int result(string EnterID, string IsComopleate)
        {
            int returnR = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + EnterID + "</Value></Eq><Eq><FieldRef Name='IsCompleate' /><Value Type='Text'>" + IsComopleate + "</Value></Eq></And></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            returnR = termItems.Count;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {

            }
            return returnR;
        }
    }
}
