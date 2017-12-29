using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
using System.Data;
using Common;
using Microsoft.SharePoint;
using System.Web;
namespace SVDigitalCampus.School_Courses.CaurseAdd
{

    public partial class CaurseAddUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string rootUrl = SPContext.Current.Web.Url;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindTeacher();
            }
        }
        #region 绑定老师信息
        private void BindTeacher()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPGroup group = oWeb.Groups["老师组"];
                        SPUserCollection users = group.Users;
                        foreach (SPUser item in users)
                        {
                            ddTeacher.Items.Add(new ListItem(item.Name, item.ID + ";#" + item.Name));
                        }
                        ddTeacher.SelectedValue = SPContext.Current.Web.CurrentUser.ID + ";#" + SPContext.Current.Web.CurrentUser.Name;
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseAdd.BindTeacher");
            }
        }
        #endregion

        /* #region 绑定专业 学科
        private void BindMajor()
        {
            UserPhoto user = new UserPhoto();
            try
            {
                DataTable dt = user.GetGradeAndSubjectBySchoolID();
                ddMajor.DataSource = dt;
                ddMajor.DataBind();
                ddMajor.Items.Insert(0, new ListItem("==请选择专业==", "-1"));
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseAddUserControl.BindMajor");
            }
        }
        protected void ddMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubJect();
        }
       
        private void BindSubJect()
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("ID");
            dts.Columns.Add("Title");

            UserPhoto user = new UserPhoto();
            try
            {
                string subjectList = "";
                DataTable dt = user.GetGradeAndSubjectBySchoolID();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["NJ"].ToString() == ddMajor.Value)
                    {
                        subjectList = dr["XK"].ToString().TrimEnd(';');
                        string[] sj = subjectList.Split(';');
                        for (int i = 0; i < sj.Length; i++)
                        {
                            DataRow tr = dts.NewRow();
                            tr["ID"] = sj[i].Split(',')[0].ToString() + dr["ID"].ToString();
                            tr["Title"] = sj[i].Split(',')[1];
                            dts.Rows.Add(tr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.SubJect");
            }
            ddSubJect.DataSource = dts;
            ddSubJect.DataBind();
            ddSubJect.Items.Insert(0, new ListItem("==请选择学科==", "-1"));

        }


        #endregion*/

        #region 绑定校本资源库目录结构
        private void BindLibraryMenu(string id, TreeNodeCollection tnc)
        {

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPSite sit = SPContext.Current.Site;
                        SPWeb web = sit.OpenWeb("SchoolLibrary");

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + id + "</Value></Eq></Where>";
                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("ID");
                        //dt.Columns.Add("Title");

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                TreeNode node = new TreeNode();
                                node.Value = item["ID"].ToString();
                                node.Text = item["Title"].ToString();
                                tnc.Add(node);
                                BindLibraryMenu(item["ID"].ToString(), node.ChildNodes);

                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseAddUserControl.BindLibraryMenu");
            }

        }
        #endregion

        //#region 绑定资源
        //private void BindData(string SubJectID)
        //{
        //    try
        //    {
        //        Privileges.Elevated((oSite, oWeb, args) =>
        //        {
        //            using (new AllowUnsafeUpdates(oWeb))
        //            {
        //                SPSite sit = SPContext.Current.Site;
        //                SPWeb web = sit.OpenWeb("SchoolLibrary");

        //                DataTable dt = BuildDataTable();
        //                SPQuery query = new SPQuery();
        //                query.Query = "<Where><And><Eq><FieldRef Name='SubJectID' /> <Value Type='Number'>" + SubJectID + "</Value></Eq><Eq> <FieldRef Name='Status' /> <Value Type='Number'>1</Value></Eq></And>  </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
        //                SPList list = web.Lists.TryGetList("校本资源库");
        //                SPListItemCollection termItems = list.GetItems(query);
        //                if (termItems != null)
        //                {
        //                    foreach (SPItem item in termItems)
        //                    {
        //                        GreatDt(dt, item);
        //                    }
        //                }

        //                Datas.DataSource = dt;
        //                Datas.DataBind();
        //            }
        //        }, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
        //    }
        //}


        //private DataTable BuildDataTable()
        //{
        //    DataTable dataTable = new DataTable();
        //    string[] arrs = new string[] { "Name", "Type", "Size", "Image", "Created", "Modified", "ID", "Url", "Title" };
        //    foreach (string column in arrs)
        //    {
        //        dataTable.Columns.Add(column);
        //    }
        //    return dataTable;
        //}
        //private void GreatDt(DataTable dt, SPItem item)
        //{
        //    DataRow dr = dt.NewRow();
        //    string Type = item["内容类型"].ToString();
        //    dr["Type"] = Type;
        //    string ImageUrl = "";
        //    string DocIcon = item["DocIcon"] == null ? "" : item["DocIcon"].ToString();
        //    dr["Name"] = item["BaseName"];

        //    if (Type == "文件夹")
        //    {
        //        dr["Image"] = "/_layouts/15/images/folder.gif?rev=23";
        //    }
        //    else
        //    {
        //        if (DocIcon == "html")
        //        {
        //            DocIcon = "htm";
        //        }
        //        ImageUrl = "/_layouts/15/images/ic" + DocIcon + ".gif";//ictxt.gif";
        //        dr["Image"] = ImageUrl;
        //    }
        //    string size = item["文件大小"].ToString();
        //    if (size == "")
        //    {
        //        dr["Size"] = "--";
        //    }
        //    else
        //    {
        //        if (int.Parse(size) < 1024 * 1024)
        //        {
        //            dr["Size"] = int.Parse(size) / 1024 + "KB";
        //        }
        //        if (int.Parse(size) > 1024 * 1024)
        //        {
        //            dr["Size"] = int.Parse(size) / 1024 / 1024 + "M";
        //        }
        //    }
        //    dr["Created"] = item["Created"];
        //    dr["Modified"] = item["Modified"];

        //    dr["ID"] = item["ID"];
        //    dr["Url"] = item["ServerUrl"];
        //    dr["Title"] = item["Title"];
        //    if (DocIcon != "")
        //    {
        //        dr["Title"] += "." + DocIcon;
        //    }

        //    dt.Rows.Add(dr);
        //}
        //#endregion

        //提交数据
        //protected void btok_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Privileges.Elevated((oSite, oWeb, args) =>
        //        {
        //            using (new AllowUnsafeUpdates(oWeb))
        //            {
        //                SPList list = oWeb.Lists.TryGetList("校本课程");
        //                SPListItem newItem = list.Items.Add();
        //                newItem["Title"] = txtName.Value.Trim();
        //                newItem["TeacherID"] = ddTeacher.SelectedValue;
        //                newItem["MajorID"] = ddMajor.SelectedValue;
        //                newItem["StudyYear"] = Year.Value.Trim();
        //                newItem["StudryDate"] = ddDate.SelectedValue;
        //                newItem["SubjectID"] = ddSubJect.SelectedValue;
        //                newItem["MaxNum"] = Convert.ToInt32(Count.Value.Trim());
        //                newItem["BeginTime"] = BeginTime.Value;
        //                newItem["EndTime"] = EndTime.Value;
        //                newItem["Introduc"] = Introduc.Value;
        //                string courseAttr = "";
        //                for (int i = 0; i < Datas.Items.Count; i++)
        //                {
        //                    CheckBox cb = (CheckBox)Datas.Items[i].FindControl("ckNew");
        //                    if (cb.Checked)
        //                    {
        //                        string id = ((Label)Datas.Items[i].FindControl("lbID")).Text;
        //                        courseAttr += id + ",";
        //                    }
        //                }
        //                newItem["CourseAttr"] = courseAttr;
        //                //判断是否上传图片
        //                if (this.fimg_Asso.PostedFile.FileName != null && this.fimg_Asso.PostedFile.FileName.Trim() != "")
        //                {
        //                    HttpPostedFile hpimage = this.fimg_Asso.PostedFile;
        //                    string photoName = hpimage.FileName;//获取初始文件名
        //                    string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
        //                    if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
        //                    {
        //                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择图片文件！');", true);
        //                        return;
        //                    }
        //                    System.IO.Stream stream = hpimage.InputStream;
        //                    byte[] imgbyte = new byte[Convert.ToInt32(hpimage.ContentLength)];
        //                    stream.Read(imgbyte, 0, Convert.ToInt32(hpimage.ContentLength));
        //                    stream.Close();
        //                    newItem.Attachments.Add(photoName, imgbyte); //为列表添加附件
        //                }
        //                newItem.Update();
        //                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>CheckDate();</script>");

        //            }
        //        }, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        com.writeLogMessage(ex.Message, "CaurseAddUserControl.btok_Click");
        //    }
        //}

        #region 根据当前登录用户判断是否是老师，返回老师身份证号
        /// <summary>
        /// 根据当前登录用户判断是否是老师，返回老师身份证号
        /// </summary>
        /// <returns></returns>
        private string GetTeachSFZ()
        {
            string SFZ = "";
            UserPhoto user = new UserPhoto();
            string UserName = SPContext.Current.Web.CurrentUser.Name;
            DataSet ds = user.GetBaseTeacherInfo(UserName);
            DataTable dt = ds.Tables[0];
            if (dt != null)
            {
                SFZ = dt.Rows[0]["SFZJH"].ToString();
            }
            return SFZ;
        }
        #endregion

        #region 添加课程-提交数据
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btOk_Click(object sender, EventArgs e)
        {
            btOk.Enabled = false;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPListItem newItem = list.Items.Add();
                        newItem["Title"] = txtName.Value.Trim();
                        newItem["TeacherID"] = ddTeacher.SelectedValue;
                        newItem["MajorID"] = hdMajor.Value;// ddMajor.Items[2].Value;
                        newItem["BeginTerm"] = hdBTime.Value;// Request.Form["BeginTime"]; //BeginTime.Items[BeginTime.SelectedIndex].Value;
                        newItem["EndTerm"] = hdETime.Value;// EndTime.Items[EndTime.SelectedIndex].Value;
                        newItem["SubjectID"] = hdSubJect.Value;// ddSubJect.Items[ddSubJect.SelectedIndex].Value;
                        newItem["MaxNum"] = Convert.ToInt32(Count.Value.Trim());
                        newItem["BeginTime"] = TermTime("F", hdBTime.Value);
                        newItem["EndTime"] = TermTime("L", hdETime.Value);
                        newItem["Introduc"] = Introduc.Value;
                        newItem["Annonce"] = Annonce.Value;
                        //判断是否上传图片

                        if (this.fimg_Asso.PostedFile.FileName != null && this.fimg_Asso.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpimage = this.fimg_Asso.PostedFile;
                            string photoName = hpimage.FileName;//获取初始文件名
                            string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                            if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择图片文件！');", true);
                                return;
                            }
                            System.IO.Stream stream = hpimage.InputStream;
                            byte[] imgbyte = new byte[Convert.ToInt32(hpimage.ContentLength)];
                            stream.Read(imgbyte, 0, Convert.ToInt32(hpimage.ContentLength));
                            stream.Close();
                            newItem.Attachments.Add(photoName, imgbyte); //为列表添加附件
                        }
                        newItem.Update();
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('添加成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/CaurseManage.aspx';", true);

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseAddUserControl.btOk_Click1");
            }
            btOk.Enabled = true;
        }
        #endregion
        private string TermTime(string Type, string StudysectionID)
        {
            string RResult = "";
            UserPhoto user = new UserPhoto();
            DataTable section = user.GetStudysection().Tables[0];
            DataRow[] rows = section.Select("StudysectionID=" + StudysectionID);
            if (Type == "F")
            {
                RResult = rows[0]["SStartDate"].safeToString();
            }
            else
                RResult = rows[0]["SEndDate"].safeToString();
            return RResult;
        }
    }
}
