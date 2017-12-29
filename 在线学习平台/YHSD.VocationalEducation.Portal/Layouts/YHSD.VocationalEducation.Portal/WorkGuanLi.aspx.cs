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
        private static List<UserInfo> list { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    CurriculumID = Request["id"];
                    BindWorkGuanLi();
                }
            }
        }
        public void BindWorkGuanLi()
        {
            string sql = "select * from(select Id,Code,Name,DomainAccount,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo,case when Sex=1 then '男' else '女' end as Sex ,((select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0 and id in(select TableID from ClickDetail where Userid=UserInfo.id and TableName='Chapter' ))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))as Percentage,((select count(*) from StudyExperience where UserID=UserInfo.id and IsDelete=0 and ChapterID in(select id from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0)) as Worktage,((select Count(*) from HomeWork where UserID=UserInfo.id and IsDelete=0 and ChapterID in(select id from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0))*100/(select count(*) from Chapter where CurriculumID='" + CurriculumID + "' and IsDelete=0)) as Hometage,(select Name from ClassInfo where ID=(select cid from ClassUser where UId=UserInfo.Id)) as ClassName from UserInfo where IsDelete=0 and  id in (select Uid from ClassUser where cid in (select ClassID from ClassCurriculum where CurriculumID='" + CurriculumID + "' and IsDelete=0))) t where t.ClassName in (select Name from ClassInfo where Teacher=(select id from UserInfo where Name='"+SPContext.Current.Web.CurrentUser.Name+"')) order by Hometage";
            list = new UserInfoManager().Find(sql);
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();

            //作业展示
            BindHomeWork();
        }

        private void BindHomeWork()
        {
            int youxiu = 0, wsc = 0;
            List<HomeWork> Worklist = new List<HomeWork>();
            List<Chapter> chapterlist = new ChapterManager().GetChapterWork(Request["id"]);
            for (int i = chapterlist.Count-1; i >=0 ; i--)
            {
                List<HomeWork> WorkTemp = new List<HomeWork>();
                foreach (UserInfo ui in list)
                {
                    if (string.IsNullOrEmpty(new StudyExperienceManager().Get(ui.Id, chapterlist[i].Id).Id))//未上传
                    {
                        wsc++;
                    }
                    else
                    {
                        HomeWork home = new HomeWorkManager().Get(ui.Id, chapterlist[i].Id);
                        if (!string.IsNullOrEmpty(home.Id))
                        {
                            home.UserID = ui.Name;
                            home.ChapterID = chapterlist[i].Id;
                            home.ChapterNum = "第" + chapterlist[i].SerialNumber + "章";
                            home.ChapterName = chapterlist[i].Title;
                            home.IsExcellentWork = home.IsExcellentWork == "False" ? "否" : "是";
                            if (home.IsExcellentWork == "是")
                            {
                                youxiu++;
                            }
                            WorkTemp.Add(home);
                        }
                    }
                }
                Worklist.AddRange(WorkTemp);
            }
            this.AspNetPagerHomeWork.PageSize = 10;
            this.AspNetPagerHomeWork.RecordCount = Worklist.Count;//分页控件要显示数据源的记录总数
            PagedDataSource pdshomework = new PagedDataSource();//数据源分页
            pdshomework.DataSource = Worklist;//得到数据源
            pdshomework.AllowPaging = true;//允许分页
            pdshomework.CurrentPageIndex = AspNetPagerHomeWork.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pdshomework.PageSize = AspNetPagerHomeWork.PageSize;//分页数据源的每页记录数
            RepeaterHomeWork.DataSource = pdshomework;
            RepeaterHomeWork.DataBind();
            this.lb_ysc.Text = Worklist.Count.ToString();
            this.lb_wsc.Text = wsc.ToString();
            this.lb_youxiu.Text = youxiu.ToString();
            this.lb_yl.Text = Worklist.Count > 0 ? ((youxiu * (1.0) / Worklist.Count) * 100).ToString() + "%" : "";
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindWorkGuanLi();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindWorkGuanLi();
        }

        protected void AspNetPagerHomeWork_PageChanged(object sender, EventArgs e)
        {
            BindHomeWork();
        }
    }
}
