using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using BLL;
using Model;
using Common;
using System.Data.SqlClient;
using System.Data.Common;

namespace UserCenterSystem
{
    public partial class CultivateArchivesManage : BaseInfo //System.Web.UI.Page//
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //btAdd.Visible = false;
                    ShowModule("列表");
                    BindGrade();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }
        
        /// <summary>
        /// 绑定所有专业信息
        /// </summary>
        private void BindGrade()
        {
            try
            {


                //Base_GradeBLL gradeBll = new Base_GradeBLL();
                //lvDisp.DataSource = gradeBll.Select("*", "Base_Course", CXTJ());
                //lvDisp.DataBind();

                SystemStudentSQL bll = new SystemStudentSQL();
                DataTable dt = bll.Select("*", "CurriculumInfo", "");

                //绑定下拉列表
                sssss.DataValueField = "Id";
                sssss.DataTextField = "Title";
                sssss.DataSource = dt;
                sssss.DataBind();
                
                Base_GradeBLL bll2 = new Base_GradeBLL();
                DataTable dt2 = bll2.Select("*", "Base_Course", "");
                DataTable dtNew = CreateDataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    drNew["KCID"]=dr["Id"].ToString();
                    drNew["KCMC"] = dr["Title"].ToString();
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        if (drNew["KCID"].ToString() == dr2["KCID"].ToString())
                        {
                            drNew["id"] = dr2["id"].ToString();
                            drNew["SC"] = dr2["SC"].ToString();
                            drNew["GD"] = dr2["GD"].ToString();
                            drNew["KHJG"] = dr2["KHJG"].ToString();
                            continue;
                        }
                    }
                    dtNew.Rows.Add(drNew);
                }

                lvDisp.DataSource = dtNew;
                lvDisp.DataBind();

            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("KCID");
            dt.Columns.Add("KCMC");
            dt.Columns.Add("SC");
            dt.Columns.Add("GD");
            dt.Columns.Add("KHJG");

            return dt;
        }



        /// <summary>
        /// 【Button】【添加】
        /// </summary>
        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ShowModule("更改");
                Reset();
                hfType.Value = "添加";
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Button】【保存】
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(tbNJMC.Text.Trim()))
                //{
                //    lbMessage.Text = "课程名称不能为空";
                //    return;
                //}

                Base_GradeBLL bll = new Base_GradeBLL();

                //if (hfType.Value == "添加" || (hfType.Value == "修改" && hfMC.Value != tbNJMC.Text.Trim()))
                //{
                //    if (bll.IsExistsGradeName(tbNJMC.Text.Trim(), "0"))
                //    {
                //        lbMessage.Text = "名称已存在";
                //        return;
                //    }
                //}
                //Base_Grade model = new Base_Grade();
                //if (hfType.Value == "修改")
                //{
                //    model.NJ = Convert.ToInt32(hfID.Value);
                //}
                //model.NJMC = tbNJMC.Text.Trim();


                //model.XGSJ = System.DateTime.Now;
                //model.BZ = tbBZ.Text.Trim();

                string KCMC = sssss.SelectedItem.Text;
                string SC = TextBox1.Text;
                string GD = ddlgd.SelectedItem.Text;
                string KHJG = tbBZ.Text;
                
                bool istrue=false;
                if (hfType.Value == "添加")
                {
                    if (bll.Insert("Base_Course", "KCMC,SC,GD,KHJG", "'" + KCMC + "','" + SC + "','" + GD + "','" + KHJG + "'") > 0)
                    {
                        istrue = true;
                    }
                }
                else if (hfType.Value == "修改")
                {
                    if (bll.Update("Base_Course", "KCMC='" + KCMC + "',SC='" + SC + "',GD='" + GD + "',KHJG='" + KHJG + "'", "id=" + hfID.Value) > 0)
                    {
                        istrue = true;
                    }
                }
                
                if (istrue)
                {
                    //重置分页
                    DPTeacher.SetPageProperties(0, DPTeacher.PageSize, false);

                    BindGrade();
                    ShowModule("列表");
                    //记录操作日志
                    Base_LogBLL.WriteLog(LogConstants.zygl, hfType.Value + "成功");
                    alert("保存成功!");
                }
                else
                {
                    //记录操作日志
                    Base_LogBLL.WriteLog(LogConstants.zygl, hfType.Value + "失败");
                    alert("保存失败");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Button】【取消】
        /// </summary>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            ShowModule("列表");
            Reset();
        }

        /// <summary>
        /// 显示模块
        /// </summary>
        /// <param name="name"></param>
        private void ShowModule(string name)
        {
            switch (name)
            {
                case "列表":
                    AddPanel.Visible = true;
                    panelDisp.Visible = true;
                    panelAdd.Visible = false;
                    break;
                case "更改":
                    AddPanel.Visible = false;
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="name"></param>
        private void Reset()
        {
            TextBox1.Text = "";
            tbBZ.Text = "";
            lbMessage.Text = "";
        }

        /// <summary>
        /// 【Button】【修改】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                //获取ID
                string hfNJ = (e.Item.FindControl("hfNJ") as HiddenField).Value;

                //修改
                if (e.CommandName == "edit")
                {
                    //修改
                    ShowModule("更改");
                    Reset();
                    //赋值名称
                    sssss.SelectedItem.Text = (e.Item.FindControl("hfNJMC") as HiddenField).Value;
                    //hfMC.Value = sssss.SelectedValue;
                    //赋值备注
                    TextBox1.Text = (e.Item.FindControl("HiddenField3") as HiddenField).Value;
                    tbBZ.Text = (e.Item.FindControl("HiddenField2") as HiddenField).Value;
                    string gd=(e.Item.FindControl("HiddenField1") as HiddenField).Value;
                    if (string.IsNullOrEmpty(gd)||gd=="否")
                    {
                        gd = "0";
                    }
                    else
                    {
                        gd = "1";
                    }
                    ddlgd.SelectedValue = gd;
                    hfID.Value = hfNJ;
                    hfType.Value = "修改";
                    sssss.Enabled = false;
                }
                //删除
                else if (e.CommandName == "del")
                { 
                    //执行删除
                    Base_GradeBLL gradeBll = new Base_GradeBLL();
                    if (gradeBll.Delete("Base_Course", "id=" + hfNJ) <= 0)
                    {
                        //记录操作日志
                        Base_LogBLL.WriteLog(LogConstants.zygl, "删除失败");
                        alert("删除失败！");
                        return;
                    }
                    //重置分页
                    DPTeacher.SetPageProperties(0, DPTeacher.PageSize, false);
                    //重新加载
                    BindGrade();
                    alert("删除成功！");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDisp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                this.DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindGrade();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }

        protected void lvDisp_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGrade();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private string CXTJ()
        {
            string CXTJ = " 1=1 ";
            if (!string.IsNullOrEmpty(txtKCMC.Text.Trim()))
            {
                CXTJ += " and KCMC like '%" + txtKCMC.Text.Trim() + "%'";

            }
            if (ddlGD1.SelectedItem.Text.Trim()!="全部")
            {
                if (ddlGD1.SelectedItem.Text.Trim() == "否")
                {
                    CXTJ += " and (GD='" + ddlGD1.SelectedItem.Text.Trim() + "' or GD='' or GD is null)";
                }
                else
                {
                    CXTJ += " and GD='" + ddlGD1.SelectedItem.Text.Trim() + "'";

                }
            }
            return CXTJ;
        }
    }

}