using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.EnterAdd
{
    public partial class EnterAddUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }
        #region 企业添加修改 删除
        //添加企业
        protected void Add_Click(object sender, EventArgs e)
        {
            
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("企业信息");
                SPListItem newItem = null;

                newItem = list.Items.Add();

                newItem["Title"] = txtName.Value;
                newItem["RelationName"] = RelationName.Value;
                newItem["RelationPhone"] = txtPhone.Value;
                
                newItem["UserID"] = UserID.Value.Trim();
                newItem["UserPwd"] = UserPwd.Value.Trim();
                newItem["Email"] = tbEmail.Value.Trim();
                newItem["Sorts"] = MaxID().ToString();
                newItem.Update();

                string joblist = names.Value;
                AddJob(names.Value.TrimEnd(','), newItem.ID);
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>complete();</script>");
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.Add_Click");
            }
        }
        /// <summary>
        /// 获取最大排序号（sorts的最大值）
        /// </summary>
        /// <returns></returns>
        private int MaxID()
        {
            int sorts = 1;

            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList termList = web.Lists.TryGetList("企业信息");
                SPQuery query = new SPQuery();
                query.Query = "<OrderBy><FieldRef Name='Sorts' Ascending='False' /></OrderBy>";
                query.RowLimit = 1;
                SPListItemCollection itemc = termList.GetItems(query);
                if (itemc.Count > 0)
                {
                    sorts = Convert.ToInt32(itemc[0]["Sorts"]);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.MaxID");
            }
            return sorts + 2;

        }
        private void AddJob(string ProfessionList, int Pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业岗位信息");
                        string[] str = ProfessionList.Split(',');
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i] != "" && IsExist(Pid.ToString(), str[i]))
                            {
                                SPListItem newItem = list.Items.Add();
                                newItem["EnterID"] = Pid.ToString();
                                newItem["Title"] = str[i];
                                newItem.Update();
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.ascx_BindListView");
            }
        }
        /// <summary>
        /// 岗位是否重复
        /// </summary>
        /// <param name="EnterID"></param>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public bool IsExist(string EnterID, string JobName)
        {
            bool flag = true;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业岗位信息");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><And><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + EnterID + "</Value></Eq><Eq><FieldRef Name='Title' /><Value Type='Text'>" + JobName + "</Value></Eq></And></Where>";
                        SPListItemCollection itemc = list.GetItems(query);
                        if (itemc.Count > 0)
                        {
                            flag = false;
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.IsExist");
            }
            return flag;
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            int m = Request.Files.Count;
        }
    }
}
