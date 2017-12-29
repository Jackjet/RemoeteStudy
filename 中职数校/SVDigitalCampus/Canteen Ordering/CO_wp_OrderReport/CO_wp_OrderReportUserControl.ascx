<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_OrderReportUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_OrderReport.CO_wp_OrderReportUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<div id="order" class="orderdiv">
    <div class="Confirmation_order">
        <!--页面名称-->
        <h1 class="Page_name">订单管理</h1>
        <!--整个展示区域-->
        <div class="Whole_display_area">
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="">
                            <div class="qiehuan fl">
                                <span class="qijin">
                                    <asp:LinkButton ID="btnToday" runat="server" OnClick="btnToday_Click" CssClass="Disable">今日订单</asp:LinkButton><asp:LinkButton ID="btnOld" runat="server" CssClass="Disable" OnClick="btnOld_Click" >历史订单</asp:LinkButton><asp:LinkButton ID="btnTotilByPic" runat="server" CssClass="Enable">图表统计</asp:LinkButton></span>
                            </div>
                        </li>

                    </ul>
                </div>
                <div class="right_add fr">
                </div>
                <div class="clear"></div>
            </div>
            <div id="test1" style="height: 400px;"></div>
            <div id="test2" style="height: 400px;"></div>

        </div>
    </div>
</div>
