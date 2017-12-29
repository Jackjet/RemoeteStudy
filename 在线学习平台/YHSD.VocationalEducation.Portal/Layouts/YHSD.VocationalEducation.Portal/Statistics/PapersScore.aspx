<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PapersScore.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Statistics.PapersScore" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../css/type.css" rel="stylesheet" />
    <link href="../css/Pager.css" rel="stylesheet" />
    <script type='text/javascript' src="../js/layer/jquery.min.js"></script>
    <script type='text/javascript' src="../js/Highcharts/highcharts.js"></script>
    <script type='text/javascript' src="../js/layer/layer.js"></script>
    <script type='text/javascript' src="../js/layer/OpenLayer.js"></script>
    <script type='text/javascript' src="../js/json.js"></script>
    <script type='text/javascript' src="../js/FormatUtil.js"></script>
    <script type='text/javascript' src="../js/Pager.js"></script>
    <script type='text/javascript'>
        var pIndex = 1;
        var pCount = 0;
        var pSize = 10;
        function BindData(data) {
            $("#tab tbody").empty();
            $($.parseJSON(data)).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'><td class='td_tit'>" + this.PaperName + "</td><td class='td_tit'>" + this.UserName + "</td><td class='td_tit'>" + this.ExamCount + "</td><td class='td_tit'>" + this.MaxScore + "</td><td>" + this.AvgScore + "</td></tr>";
                $("#tab tbody").append(tr);
            });

            $('#container').highcharts({
                title: {
                    text: 'Monthly Average Temperature',
                    x: -20 //center
                },
                subtitle: {
                    text: 'Source: WorldClimate.com',
                    x: -20
                },
                xAxis: {
                    categories: ['陈文峰', '贾小剑', '张世宏', '邓雨轩', 'May', 'Jun',
                        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
                yAxis: {
                    title: {
                        text: '分值 (分)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '分'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: '荷兰近代史',
                    data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                }, {
                    name: '5.5',
                    data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                }, {
                    name: '测试试卷',
                    data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                }, {
                    name: 'London',
                    data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                }]
            });
        }
        function ShowData(index) {
            var postData = { CMD: "ExamToStuden", PageSize: pSize, PageIndex: index };
            var model = {};
            var paperName = $("#txtPaperName").val().trim();
            if (paperName != "") {
                model.PaperName = paperName;
            }
            var userName = $("#txtUserName").val().trim();
            if (userName != "") {
                model.UserName = userName;
            }

            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("../Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
                returnVal = $.parseJSON(returnVal);
                SetPageCount(Math.ceil(returnVal.PageCount / pSize));
                BindData(returnVal.Data);
            });
        }
        $(function () {
            var postData = { CMD: "ExamToStuden", PageSize: pSize, PageIndex: pIndex };
            var model = {};
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }

            AjaxRequest("../Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
                BindData(returnVal.Data);
                pCount = returnVal.PageCount;
                LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize));
            });

            $("#btnQuery").click(function () {
                ShowData(1);
            });
        })
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="head">
        <span>试卷标题:</span><input type="text" class="input_part" id="txtPaperName" /><span>学生姓名:</span><input type="text" class="input_part" id="txtUserName" /><input type="button" value="查询" class="button_n" id="btnQuery" />
    </div>
    <div id="main_list_table" class="main_list_table">
        <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
            <thead>
                <tr class="tab_th top_th">
                    <th class="th_tit">试卷名称</th>
                    <th class="th_tit">学生姓名</th>
                    <th class="th_tit">考试次数</th>
                    <th class="th_tit">最高分</th>
                    <th class="th_tit">平均分</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div id="pageDiv" class="pageDiv">
    </div>
    <div id="container" style="min-width:300px;height:300px;max-width:500px;"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
学员成绩统计
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
学员成绩统计
</asp:Content>
