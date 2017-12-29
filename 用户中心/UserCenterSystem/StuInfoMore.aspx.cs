using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using System.Data;

namespace UserCenterSystem
{
    public partial class StuInfoMore : BaseInfo
    {
        Base_StudentBLL stuBll = new Base_StudentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strSfzjh = Request.QueryString["uid"];
                if (!string.IsNullOrEmpty(strSfzjh))
                {
                    DataTable dtSingleStu = stuBll.GetSingleStuInfoById(strSfzjh);
                    if (dtSingleStu.Rows.Count > 0)
                    {

                        //个人信息
                        lblyhzh.Text = dtSingleStu.Rows[0]["yhzh"].ToString();
                        lblsfzjh.Text = dtSingleStu.Rows[0]["sfzjh"].ToString();
                        lblxm.Text = dtSingleStu.Rows[0]["xm"].ToString();
                        lblxbm.Text = dtSingleStu.Rows[0]["xbm"].ToString();
                        lblmzm.Text = dtSingleStu.Rows[0]["mzm"].ToString();
                        lblzzmm.Text = dtSingleStu.Rows[0]["zzmmm"].ToString();
                        lbllxfs.Text = dtSingleStu.Rows[0]["lxdh"].ToString();
                        lbltxdz.Text = dtSingleStu.Rows[0]["txdz"].ToString();
                        lblcsrq.Text = DAL.ConvertHelper.DateTime(dtSingleStu.Rows[0]["csrq"]).DateTimeResult.ToShortDateString();
                        lblxzz.Text = dtSingleStu.Rows[0]["xzz"].ToString();
                        lblshsldrk.Text = dtSingleStu.Rows[0]["sfldrk"].ToString();
                        lblhkxz.Text = dtSingleStu.Rows[0]["hkxzm"].ToString();
                        lbljkzk.Text = dtSingleStu.Rows[0]["jkzkm"].ToString();
                        lbljwbs.Text = dtSingleStu.Rows[0]["jwbs"].ToString();
                        lblgms.Text = dtSingleStu.Rows[0]["gms"].ToString();
                        lblsfsdznv.Text = dtSingleStu.Rows[0]["sfdszn"].ToString();
                        //电子邮箱
                        lbldzyx.Text = dtSingleStu.Rows[0]["dzxx"].ToString();
                        lblxjh.Text = dtSingleStu.Rows[0]["xjh"].ToString();
                        lblywxm.Text = dtSingleStu.Rows[0]["ywxm"].ToString();
                        lblxmpy.Text = dtSingleStu.Rows[0]["xmpy"].ToString();

                        // 学校信息
                        lblnj.Text = dtSingleStu.Rows[0]["nj"].ToString();
                        lblbh.Text = dtSingleStu.Rows[0]["bh"].ToString();
                        lblxslbm.Text = dtSingleStu.Rows[0]["xslbm"].ToString();
                        lblsfstcs.Text = dtSingleStu.Rows[0]["sfstcs"].ToString();
                        lblxszt.Text = dtSingleStu.Rows[0]["xszt"].ToString();
                        lblrxny.Text =  DAL.ConvertHelper.DateTime(dtSingleStu.Rows[0]["rxny"]).DateTimeResult.ToShortDateString();
                        lblsxzkzh.Text = dtSingleStu.Rows[0]["sxzkzh"].ToString();
                        lblyxxmc.Text = dtSingleStu.Rows[0]["yxxmc"].ToString();

                        //xinzeng 0530
                        this.lblrxfsm.Text = dtSingleStu.Rows[0]["rxfsm"].ToString();//入学方式
                        this.lblxsly.Text =dtSingleStu.Rows[0]["jdfsm"].ToString();  //就读方式
                        this.lbllydq.Text =dtSingleStu.Rows[0]["lydqm"].ToString();
                        this.lbllcdq.Text = dtSingleStu.Rows[0]["lydq"].ToString();


                        //家庭信息
                        lblfqxm.Text = dtSingleStu.Rows[0]["fqxm"].ToString();
                        lblfqdh.Text = dtSingleStu.Rows[0]["fqdh"].ToString();
                        lblfqdw.Text = dtSingleStu.Rows[0]["fqdw"].ToString();
                        lblmqxm.Text = dtSingleStu.Rows[0]["mqxm"].ToString();
                        lblmqdh.Text = dtSingleStu.Rows[0]["mqdh"].ToString();
                        lblmqdw.Text = dtSingleStu.Rows[0]["mqdw"].ToString();
                      


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