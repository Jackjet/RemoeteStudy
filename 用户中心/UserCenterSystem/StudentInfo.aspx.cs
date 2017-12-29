using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using DAL;

namespace UserCenterSystem
{
    public partial class StudentInfo : System.Web.UI.Page
    {
        Base_StudentBLL stuBll = new Base_StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string strSfzjh = Request.QueryString["sfzjh"];
                    if (!string.IsNullOrEmpty(strSfzjh))
                    {
                        ADWS.ADWebService adw = new ADWS.ADWebService();
                        string Result = adw.ManagerResetPassWord(strSfzjh);

                        Lb_No.Text = strSfzjh;
                        Lb_Pw.Text = Result;


                        //DataTable dtSingleStu = stuBll.GetSingleStuInfoById(strSfzjh);
                        //if (dtSingleStu.Rows.Count > 0)
                        //{

                        //    lblYhzh.Text = dtSingleStu.Rows[0]["yhzh"].ToString();
                        //    lblsfzjh.Text = dtSingleStu.Rows[0]["sfzjh"].ToString();
                        //    lblxm.Text = dtSingleStu.Rows[0]["xm"].ToString();
                        //    lblxbm.Text = dtSingleStu.Rows[0]["xbm"].ToString();
                        //    lblmzm.Text = dtSingleStu.Rows[0]["mzm"].ToString();
                        //    lblzzmm.Text = dtSingleStu.Rows[0]["zzmmm"].ToString();
                        //    lbllxfs.Text = dtSingleStu.Rows[0]["lxdh"].ToString();
                        //    lbltxdz.Text = dtSingleStu.Rows[0]["txdz"].ToString();
                        //    lblcsrq.Text = DAL.ConvertHelper.DateTime(dtSingleStu.Rows[0]["csrq"]).DateTimeResult.ToShortDateString();
                        //    lblxzz.Text = dtSingleStu.Rows[0]["xzz"].ToString();
                        //    lblshsldrk.Text = dtSingleStu.Rows[0]["sfldrk"].ToString();
                        //    lblhkxz.Text = dtSingleStu.Rows[0]["hkxzm"].ToString();

                        //    HiddUserid.Value = dtSingleStu.Rows[0]["sfzjh"].ToString();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
            }


        }
    }
}