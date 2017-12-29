<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_EditSurveyPaper2UserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditSurveyPaper2.SE_wp_EditSurveyPaper2UserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/StuAssociate.css" rel="stylesheet" />
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
<script type="text/javascript">
    $(function(){
        $(":checkbox").click(function(){
            var chked = $(this).attr('checked');
            if (chked == undefined) {
                if(confirm("你确定要将这题从此问卷中排除吗？")){
                    $(this).closest('li').remove();
                }
            }
        });
    });
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function refleshData(ids) {
        $("#mask,#maskTop").fadeOut();
        //重新刷题
        $("#SelectIds").val(ids);
        $("#Reload").click();
    }
    function getItem() {
        //单选
        var ids = "";
        var singleChk = $("input[name='ck_single']:checked");
        if (singleChk.length > 0) {
            for (var i = 0; i < singleChk.length; i++) {
                ids += singleChk[i].value;
                if(i<singleChk.length-1)
                { ids += ","; }
            }
        }
        //多选
        var mutiChk = $("input[name='ck_muti']:checked");
        if (mutiChk.length > 0) {
            ids += "#";
            for (var i = 0; i < mutiChk.length; i++) {
                ids += mutiChk[i].value;
                if(i<mutiChk.length-1)
                { ids += ","; }
            }
        }
        //问答
        var quesChk = $("input[name='ck_ques']:checked");
        if (quesChk.length > 0) {
            ids += "#";
            for (var i = 0; i < quesChk.length; i++) {
                ids += quesChk[i].value;
                if(i<quesChk.length-1)
                {ids += ",";}
            }
        }
        $("#QuestIds").val(ids);
    }
</script>
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">问卷管理</span>
    </dt> 
    <dd>  
        <ol>
            <li class="selected"><a href="#" class="tab_l"><span class="wenzi">完善信息</span></a></li>
            <li class="selected"><a href="#" class="tab_c"><span class="wenzi">选题组卷</span></a></li>
            <li><a href="#" class="tab_r"><span class="wenzi">完成</span></a></li>
        </ol>
    </dd>
    <dd class="shijuan_nr">
        <h1><asp:Literal runat="server" ID="lt_Title" /></h1>
        <div class="fubiaoti">
            <p>（问卷类别：<asp:Literal runat="server" ID="lt_Type" /></p>
            <p>时间：<asp:Literal runat="server" ID="lt_Date" /></p>
            <p>参与者范围：<asp:Literal runat="server" ID="lt_Ranger" /></p>
            <p>评分类型：<asp:Literal runat="server" ID="lt_Target" />）</p>
        </div>
        <div class="zhubiaoti">
            <h2>第一部分单选题</h2><span onclick="openPage('添加单选题','/SitePages/SelectQuestion.aspx?type=0','700','400');return false">添加</span>
        </div>
        <asp:ListView ID="lvSingle" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷暂无单选题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <ul class="zhubiaoti_nr" id="0">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><input type="checkbox" name="ck_single" value='<%#Eval("ID")%>' checked /><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                    <label>A、<%#Eval("AnswerA")%></label>
                    <label>B、<%#Eval("AnswerB")%></label>
                    <label>C、<%#Eval("AnswerC")%></label>
                    <label>D、<%#Eval("AnswerD")%></label>
                </li>
            </ItemTemplate>
        </asp:ListView>
        <div class="zhubiaoti">
            <h2>第二部分多选题</h2><span onclick="openPage('添加多选题','/SitePages/SelectQuestion.aspx?type=1','700','400');return false">添加</span>
        </div>
        <asp:ListView ID="lvMuti" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷暂无多选题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <ul class="zhubiaoti_nr" id="1">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><input type="checkbox" name="ck_muti" value='<%#Eval("ID")%>' checked /><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                    <label>A、<%#Eval("AnswerA")%></label>
                    <label>B、<%#Eval("AnswerB")%></label>
                    <label>C、<%#Eval("AnswerC")%></label>
                    <label>D、<%#Eval("AnswerD")%></label>
                </li>
            </ItemTemplate>
        </asp:ListView>        
        <div class="zhubiaoti">
            <h2>第三部分问答题</h2><span onclick="openPage('添加问答题','/SitePages/SelectQuestion.aspx?type=2','700','400');return false">添加</span>
        </div>
        <asp:ListView ID="lvQuestion" runat="server">
            <EmptyDataTemplate>
                <p style="text-align: center">此问卷暂无问答题目</p>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <ul class="zhubiaoti_nr" id="2">
                    <li id="itemPlaceholder" runat="server"></li>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <p><input type="checkbox" name="ck_ques" value='<%#Eval("ID")%>' checked /><%#Eval("SortID")%>、<%#Eval("Title")%></p>
                </li>
            </ItemTemplate>
        </asp:ListView>
        <div style="margin:20px auto;width:210px">
            <asp:Button ID="btnSure" CssClass="btn btn_sure" runat="server" Text="完成" OnClientClick="getItem()" OnClick="Btn_Sure_Click" />
            <input type="button" class="btn btn_cancel" value="上一步" onclick="window.location = 'EditSurveyPaper.aspx?itemid=<%=this.ItemId %>    '" />
        </div>
    </dd>
</dl>
<div style="display: none">
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="QuestIds" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="SelectIds" />
    <asp:Button runat="server" ClientIDMode="Static" ID="Reload" OnClick="Reload_Click" />
</div>