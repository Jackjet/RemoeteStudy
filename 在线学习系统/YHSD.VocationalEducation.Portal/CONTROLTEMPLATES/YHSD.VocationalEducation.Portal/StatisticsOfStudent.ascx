<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatisticsOfStudent.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.StatisticsOfStudent" %>

<script type='text/javascript' src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Highcharts/highcharts.js"></script>


<style type="text/css">

    .switchTitle {
        height: 39px;
        width: 801px;
        border: 1px solid silver;
        background-image: url('/_layouts/15/YHSD.VocationalEducation.Portal/images/toolBg.gif');
        background-repeat: repeat-x;
        font-family: "微软雅黑","宋体";
        font-size: 16px;
        text-align: center;
        line-height: 39px;
        margin-left: 5px;
    }

    .lightBtn {
        background-color: rgba(72, 182, 0, 1);
        color: white !important;
        cursor: default !important;
    }

    .main_list_table {
        margin-top: 0px;
    }

    .btnDiv {
        height: 30px;
        width: 70px;
        float: right;
        padding: 0 5px 5px 5px;
        font-size: 14px;
        cursor: pointer;
        color: black;
    }
</style>
<script type='text/javascript'>
    function CreateArrByLength(length) {
        var arr = new Array();
        for (var i = 0; i < length; i++) {
            arr.push(0);
        }
        return arr;
    }
    function BindDataToChart(data) {
        var NameArr = new Array();
        var PaperArr = new Array();
        var zeroArr = new Array();

        for (var i = 0; i < data.length; i++) {
            var nIndex = NameArr.indexOf(data[i].UserName);
            if (nIndex == -1) {
                NameArr.push(data[i].UserName);
            }
        }
        for (var i = 0; i < NameArr.length; i++) {
            zeroArr.push(0);
        }
        for (var i = 0; i < data.length; i++) {
            var obj = ArrayFind(PaperArr, "name", data[i].PaperName);
            if (obj == undefined) {
                PaperArr.push({ name: data[i].PaperName, data: CreateArrByLength(NameArr.length) });
            }
        }
        for (var i = 0; i < data.length; i++) {
            var pName = data[i].PaperName;
            var uIndex = NameArr.indexOf(data[i].UserName);
            var index = PaperArr.indexOf(ArrayFind(PaperArr, "name", pName));
            if (index != -1) {
                PaperArr[index].data[uIndex] = parseFloat(data[i].AvgScore);
            }
        }

        $('#container').highcharts({
            title: {
                text: '',
                x: -20
            },
            exporting: { enabled: true },
            xAxis: {
                categories: NameArr
            },
            yAxis: {
                title: {
                    text: '最高分'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            credits: { enabled: false },
            tooltip: {
                valueSuffix: '分',
                crosshairs: true,
                shared: true
            },
            legend: {
                align: 'center', //水平方向位置
                verticalAlign: 'bottom', //垂直方向位置
                x: 0, //距离x轴的距离
                y: 0 //距离Y轴的距离
            },
            series: PaperArr
        });
    }
    function BindDataToTab(data) {
        $("#tab tbody").empty();
        $(data).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.PaperName + "</td>\
                        <td class='td_tit_left'>" + this.UserName + "</td>\
                        <td class='td_tit_left'>" + this.Class + "</td>\
                        <td class='td_tit'>" + this.MaxScore + "</td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
    }
    
    function ShowChartData() {
        var postData = { CMD: "ExamToStuden", PageSize: pageSize, PageIndex: -1 };
        var model = {};
        var paperName = $("#<%=txtPaperName.ClientID%>").val().trim();
        if (paperName != "") {
            model.PaperName = paperName;
        }
        var userName = $("#<%=txtUserName.ClientID%>").val().trim();
        if (userName != "") {
            model.UserName = userName;
        }
        var className = $("#<%=txtClassName.ClientID%>").val().trim();
        if (className != "") {
            model.Class = className;
        }

        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
            BindDataToChart($.parseJSON(returnVal.Data));
        })
    }
    function ShowData(index) {
        var postData = { CMD: "ExamToStuden", PageSize: pageSize, PageIndex: index };
        var model = {};
        var paperName = $("#<%=txtPaperName.ClientID%>").val().trim();
        if (paperName != "") {
            model.PaperName = paperName;
        }
        var userName = $("#<%=txtUserName.ClientID%>").val().trim();
        if (userName != "") {
            model.UserName = userName;
        }
        var className = $("#<%=txtClassName.ClientID%>").val().trim();
        if (className != "") {
            model.Class = className;
        }

        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }
    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);
        $("#container").hide();

        $("#<%=txtPaperName.ClientID%>,#<%=txtUserName.ClientID%>,#<%=txtClassName.ClientID%>").keypress(function () {
            EnterEvent("btnQuery");
        })

        $("#btnQuery").click(function () {
            ShowData(1);
        });
        $("#divPicture").click(function () {
            $("#divPicture").addClass("lightBtn");
            $("#divList").removeClass("lightBtn");
            $("#listDiv").hide();
            $("#container").show();
            ShowChartData();
        })
        $("#divList").click(function () {
            $("#divPicture").removeClass("lightBtn");
            $("#divList").addClass("lightBtn");
            $("#listDiv").show();
            $("#container").hide();
        })
    })
</script>
<div class="TopToolbar">
    <span>试卷名称&nbsp</span><input type="text" class="input_part" id="txtPaperName" runat="server" style="width: 110px !important;" />&nbsp&nbsp
    <span>学生姓名&nbsp</span><input type="text" class="input_part" id="txtUserName" runat="server" style="width: 110px !important;" />&nbsp&nbsp
    <span>班级名称&nbsp</span><input type="text" class="input_part" id="txtClassName" runat="server" style="width: 110px !important;" />
    <input type="button" value="查询" class="button_s" id="btnQuery" />
    <asp:Button runat="server" Text="导出" UseSubmitBehavior="false" ID="btnExport" CssClass="button_s" OnClick="btnExport_Click" />
</div>
<div class="switchTitle">
    <span>学员成绩统计</span>
    <div class="btnDiv" id="divPicture">图表</div>
    <div class="btnDiv lightBtn" id="divList">列表</div>
</div>
<div id="listDiv">
    <div id="main_list_table" class="main_list_table">
        <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
            <thead>
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">试卷名称</th>
                    <th class="th_tit_left">学生姓名</th>
                    <th class="th_tit_left">班级名称</th>
                    <th class="th_tit">最高分</th>
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
<div id="container" style="width: 800px; height: 300px;margin-top:10px;"></div>
