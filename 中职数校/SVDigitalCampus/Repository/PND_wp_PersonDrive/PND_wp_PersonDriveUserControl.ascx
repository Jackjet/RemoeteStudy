<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PND_wp_PersonDriveUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.PND_wp_PersonDrive.PND_wp_PersonDriveUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/test/type.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/test/Pager.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/test/Pager.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/json.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/VocationalEducation.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/test/FormatUtil.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Drive/DriveList.js"></script>

<style type="text/css">
    .hid {
        display: none;
    }

    .selected {
        display: inline-block;
        background: #5493D7;
    }

        .selected a {
            background: #5493D7;
            color: #fff;
        }
</style>
<script type="text/javascript">
    function openShare() {
        if (isNull()) {
            var FirstUrl = window.location.href;
            FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

            popWin.showWin('400', '300', '文件分享', FirstUrl + '/SitePages/PND_wp_Shrar.aspx', "no");
        }
        else {
            alert("请选择要共享的文件");
        }
    }
    function isNull() {
        var ids = '';
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
        if (ids == "") {
            return false;
        }
        else
            return true;
    }
    function Share(type, subjectid, id) {
        var ids = '';
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
        $("#maskTop").fadeOut();
        $("#mask").fadeOut();

        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        if (ids == "") {
            alert("请选择要共享的文件");
        }
        else {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
                data: { CMD: "Share", "ShareId": ids, "subjectid": subjectid, "type": type, "CatagoryID": id },
                dataType: "text",
                success: function (returnVal) {

                    if (returnVal.split(":")[0] == "1") {
                        alert("文件分享成功【注：只允许分享文件不允许分享文件夹】");
                        ShowData(1);
                    }
                    else {
                        if (returnVal.split(";")[1] > 0) {
                            alert("只能分享具体文件");
                        }
                        else {
                            alert('分享失败！');
                        }
                    }
                },
                error: function (errMsg) {
                    alert('操作失败！');
                }
            });
        }
    }
    function ShareTolibrary() {
        var ids = '';
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        if (ids == "") {
            alert("请选择要共享的文件");
        }
        else {
            $.ajax({
                type: "Post",
                url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/PersonDrive.aspx",
                data: { CMD: "ShareTolibrary", "ShareId": ids },
                dataType: "text",
                success: function (returnVal) {
                    if (returnVal.split(":")[0] == "1") {
                        alert("文件分享成功【注：只允许分享文件不允许分享文件夹】");
                        ShowData(1);
                    }
                    else {
                        if (returnVal.split(";")[1] > 0) {
                            alert("只能分享具体文件");
                        }
                        else {
                            alert('分享失败！');
                        }
                    }
                    //if (returnVal == "1") {
                    //    alert("分享成功");
                    //    ShowData(1);
                    //}
                    //else {
                    //    alert('分享失败！');
                    //}
                },
                error: function (errMsg) {
                    alert('操作失败！');
                }
            });
        }
    }
</script>
<div class="Personal_skyDrive">

    <!--页面名称-->
    <h1 class="Page_name">个人网盘</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Sear">
                        <input type="text" id="txtName" placeholder=" 请输入关键字" class="search" name="search" /><i class="iconfont" id="btnQuery" onclick="Query()">&#xe62d;</i>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                <a href="#" class="add" onclick="uploader()"><i class="iconfont">&#xe65a;</i>上传文件</a>
                <a href="#" class="add" onclick="javascript: appendAfterRow('tab', '1')"><i class="iconfont">&#xe601;</i>新建文件夹</a>
                <div class="Batch_operation fl">
                    <a href="#" class="add"><i class="iconfont">&#xe602;</i>批量操作</a>
                    <ul class="B_con" style="display: none;" id="B_con">
                        <li><a href="#" onclick="Move('')">移动到</a></li>
                        <li><a href="#" onclick="Move()">复制到</a></li>
                        <li><a href="#" onclick="DelMore()">删除</a></li>
                        <li><a href="#" onclick="Down('')">下载</a></li>
                    </ul>
                </div>
                <a href="#" class="add" onclick="ShareTolibrary()"><i class="iconfont">&#xe630;</i>分享到资源库</a>

                <a href="#" class="add" onclick="openShare()"><i class="iconfont">&#xe630;</i>分享到校本资源库</a>

            </div>
        </div>
        <div class="clear"></div>
        <!--展示区域-->
        <div class="Display_form">
            <!--文件类型-->
            <div class="File_type">
                <input id="Hidden2" type="hidden" />
                <input id="Hidden3" type="hidden" />
                <input id="HFoldUrl" type="hidden" runat="server" />

                <dl class="F_type" id="F_type">
                </dl>
                <div class="clear"></div>
                <dl class="F_time">
                    <dt>上传时间：</dt>
                    <dd class="selected"><a href="#" onclick="serchTime('',this)">不限</a></dd>
                    <dd><a href="#" onclick="serchTime('一周内',this)">一周内</a></dd>
                    <dd><a href="#" onclick="serchTime('一月内',this)">一月内</a></dd>
                    <dd><a href="#" onclick="serchTime('半年内',this)">半年内</a></dd>
                </dl>

            </div>
            <div class="navgition" style="width: 70%">全部文件</div>
            <div style="width: 25%; float: right; text-align: right;">
                网盘容量（
                <span id="UPloaded" type="text" runat="server"></span>M/<span id="Contenner" type="text" runat="server"></span>M）
            </div>

            <table class="P_form" id="tab">
                <thead>
                    <tr style="font-weight: bold">
                        <!--表头tr名称-->
                        <th class="Check">
                            <input type="checkbox" class="Check_box" id="checkAll" /></th>
                        <th class="name">文件名称 </th>
                        <th class="File_size">文件大小 </th>
                        <th class="Change_date">修改日期 </th>
                    </tr>
                </thead>
                <tbody></tbody>


            </table>
            <div id="pageDiv" class="pageDiv">
            </div>

        </div>
    </div>
</div>


