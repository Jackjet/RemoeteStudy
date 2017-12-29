using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_AddDepartment
{
    public partial class SA_wp_AddDepartmentUserControl : UserControl
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
                    BindDepartmentData(itemId);
                }
                else
                {
                    BindDropMembers();
                }
            }
        }
        private void BindDepartmentData(string departid)
        {
            try
            {
                int itemId = Convert.ToInt32(departid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        #region 学生会组织机构
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        SPListItem item = list.GetItemById(itemId);
                        this.TB_Title.Text = item.Title;                       
                        this.TB_Content.Text = item["Introduce"].SafeToString();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        #endregion                        

                        #region 为部长，副部长绑定值
                        this.DDL_Leader.Items.Clear();
                        this.DDL_SecLeader.Items.Clear();
                        SPList melist = oWeb.Lists.TryGetList("部门成员");
                        SPListItemCollection items = melist.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(itemId)))
                        });
                        if (items != null && items.Count > 0)
                        {
                            foreach (SPListItem meitem in items)
                            {
                                string[] arrs = meitem["Member"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                DDL_Leader.Items.Add(new ListItem(arrs[1], arrs[0]));
                                DDL_SecLeader.Items.Add(new ListItem(arrs[1], arrs[0]));
                            }
                        }                        
                        string leaderId =item["Leader"].SafeToString().Substring(0, item["Leader"].SafeToString().IndexOf(";#"));
                        DDL_Leader.SelectedValue = leaderId;
                        #endregion
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddDepartmentUserControl.ascx");
            }
        }
        private void BindDropMembers()
        {
            try
            {                
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        this.DDL_Leader.Items.Clear();
                        this.DDL_SecLeader.Items.Clear();
                        SPGroup group = oWeb.Groups["学生组"];
                        SPUserCollection users = group.Users;
                        foreach (SPUser item in users)
                        {
                            if (item.Name != SPContext.Current.Web.CurrentUser.Name && item.Name != "每个人")
                            {
                                this.DDL_Leader.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                                this.DDL_SecLeader.Items.Add(new ListItem(item.Name, item.ID.ToString()));
                            }
                        }              
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddDepartment_BindDropMembers");
            }            
        }
        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {                        
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        string queryStr = CAML.Eq(CAML.FieldRef("Title"), CAML.Value(TB_Title.Text)); 
                        SPListItem item;
                        SPFieldUserValueCollection allleaderCols=new SPFieldUserValueCollection();
                        if (!string.IsNullOrEmpty(ViewState["itemid"].SafeToString())) //修改部门      
                        {
                            int intItemId = Convert.ToInt32(ViewState["itemid"]);
                            item = list.GetItemById(intItemId);
                            queryStr = CAML.And(CAML.Neq(CAML.FieldRef("ID"), CAML.Value(intItemId)), queryStr);                            
                        }
                        else  //创建部门      
                        {                           
                            item = list.AddItem();                    
                            item["ParentID"] = Request.QueryString["pId"];
                        }
                        string leader = Hid_Leader.Value.SafeToString();
                        if (leader != "")  //为部长赋值
                        {
                            SPFieldUserValueCollection leaderCol= GetSelectValue(oWeb, leader);
                            allleaderCols=leaderCol;
                            item["Leader"] = leaderCol;
                        }                     
                        string secLeader=Hid_SecLeader.Value.SafeToString();
                        if (secLeader != "")  //为副部长赋值
                        {
                            SPFieldUserValueCollection secLeaderCol = GetSelectValue(oWeb, secLeader);
                            allleaderCols.AddRange(secLeaderCol);
                            item["SecondLeader"] = secLeaderCol;
                        }                          
                        SPListItemCollection departlist = list.GetItems(new SPQuery() {
                            Query = CAML.Where(queryStr)
                        });
                        //判断"学生会组织机构"列表中是否存在名称相同的部门
                        if (departlist != null && departlist.Count > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('创建部门失败，已存在同名部门！');", true);
                            return;
                        }
                        item["Title"] = TB_Title.Text;                        
                        item["Introduce"] = TB_Content.Text;
                        //判断是否上传图片
                        if (this.fimg_Depart.PostedFile.FileName != null && this.fimg_Depart.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpimage = this.fimg_Depart.PostedFile;
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
                        
                        if(string.IsNullOrEmpty(ViewState["itemid"].SafeToString())){
                            AddDepartMembers(allleaderCols, item);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('提交失败，请重试...')";
                com.writeLogMessage(ex.Message, "SA_wp_AddDepartmentUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        private SPFieldUserValueCollection GetSelectValue(SPWeb oWeb,string hidValue)
        {
            SPFieldUserValueCollection spscoll = new SPFieldUserValueCollection();
            string[] uids = hidValue.Split(',');
            foreach (string uid in uids)
            {
                SPUser user = oWeb.AllUsers.GetByID(Convert.ToInt32(uid));
                SPFieldUserValue u = new SPFieldUserValue(oWeb, user.ID, user.Name);
                spscoll.Add(u);
            }
            return spscoll;
        }

        private void AddDepartMembers(SPFieldUserValueCollection userCols,SPListItem departItem)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList memList = oWeb.Lists.TryGetList("部门成员");
                        foreach (SPFieldUserValue spvUser in userCols)
                        {
                            SPListItemCollection items = memList.GetItems(new SPQuery() {
                                Query = CAML.Where(CAML.And(
                                                  CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", spvUser.User.Name)),
                                                  CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(departItem.ID))))
                            });
                            if (items.Count == 0)
                            {
                               SPListItem memitem=memList.AddItem();
                               memitem["Title"] = departItem.Title;
                               memitem["DepartmentID"] = departItem.ID;
                               memitem["Member"] = spvUser;
                               memitem.Update();
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddDepartment_AddDepartMembers");
            }
        }
    }
}
