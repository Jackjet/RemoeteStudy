using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ADManager
{
    public partial class UserInfoText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Btn_Sbmit_Click(object sender, EventArgs e)
        {
            string strResult = "";
            TextTable text = new TextTable();

            List<string> ColumnTitle = new List<string>();
            text.Login = Txt_LoginName.Text;

            string[] array = Txt_Column.Text.Split(',');
            foreach (string item in array)
            {
                ColumnTitle.Add(item);
            }

            text.SchoolCode = Txt_SchoolCode.Text;
            text.strTableName = Txt_TableName.Text;
            text.strFunName = Txt_Function.Text;
            ADManager.UserInfo ui = new ADManager.UserInfo();

            DataTable dt = new DataTable();
          //  dt = ui.GetCommonInfo(text.Login, ColumnTitle, text.SchoolCode, text.strFunName, text.strTableName, out strResult);
          //  dt = ui.GetUserInfoByLoginName(text.Login, ColumnTitle, text.SchoolCode, text.strTableName, out strResult);
          //  dt = ui.GetInferConfig(text.Login);

            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }
    }
    /// <summary>
    /// Common Class
    /// </summary>
    public class TextTable
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public List<string> ColumnTitle { get; set; }

        /// <summary>
        /// 学校Code
        /// </summary>
        public string SchoolCode { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string strFunName { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string strTableName { get; set; }
    }
}