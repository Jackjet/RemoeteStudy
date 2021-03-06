﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassCount.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.Education.ClassCount" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <div id="divPieChart">
    </div>
    <script type="text/javascript" src="js/FusionChart/FusionCharts.js"></script>
    <script type="text/javascript">

        //画图 (以指定 xml格式字符串为数据源)
        function DrawChart(divId, flashFileName, width, height) {
            var myChartId = new Date().getTime();
            var myChart = new FusionCharts("js/FusionChart/" + flashFileName, myChartId, width, height);
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

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
应用程序页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的应用程序页
</asp:Content>
