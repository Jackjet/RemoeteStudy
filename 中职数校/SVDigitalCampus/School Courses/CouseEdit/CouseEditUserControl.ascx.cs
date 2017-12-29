using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;
using Common.SchoolUser;
using System.Web;
using System.Text;
namespace SVDigitalCampus.School_Courses.CouseEdit
{

    public partial class CouseEditUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindTeacher();
                //BindMajor();
                Bind();
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
                        ddTeacher.Items.Insert(0, new ListItem("==请选择老师==", "-1"));
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CouseEdit.BindTeacher");
            }
            //UserPhoto user = new UserPhoto();
            //DataTable dt = user.GetTeacherALL();
            //ddTeacher.DataSource = dt;
            //ddTeacher.DataTextField = "XM";
            //ddTeacher.DataValueField = "SFZJH";
            //ddTeacher.DataBind();
            //ddTeacher.Items.Insert(0, new ListItem("==请选择老师==", "-1"));

        }
        #endregion
        private void Bind()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(Request["CourseID"]));
                        if (item != null)
                        {
                            txtName.Value = item["Title"].safeToString();
                            ddTeacher.SelectedValue = item["TeacherID"].safeToString();
                           // ddMajor.Value = item["MajorID"].safeToString();
                            //BindSubJect(ddMajor.SelectedValue);
                            //Year.Value = item["StudyYear"].safeToString();
                            //ddDate.SelectedValue = item["StudryDate"].safeToString();
                            //ddSubJect.Value = item["SubjectID"].safeToString();
                            Count.Value = item["MaxNum"].safeToString();
                            //BeginTime.Value = item["BeginTerm"].safeToString();
                           // EndTime.Value = item["EndTerm"].safeToString();
                            Introduc.Value = item["Introduc"].safeToString();
                            Annonce.Value = item["Annonce"].safeToString();

                            #region 查看附件

                            if (item.Attachments.Count > 0)
                            {
                                img_Pic.ImageUrl = item.Attachments.UrlPrefix + item.Attachments[0];
                            }
                            #endregion
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CouseEditUserControl.ascx_BindListView");
            }

        }
        /*
        #region
        #region 绑定专业
        private void BindMajor()
        {
            UserPhoto user = new UserPhoto();
            try
            {
                DataTable dt = user.GetGradeAndSubjectBySchoolID();
                ddMajor.DataSource = dt;
                ddMajor.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CouseEditUserControl.BindMajor");
            }
        }

        #endregion

        #region 绑定学科
        private void BindSubJect(string MajorID)
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
                    if (dr["NJ"].ToString() == MajorID)
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
                com.writeLogMessage(ex.Message, "CouseEditUserControl.SubJect");
            }
            ddSubJect.DataSource = dts;
            ddSubJect.DataBind();
        }


        #endregion
        #endregion
        */

        protected void btOk_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPListItem newItem = list.Items.GetItemById(Convert.ToInt32(Request["CourseID"]));
                        newItem["Title"] = txtName.Value.Trim();
                        newItem["TeacherID"] = ddTeacher.SelectedValue;
                        newItem["MajorID"] =hdMajor.Value;
                        newItem["SubjectID"] =hdSubJect.Value;
                        newItem["MaxNum"] = Convert.ToInt32(Count.Value.Trim());
                        newItem["BeginTerm"] = hdBTime.Value;
                        newItem["EndTerm"] = hdETime.Value;
                        newItem["BeginTime"] = TermTime("F", hdBTime.Value);
                        newItem["EndTime"] = TermTime("L", hdETime.Value);

                        newItem["Introduc"] = Introduc.Value;
                        newItem["Annonce"] = Annonce.Value;
                        //判断是否上传图片
                        if (this.fimg_Asso.PostedFile.FileName != null && this.fimg_Asso.PostedFile.FileName.Trim() != "")
                        {
                            SPAttachmentCollection att = newItem.Attachments;
                            if (att.Count > 0)
                            {
                                string name = newItem.Attachments[0];
                                newItem.Attachments.Delete(name);
                            }

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
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/CaurseManage.aspx';", true);

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseAddUserControl.btok_Click");
            }
        }
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
