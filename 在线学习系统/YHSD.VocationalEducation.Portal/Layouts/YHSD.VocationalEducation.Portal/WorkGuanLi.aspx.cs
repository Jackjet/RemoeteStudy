using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class WorkGuanLi : LayoutsPageBase
    {
        public string CurriculumID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                 
                    BindWorkGuanLi();
                }
            }
        }
        public void BindWorkGuanLi()
        {
            CurriculumID = Request["id"];
            string sql = "select Id,Code,Name,DomainAccount,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo,case when Sex=1 then '男' else '女' end as Sex ,((select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0 and id in(select TableID from ClickDetail where Userid=UserInfo.id and TableName='Chapter' ))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))as Percentage,((select count(*) from StudyExperience where UserID=UserInfo.id and IsDelete=0 and ChapterID in(select id from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0)) as Worktage,((select Count(*) from HomeWork where UserID=UserInfo.id and IsDelete=0 and ChapterID in(select id from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0)) as Hometage,(select Name from ClassInfo where ID=(select cid from ClassUser where UId=UserInfo.Id)) as ClassName from UserInfo where IsDelete=0 and  id in (select Uid from ClassUser where cid in (select ClassID from ClassCurriculum where CurriculumID='" + CurriculumID + "' and IsDelete=0)) order by Hometage";
            List<UserInfo> list = new UserInfoManager().Find(sql);
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindWorkGuanLi();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindWorkGuanLi();
        }
    }
}
