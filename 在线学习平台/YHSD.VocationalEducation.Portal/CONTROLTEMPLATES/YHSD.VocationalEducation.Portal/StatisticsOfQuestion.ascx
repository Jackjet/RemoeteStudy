<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatisticsOfQuestion.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.StatisticsOfQuestion" %>

<script type='text/javascript'>
    function CreateArrByLength(length) {
        var arr = new Array();
        for (var i = 0; i < length; i++) {
            arr.push(0);
        }
        return arr;
    }
    function BindDataToTab(data) {
        $("#tab tbody").empty();
        $(data).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left' title='" + this.Title + "'>" + SubText(this.Title, 15) + "</td>\
                        <td class='td_tit_left'>" + this.QuestionType + "</td>\
                        <td class='td_tit_right'>" + this.ErrorPercent + "</td>\
                        <td class='td_tit'>" + this.ShowCount + "</td>\
                        <td class='td_tit_left'>" + this.CreateUser + "</td>\
                        <td class='td_tit'>" + this.CreateTime + "</td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
    }
    function BindDataToChart(data) {
        var NameArr = new Array();
        var PaperArr = new Array();
        var zeroArr = new Array();

        for (var i = 0; i < data.length; i++) {
            var nIndex = NameArr.indexOf(data[i].Class);
            if (nIndex == -1) {
                NameArr.push(data[i].Class);
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
            var uIndex = NameArr.indexOf(data[i].Class);
            var index = PaperArr.indexOf(ArrayFind(PaperArr, "name", pName));
            if (index != -1) {
                PaperArr[index].data[uIndex] = parseFloat(data[i].AvgScore);
            }
        }

        //$('#container').highcharts({
        //    title: {
        //        text: '',
        //        x: -20
        //    },
        //    exporting: { enabled: true },
        //    xAxis: {
        //        categories: NameArr
        //    },
        //    yAxis: {
        //        title: {
        //            text: '平均分'
        //        },
        //        plotLines: [{
        //            value: 0,
        //            width: 1,
        //            color: '#808080'
        //        }]
        //    },
        //    credits: { enabled: false },
        //    tooltip: {
        //        valueSuffix: '分',
        //        crosshairs: true,
        //        shared: true
        //    },
        //    legend: {
        //        align: 'center', //水平方向位置
        //        verticalAlign: 'bottom', //垂直方向位置
        //        x: 0, //距离x轴的距离
        //        y: 0 //距离Y轴的距离
        //    },
        //    series: PaperArr
        //});
    }

    function ShowData(index) {
        var postData = { CMD: "ExamToQuestion", PageSize: pageSize, PageIndex: index };
        var model = {};
        var title = $("#<%=txtTitle.ClientID%>").val().trim();
        if (title != "") {
            model.Title = title;
        }
        var createUser = $("#<%=txtCreateUser.ClientID%>").val().trim();
        if (createUser != "") {
            model.CreateUser = createUser;
        }
        var questionType = $("#<%=hfQuestionType.ClientID%>").val().trim();
        if (questionType != "") {
            model.QuestionType = questionType;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }
    function ShowChartData() {
        var postData = { CMD: "ExamToQuestion", PageSize: pageSize, PageIndex: -1 };
        var model = {};
        var title = $("#<%=txtTitle.ClientID%>").val().trim();
        if (title != "") {
            model.Title = title;
        }
        var createUser = $("#<%=txtCreateUser.ClientID%>").val().trim();
        if (createUser != "") {
            model.CreateUser = createUser;
        }
        var questionType = $("#<%=hfQuestionType.ClientID%>").val().trim();
        if (questionType != "") {
            model.QuestionType = questionType;
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/StatisticsMgrHandler.aspx", postData, function (returnVal) {
            BindDataToChart($.parseJSON(returnVal.Data));
        })
    }
    $(function () {
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);

        $("#btnQuery").click(function () {
            ShowData(1);
        })

        $("#<%=txtTitle.ClientID%>,#<%=txtCreateUser.ClientID%>").keypress(function () {
            EnterEvent("btnQuery");
        })

        $("#btnQuery").click(function () {
            ShowData(1);
        });
        $("#EasyuiQt").combobox({
            onSelect: function (item) {
                $("#<%=hfQuestionType.ClientID%>").val(item.value);
                ShowData(1);
            }
        })
        $('#EasyuiQt').combobox('setValue', '');
    })
</script>
<div class="TopToolbar">
    <span>问题名称&nbsp</span><input type="text" class="input_part" id="txtTitle" runat="server" style="width:110px !important;"/>&nbsp&nbsp
    <span>出题人&nbsp</span><input type="text" class="input_part" id="txtCreateUser" runat="server" style="width:110px !important;"/>&nbsp&nbsp
    <span>题型&nbsp</span>
    <select class="easyui-combobox" name="state" id="EasyuiQt" data-options="editable:false" panelHeight="120" style="width:110px;height:28px;">
        <option value="">所有</option>
        <option value="单选题">单选题</option>
        <option value="多选题">多选题</option>
        <option value="判断题">判断题</option>
        <option value="简答题">简答题</option>
    </select>
    <input type="button" value="查询" class="button_s" id="btnQuery" />
    <asp:Button runat="server" Text="导出" ID="btnExport" UseSubmitBehavior="false" CssClass="button_s" onclick="btnExport_Click"/>
    <input type="hidden" id="hfQuestionType" runat="server"/>
</div>
<div id="main_list_table" class="main_list_table">
    <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
        <thead>
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">问题名称</th>
                <th class="th_tit_left">题型</th>
                <th class="th_tit_right">错误率</th>
                <th class="th_tit">出题次数</th>
                <th class="th_tit_left">出题人</th>
                <th class="th_tit">出题时间</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>
<div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
        <div id="pageDiv" class="pageDiv">
        </div>
    </div>
</div>
<div id="container" style="width: 800px; height: 300px;"></div>
