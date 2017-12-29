<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_EditSurveyPaper3UserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditSurveyPaper3.SE_wp_EditSurveyPaper3UserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/list_nr.css" rel="stylesheet" >
<style type="text/css">
    ol {
        margin:auto;width:650px;
    }
    ol li {
        display: block;
        float: left;
        line-height: 45px;
        text-align: center;
        font-size: 16px;
        color: #757575;
        font-family: microsoft yahei ui;
    }
    ol li a span.wenzi {
        color: #10a7ed;
        font-size: 14px;
        margin-bottom: 72px;
        display: block;
    }
    /*li a 当前状态*/
    ol li a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_l.png) no-repeat center;
        width: 214px;
    }
    ol li a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_c_d.png) no-repeat center;
        width: 214px;
    }
    ol li a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_r_d.png) no-repeat center;
        width: 214px;
    }
    /*li a  选中状态 */
    ol li.selected a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_l.png) no-repeat center;
        width: 214px;
    }
    ol li.selected a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_c.png) no-repeat center;
        width: 214px;
    }
    ol li.selected a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/Images/tab_r.png) no-repeat center;
        width: 214px;
    }
</style>
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">问卷管理</span>
    </dt> 
    <dd>  
        <ol>
            <li class="selected"><a href="#" class="tab_l"><span class="wenzi">完善信息</span></a></li>
            <li class="selected"><a href="#" class="tab_c"><span class="wenzi">选题组卷</span></a></li>
            <li class="selected"><a href="#" class="tab_r"><span class="wenzi">完成</span></a></li>
        </ol>
    </dd>
    <dd class="shijuan_nr" style="text-align:center">
        您的试卷已成功新建，请回到<a href="SurveyPaper.aspx">[问卷管理]</a>中查看
    </dd>
    </dl>