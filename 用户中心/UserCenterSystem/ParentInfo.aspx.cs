using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using Model;
using System.Data;

namespace UserCenterSystem
{
    public partial class ParentInfo : System.Web.UI.Page
    {
        Base_ParentBLL parBLL = new Base_ParentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string strSfzjh = Request.QueryString["cysfzjh"];
                    if (!string.IsNullOrEmpty(strSfzjh))
                    {
                        DataTable dtSinglePar = parBLL.GetSingleParent(strSfzjh);
                        if (dtSinglePar.Rows.Count > 0)
                        {

                            this.lblcyxm.Text = dtSinglePar.Rows[0]["cyxm"].ToString();
                            this.lblgxm.Text = dtSinglePar.Rows[0]["gxm"].ToString();
                            this.lblsfsjhr.Text = dtSinglePar.Rows[0]["sfjhr"].ToString();
                            lblxbm.Text = dtSinglePar.Rows[0]["xbm"].ToString();
                            lblmzm.Text = dtSinglePar.Rows[0]["mzm"].ToString();

                            lblsjhm.Text = dtSinglePar.Rows[0]["sjhm"].ToString();
                            lbllxdh.Text = dtSinglePar.Rows[0]["dh"].ToString();
                            lbllxdz.Text = dtSinglePar.Rows[0]["lxdz"].ToString();
                            lblcsny.Text = DAL.ConvertHelper.DateTime(dtSinglePar.Rows[0]["csny"]).DateTimeResult.ToShortDateString();
                            lbljkzkm.Text = dtSinglePar.Rows[0]["jkzkm"].ToString();

                            lblgzdw.Text = dtSinglePar.Rows[0]["cygzdw"].ToString();
                            lbldzxx.Text = dtSinglePar.Rows[0]["dzxx"].ToString();
                            lblxlm.Text = dtSinglePar.Rows[0]["xlm"].ToString();
                            lblbz.Text = dtSinglePar.Rows[0]["bz"].ToString();
                          
                        }
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