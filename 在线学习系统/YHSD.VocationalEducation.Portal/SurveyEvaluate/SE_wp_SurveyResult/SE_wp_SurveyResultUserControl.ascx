<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_SurveyResultUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_SurveyResult.SE_wp_SurveyResultUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/list_nr.css" rel="stylesheet" />
<style type="text/css">
.table { text-align:center;width:100%;line-height:36px }
</style>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/Control/FusionChart/FusionCharts.js"></script>
<script type="text/javascript" src="/_layouts/15/Script/jquery.jqprint.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#print").click(function () {
                $(".yy_dialog").jqprint({
                    debug: false, //如果是true则可以显示iframe查看效果（iframe默认高和宽都很小，可以再源码中调大），默认是false
                    importCSS: true, //true表示引进原来的页面的css，默认是true。（如果是true，先会找$("link[media=print]")，若没有会去找$("link")中的css文件）
                    printContainer: true, //表示如果原来选择的对象必须被纳入打印（注意：设置为false可能会打破你的CSS规则）。
                    operaSupport: true//表示如果插件也必须支持歌opera浏览器，在这种情况下，它提供了建立一个临时的打印选项卡。默认是true
                });
            })
        });
        //画图(以指定 xml格式文件为数据源)
        function drawChart(xmlData) {
            var myChartId = new Date().getTime();
            var myChart = new FusionCharts("/_layouts/15/Control/FusionChart/StackedColumn3D.swf", myChartId, "100%", "396");
            myChart.setDataXML(xmlData); //获取xml格式数据源
            myChart.addParam("wmode", "Opaque");
            myChart.render("divChart");
        }
    </script>
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">结果汇总</span>
    </dt> 
    <dt style="margin: 10px; height: 30px; line-height: 30px">
        <span style="float: left">
            <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_LearnYear_SelectedIndexChanged" />
            <asp:DropDownList CssClass="option" ID="DDL_Target" runat="server" />
        </span>
        <span style="float: right">
            <asp:LinkButton runat="server" ID="LK_Excel" CssClass="add_GL" Text="导出excel" OnClick="LK_Excel_Click" />&nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="LK_Word" CssClass="add_GL" Text="导出word" OnClick="LK_Word_Click" />&nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="LK_Pdf" CssClass="add_GL" Text="导出pdf" OnClick="LK_Pdf_Click" />&nbsp;&nbsp;
            <a href="javascript:void(0)" id="print" class="add_GL">打印报表</a>
        </span>
    </dt>
    <dd class=".yy_dialog" style="margin:0px 10px;overflow:hidden">
        <div id="divChart"></div>
        <asp:DataGrid runat="server" ID="DG_SurveyResult" CssClass="table" AllowPaging="true" PageSize="10">
            <HeaderStyle BackColor="#f1f1f1" Height="36px" />
            <ItemStyle Height="36px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#e1e1e1" />
            <PagerStyle Mode="NumericPages" Visible="false"/>
        </asp:DataGrid>
        <div class="page">
            <span>
                <asp:LinkButton runat ="server" ID ="FirstPage" Text="首页" CommandArgument="0" OnClick="PageNavigation_Click" />                
                <asp:LinkButton runat ="server" ID ="PrevPage" Text="上一页" OnClick="PageNavigation_Click"/>
                <span class="number now"><asp:Label runat="server" ID="CurrentPage" /></span>
                <asp:LinkButton runat ="server" ID ="NextPage" Text="下一页" OnClick="PageNavigation_Click" />
                <asp:LinkButton runat ="server" ID ="EndPage" Text="末页" OnClick="PageNavigation_Click" />
                |<asp:Label runat="server" ID="CurrentIndex" />/<asp:Label runat="server" ID="PageCount" />页(共<asp:Label runat="server" ID="RecordCount" />项)
            </span>
        </div>
    </dd>
</dl>