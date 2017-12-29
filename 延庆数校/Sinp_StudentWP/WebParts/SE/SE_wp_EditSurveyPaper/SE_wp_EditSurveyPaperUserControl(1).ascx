<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_EditSurveyPaperUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_EditSurveyPaper.SE_wp_EditSurveyPaperUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>
<style type="text/css">
    td {
        vertical-align: middle;
    }

    ul li {
        display: block;
        float: left;
        line-height: 45px;
        text-align: center;
        font-size: 16px;
        color: #757575;
        font-family: microsoft yahei ui;
    }

    ul li a span.wenzi {
        color: #10a7ed;
        font-size: 14px;
        margin-bottom: 72px;
        display: block;
    }
    /*li a 当前状态*/
    ul li a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_l.png) no-repeat center;
        width: 214px;
    }

    ul li a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_c_d.png) no-repeat center;
        width: 214px;
    }

    ul li a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_r_d.png) no-repeat center;
        width: 214px;
    }
    /*li a  选中状态 */
    ul li.selected a.tab_l {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_l.png) no-repeat center;
        width: 214px;
    }

    ul li.selected a.tab_c {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_c.png) no-repeat center;
        width: 214px;
    }

    ul li.selected a.tab_r {
        display: block;
        float: left;
        background: url(/_layouts/15/TeacherImages/tab_r.png) no-repeat center;
        width: 214px;
    }
</style>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">问卷管理</span>
        </h2>
    </div>
    <div class="writing_form" style="margin:auto;width:650px">
            <ul>
                <li class="selected"><a href="#" class="tab_l" ><span class="wenzi">完善信息</span></a></li>
                <li><a href="#" class="tab_c"><span class="wenzi">选题组卷</span></a></li>
                <li><a href="#" class="tab_r"><span class="wenzi">完成预览</span></a></li>
            </ul>
        <table class="winTable" style="margin-left:90px">
            <tr>
                <td class="wordsinfo">问卷类别：</td>
                <td class="ku">
                    <asp:DropDownList ID="DDL_Type" runat="server" CssClass="droplist" Width="350px" /></td>
            </tr>
            <tr>
                <td class="wordsinfo">评价类型：</td>
                <td class="ku">
                    <asp:DropDownList ID="DDL_Target" runat="server" CssClass="droplist" Width="350px" /></td>
            </tr>
            <tr>
                <td class="wordsinfo">参与者组：</td>
                <td class="ku">
                    <asp:DropDownList ID="DDL_Ranger" runat="server" CssClass="droplist" Width="350px" /></td>
            </tr>
            <tr>
                <td class="wordsinfo">问卷名称：</td>
                <td class="ku">
                    <asp:TextBox ID="TB_Title" TextMode="MultiLine" Width="350px" Height="26px" runat="server" /></td>
            </tr>
            <tr>
                <td class="wordsinfo">考查时间：
                </td>
                <td class="ku">
                    <input type="text" class="Wdate" readonly="readonly" runat="server" id="dtStartDate" name="dtStartDate" onclick="WdatePicker()" style="width: 147px;" />至<input type="text" class="Wdate" readonly="readonly" runat="server" id="dtEndDate" name="dtEndDate" onclick="WdatePicker()" style="width: 147px;" />
                </td>
            </tr>
        </table>
        <div style="margin:20px 0px 20px 200px">
            <asp:Button ID="Btn_Next" CssClass="btn btn_sure" runat="server" Text="下一步" OnClick="Btn_Next_Click" />
            <asp:Button ID="Btn_Sure" CssClass="btn btn_cancel" runat="server" Text="完成" OnClick="Btn_Sure_Click" />
            <input type="button" class="btn btn_cancel" value="取消" onclick="window.location = 'SurveyPaper.aspx'" />
        </div>
    </div>
</div>