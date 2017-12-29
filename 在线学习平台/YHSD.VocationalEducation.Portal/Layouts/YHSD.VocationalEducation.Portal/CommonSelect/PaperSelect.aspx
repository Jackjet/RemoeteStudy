<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaperSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CommonSelect.PaperSelect" %>

<html>
<head>
    <title>选择试卷</title>
    <script src="../js/layer/jquery.min.js"></script>
    <script src="../js/layer/layer.js"></script>
    <script src="../js/layer/OpenLayer.js"></script>
    <script src="../js/Pager.js"></script>
    <script src="../js/FormatUtil.js"></script>
    <script src="../js/json.js"></script>
    <link href="../css/type.css" rel="stylesheet" />
    <link href="../css/Pager.css" rel="stylesheet" />
    <style type="text/css">
        .TopToolbars {
            margin-left: 16px;
            margin-top: 10px;
        }
        body{
            font-size:12px;
            font-family:"微软雅黑","宋体";
            overflow:hidden;
        }
    </style>
    <script type="text/javascript">
        var classId = "<%= Request["ClassID"]%>";
        function Cancel() {
            OL_CloseLayerIframe(objJson);
        }

        function SltTr(objJson) {
            OL_CloseLayerIframe(objJson);
        }
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Title + "</td>\
                            <td class='td_tit_left'>" + this.CreateUser + "</td>\
                            <td class='td_tit'>" + this.CreateTime + "</td>\
                            <td class='td_tit_left'>" + this.QuestionCount + "</td>\
                            <td class='td_tit'>\
                                <a href='javascript:;' onclick='SltTr(" + JSON.stringify(this) + ")'>选择</a>\
                            </td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index,Query) {
            var postData = { CMD: "FullTab", TypeName: "Papers", PageSize: pageSize, PageIndex: index };
            var paperName = $("#txtPaperName").val();
            var createUser = $("#txtCreateUser").val();
            var model = {};
            if (paperName != "")
                model.Title = paperName;
            if (createUser != "")
                model.CreateUser = createUser;
            model.SelectQuery = Query;
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("../Handler/CommonHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1, "*");

            $("#txtPaperName,#txtCreateUser").keypress(function () {
                EnterEvent("btnQuery");
            })

            $("#btnQuery").click(function () {
                ShowData(1,"");
            })
        })
    </script>
</head>
<body>
        <div id="topToolbar" class="TopToolbars">
            <span>试卷名称</span><input type="text" class="input_part" id="txtPaperName" style="height:30px !important"/>&nbsp
            <span>组卷人</span><input type="text" class="input_part" id="txtCreateUser" style="height:30px !important"/>
            <input type="button" value="查询" class="button_s" id="btnQuery" />
        </div>
        <div class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">试卷名称</th>
                        <th class="th_tit_left">组卷人</th>
                        <th class="th_tit">组卷日期</th>
                        <th class="th_tit_left">题量</th>
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
            <input type="button" value="取消" onclick="SltTr()" class="button_s" />
        </div>
</body>
</html>
