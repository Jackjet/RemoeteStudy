using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserCenterSystem.UserInfo;

namespace UserCenterSystem
{
    public partial class TestWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btGetConfig_Click(object sender, EventArgs e)
        {
            GetConfig(tbLogoinName.Text.Trim());
        }

        protected void btSQL_Click(object sender, EventArgs e)
        {
            TestFun();
        }

        /// <summary>
        /// 测试WebServices，读取用户信息（包含教师、家长、学生）
        /// </summary>
        private void TestFun()
        {
            //测试账号：系统管理员，yqadmin；配置账户,systemA
            //型号组织机构号：8
            //测试列：
            /*教师：ZZMMM,SFZJH,YHZH,XM,
              学生：SFZJH,XM,
              家长：CYSFZJH,YHZH,SFZJH,CYXM*/

            UserInfoSoapClient userInfoClient = new UserInfoSoapClient();
            string strColumns = tbColumns.Text.Trim().ToUpper();
            string[] arrayColumns = { };
            if (!string.IsNullOrEmpty(strColumns))
            {
                arrayColumns = strColumns.Split(',');
            }
            string strResult = "测试";
            string strValidats = string.Empty;
            string strLoginName = tbLogoinName.Text.Trim();//"systemA"; //"yqadmin";//
            string SchoolCode = tbXXZZJGH.Text.Trim();//"8";
            string strTableName = tbTableName.Text.Trim();
            DataTable dt = userInfoClient.GetUserInfo(strLoginName, arrayColumns, SchoolCode, strTableName, out strResult);
            gv.DataSource = dt;
            gv.DataBind();
            if (dt == null || dt.Rows.Count == 0)
            {
                lbOut.Text = "“" + strResult + "”";
            }
            else
            {
                lbOut.Text = "";
            }
        }

        private void GetConfig(string strLoginName)
        {
            UserInfoSoapClient userInfoClient = new UserInfoSoapClient();
            DataTable dt = userInfoClient.GetInferConfig(strLoginName);
            gvConfig.DataSource = dt;
            gvConfig.DataBind();

            if (dt == null || dt.Rows.Count == 0)
            {
                lbMessage.Text = "“无此用户配置信息”";
            }
            else
            {
                lbMessage.Text = "";
            }
        }
    }
}