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

namespace UserCenterSystem
{
    public partial class GradeManage : BaseInfo //System.Web.UI.Page//
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
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
                Base_GradeBLL gradeBll = new Base_GradeBLL();
                lvDisp.DataSource = gradeBll.SelectAllGradeInfo();
                lvDisp.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
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
                if (string.IsNullOrEmpty(tbNJMC.Text.Trim()))
                {
                    lbMessage.Text = "专业名称不能为空";
                    return;
                }

                Base_GradeBLL bll = new Base_GradeBLL();

                if (hfType.Value == "添加" || (hfType.Value == "修改" && hfMC.Value != tbNJMC.Text.Trim()))
                {
                    if (bll.IsExistsGradeName(tbNJMC.Text.Trim(), "0"))
                    {
                        lbMessage.Text = "名称已存在";
                        return;
                    }
                }
                Base_Grade model = new Base_Grade();
                if (hfType.Value == "修改")
                {
                    model.NJ = Convert.ToInt32(hfID.Value);
                }
                model.NJMC = tbNJMC.Text.Trim();


                model.XGSJ = System.DateTime.Now;
                model.BZ = tbBZ.Text.Trim();
                
                bool istrue=false;
                if (hfType.Value == "添加")
                {
                    istrue = bll.InsertGrade(model);
                }
                else if (hfType.Value == "修改")
                {
                    istrue = bll.UpdateGrade(model);
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
            tbNJMC.Text = "";
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
                    tbNJMC.Text = (e.Item.FindControl("hfNJMC") as HiddenField).Value;
                    hfMC.Value = tbNJMC.Text;
                    //赋值备注
                    tbBZ.Text = (e.Item.FindControl("hfBZ") as HiddenField).Value;
                    hfID.Value = hfNJ;
                    hfType.Value = "修改";
                }
                //删除
                else if (e.CommandName == "del")
                { 
                    //执行删除
                    Base_GradeBLL gradeBll = new Base_GradeBLL();
                    if (!gradeBll.DeleteGrade(hfNJ))
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
    }

}