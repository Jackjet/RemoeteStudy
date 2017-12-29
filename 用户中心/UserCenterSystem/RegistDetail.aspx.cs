using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class RegistDetail : BaseInfo
    {
        public bool IsXJadmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Base_TeacherBLL deptBll = new Base_TeacherBLL();
            DataTable dt = new DataTable();
            //创建列
            DataColumn dtc = new DataColumn();
            //dtc = new DataColumn("学校总数");
            //dt.Columns.Add(dtc);
            //dtc = new DataColumn("已参与注册的学校数量");
            //dt.Columns.Add(dtc);
            //dtc = new DataColumn("教师总数");
            //dt.Columns.Add(dtc);
            dtc = new DataColumn("总注册人数");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("昨日注册人数");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("今日注册人数");
            dt.Columns.Add(dtc);
            DataSet set = deptBll.getRegcount();
            DataRow dr = dt.NewRow();
            //dr["学校总数"] = set.Tables[0].Rows[0][0];
            //dr["已参与注册的学校数量"] = set.Tables[1].Rows[0][0];
            dr["总注册人数"] = set.Tables[2].Rows[0][0];
            dr["今日注册人数"] = set.Tables[3].Rows[0][0];
            dr["昨日注册人数"] = set.Tables[4].Rows[0][0];
            //dr["教师总数"] = set.Tables[5].Rows[0][0];
            dt.Rows.Add(dr);
            gvlist2.DataSource = dt;
            gvlist2.DataBind();

            //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //string AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员
            //Base_TeacherBLL deptBll = new Base_TeacherBLL();
            //if (teacher != null)
            //{
            //    if (teacher.YHZH.Trim() == AdminName)//超管登陆
            //    {
            //        #region 表1
            //        DataTable deptList = deptBll.getReg();
            //        gvlist.DataSource = deptList;
            //        gvlist.DataBind();
            //        #endregion

            //        #region 表2
            //        DataTable dt = new DataTable();
            //        //创建列
            //        DataColumn dtc = new DataColumn();
            //        dtc = new DataColumn("学校总数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("已参与注册的学校数量");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("教师总数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("总注册人数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("昨日注册人数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("今日注册人数");
            //        dt.Columns.Add(dtc);
            //        DataSet set = deptBll.getRegcount();
            //        DataRow dr = dt.NewRow();
            //        dr["学校总数"] = set.Tables[0].Rows[0][0];
            //        dr["已参与注册的学校数量"] = set.Tables[1].Rows[0][0];
            //        dr["总注册人数"] = set.Tables[2].Rows[0][0];
            //        dr["今日注册人数"] = set.Tables[3].Rows[0][0];
            //        dr["昨日注册人数"] = set.Tables[4].Rows[0][0];
            //        dr["教师总数"] = set.Tables[5].Rows[0][0];
            //        dt.Rows.Add(dr);
            //        gvlist2.DataSource = dt;
            //        gvlist2.DataBind();
            //        #endregion
            //    }
            //    else //校管登陆
            //    {
            //        IsXJadmin = true;
            //        DataSet deptList = deptBll.XJgetRegcount(teacher.XXZZJGH.ToString());

            //        DataTable dt = new DataTable();
            //        DataColumn dtc = new DataColumn();
            //        dtc = new DataColumn("本校教师总数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("总注册人数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("昨日注册人数");
            //        dt.Columns.Add(dtc);
            //        dtc = new DataColumn("今日注册人数");
            //        dt.Columns.Add(dtc);
            //        DataRow dr = dt.NewRow();
            //        dr["本校教师总数"] = deptList.Tables[0].Rows[0][0];
            //        dr["总注册人数"] = deptList.Tables[1].Rows[0][0];
            //        dr["昨日注册人数"] = deptList.Tables[2].Rows[0][0];
            //        dr["今日注册人数"] = deptList.Tables[3].Rows[0][0];
            //        dt.Rows.Add(dr);
            //        gvXJlist.DataSource = dt;
            //        gvXJlist.DataBind();
            //    }
            //}
        }
    }
}