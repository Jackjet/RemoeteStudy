using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_MoralEduInfo
{
    public partial class ME_wp_MoralEduInfoUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("考评模板", false); } }
        private SPList TempBaseList { get { return ListHelp.GetCureenWebList("考评模板标准", false); } }
        private SPList ClaScoreList { get { return ListHelp.GetCureenWebList("班级德育分数", false); } }
        private SPList StuScoreList { get { return ListHelp.GetCureenWebList("学生德育分数", false); } }
        private SPList ClassList { get { return ListHelp.GetCureenWebList("年级信息", true); } }
        private SPList StuList { get { return ListHelp.GetCureenWebList("学生信息", true); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
            }
        }
        private void BindType()
        {
            try
            {
                SPField fieldPrizeGrade = CurrentList.Fields.GetField("Type");
                SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                foreach (string type in choicePrizeGrade.Choices)
                {
                    this.DDL_Type.Items.Add(new ListItem(type, type));
                }
                this.DDL_Type_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MoralEduInfoUserControl_BindType");
            }
        }
        protected void DDL_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DDL_Temp.Items.Clear();
                SPQuery query = new SPQuery
                {
                    Query = @"<Where>
                                            <And>
                                             <Neq><FieldRef Name='Status' /><Value Type='Choice'>禁用</Value></Neq>
                                             <Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + @"</Value></Eq>
                                            </And>
                                        </Where><OrderBy><FieldRef Name='Modified' Ascending='False'/></OrderBy>"
                };
                SPListItemCollection items = CurrentList.GetItems(query);
                if (items.Count > 0)
                {
                    foreach (SPListItem item in items)
                    {
                        this.DDL_Temp.Items.Add(new ListItem(item.Title, item.ID.ToString()));
                    }
                }
                else
                {
                    this.DDL_Temp.Items.Add(new ListItem("暂无考评模板", ""));
                }
                this.DDL_Temp_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MoralEduInfoUserControl_DDL_Type_SelectedIndexChanged");
            }
        }
        protected void DDL_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region DataGrid
                DataTable dt = new DataTable();
                if(this.DDL_Temp.SelectedValue!="")
                {
                    SPListItem item = CurrentList.GetItemById(Convert.ToInt32(this.DDL_Temp.SelectedValue));
                    string tempBaseIds = item["ScoreItem"].SafeToString();
                    if (!string.IsNullOrEmpty(tempBaseIds))
                    {
                        dt.Columns.Add(this.DDL_Type.SelectedValue);//第一列显示适用对象
                        string[] ids = tempBaseIds.Split(',');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            string temp = TempBaseList.GetItemById(Convert.ToInt32(ids[i])).Title;
                            dt.Columns.Add(temp);
                        }
                        List<Target> tarlst = new List<Target>();
                        if (this.DDL_Type.SelectedValue == "班级")
                        {
                            SPListItemCollection claitems = ClassList.GetItems(new SPQuery()
                            {
                                Query = @"<Where><Neq><FieldRef Name='ParentID' /><Value Type='Number'>0</Value></Neq></Where>"
                            });
                            foreach (SPListItem claitem in claitems)
                            {
                                Target col = new Target() { ID = claitem.ID.ToString(), Name = claitem.Title };
                                tarlst.Add(col);
                            }
                        }
                        else //学生
                        {
                            SPListItemCollection stuitems = StuList.GetItems();
                            foreach (SPListItem stuitem in stuitems)
                            {
                                Target col = new Target() { ID = stuitem.ID.ToString(), Name = stuitem["Name"].ToString() };
                                tarlst.Add(col);
                            }
                        }
                        string[] values = new string[ids.Length + 1];
                        foreach (Target tg in tarlst)
                        {
                            values[0] = tg.Name;
                            for (int i = 0; i < ids.Length; i++)
                            {
                                //查询记录的分数，
                                string scid = string.Empty;
                                string value = string.Empty;
                                SPListItem scoreItem = this.GetEduScore(tg.ID, tg.Name, ids[i]);
                                if (scoreItem != null)
                                {
                                    scid = scoreItem.ID.ToString();
                                    value = scoreItem["Score"].SafeToString();
                                }
                                values[i + 1] = "<input type=\"text\" id=\"" + scid + "#" + tg.ID + "#" + ids[i] + "\" value=\"" + value + "\" />";
                            }
                            dt.Rows.Add(values);
                        }
                    }
                }
                this.DG_MoralEdu.DataSource = dt;
                this.DG_MoralEdu.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MoralEduInfoUserControl_DDL_Temp_SelectedIndexChanged");
            }
        } 
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.itemIds.Value))
            {
                string[] idValue = this.itemIds.Value.Split('#');
                string scid = idValue[0];
                if (this.DDL_Type.SelectedValue == "班级")
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            SPList list = oWeb.Lists.TryGetList("班级德育分数");
                            SPListItem item = null;
                            if (string.IsNullOrEmpty(scid))
                            {
                                item = list.AddItem();
                                item["Title"] = idValue[1];
                                item["TempID"] = this.DDL_Temp.SelectedValue;
                                item["Item"] = idValue[2];
                            }
                            else
                            {
                                item = list.GetItemById(Convert.ToInt32(scid));
                            }
                            item["Score"] = idValue[3];
                            item.Update();
                        }
                    }, true);
                }
                else
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            SPList list = oWeb.Lists.TryGetList("学生德育分数");
                            SPListItem item = null;
                            if (string.IsNullOrEmpty(scid))
                            {
                                item = list.AddItem();
                                item["Title"] = idValue[2];
                                item["TempID"] = this.DDL_Temp.SelectedValue;
                                item["StuID"] = idValue[1];
                            }
                            else
                            {
                                item = list.GetItemById(Convert.ToInt32(scid));
                            }
                            item["Score"] = idValue[3];
                            item.Update();
                        }
                    }, true);
                }
                this.DDL_Temp_SelectedIndexChanged(null, null);
            }
        }
        //取班级德育分数
        private SPListItem GetEduScore(string tgid,string name, string itemid)
        {
            try
            {
                SPListItemCollection itemColl=null;
                string where = string.Empty;
                if (this.DDL_Type.SelectedValue == "班级")
                {
                    itemColl = ClaScoreList.GetItems(new SPQuery()
                    {
                        Query = @"<Where>
                                      <And>
                                         <Eq><FieldRef Name='Title' /><Value Type='Text'>" + tgid + @"</Value></Eq>
                                         <Eq><FieldRef Name='Item' /><Value Type='Number'>" + itemid + @"</Value></Eq>
                                      </And>
                                   </Where>"
                    });
                }
                else //学生
                {
                    itemColl = StuScoreList.GetItems(new SPQuery()
                    {
                        Query = @"<Where>
                                      <And>
                                         <Eq><FieldRef Name='Title' /><Value Type='Text'>" + itemid + @"</Value></Eq>
                                         <Eq><FieldRef Name='StuID' /><Value Type='Number'>" + tgid + @"</Value></Eq>
                                      </And>
                                   </Where>"
                    });
                }
                if (itemColl != null && itemColl.Count > 0)
                {
                    return itemColl[0];
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MoralEduInfoUserControl_GetEduScore");
            }
            return null;
        }

        private class Target
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }
    }
}
