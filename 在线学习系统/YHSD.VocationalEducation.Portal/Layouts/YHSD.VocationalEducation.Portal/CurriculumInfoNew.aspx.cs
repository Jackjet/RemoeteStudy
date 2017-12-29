using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Web;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumInfoNew : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Request.UrlReferrer != null)
                {
                    ViewState["retu"] = Request.UrlReferrer.ToString();
                }
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    BindCurriculum(Request["id"]);
                }
                if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
                {
                    GetCurriculumPhoto();
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["DeleteID"]))
                {
                    DeleteCurriculum(Request["DeleteID"].ToString());
                }
            }
            this.TB_StartTime.Text = DateTime.Now.SafeToDataTime();
            this.TB_EndTime.Text = DateTime.Now.SafeToDataTime();
        }
        public void DeleteCurriculum(string id)
        { 
            string Value = "";
            if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID='" + id + "' and IsDelete=0")) > 0)
            {
                Value = "请先删除此课程下面的知识点再删除此课程!!!!";
            }
            else if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from ClassCurriculum where CurriculumID='" + id + "' and IsDelete=0")) > 0)
            {
                Value = "此课程已分配给班级,请在班级管理移除此课程再删除!!!!";
            }
            else 
            {
                //将此课程相关课程表的数据删除;
                new CurriculumRelationManager().DeleteQuan(id);
                //删除课程表
                new CurriculumInfoManager().Delete(id);
                Value = "ok";
            }
            Response.Write(Value);
            Response.End();
        }
        public void GetCurriculumPhoto()
        {
            HttpPostedFile file = Request.Files["FileData"];
            string UserPhoto = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, Guid.NewGuid().ToString() + "_" + file.FileName, file.InputStream, "");
            Response.Write(UserPhoto);
            Response.End();
        }
        public void BindCurriculum(string id)
        {
            CurriculumInfo cur = new CurriculumInfoManager().Get(id);
            txtDescription.Text = cur.Description;
            txtTitle.Text = cur.Title;
            HDID.Value = cur.Id;
            HDCurriculumInfoType.Value = cur.ResourceID;
            if (cur.IsOpenCourses.ToLower() == "true")
            {
                radYes.Checked = true;
                radNo.Checked = false;
            }
            else
            {
                radNo.Checked = true;
                radYes.Checked = false;
                if (!string.IsNullOrEmpty(new ClassCurriculumManager().GetCurriculumID(HDID.Value).ID))
                {
                    radYes.Enabled = false;
                    radNo.Enabled = false;
                }
            }
            //shifoumianfei
            //if (cur.IfFree.ToLower() == "true")
            //{
            //    ShouFei.Checked = true;
            //    BuShouFei.Checked = false;
            //}
            //else
            //{
            //    BuShouFei.Checked = true;
            //    ShouFei.Checked = false;
            //    if (!string.IsNullOrEmpty(new ClassCurriculumManager().GetCurriculumID(HDID.Value).ID))
            //    {
            //        ShouFei.Enabled = false;
            //        BuShouFei.Enabled = false;
            //    }
            //}
            //Class_Price.Text = cur.ClassPrice;
            //ParentNameId.InnerText = new ResourceClassificationManager().Get(cur.ResourceID).Name;
            if (!string.IsNullOrEmpty(cur.ImgUrl))
            {
                KeChenImg.Src = cur.ImgUrl;
                //LabelImgUrl.Visible = true;
               
            }
            if (Convert.ToInt32(ConnectionManager.GetSingle(string.Format("select count(*) from CurriculumRelation where CurriculumID='{0}'", cur.Id))) > 0)
            {
                XiangGuanName.InnerText = GetCurriculumName(cur.Id);
            }
        }
        public string GetCurriculumName(string CurriculumId)
        {
            string CurriculumName = "";
            CurriculumRelation relation = new CurriculumRelation();
            relation.CurriculumID = CurriculumId;
            List<CurriculumRelation> list = new CurriculumRelationManager().Find(relation, -1, 0);
            for (int i = 0; i < list.Count; i++)
            {
                CurriculumName = CurriculumName + ConnectionManager.GetSingle(string.Format("select Title from CurriculumInfo where Id='{0}'", list[i].CurriculumRelationID)).ToString() + ";";
            }
            return CurriculumName;
        }
        protected void BTCompanySave_Click(object sender, EventArgs e)
        {
            CurriculumInfo cur = new CurriculumInfo();
            CurriculumInfoManager curManager = new CurriculumInfoManager();
            if (!string.IsNullOrEmpty(HDID.Value))
            {
                CurriculumInfo Laocur = new CurriculumInfoManager().Get(HDID.Value);
                cur.Id = HDID.Value;
                cur.Description = txtDescription.Text.Trim();
                if (!string.IsNullOrEmpty(HDCurriculumImage.Value))
                {
                    string NewFileImage = CommonUtil.GetChildWebUrl()+ConnectionManager.ImgKuUrl + "/" + ConnectionManager.CurriculumName + HDCurriculumImage.Value.Substring(HDCurriculumImage.Value.LastIndexOf('/'));
                    CommonUtil.MoveFuJian(HDCurriculumImage.Value, NewFileImage);
                    CommonUtil.DeleteFuJian(Laocur.ImgUrl);
                    cur.ImgUrl = NewFileImage;
                    
                }
                else
                {
                    cur.ImgUrl = Laocur.ImgUrl;
                }
                cur.IsOpenCourses = radYes.Checked == true ? "1" : "0";
                //是否免费
                cur.IfFree = ShouFei.Checked == true ? "1" : "0";
                //线上线下
                cur.ClassMode = XianShang.Checked == true ? "线上" : "线下";
                
                cur.ClickNumber = Laocur.ClickNumber;
                cur.CreaterTime = Laocur.CreaterTime;
                cur.IsDelete = Laocur.IsDelete;
                cur.ResourceID = HDCurriculumInfoType.Value;
                cur.Title = txtTitle.Text;
                cur.ClassPrice = Class_Price.Text;
                if (!string.IsNullOrEmpty(TB_StartTime.Text))
                {
                    cur.StartTime = Convert.ToDateTime(TB_StartTime.Text);
                }
                if (!string.IsNullOrEmpty(TB_StartTime.Text))
                {
                    cur.EndTime = Convert.ToDateTime(TB_EndTime.Text);
                }
                
                curManager.Update(cur);
                //保存相关课程
                if (!string.IsNullOrEmpty(HDSelectCurriculum.Value))
                {
                    CurriculumRelation relation = new CurriculumRelation();
                    CurriculumRelationManager relationManager = new CurriculumRelationManager();
                    if (Convert.ToInt32(ConnectionManager.GetSingle(string.Format("select count(*) from CurriculumRelation where CurriculumID='{0}'", cur.Id))) > 0)
                    {
                        relationManager.Delete(cur.Id);
                    }
                    string[] CurriculumString = HDSelectCurriculum.Value.Split(';');
                    for (int i = 0; i < CurriculumString.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(CurriculumString[i]))
                        {
                            relation.Id = Guid.NewGuid().ToString();
                            relation.CurriculumID = cur.Id;
                            relation.CurriculumRelationID = CurriculumString[i].ToString();
                            relationManager.Add(relation);
                        }
                    }
                }
                if (ViewState["retu"] != null && !string.IsNullOrEmpty(ViewState["retu"].ToString()))
                {
                    this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('编辑课程成功!!',function(){window.location.href ='" + ViewState["retu"].ToString() + "';});</script>;");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('编辑课程成功!!',function(){window.history.go(-2);});</script>;");
                }
            }
            else
            {

                cur.Id = Guid.NewGuid().ToString();
                cur.ClickNumber = "0";
                cur.CreaterTime = DateTime.Now.ToString();
                cur.Description = txtDescription.Text.Trim();
                if (!string.IsNullOrEmpty(HDCurriculumImage.Value))
                {
                    string NewFileImage = CommonUtil.GetChildWebUrl()+ConnectionManager.ImgKuUrl + "/" + ConnectionManager.CurriculumName + HDCurriculumImage.Value.Substring(HDCurriculumImage.Value.LastIndexOf('/'));
                    CommonUtil.MoveFuJian(HDCurriculumImage.Value, NewFileImage);
                    cur.ImgUrl = NewFileImage;
                   // byte[] b = FildImgUrl.FileBytes;
                   // cur.ImgUrl = new CommonUtil().CreatetFuJianName(ConnectionManager.CurriculumName, ConnectionManager.ImgKuUrl, cur.Id + "_" + FildImgUrl.FileName, b, "");
                }
                cur.IsDelete = "0";
                cur.ResourceID = HDCurriculumInfoType.Value;
                cur.Title = txtTitle.Text.Trim();
                cur.CreaterUserID = CommonUtil.GetSPADUserID().Id;
                cur.IsOpenCourses = radYes.Checked == true ? "1" : "0";
                //是否免费
                cur.IfFree = ShouFei.Checked == true ? "1" : "0";
                cur.ClassPrice = Class_Price.Text;
                if (!string.IsNullOrEmpty(TB_StartTime.Text))
                {
                    cur.StartTime = Convert.ToDateTime(TB_StartTime.Text);
                }
                if (!string.IsNullOrEmpty(TB_StartTime.Text))
                {
                    cur.EndTime = Convert.ToDateTime(TB_EndTime.Text);
                }
                //线上线下
                cur.ClassMode = XianShang.Checked == true ? "线上" : "线下";
                if (XianShang.Checked)
                {
                    curManager.Add(cur);
                }
                else 
                {
                    SPWeb web = SPContext.Current.Web;
                        SPList list_XianXIaClass = web.Lists.TryGetList("XianXIaClass");
                        SPListItem item = list_XianXIaClass.AddItem();
                        item["Title"] = txtTitle.Text;
                        item["KaiBanShiJian"] = System.DateTime.Now;
                        item["ShangKeShiJian"] = "9:00-11:00";
                        item["ShangKeDiDian"] = "2号教学楼301教室";
                        item["ShouKeJiaoShi"] = "李明";
                        item.Update();                     
                }
                
                //保存相关课程
                if (!string.IsNullOrEmpty(HDSelectCurriculum.Value))
                {
                    CurriculumRelation relation = new CurriculumRelation();
                    CurriculumRelationManager relationManager = new CurriculumRelationManager();
                    string[] CurriculumString = HDSelectCurriculum.Value.Split(';');
                    for (int i = 0; i < CurriculumString.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(CurriculumString[i]))
                        {
                            relation.Id = Guid.NewGuid().ToString();
                            relation.CurriculumID = cur.Id;
                            relation.CurriculumRelationID = CurriculumString[i].ToString();
                            relationManager.Add(relation);
                        }
                    }
                }
                if (ViewState["retu"] != null && !string.IsNullOrEmpty(ViewState["retu"].ToString()))
                {
                    this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('添加课程成功!!',function(){window.location.href ='" + ViewState["retu"].ToString() + "';});</script>;");
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('添加课程成功!!',function(){window.history.go(-2);});</script>;");
                }
            }
           
        }
    }
}
