using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Repository.SR_wp_SchoolLibrary
{
    public partial class SR_wp_SchoolLibraryUserControl : UserControl
    {
        public SR_wp_SchoolLibrary School { get; set; }
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                HAdmin.Value = School.SuperAdmin;
                CatchType();
                BindMajor();
            }
        }
        private void CatchType()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("文档类型") == null)
                        {
                            SPList termList = oWeb.Lists.TryGetList("文档类型");
                            SPListItemCollection listcolection = termList.GetItems();
                            DataTable dt = listcolection.GetDataTable();

                            Cache.Insert("DocType", (DataTable)dt);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PND_wp_PersonDriveUserControl.ascx_BindListView");
            }
        }
        protected void FielAdd_Click(object sender, EventArgs e)
        {
            string RepeatDoc = "";//重复文件名称
            int count = 0;//成功上传文件个数
            string script = "";//提示信息
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("校本资源库");
            if (list != null)
            {
                SPFolder RootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + HFoldUrl.Value);

                SPDocumentLibrary docLib = (SPDocumentLibrary)list;

                if (Request.Files.Count > 0)
                {

                    for (int i = 0; i < Request.Files.Count - 1; i++)
                    {
                        string strDocName = Request.Files[i].FileName;
                        string strName = strDocName.Split('\\')[strDocName.Split('\\').Length - 1];

                        SPUser currentUser = web.CurrentUser;
                        int f = 0;
                        SPFileCollection oldfiles = folder.Files;
                        int index = Hidden1.Value.IndexOf(strName);
                        if (index < 0)
                        {
                            f = 1;
                        }
                        else
                        {
                            foreach (SPFile file in oldfiles)
                            {
                                if (file.Author.LoginName.Equals(currentUser.LoginName) && file.ServerRelativeUrl.Split('/')[file.ServerRelativeUrl.Split('/').Length - 1].Equals(strName))
                                {
                                    RepeatDoc += strName + ";";
                                    f = 1;
                                }
                            }
                        }
                        if (f < 1)
                        {
                            count++;
                            SPFolder Newfolder = web.GetFolder(list.RootFolder.ServerRelativeUrl);
                            if (!folder.Exists)
                            {
                                folder = RootFolder;
                            }
                            System.IO.Stream stream = Request.Files[i].InputStream;
                            byte[] bytFile = new byte[Convert.ToInt32(Request.Files[i].ContentLength)];
                            stream.Read(bytFile, 0, Convert.ToInt32(Request.Files[i].ContentLength));
                            stream.Close();

                            web.AllowUnsafeUpdates = true;
                            SPFile file = folder.Files.Add(System.IO.Path.GetFileName(Request.Files[i].FileName), bytFile, true);

                            SPItem item = file.Item;
                            item["SubJectID"] = hSubject.Value;
                            item["CatagoryID"] = hContent.Value;
                            item["TypeName"] = HStatus.Value;// lbleixing.Text;
                            item.Update();

                            web.AllowUnsafeUpdates = false;
                        }
                    }

                }

            }

            if (RepeatDoc.Length > 0)
            {
                script = "以下文件名重复未上传【" + RepeatDoc + "】-成功上传文件个数【" + count.ToString() + "】";
            }
            else
            {
                script = "所有文件上传成功-文件个数【" + count.ToString() + "】";

            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "ShowData(1)", true);

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('" + script + "！');", true);

        }

        
        private void BindMajor()
        {
            //try
            //{
            //    Privileges.Elevated((oSite, oWeb, args) =>
            //    {
            //        using (new AllowUnsafeUpdates(oWeb))
            //        {
            //            DataTable dtMajor = new DataTable();
            //            dtMajor.Columns.Add("ID");
            //            dtMajor.Columns.Add("Title");
            //            DataRow dr = dtMajor.NewRow();
            //            dr["ID"] = "0";
            //            dr["Title"] = "请选择专业";
            //            dtMajor.Rows.Add(dr);
            //            SPList GetSPList = oWeb.Lists.TryGetList("资源列表");
            //            SPQuery query = new SPQuery();
            //            query.Query = @"<Where><Eq><FieldRef Name='CType' /><Value Type='Text'>专业</Value></Eq></Where>";
            //            SPListItemCollection itemlist = GetSPList.GetItems(query);
            //            for (int i = 0; i < itemlist.Count; i++)
            //            {
            //                DataRow dr1 = dtMajor.NewRow();
            //                dr1["ID"] = itemlist[i]["ID"];
            //                dr1["Title"] = itemlist[i]["Title"];
            //                dtMajor.Rows.Add(dr1);
            //            }
            //            // itemlist.GetDataTable();

            //            dpSubject.DataSource = dtMajor;
            //            dpSubject.DataBind();
            //            //dpSubject.Items.Add(new ListItem("",""),0)

            //        }
            //    }, true);
            //}
            //catch (Exception ex)
            //{
            //    com.writeLogMessage(ex.Message, "SchoolLibrary.Major");
            //}
        }

        protected void btadd_Click(object sender, EventArgs e)
        {
            //if (dpMajor.SelectedValue == "2")
            //{
            //    if (dpSubject.SelectedValue == "0")
            //    {
            //        dpSubject.Focus();
            //    }
            //    else
            //    {
            //        if (this.txtName.Value.Trim() == "")
            //        {
            //            txtName.Focus();
            //        }
            //        else
            //        {
            //            //添加学科
            //            Add(txtName.Value, dpSubject.SelectedValue);
            //        }
            //    }
            //}
            //else
            //{
            //    if (this.txtName.Value.Trim() == "")
            //    {
            //        txtName.Focus();
            //    }
            //    else
            //    {
            //        //添加专业
            //        Add(txtName.Value, "0");
            //    }
            //}
        }
        private void Add(string name, string pid)
        {

            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList termList = oWeb.Lists.TryGetList("资源列表");
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('名称重复！')</script>");
                    }
                    else
                    {
                        SPListItem newItem = termList.Items.Add();

                        newItem["Title"] = name;
                        newItem["Pid"] = pid;
                        if (pid == "0")
                        {
                            newItem["CType"] = "专业";
                        }
                        else
                        {
                            SPListItem olditem = termList.Items.GetItemById(Convert.ToInt32(pid));
                            if (olditem != null)
                            {
                                newItem["CType"] = "学科";
                            }
                        }
                        newItem.Update();
                        this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('添加成功！')</script>");
                    }
                }

            }, true);
        }

    }
}
