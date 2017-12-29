<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CommonSelect.ClassSelect" %>

<html>
<head>
    <title>选择班级</title>
    <script src="../js/layer/jquery.min.js"></script>
    <script src="../js/layer/layer.js"></script>
    <script src="../js/layer/OpenLayer.js"></script>
    <script src="../js/Pager.js"></script>
    <link href="../css/type.css" rel="stylesheet" />
    <link href="../css/Pager.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#btnSearch").click(function () {
                LayerAlert("搜索");

            })

        })
    </script>
</head>
<body>
    <div>
        <div class="SearchTool">
            <input type="text" class="" id="txtClassName" /><input type="button" value="搜索" id="btnSearch" />
        </div>
        <div class="main">
            <table id="tab" class="workzone-div-tab">
                <thead>
                    <tr>
                        <td>班级名称</td>
                        <td>教师</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div id="pageDiv" class="pageDiv">
        </div>
        <div class="buttom-toolbar">
            <input type="button" value="取消" onclick="javascript: window.close()" />
            <input type="button" value="确定" onclick="    Enter()" />
        </div>
        <div class="userlist" id="userlist">
        </div>
    </div>
</body>
</html>
