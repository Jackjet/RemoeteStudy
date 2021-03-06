﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PND_wp_ShrarUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.WP.PND_wp_Shrar.PND_wp_ShrarUserControl" %>
<link href="../../../../_layouts/15/zTree_v3-master/css/demo.css" rel="stylesheet" />
<link href="../../../../_layouts/15/zTree_v3-master/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
<script src="../../../../_layouts/15/zTree_v3-master/js/jquery-1.4.4.min.js"></script>
<script src="../../../../_layouts/15/zTree_v3-master/js/jquery.ztree.core-3.5.js"></script>

<script type="text/javascript">
    var setting = {
        data: {
            key: {
                title: "t"
            },
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeClick: beforeClick,
            onClick: onClick
        }
    };

    var log, className = "dark";
    function beforeClick(treeId, treeNode, clickFlag) {
        className = (className === "dark" ? "" : "dark");
        showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
        return (treeNode.click != false);
    }
    function onClick(event, treeId, treeNode, clickFlag) {
        if (treeNode.type == "专业") {
            alert("必须选择具体的学科或目录");
        }
        else {
            $("#Htype").val(treeNode.type);
            $("#Hpid").val(treeNode.SubjectID);
            $("#Hid").val(treeId);
        }
        //showLog("[ " + getTime() + " onClick ]&nbsp;&nbsp;clickFlag = " + clickFlag + " (" + (clickFlag === 1 ? "普通选中" : (clickFlag === 0 ? "<b>取消选中</b>" : "<b>追加选中</b>")) + ")");
    }
    function showLog(str) {
        if (!log) log = $("#log");
        log.append("<li class='" + className + "'>" + str + "</li>");
        if (log.children("li").length > 8) {
            log.get(0).removeChild(log.children("li")[0]);
        }
    }
    function getTime() {
        var now = new Date(),
        h = now.getHours(),
        m = now.getMinutes(),
        s = now.getSeconds();
        return (h + ":" + m + ":" + s);
    }

    $(document).ready(function () {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
        var GradID = $("[id$='GradID']").val();

        $.ajax({
            type: "get",
            url: FirstUrl + "/_layouts/15/hander/LibraryList.aspx",
            data: { GradID: GradID },

            dataType: "text",
            success: function (returnVal) {
                $.fn.zTree.init($("#treeDemo"), setting, $.parseJSON(returnVal));
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    });
    function treeClick() {
        this.close();
        var type = $("#Htype").val();
        var SubjectID = $("#Hpid").val();
        var id = $("#Hid").val();

        parent.Share(type, SubjectID, id);
    }
    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串

        var theRequest = new Object();

        if (url.indexOf("?") != -1) {

            var str = url.substr(1);

            strs = str.split("&");

            for (var i = 0; i < strs.length; i++) {

                theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);

            }

        }

        return theRequest;

    }
</script>

<div class="content_wrap">
    <input id="Htype" type="hidden" />
    <input id="Hpid" type="hidden" />
    <input id="Hid" type="hidden" />
    <input id="GradID" type="hidden" value="" runat="server" />

    <div class="zTreeDemoBackground left">
        <ul id="treeDemo" class="ztree"></ul>
        <input type="button" value="确定" onclick='treeClick()' />
    </div>

</div>
