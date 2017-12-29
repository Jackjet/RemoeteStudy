<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.TestMgr" %>

<script type="text/javascript">
    function StartTest(obj) {
        var paperId = obj.PaperID;
        var layerIndex = OL_ShowLayer(2, "考试", 860, 660, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/StartToTest.aspx?PaperID=" + paperId, function (flag) {
            if (flag)
                ShowData(1);
        });
        layer.full(layerIndex);
    }
    function GetExamState(data) {
        switch (data.State) {
            case 0:
                return "<a href='javascript:;' onclick='StartTest(" + JSON.stringify(data) + ")'>考试</a>";
                break;
            case 1:
                return "<span>未开考</span>";
                break;
            case 2:
                return "<span>已结束</span>";
                break;
        }
    }
    function GetScore(data) {
        //if (data.State == 0 && data.HighestScore == "") {
        //    return "请考试";
        //}
        if (data.State == 2 && data.HighestScore >= data.PassScore) {
            return data.HighestScore;
        }
        else if (data.State == 2 && data.HighestScore < data.PassScore) {
            return data.HighestScore+"(未合格)";
        }
        return data.HighestScore;
    }
    function GetState(data) {
        return data == "" ? "未阅卷" : "已阅卷";
    }
    function BindDataToTab(bindData) {
        $("#tab tbody").empty();
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.Title + "</td>\
                        <td class='td_tit'>" + this.StartDate + "</td>\
                        <td class='td_tit'>" + this.EndDate + "</td>\
                        <td class='td_tit_right'>" + this.QuestionCount + "</td>\
                        <td class='td_tit_right'>" + this.TotalScore + "</td>\
                        <td class='td_tit_right'>" + this.PassScore + "</td>\
                        <td class='td_tit_right'>" + GetScore(this) + "</td>\
                        <td class='td_tit_right'>" + GetState(this.MarkingTime) + "</td>\
                        <td class='td_tit'>" + GetExamState(this) + "</td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
    }
    function ShowData(index) {
        var postData = { CMD: "MyExams", PageSize: pageSize, PageIndex: index };
        var name = $("#txtPaperName").val();
        var model = {};
        if (name != "") {
            model.Title = name;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/PapersMgrHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }
    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);

        $("#txtPaperName").keypress(function () {
            EnterEvent("btnQuery");
        })

        $("#btnQuery").click(function () {
            ShowData(1);
        })
    })
</script>
<div>
    <div class="TopToolbar">
        <span>试卷名称&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtPaperName" />
        <input type="button" value="查询" id="btnQuery" class="button_s" />
    </div>
    <div class="main_list_table">
        <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
            <thead>
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">试卷名称</th>
                    <th class="th_tit">开考时间</th>
                    <th class="th_tit">截止时间</th>
                    <th class="th_tit_right">题量</th>
                    <th class="th_tit_right">总分</th>
                    <th class="th_tit_right">合格分</th>
                    <th class="th_tit_right">最高分</th>
                    <th class="th_tit_right">状态</th>
                    <th class="th_tit">操作</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div style="text-align: center;">
        <div id="containerdiv" style="overflow: hidden; display: inline-block;">
            <div id="pageDiv" class="pageDiv">
            </div>
        </div>
    </div>
</div>
