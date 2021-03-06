﻿using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class OpenCoursesList : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCurriculumInfo();
            }
        }
        public void BindCurriculumInfo()
        {
            CurriculumInfo Curriculum = new CurriculumInfo();
            Curriculum.IsOpenCourses = "1";
            List<CurriculumInfo> ListCurr = new List<CurriculumInfo>();
            List<CurriculumInfo> ListCurrTemp = new CurriculumInfoManager().Find(Curriculum);
            DateTime now_time = System.DateTime.Now;
            for (int i = 0; i < ListCurrTemp.Count;i++ ) 
            {
                if (now_time >= ListCurrTemp[i].StartTime && now_time <= ListCurrTemp[i].EndTime)
                {
                    if (ListCurrTemp[i].IfFree == "0") { ListCurrTemp[i].IfFree = "免费"; }
                    else { ListCurrTemp[i].IfFree = "收费"; }
                    ListCurr.Add(ListCurrTemp[i]);
                }
                //if (ListCurrTemp[i].StartTime != null && now_time < ListCurrTemp[i].StartTime) { ListCurrTemp.Remove(ListCurrTemp[i]); }
                //if (ListCurrTemp[i].EndTime != null && now_time > ListCurrTemp[i].EndTime) { ListCurrTemp.Remove(ListCurrTemp[i]); }
            }
           

            this.AspNetPageCurriculum.PageSize = 8;
            this.AspNetPageCurriculum.RecordCount = ListCurr.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = ListCurr;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterCourseList.DataSource = pds;
            RepeaterCourseList.DataBind();

        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindCurriculumInfo();
        }
        public static string GetRemoveString(string text)
        {
            if (text.Length > 12)
            {
                return text.Remove(10) + "..";
            }
            else
            {
                return text;
            }
        }
    }
}
