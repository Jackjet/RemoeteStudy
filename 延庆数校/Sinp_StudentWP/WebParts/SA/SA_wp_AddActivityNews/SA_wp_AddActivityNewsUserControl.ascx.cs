using Common;
using Microsoft.SharePoint;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_AddActivityNews
{
    public partial class SA_wp_AddActivityNewsUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data"); 
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                }
            }
        }

        protected void Btn_NewsSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();parent.closePages();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("部门新闻");
                        string queryStr = CAML.Eq(CAML.FieldRef("Title"),CAML.Value(TB_Title.Text));
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["itemid"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["itemid"]);
                            item = list.GetItemById(intItemId);
                            queryStr = string.Format(CAML.And("{0}", CAML.Neq(CAML.FieldRef("ID"), CAML.Value(intItemId))), queryStr);
                        }
                        else
                        {
                            SPUser curre = SPContext.Current.Web.CurrentUser;
                            item = list.AddItem();
                            SPFieldUserValue sfvUser = new SPFieldUserValue(oWeb, curre.ID, curre.Name);
                            item["Author"] = sfvUser;
                            item["DepartmentID"] = Request.QueryString["departid"];
                            item["ClickCount"] = 0;
                        }
                        SPListItemCollection actlist = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(queryStr)
                        });
                        //判断"部门新闻"列表中是否存在名称相同的新闻
                        if (actlist != null && actlist.Count > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败，已存在同名新闻！');", true);
                            return;
                        }
                        item["Title"] = TB_Title.Text;
                        item["Content"] = TB_Content.Text;

                        //判断是否上传图片
                        if (this.fimg_News.PostedFile.FileName != null && this.fimg_News.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpimage = this.fimg_News.PostedFile;
                            string photoName = hpimage.FileName;//获取初始文件名
                            string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                            if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择图片文件！');", true);
                                return;
                            }
                            System.IO.Stream stream = hpimage.InputStream;
                            byte[] bytPhoto = new byte[Convert.ToInt32(hpimage.ContentLength)];
                            stream.Read(bytPhoto, 0, Convert.ToInt32(hpimage.ContentLength));
                            stream.Close();
                            item.Attachments.Add(photoName, bytPhoto); //为列表添加附件
                        }
                        item.Update();                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('发布失败，请重试...')";
                com.writeLogMessage(ex.Message, "SA_wp_AddActivityNews_Btn_NewsSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);   
        }
    }
}
