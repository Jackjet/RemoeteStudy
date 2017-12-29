using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Moblie
{
    public partial class TeachingInteraction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetClassDynamics();
            }
        }
        public void GetClassDynamics()
        {
            List<DynamicInformationEntity> DynamicInformationEntityList = new List<DynamicInformationEntity>();
            DynamicInformationEntityList.Add(new DynamicInformationEntity("课程安排", 20, "每日更新课程安排", "images/icon12x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("上课通知", 34, "上课通知详情", "images/icon12x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("班级活动", 67, "最新班级活动", "images/icon42x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("班会通知", 23, "班会时间通知", "images/icon32x.png"));
            this.ClassDynamicsRepeater.DataSource = DynamicInformationEntityList;
            this.ClassDynamicsRepeater.DataBind();
        }
    }
}