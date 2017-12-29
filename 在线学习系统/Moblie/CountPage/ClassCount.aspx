<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="ClassCount.aspx.cs" Inherits="Moblie.CountPage.ClassCount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="divPieChart">
    </div>
    <script type="text/javascript" src="../js/FusionChart/FusionCharts.js"></script>
    <script type="text/javascript">

        //画图 (以指定 xml格式字符串为数据源)
        function DrawChart(divId, flashFileName, width, height) {
            var myChartId = new Date().getTime();
            var myChart = new FusionCharts("../js/FusionChart/" + flashFileName, myChartId, width, height);
            myChart.setDataXML('<chart caption="课程统计" numberPrefix=""><set value="25" label="9月" color="AFD8F8" /><set value="17" label="10月" color="F6BD0F" /><set value="23" label="11月" color="8BBA00" isSliced="1" /></chart>');
            myChart.addParam("wmode", "Opaque");
            myChart.render(divId);
        }

        DrawChart("divPieChart", "Column3D.swf", "100%", "250");

    </script>
    <div class="decoration"></div>
    <p class="center-text uppercase footer-text">
        Powered by <a href="" target="_blank"></a>Copyright © 2015 - 2016
    </p>
</asp:Content>
