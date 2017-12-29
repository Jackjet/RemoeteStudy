<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassMgr.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.ClassMgr" %>
<script type="text/javascript">
    function ShowStudent(id) {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/StudentList.aspx?ClassID=" + id;
    }
    function ShowCurriculum(id) {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ClassCurriculumList.aspx?ClassID=" + id;
    }
    function ShowPapers(id) {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ExamList.aspx?ClassID=" + id;
    }
    function BindDataToTab(bindData) {
        $("#tab tbody").empty();
        var trag=<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.IsClassTutor()%>;
        if(trag)
        {
            $("#tab thead").empty();
            var theadtr=" <tr class='tab_th top_th'>\
                    <th class='th_tit'>序号</th>\
                    <th class='th_tit_left'>班级名称</th>\
                    <th class='th_tit_left'>教师</th>\
                    <th class='th_tit'>操作</th>\
                </tr>"
            $("#tab thead").append(theadtr);
                $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.Name + "</td>\
                        <td class='td_tit_left'>" + this.TeacherName + "</td>\
                        <td class='td_tit'>\
                            <a title='编辑' onclick='EditClass(\"" + this.ID + "\")'><img class='EditImg'/></a>\
                            <a title='删除' onclick='DelClass(\"" + this.ID + "\")'><img class='DelImg'/></a>\
                        </td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
        }
         else
        {
        $(bindData).each(function (k) {
            var tr = "<tr class='" + GetTrClass(k) + "'>\
                        <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                        <td class='td_tit_left'>" + this.Name + "</td>\
                        <td class='td_tit_left'>" + this.TeacherName + "</td>\
                        <td class='td_tit'>\
                            <a href=\"javascript:ShowCurriculum('" + this.ID + "')\">查看</a>\
                        </td>\
                        <td class='td_tit'>\
                            <a href=\"javascript:ShowStudent('" + this.ID + "')\">查看</a>\
                        </td>\
                        <td class='td_tit'>\
                            <a href=\"javascript:ShowPapers('" + this.ID + "')\">查看</a>\
                        </td>\
                      </tr>";
            $("#tab tbody").append(tr);
        });
        //$(".main_button_part").hide();
        }
    }
    function EditClass(editId) {
        location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ClassEdit.aspx?EditID=" + editId;
    }
    function DelClass(delId) {
        LayerConfirm("确定删除此班级?", function () {
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "ClassInfo", DelId: delId }, function (returnVal) {
                ShowData(1);
                layer.closeAll();
            })
        });
    }
    function ShowData(index) {
        var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
        var model = {};
        if ($("#txtClassName").val() != "") {
            model.Name = $("#txtClassName").val();
        }
        if (PropertyCount(model) > 0) {
            postData.ConditionModel = JSON.stringify(model);
        }
        AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ClassMgrHandler.aspx", postData, function (returnVal) {
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
        $("#txtClassName").keypress(function () {
            EnterEvent("btnQuery");
        })
        $("#btnAdd").click(function () {
            location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ClassEdit.aspx";
        });
    });
</script>
<div>
    <div class="TopToolbar">
        <span>班级名称&nbsp</span><input type="text" class="input_part" id="txtClassName" style="width: 150px;" /><input type="button" value="查询" class="button_s" id="btnQuery" />
    </div>
    <div id="main_list_table" class="main_list_table">
        <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
            <thead>
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">班级名称</th>
                    <th class="th_tit_left">教师</th>
                    <th class="th_tit">课程</th>
                    <th class="th_tit">班级人员</th>
                    <th class="th_tit">考试</th>
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
    <div class="main_button_part">
        <input type="button" class="button_s" value="添加" id="btnAdd" />
    </div>
</div>
