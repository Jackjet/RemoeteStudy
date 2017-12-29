<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarkingMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.MarkingMgr" %>

<style type="text/css">
    .switchTitle {
        height: 39px;
        width: 801px;
        border: 1px solid silver;
        background-image: url('/_layouts/15/YHSD.VocationalEducation.Portal/images/toolBg.gif');
        background-repeat: repeat-x;
        font-family: "微软雅黑","宋体";
        font-size: 16px;
        text-align: center;
        line-height: 39px;
        margin-left: 5px;
    }

    .main_list_table {
        margin-top: 0px;
    }

    .lightBtn {
        background-color: rgba(72, 182, 0, 1);
        color: white !important;
        cursor: default !important;
    }

    .btnDiv {
        height: 30px;
        width: 70px;
        float: right;
        padding: 0 5px 5px 5px;
        font-size: 14px;
        cursor: pointer;
        color: black;
    }
</style>
<script type="text/javascript">
    var isTab1 = true;
    function StartMarking(obj) {
        var ERID = obj.ID;
        var layerIndex = OL_ShowLayer(2, "阅卷", 860, 660, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Marking.aspx?ERID=" + ERID, function (flag) {
            if (flag)
                ShowData(1);
        });
        layer.full(layerIndex);
    }
    function GetState(str) {
        return str == "" ? "未阅卷" : str;
    }
    function BindDataToTab(bindData) {
        $("#tab1 tbody").empty();
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.PaperName + "</td>\
                        <td class='td_tit_left'>" + this.UserName + "</td>\
                        <td class='td_tit_left'>" + this.ClassName + "</td>\
                        <td class='td_tit'>" + this.CreateTime + "</td>\
                        <td class='td_tit_right'>" + this.Score + "</td>\
                        <td class='td_tit_right'>" + GetState(this.MarkingTime) + "</td>\
                        <td class='td_tit'>\
                            <a href='javascript:;' onclick='StartMarking(" + JSON.stringify(this) + ")'>阅卷</a>\
                        </td>\
                      </tr>";
            $("#tab1 tbody").append(tr);
        });
    }
    function ShowData(index) {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/TestMgrHandler.aspx";
        var postData = { CMD: "GetMakingInfo", PageSize: pageSize, PageIndex: index };
        var paperName = $("#txtPaperName").val().trim();
        var stuName = $("#txtStuName").val().trim();
        var className = $("#txtClassName").val().trim();
        var model = {};
        if (!isTab1) {//如果不是在第一个选项卡,则查询已阅卷的数据
            model.IsMarking = 1;
        }
        if (paperName != "") {
            model.PaperName = paperName;
        }
        if (stuName != "") {
            model.UserName = stuName;
        }
        if (className != "") {
            model.ClassName = className;
        }

        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest(url, postData, function (returnVal) {
            PagerRefresh(index, returnVal.PageCount);
            BindDataToTab($.parseJSON(returnVal.Data));
        })
    }
    function ShowTab1() {
        isTab1 = true;
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);

        $("#listDiv1").addClass("lightBtn");
        $("#listDiv2").removeClass("lightBtn");

        //$("#div1").show();
        //$("#div2").hide();
    }
    function ShowTab2() {
        isTab1 = false;
        LoadPageControl(ShowData, "pageDiv");
        ShowData(1);

        $("#listDiv2").addClass("lightBtn");
        $("#listDiv1").removeClass("lightBtn");

        //$("#div2").show();
        //$("#div1").hide();
    }
    $(function () {
        ShowTab1();
        $("#btnQuery").click(function () {
            ShowData(1);
        })
        $("#txtPaperName,#txtStuName,#txtClassName").keypress(function () {
            EnterEvent("btnQuery");
        })
        $("#listDiv1").click(function () {
            ShowTab1();
        })
        $("#listDiv2").click(function () {
            ShowTab2();
        })
    })
</script>
<div>
    <div class="TopToolbar">
        <span>试卷名称&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtPaperName" />&nbsp&nbsp
        <span>考生名称&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtStuName" />&nbsp&nbsp
        <span>班级名称&nbsp</span><input type="text" class="input_part" style="width: 150px;" id="txtClassName" />
        <input type="button" value="查询" id="btnQuery" class="button_s" />
    </div>
    <div class="switchTitle">
        <span>阅卷</span>
        <div class="btnDiv" id="listDiv2">已阅卷</div>
        <div class="btnDiv lightBtn" id="listDiv1">未阅卷</div>
    </div>
    <div id="div1">
        <div class="main_list_table">
            <table id="tab1" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">试卷名称</th>
                        <th class="th_tit_left">考生名称</th>
                        <th class="th_tit_left">班级名称</th>
                        <th class="th_tit">交卷时间</th>
                        <th class="th_tit_right">得分</th>
                        <th class="th_tit">阅卷时间</th>
                        <th class="th_tit">操作</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div style="text-align: center;">
            <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                <div id="pageDiv" class="pageDiv">
                </div>
            </div>
        </div>
    </div>
   <%-- <div id="div2">
        <div class="main_list_table">
            <table id="tab2" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">试卷名称</th>
                        <th class="th_tit_left">考生名称</th>
                        <th class="th_tit_left">班级名称</th>
                        <th class="th_tit">交卷时间</th>
                        <th class="th_tit_right">得分</th>
                        <th class="th_tit">阅卷时间</th>
                        <th class="th_tit">操作</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div style="text-align: center;">
            <div id="containerdiv2" style="overflow: hidden; display: inline-block;">
                <div id="pageDiv2" class="pageDiv">
                </div>
            </div>
        </div>
    </div>--%>
</div>
