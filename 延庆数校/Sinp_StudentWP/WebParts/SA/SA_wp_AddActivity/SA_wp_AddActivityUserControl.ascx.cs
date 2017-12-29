using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_AddActivity
{
    public partial class SA_wp_AddActivityUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        public string Department_ID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Department_ID = Request.QueryString["departid"];
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindActivityDDL();
                BindDepartmentUser(this.Department_ID);
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindActivityData(itemId);
                }                
            }
        }
        private void BindActivityData(string actid)
        {
            try
            {
                int itemId = Convert.ToInt32(actid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.TB_Title.Text = item.Title.SafeToString(); ;
                        this.DDL_Range.SelectedValue = item["Range"].SafeToString();
                        this.DDL_ActivityType.SelectedValue = item["Type"].SafeToString();
                        this.TB_ActBeginDate.Text = item["ActBeginDate"].SafeToDataTime();
                        this.TB_ActEndDate.Text = item["ActEndDate"].SafeToDataTime();
                        this.TB_MaxCount.Text = item["MaxCount"].SafeToString();
                        this.TB_Introduction.Text = item["Introduction"].SafeToString();
                        this.TB_Condition.Text = item["Condition"].SafeToString();
                        this.TB_BeginDate.Text = item["BeginDate"].SafeToDataTime();
                        this.TB_EndDate.Text = item["EndDate"].SafeToDataTime();
                        this.TB_Address.Text = item["Address"].SafeToString();                        
                        this.DDL_OrganizeUser.SelectedValue =item["OrganizeUser"].SafeLookUpToString();

                        SPList prolist = oWeb.Lists.TryGetList("活动项目");
                        SPListItemCollection proitems = prolist.GetItems(new SPQuery(){
                            Query=CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(actid)))
                        });                        
                        List<string> proArrs=new List<string>();
                        foreach (SPListItem proitem in proitems)
                        {
                            proArrs.Add(proitem.Title);
                        }
                        this.TB_Project.Text = string.Join(";", proArrs.ToArray()); 
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddActivity_BindActivityData");
            }
        }
        private void BindActivityDDL()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPField fieldRange = list.Fields.GetField("活动范围");
                        SPFieldChoice choiceRange = list.Fields.GetField(fieldRange.InternalName) as SPFieldChoice;
                        SPField fieldType = list.Fields.GetField("活动类型");
                        SPFieldChoice choiceType = list.Fields.GetField(fieldType.InternalName) as SPFieldChoice;
                        foreach (string type in choiceRange.Choices)
                        {
                            DDL_Range.Items.Add(new ListItem(type, type));
                        }
                        this.Btn_ActivitySave.Text = this.DDL_Range.SelectedValue == "全校" ? "送审" : "发布";
                        foreach (string type in choiceType.Choices)
                        {
                            DDL_ActivityType.Items.Add(new ListItem(type, type));
                        }
                        SetVisibleByType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddActivity_BindActivityDDL");
            }
        }
        protected void DDL_ActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisibleByType();
        }
        private void SetVisibleByType()
        {
            if (DDL_ActivityType.SelectedValue == "招新")
            {
                this.TR_MaxCount.Visible = false;
                this.TR_Project.Visible = false;              
            }
            else
            {
                this.TR_MaxCount.Visible = true;
                this.TR_Project.Visible = true;               
            }
        }
        private void BindDepartmentUser(string departid)
        {
            this.DDL_OrganizeUser.Items.Clear();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("部门成员");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(departid)))
                                    + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            string[] arrs = item["Member"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                            DDL_OrganizeUser.Items.Add(new ListItem(arrs[1], arrs[0]));
                        }                       
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddActivity_BindDepartmentUser");
            }
        }
        protected void DDL_Range_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Btn_ActivitySave.Text = this.DDL_Range.SelectedValue == "全校" ? "送审" : "发布";            
        }
        protected void Btn_ActivitySave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string actType = DDL_ActivityType.SelectedItem.Value;
                        if (actType != "招新"&&string.IsNullOrEmpty(Hid_Pros.Value))
                        {
                            script = "alert('请添加项目名称!');";
                            return;
                        }
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        string queryStr = CAML.Eq(CAML.FieldRef("Title"), CAML.Value(TB_Title.Text));                        
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
                            item["LearnYear"] = GetLearnYear().Trim();
                            item["DepartmentID"] = Department_ID;
                        }
                        SPListItemCollection actlist = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(queryStr)
                        });
                        //判断"活动信息"列表中是否存在名称相同的活动
                        if (actlist != null && actlist.Count > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败，已存在同名活动！');", true);
                            return;
                        }                       
                        item["Title"] = TB_Title.Text;
                        item["Range"]=DDL_Range.SelectedItem.Value;
                        item["Type"] = actType;
                        item["ActBeginDate"]=TB_ActBeginDate.Text;
                        item["ActEndDate"] = TB_ActEndDate.Text;
                        if (actType != "招新") { item["MaxCount"] = TB_MaxCount.Text; }                        
                        item["Introduction"]=TB_Introduction.Text;
                        item["Condition"]=TB_Condition.Text;
                        item["BeginDate"]= TB_BeginDate.Text;
                        item["EndDate"]=TB_EndDate.Text; 
                        item["Address"]=TB_Address.Text;
                        string orgUser =this.Hid_OrganizeUser.Value.SafeToString();
                        if (orgUser != "")  //为发起人赋值
                        {
                            SPFieldUserValueCollection orgUserCol = GetSelectValue(oWeb, orgUser);
                            item["OrganizeUser"] = orgUserCol;
                        }                       
                        //判断是否上传图片
                        if (this.fimg_Active.PostedFile.FileName != null && this.fimg_Active.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpimage = this.fimg_Active.PostedFile;
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
                        if (Request.Files.Count > 0)
                        {
                            UploadHelp.UpLoadAttachs("活动资料", Department_ID, TB_Title.Text, false, true, "ActivityID", item.ID.ToString());
                        }
                        if (actType != "招新")
                        {
                            SPList prolist = oWeb.Lists.TryGetList("活动项目");
                            string[] projects = Hid_Pros.Value.SafeToString().Split('㊣');
                            foreach (string pro in projects)
                            {
                                SPListItem proitem = prolist.AddItem();
                                proitem["ActivityID"] = item.ID;
                                proitem["Title"] = pro;
                                proitem.Update();
                            }
                        }                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('提交失败，请重试...')";
                com.writeLogMessage(ex.Message, "SA_wp_AddActivity_Btn_ActivitySave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);        
        }
        private SPFieldUserValueCollection GetSelectValue(SPWeb oWeb, string hidValue)
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
        private string GetLearnYear()
        {
            string result = "2015年第一学期";
            try
            {

                foreach (DataTable itemdt in up.GetStudysection().Tables)
                {
                    foreach (DataRow itemdr in itemdt.Rows)
                    {
                        if (DateTime.Now >= Convert.ToDateTime(itemdr["SStartDate"]) && DateTime.Now <= Convert.ToDateTime(itemdr["SEndDate"]))
                        {
                            result = itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"];
                            break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AllActivity_GetLearnYear");
            }
            return result;
        }
             
    }
}
