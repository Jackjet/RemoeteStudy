<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.UserSelect" %>

<html>
<head>
    <title>添加人员</title>
    <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <link href="css/Pager.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
    <script type="text/javascript" src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <script type="text/javascript" src="js/Pager.js"></script>
    <script type="text/javascript" src="js/json.js"></script>
    <script type="text/javascript" src="js/FormatUtil.js"></script>
    <style type="text/css">
        body{
            font-size:12px;
            font-family:"微软雅黑","宋体";
        }
        .nav {
            width: 240px;
            height: 452px;
            border: 1px solid #dedede;
            margin:16px 0 16px 0;
        }

        .list_table tbody tr {
            cursor: pointer;
        }

        .userlist div {
            line-height: 22px;
            border: 1px dashed silver;
            width: 70px;
            text-align: center;
            cursor: pointer;
            margin: 16px;
            float:left;
        }

        .main_list_table {
            width: 570px;
            margin:5px;
            border: 1px solid #dedede;
            height:416px;
        }
        
            .main_list_table .list_table {
                width: 570px;
            }
            .main_list_table .list_table .tab_td{
                line-height:32px;
                height:38px;
            }

        .main {
            width: 820px;
            height: 430px;
        }

        .left {
            float: left;
            width: 240px;
        }
        .right{
            float: left;
            width: 570px;
            min-height: 440px;
        }
        .tools{
            margin-left:5px;
            margin-top:16px;
        }

    </style>
    <script type="text/javascript">
        var CMDStr = "UserSelect";

        //当前班级ID -- 不查询此班级下的学生
        var notInClassId = "<%=Request["notInClassId"]%>";
        if (notInClassId != "")
            CMDStr = "NotInClass";
        var type = "<%=Request["type"]%>";
        if (type == "role") {
            CMDStr = "NotInRole";
        }
        //查询班级ID -- 树形点击的ID
        var deptID;

        var role = "<%=Request["Role"]%>";

        var questionArr = new Array();
        function Check(ckbox, flag) {
            var qModel = JSON.parse($(ckbox).attr("model"));
            if (flag == undefined)//如果未传值，则为true
                flag = $(ckbox).prop("checked");
            if (flag) {
                questionArr.push(qModel);
            }
            else {
                questionArr = ArrayRemove(questionArr, "Id", qModel.Id);
            }
            $(ckbox).prop("checked", flag);
            stopBubble(window.event);
        }
        function GetChecked(dbid) {
            return ArrayFind(questionArr, "Id", dbid) != undefined ? "checked='checked'" : "";
        }
        function stopBubble(e) {
            if (e && e.stopPropagation)
                e.stopPropagation();
            else
                window.event.cancelBubble = true;
        }
        function TrClick(tr) {
            var chk = $(tr).find(":checkbox[class=cb]");
            var flag = chk.prop("checked");
            Check(chk, !flag);
        }

        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {// onclick='sltUser(\"" + this.Name + "\",\"" + this.Id + "\")'
                var tr = "<tr class='" + GetTrClass(k) + "' onclick='TrClick(this)'>\
                            <td class='td_tit'>\
                                <input type='checkbox' onclick='Check(this)' " + GetChecked(this.Id) + " class='cb' name='ck' model='" + JSON.stringify(this) + "'/>\
                            </td>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Name + "</td>\
                            <td class='td_tit_left'>" + GetSex(this.Sex) + "</td>\
                            <td class='td_tit_left'>" + this.Telephone + "</td>\
                            <td class='td_tit_left'>" + this.Email + "</td>\
                            <td class='td_tit_left'>" + this.CardID + "</td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index) {
            var postData = { CMD: CMDStr, PageSize: pageSize, PageIndex: index };
            var model = {};
            var name = $("#txtName").val();
            var cardID = $("#txtCardID").val();
            if (notInClassId != null && notInClassId != "") {
                model.NotInClassId = notInClassId;
            }
            if (deptID != null && deptID != "") {
                model.DeptID = deptID;
            }
            if (role != "") {
                model.Role = role;
            }
            if (name != "") {
                model.Name = name;
            }
            if (cardID != "") {
                model.CardID = cardID;
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("StudentMgrHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        function Enter() {
            //var userArr = [];
            //$("#userlist div[userId]").each(function () {
            //    userArr.push($(this).attr("userId"));
            //})
            //window.returnValue = userArr;
            //window.close();
            var userArr = [];
            for (var i = 0; i < questionArr.length; i++) {
                userArr.push(questionArr[i].Id);
            }
            OL_CloseLayerIframe(userArr);
        }
        function Cancel() {
            OL_CloseLayerIframe();
        }
        function sltUser(name, id) {
            if ($("#userlist div[userId=" + id + "]").length > 0)
                return;
            var div = "<div style='display:none' userId='" + id + "'>" + name + "</div>";
            $(div).appendTo("#userlist").fadeIn("slow").click(function () {
                $(this).remove();
            });
        }
        $(function () {
            $("#organizationTree").tree({
                method: 'POST',
                animate: true,
                lines: true,
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx',
                onClick: function (node) {
                    if (deptID == node.id) {
                        deptID = "";
                        $(node.target).removeClass("tree-node-selected");
                    }
                    else {
                        deptID = node.id;
                    }
                    ShowData(1);
                }
            });
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);
            $("#txtName,#txtCardID").keypress(function () {
                EnterEvent("btnQuery");
            })

            $("#btnQuery").click(function () {
                ShowData(1);
            })
            $("#checkAll").click(function () {
                $("#tab :checkbox[class=cb]").each(function (k) {
                    Check(this, $("#checkAll").prop("checked"));
                });
            });
        })

    </script>
</head>
<body>
    <div class="main">
        <div class="left">
            <div class="nav">
                <ul id="organizationTree" class="easyui-tree"></ul>
            </div>
        </div>
        <div class="right">
                <div class="tools">
                    <span>姓名&nbsp</span><input type="text" id="txtName" class="input_part" style="height: 30px !important" />&nbsp&nbsp
                    <span>身份证&nbsp</span><input type="text" id="txtCardID" class="input_part" style="height: 30px !important" />
                    <input type="button" value="查询" id="btnQuery" class="button_s" />
                </div>
            <div class="main_list_table">
                <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr class="tab_th top_th">
                            <th class="th_tit">
                                <input type='checkbox' id="checkAll" />
                            </th>
                            <th class="th_tit">序号</th>
                            <th class="th_tit_left">姓名</th>
                            <th class="th_tit_left">性别</th>
                            <th class="th_tit_left">电话</th>
                            <th class="th_tit_left">邮箱</th>
                            <th class="th_tit_left">身份证</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="userlist" id="userlist" style="display:none">
            </div>
        </div>
            <div style="text-align:center;float:left;width:100%;height:30px;"> 
                <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                    <div id="pageDiv" class="pageDiv">
                    </div>
                </div>
            </div>
    <div style="text-align: center;float:left;width:100%;margin-top:10px;" class="bottom_toolbar">
        <input type="button" value="确定" class="button_s" onclick="Enter()" />
        <input type="button" value="取消" class="button_n" onclick="Cancel()" />
    </div>
    </div>
</body>
</html>
