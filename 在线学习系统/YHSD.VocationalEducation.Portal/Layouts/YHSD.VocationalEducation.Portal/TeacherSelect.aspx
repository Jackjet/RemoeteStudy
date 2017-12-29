<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.TeacherSelect" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>老师选择</title>
    <link href="css/type.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/css/Pager.css" rel="stylesheet" />
    <script type="text/javascript" src="js/layer/jquery.min.js"></script>
    <script type="text/javascript" src="js/layer/layer.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Pager.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/json.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/FormatUtil.js"></script>
    <script type="text/javascript" src="js/layer/OpenLayer.js"></script>
    <style type="text/css">
        body{
            font-size:12px;
            font-family:"微软雅黑","宋体";
            overflow:hidden;
        }
    </style>
    <script type="text/javascript">
        function Slt(Id, Name) {
            if (Id != null && Name != null) {
                var obj = { id: Id, name: Name };
                OL_CloseLayerIframe(obj);
                return;
            }
            OL_CloseLayerIframe();
        }
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Name + "</td>\
                            <td class='td_tit'>" + GetSex(this.Sex) + "</td>\
                            <td class='td_tit_left'>" + this.Telephone + "</td>\
                            <td class='td_tit_left'>" + this.Email + "</td>\
                            <td class='td_tit'>\
                                <a href='javascript:;' onclick=Slt('" + this.Id + "','" + this.Name + "')>选择</a>\
                            </td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
            var model = { Role: PositionTeacher };
            if ($("#txtTeacherName").val().trim() != "") {
                model.Name = $("#txtTeacherName").val();
            }

            postData.ConditionModel = JSON.stringify(model);

            AjaxRequest("StudentMgrHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            });
        }
        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);

            $("#txtTeacherName").keypress(function () {
                EnterEvent("btnQuery");
            })

            $("#btnQuery").click(function () {
                ShowData(1);
            })
        })

    </script>

</head>
<body>
        <div class="TopToolbar">
            <span>姓名&nbsp</span><input type="text" class="input_part" id="txtTeacherName" style="height:30px !important" /><input type="button" class="button_s" value="查询" id="btnQuery" />
        </div>
        <div id="main" class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">姓名</th>
                        <th class="th_tit">性别</th>
                        <th class="th_tit_left">电话</th>
                        <th class="th_tit_left">邮箱</th>
                        <th class="th_tit">操作</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div style="text-align:center;"> 
            <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                <div id="pageDiv" class="pageDiv">
                </div>
            </div>
        </div>
        <div class="main_button_part">
            <input type="button" class="button_n" value="取消" onclick="Slt(null, null)" />
        </div>
</body>
</html>
