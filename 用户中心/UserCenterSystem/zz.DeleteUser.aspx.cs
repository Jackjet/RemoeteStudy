using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ADWS.ADWebService ad = new ADWS.ADWebService();
            bool Result = ad.DeleteUser(TextBox1.Text);
            if (Result)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑成功')", true);
            }
             
        }
    }
}