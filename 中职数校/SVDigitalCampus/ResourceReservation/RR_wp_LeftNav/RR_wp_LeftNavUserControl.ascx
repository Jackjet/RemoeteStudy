<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_LeftNavUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_LeftNav.RR_wp_LeftNavUserControl" %>
<link href="/_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<link href="/_layouts/15/SVDigitalCampus/test/type.css" rel="stylesheet" />
<link href="/_layouts/15/SVDigitalCampus/test/Pager.css" rel="stylesheet" />
<script src="/_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="/_layouts/15/SVDigitalCampus/test/json.js"></script>
<script src="/_layouts/15/SVDigitalCampus/test/VocationalEducation.js"></script>
<script src="/_layouts/15/SVDigitalCampus/test/FormatUtil.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/Drive/LeftNav.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/uploadFile2.js"></script>
<style type="text/css">
    .hid {
        display: none;
    }

    .fileup {
        border-radius: 3px;
        background-color: #0DA6EC;
        color: white;
        border: 0px;
    }

    .shenhe {
        display: block;
        padding: 0px 4px;
        background: #5493d7;
        border-radius: 3px;
        color: #fff;
        float: left;
        margin: 0 4px;
        height: 20px;
        line-height: 20px;
    }

    .selected {
        display: inline-block;
        background: #abd0f5;
        color: #fff;
    }

    .star {
        padding: 0;
        margin: 0;
        list-style: none;
        float: left;
    }

        .star li {
            float: left;
            height: 20px;
            width: 20px;
            margin-right: 4px;
        }

            .star li.on {
                color: #f60;
            }

            .star li.off {
                color: #ccc;
            }
</style>
<script type="text/javascript">
    function Addmenu() {
        popWin.showWin("600", "480", "添加资源", "/AddResource.aspx");
    }
    var webUrl1 = _spPageContextInfo.webAbsoluteUrl;
    function openPage1(pageTitle, pageName, pageWidth, pageHeight) {
        var typeId = $("#hContent").val();
        if (typeId=="")
        {
            alert("请选择资源类型");
            return false;
        }
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl1 + pageName+"?typeId="+typeId);
    }
    function editpdetail1(pageTitle, pageName, pageWidth, pageHeight) {
        
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl1 + pageName);
    }
</script>

<!--校本资源库-->

<div class="School_library">
    <input id="hContent" type="hidden" />
    <input id="hOperate" type="hidden" />
    <!--页面名称-->
    <h1 class="Page_name">基础数据维护</h1>

    <div class="Whole_display_area">

        <div class="clear"></div>
        <div class="Schoolcon_wrap">
            <div class="left_navcon fl">
                <h1>资源类型</h1>
                <h3 class="tit" style="background: #F6F6F6" id="quanbu" onclick="navTopClick()">
                    <i class="icon" style="background: #0da6ea; border: 1px solid #0da6ea"></i>全部
                    <%--<i class="iconfont" style="cursor: pointer;" id="toptianjia" onclick="AddMenuTop('')">&#xe630;</i>--%></h3>
                <div class="select-box" id="Tree">
                </div>
            </div>
            <div class="right_dcon fr">
                <!--操作区域-->
                <div class="Operation_area">
                    <div class="left_choice fl">
                        <ul>
                            <li class="Sear">
                                <%--<asp:TextBox ID="TB_Search" CssClass="search" runat="server"></asp:TextBox>--%>
                                <input type="text" id="tb_search" class="search" />
                                <i class="iconfont" id="btnQuery" onclick="btnQuery()">&#xe62d;</i>
                            </li>
                        </ul>
                    </div>
                    <div class="right_add fr" id="opertion">
                        <a href="#" class="add" onclick="serchTime();"><i class="iconfont">&#xe62d;</i>搜索</a>
                        <div class="Batch_operation fl" id="plcz">
                            <a href="#" class="add" onclick="openPage1('添加资源','/SitePages/AddResource.aspx','700','500');"><i class="iconfont">&#xe612;</i>新增资源</a>
                        </div>
                    </div>
                </div>


                <div class="clear"></div>
                
                <!--展示区域-->
                <div class="Display_form">
                    <table id="tabroom" class="D_form" style="width:100%; margin-top:30px;">
                                
                            </table>
                    <div id="pageDiv" class="pageDiv" style="display: none"></div>
                    
                </div>
            </div>
        </div>
    </div>

</div>




