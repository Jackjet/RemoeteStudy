<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAdd.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.StudentAdd" %>

<html>
<head>
    <title>添加人员</title>
    <link href="css/Pager.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="js/Pager.js"></script>
    <script type="text/javascript" src="js/json.js"></script>
    <script type="text/javascript" src="js/FormatUtil.js"></script>
    <style type="text/css">
        .nav{
            float:left;
            width:280px;
        }
        .workzone{
            float:left;
            width:430px;
        }
        .workzone-div-tab {
            width: 100%;
            border: 1px solid black;
        }
        .workzone-div-tab tr{
            cursor:pointer;
        }
        .userlist div{
            line-height:22px;
            border:1px dashed silver;
            width:70px;
	        text-align:center;
            cursor:pointer;
	        float:left;
	        margin:5px;
        }
    </style>
    <script type="text/javascript">
        var pIndex = 1;
        var pCount = 0;
        var pSize = 10;
        //当前班级ID -- 不查询次班级下的学生
        var ClassID = window.dialogArguments.classId;
        //查询班级ID -- 树形点击的ID
        var deptID;
        function BindDataToTab(bindData) {
            $(bindData).each(function () {
                var tr = "<tr onclick='sltUser(\"" + this.Name + "\",\"" + this.Id + "\")'><td>" + this.Name + "</td><td>" + GetSex(this.Sex) + "</td><td>" + this.Telephone + "</td><td>" + this.Email + "</td></tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index) {
            var postData = { CMD: "NotInClass", PageSize: pSize, PageIndex: index };
            var model = {Role:"学生"};
            if (ClassID != null && ClassID != "") {
                model.NotInClassId = ClassID;
            }
            if (deptID != null && deptID != "") {
                model.DeptID = deptID;
            }
            if (model != null) {
                postData.ConditionModel = JSON.stringify(model);
            }
            $.ajax({
                type: "Post",
                url: "StudentMgrHandler.aspx",
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    returnVal = $.parseJSON(returnVal);
                    SetPageCount(Math.ceil(returnVal.PageCount / pSize));
                    $("#tab tbody").empty();
                    BindDataToTab($.parseJSON(returnVal.Data));
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        }
        function Enter() {
            var userArr = [];
            $("#userlist div[userId]").each(function () {
                userArr.push($(this).attr("userId"));
            })
            window.returnValue = userArr;
            window.close();
        }
        function sltUser(name, id) {
            if ($("#userlist div[userId=" + id + "]").length > 0)
                return;
            var div = "<div style='display:none' userId='"+id+"'>" + name + "</div>";
            $(div).appendTo("#userlist").fadeIn("slow").click(function () {
                $(this).remove();
            });
        }
        $(function () {
            $("#organizationTree").tree({
                method: 'POST',
                animate: true,
                lines: true,
                url: '/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx',
                onClick: function (node) {
                    deptID = node.id;
                    ShowData(1);
                }
            });

            var postData = { CMD: "NotInClass", PageSize: pSize, PageIndex: pIndex };
            var model = { Role: "学生" };
            if (ClassID != null && ClassID != "") {
                model.NotInClassId = ClassID;
            }
            if (deptID != null && deptID != "") {
                model.DeptID = deptID;
            }
            if (model != null) {
                postData.ConditionModel = JSON.stringify(model);
            }
            $.ajax({
                type: "Post",
                url: "StudentMgrHandler.aspx",
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    returnVal = $.parseJSON(returnVal);
                    BindDataToTab($.parseJSON(returnVal.Data));
                    pCount = returnVal.PageCount;
                    LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize));
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
            $("#btnQuery").click(function () {
                ShowData(1);
            })
        })

    </script>
</head>
<body>
    <div class="nav">
            <ul id="organizationTree" class="easyui-tree"></ul>
    </div>
    <div class="workzone">
        <div class="workzone-div">
            <table id="tab" class="workzone-div-tab">
                <thead>
                    <tr>
                        <td>姓名</td>
                        <td>性别</td>
                        <td>电话</td>
                        <td>邮箱</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div id="pageDiv" class="pageDiv">
        </div>
        <div class="buttom-toolbar">
            <input type="button" value="取消" onclick="javascript:window.close()" /> <input type="button" value="确定" onclick="Enter()"/>
        </div>
        <div class="userlist" id="userlist">

        </div>
    </div>
</body>
</html>
