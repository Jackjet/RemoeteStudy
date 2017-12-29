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
    public partial class SpecialtyManage : BaseInfo //System.Web.UI.Page//
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ShowModule("列表");
                    //加载专业信息
                    BindInfo();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 加载专业信息列表
        /// </summary>
        private void BindInfo()
        {
            try
            {
                //查询并绑定所有专业信息
                Base_SpecialtyBLL SpecialtyBLL = new Base_SpecialtyBLL();
                lvDisp.DataSource = SpecialtyBLL.Select("");
                lvDisp.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 添加按钮
        /// 本方法代码只做准备
        /// </summary>
        protected void btAdd_Click(object sender, EventArgs e)
        {
            //将操作状态设置为添加
            hidOperation.Value = "添加";
            //现实添加模块
            ShowModule("更改");
            //重置文本项
            ResetText();
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ZYMC = tbZYMC.Text.Trim();//专业名称
                if (string.IsNullOrEmpty(ZYMC))
                {
                    lbMessage.Text = "专业名称不能为空";
                    return;
                }
                Base_SpecialtyBLL SpecialtyBLL = new Base_SpecialtyBLL();
                Base_Specialty model = new Base_Specialty();

                #region 添加
                if (hidOperation.Value=="添加")
                {
                    //判断名称是否已存在
                    if (SpecialtyBLL.ExistName(ZYMC))
                    {
                        lbMessage.Text = "专业名称已存在";
                        return;
                    }
                    //model赋值
                    model.ZYMC = ZYMC;//专业名称
                    model.TJSJ = System.DateTime.Now;//添加时间
                    model.BZ = tbBZ.Text.Trim();//备注
                    
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.zygl, ActionConstants.add);
                    bool insert = SpecialtyBLL.Insert(model);
                    if (insert)
                    {
                        alert("添加成功！");
                        ShowModule("列表");
                        BindInfo();
                        
                    }
                    else 
                    {
                        alert("添加失败！");
                    }
                    return;
                }
                #endregion

                #region 修改
                if (hidOperation.Value == "修改")
                {
                                    
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.zygl, ActionConstants.xg);
                    //获取ID
                    model = SpecialtyBLL.GetModel(Convert.ToInt32(hfSaveID.Value));
                    //判断名称是否改动，是则查询新名字是存在
                    if (model.ZYMC != ZYMC)
                    {
                        if (SpecialtyBLL.ExistName(ZYMC))
                        {
                            lbMessage.Text = "专业名称已存在";
                            return;
                        }
                    }
                    model.ZYMC = ZYMC;
                    model.XGSJ = System.DateTime.Now;//更新修改时间
                    model.BZ = tbBZ.Text.Trim();//备注
                    bool update = SpecialtyBLL.Update(model);
                    if (update)
                    {
                        alert("修改成功!");
                        ShowModule("列表");
                        BindInfo();
                    }
                    else
                    {
                        alert("修改失败!");
                    }
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                alert("操作失败!");
                return;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            //显示列表模块
            ShowModule("列表");
            //重置文本项
            ResetText();
        }

        /// <summary>
        /// 展示模块
        /// </summary>
        private void ShowModule(string ModuleName)
        {
            switch (ModuleName)
            {
                case "列表":
                    panelDisp.Visible = true;
                    panelAdd.Visible = false;
                    btAdd.Visible = true;
                    break;
                case "更改":
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    btAdd.Visible = false;
                    break;
                default:
                    panelDisp.Visible = true;
                    panelAdd.Visible = false;
                    btAdd.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// 重置文本项
        /// </summary>
        private void ResetText()
        {
            lbMessage.Text = "";//重置消息提示
            tbZYMC.Text = "";//重置专业名称
            tbBZ.Text = "";//重置备注
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                #region 修改准备
                try
                {
                    //加载被选中的专业信息
                    HiddenField hfZYMC = e.Item.FindControl("hfZYMC") as HiddenField;
                    tbZYMC.Text = hfZYMC.Value;
                    HiddenField hfBZ = e.Item.FindControl("hfBZ") as HiddenField;
                    tbBZ.Text = hfBZ.Value;
                    hfSaveID.Value = (e.Item.FindControl("hfID") as HiddenField).Value;

                    //重置提示信息
                    lbMessage.Text = "";
                    //显示修改模块
                    ShowModule("更改");
                    hidOperation.Value = "修改";//将操作状态设置为修改

                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
                #endregion
            }
            else if (e.CommandName == "del")
            {
                #region 删除
                try
                {
                    //获取ID
                    HiddenField hfID = e.Item.FindControl("hfID") as HiddenField;
                    Base_SpecialtyBLL BLL = new Base_SpecialtyBLL();
                    bool delete = BLL.Delete(Convert.ToInt32(hfID.Value));
                    if (delete)
                    {
                        alert("删除成功！");
                        //加载数据
                        BindInfo();
                        return;
                    }
                    else
                    {
                        alert("删除失败！");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                    alert("删除失败！");
                    return;
                }

                #endregion
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
                BindInfo();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 输出JavaScript-alert()消息
        /// </summary>
        /// <param name="strMessage"></param>
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }

        protected void lvDisp_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

    }

}