<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassInfoSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ClassInfoSelect" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>老师选择</title>
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/css/Pager.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/uploadify/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Pager.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/json.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/FormatUtil.js"></script>
    <style type="text/css">
        .main-tab {
            width: 100%;
            border: 1px solid black;
        }
    </style>
    <script type="text/javascript">
        function Slt(Id, Name) {
            if (Id != null && Name != null) {
                var obj = { id: Id, name: Name };
                window.returnValue = obj;
            }
            window.close();
        }
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function () {
                var tr = "<tr>\
                            <td class='td_tit_left'>" + this.Name + "</td>\
                            <td class='td_tit'>" + GetSex(this.Sex) + "</td>\
                            <td class='td_tit_right'>" + this.Telephone + "</td>\
                            <td class='td_tit_left'>" + this.Email + "</td>\
                            <td class='td_tit'><input type='button' onclick=Slt('" + this.Id + "','" + this.Name + "') value='选择'/></td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
            if ($("#txtClassName").val().trim() != "") {
                var model = { Name: $("#txtClassName").val() };
                postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index, ConditionModel: JSON.stringify(model) };
            }
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/StudentMgrHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);
            $("#btnQuery").click(function () {
                ShowData(1);
            })
        })

    </script>

</head>
<body>
    <form id="Form" runat="server">
        <div id="head">
            <span>姓名</span><input type="text" id="txtClassName" /><input type="button" value="查询" id="btnQuery" />
        </div>
        <div id="main">
            <table id="tab" class="main-tab">
                <thead>
                    <tr>
                        <td>姓名</td>
                        <td>性别</td>
                        <td>电话</td>
                        <td>邮箱</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div id="pageDiv" class="pageDiv">
        </div>
        <div>
            <input type="button" value="取消" onclick="Slt(null,null)" />
        </div>
    </form>
</body>
</html>
