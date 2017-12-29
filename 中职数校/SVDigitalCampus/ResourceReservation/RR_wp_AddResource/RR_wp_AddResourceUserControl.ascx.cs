using Common;
using Microsoft.SharePoint;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_AddResource
{
    public partial class RR_wp_AddResourceUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        SPWeb spweb = SPContext.Current.Web;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            

            if (!IsPostBack)
            {
                BindSourceType();
                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    ViewState["itemId"] = itemid;
                    BindForm(Convert.ToInt32(itemid));
                }
            }

        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                    using (new AllowUnsafeUpdates(spweb))
                    {
                        SPList list = spweb.Lists.TryGetList("专业教室资源表");
                        
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["itemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["itemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                        }
                        if (item["ResourcesType"].SafeToString().Equals("资产管理") || ViewState["ResourcesType"].SafeToString()=="资产管理")
                        {
                            
                            Lit_Type.Text = item["ResourcesType"].SafeToString();
                            Hid_TypeId.Value = item["ResourcesTypeId"].SafeToString();

                            item["Title"]=TB_Title.Text;
                            item["Holder"] = TB_Place.Text;
                            item["BelongSchool"] = TB_Area.Text;
                            item["Department"] = TB_LimitCount.Text;

                        }
                        else
                        {
                            item["Title"] = TB_Title.Text;
                            item["ResourcesType"] = Lit_Type.Text;
                            item["ResourcesTypeId"] = Hid_TypeId.Value;

                            item["Place"] = TB_Place.Text;
                            item["Area"] = TB_Area.Text;
                            item["LimitCount"] = TB_LimitCount.Text;
                        }
                        item["OpenTime"] = TB_OpenTime.Text;
                        item["CloseTime"] = TB_CloseTime.Text;
                        string fileContentType = zpload.PostedFile.ContentType;
                        if(zpload.HasFile)
                        {
                            if (fileContentType == "image/png" || fileContentType == "image/jpg" || fileContentType == "image/jpeg")
                            {
                                if (Request.Files.Count > 0)
                                {
                                    string strDocName = Path.GetFileName(Request.Files[0].FileName);
                                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                                    if (extension != ".jpg" && extension != ".png")
                                    {
                                        //isPicture = false;
                                    }
                                    else
                                    {
                                        byte[] upBytes = new Byte[Request.Files[0].ContentLength];
                                        Stream upstream = Request.Files[0].InputStream;
                                        upstream.Read(upBytes, 0, Request.Files[0].ContentLength);
                                        upstream.Dispose();
                                        SPAttachmentCollection attachments = item.Attachments;
                                        attachments.Add(strDocName, upBytes);
                                    }

                                }
                                item.Update();
                                ViewState["itemId"] = "";
                            }
                            else
                            {
                                script = "alert('请选择图片格式文件');";
                            }
                        }
                        else
                        {
                            item.Update();
                        }
                    }
                
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        private void BindForm(int itemid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPListItem item = list.GetItemById(itemid);
                        if (item["ResourcesType"].SafeToString().Equals("车辆"))
                        {
                            this.Lit_Place.Text = "车辆型号：";
                            this.Lit_Area.Text = "生产年代：";
                            this.Lit_LimitCount.Text = "座位数：";
                            this.Lit_Title.Text = "车辆名称：";
                        }
                        if (item["ResourcesType"].SafeToString().Equals("会议室") || item["ResourcesType"].SafeToString().Equals("专业教室"))
                        {
                            this.Pan_show.Visible = true;
                        }
                        Lit_Type.Text = item["ResourcesType"].SafeToString();
                        Hid_TypeId.Value = item["ResourcesTypeId"].SafeToString();
                        TB_Title.Text = item["Title"].SafeToString();
                        TB_Place.Text = item["Place"].SafeToString();
                        TB_Area.Text = item["Area"].SafeToString();
                        TB_LimitCount.Text = item["LimitCount"].SafeToString();
                        TB_OpenTime.Text = item["OpenTime"].SafeToString();
                        TB_CloseTime.Text = item["CloseTime"].SafeToString();
                        if (item["ResourcesType"].SafeToString().Equals("资产管理"))
                        {
                            this.Lit_Title.Text = "资产名称：";
                            this.Lit_Place.Text = "持有者：";
                            this.Lit_Area.Text = "所属学校：";
                            this.Lit_LimitCount.Text = "部门：";
                            Lit_Type.Text = item["ResourcesType"].SafeToString();
                            Hid_TypeId.Value = item["ResourcesTypeId"].SafeToString();

                            TB_Title.Text = item["Title"].SafeToString();
                            TB_Place.Text = item["Holder"].SafeToString();
                            TB_Area.Text = item["BelongSchool"].SafeToString();
                            TB_LimitCount.Text = item["Department"].SafeToString();

                        }
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        private void BindSourceType() 
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string typeId = Request.QueryString["typeId"].SafeToString();
                        SPList list = oWeb.Lists.TryGetList("资源类型表");
                        if(typeId!="")
                        {
                            SPListItem item = list.GetItemById(Convert.ToInt32(typeId));
                            Lit_Type.Text = item.Title;
                            if (item.Title == "车辆")
                            {
                                this.Lit_Title.Text = "车辆名称：";
                                this.Lit_Place.Text = "车辆型号：";
                                this.Lit_Area.Text = "生产年代：";
                                this.Lit_LimitCount.Text = "座位数：";

                            }
                            else if (item.Title == "会议室" || item.Title == "专业教室")
                            {
                                this.Pan_show.Visible = true;
                            }
                            if (item.Title == "资产管理")
                            {
                                this.Lit_Title.Text = "资产名称：";
                                this.Lit_Place.Text = "持有者：";
                                this.Lit_Area.Text = "所属学校：";
                                this.Lit_LimitCount.Text = "部门：";
                                ViewState["ResourcesType"] = "资产管理";
                            }

                            Hid_TypeId.Value = item.ID.SafeToString();
                        }
                        //query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("")));
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }
    }
}
