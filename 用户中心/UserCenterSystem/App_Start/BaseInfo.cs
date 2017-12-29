using System;
using Common;
using Model;
using System.Text;

namespace UserCenterSystem
{
    public class BaseInfo : System.Web.UI.Page
    {
        public BaseInfo()
        {
            //this.Init += new EventHandler(BaseInfo_Load);
        }
        private void BaseInfo_Load(object sender, EventArgs e)
        {
            //判断是否登录
            if (!IsLogin())
            {
                string js = "<script type=\"text/javascript\">window.parent.location.href='/Login.aspx';</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
        }
        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            try
            {
                if (Session[UCSKey.SESSION_LoginInfo] == null)
                    return false;
                else
                {
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    if (!string.IsNullOrWhiteSpace(teacher.SFZJH))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("出错页面：BaseInfo");
                sb.AppendLine("出错方法：IsLogin()");
                sb.AppendLine("出错信息：" + ex.Message);
                sb.AppendLine("出错位置：" + ex.StackTrace);
                Common.LogCommon.writeLogUserCenter(ex.Message, sb.ToString());
                return false;
            }
        }
    }
}