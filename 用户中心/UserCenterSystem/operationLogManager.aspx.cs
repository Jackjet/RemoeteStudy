using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class operationLogManager : BaseInfo
    {
        string strLoginName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                if (teacher != null)
                {
                    //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
                    strLoginName = teacher.XXZZJGH;

                }
                BindListView();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindListView()
        {
            try
            {
                //DateTime starDate = string.IsNullOrWhiteSpace(starDateHidden.Value) ? DateTime.MaxValue : DateTime.Parse(starDateHidden.Value);
                //DateTime endDate = string.IsNullOrWhiteSpace(endDateHidden.Value) ? DateTime.MaxValue : DateTime.Parse(endDateHidden.Value).AddDays(1);
                //DataTable dtuser = Base_LogBLL.Query(PersonName.Text.Trim(), Modelnode.SelectedItem.Value, starDate, endDate, strLoginName == null ? "" : strLoginName);
                //将上面三行代码封装成GetLogInfo()方法 以便导出功能使用

                this.lvLog.DataSource = GetLogInfo();
                this.lvLog.DataBind();
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }

        }
        /// <summary>
        /// 根据查询条件查询日志信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetLogInfo()
        {
            DateTime starDate = string.IsNullOrWhiteSpace(starDateHidden.Value) ? DateTime.MaxValue : DateTime.Parse(starDateHidden.Value);
            DateTime endDate = string.IsNullOrWhiteSpace(endDateHidden.Value) ? DateTime.MaxValue : DateTime.Parse(endDateHidden.Value).AddDays(1);
            DataTable dtuser = Base_LogBLL.Query(PersonName.Text.Trim(), Modelnode.SelectedItem.Value, starDate, endDate, strLoginName == null ? "" : strLoginName);
            return dtuser;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDPLog_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this.DPLog.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_Search_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.czrzgl, ActionConstants.Search);

            BindListView();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.czrzgl, ActionConstants.exceldc);

                //查询出数据
                //DataTable oTable = Base_LogBLL.ReadData();
                //调用根据查询条件获得数据的方法
                DataTable oTable = GetLogInfo();
                DataTable dt = new DataTable();

                //创建列
                DataColumn dtc = new DataColumn();
                dtc = new DataColumn("人员名称");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("模块名称");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("操作信息");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("操作时间");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("IP地址");
                dt.Columns.Add(dtc);
                foreach (DataRow item in oTable.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["人员名称"] = item["RYXM"];
                    dr["模块名称"] = item["MKMC"];
                    dr["操作信息"] = item["CZXX"];
                    dr["操作时间"] = item["CZSJ"];
                    dr["IP地址"] = item["IP"];
                    dt.Rows.Add(dr);
                }
                //导出
                ExcelCommon.ExportExcelByFileName(dt, "操作日志");
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }

        }
    }
}