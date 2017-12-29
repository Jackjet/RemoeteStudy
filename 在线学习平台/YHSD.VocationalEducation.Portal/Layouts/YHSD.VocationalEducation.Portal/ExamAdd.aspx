<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamAdd.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ExamAdd" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script src="js/DatePicker/WdatePicker.js"></script>
    <link href="css/edit.css" rel="stylesheet" />
    <style type="text/css">

    </style>
    <script type="text/javascript">
        var classId = "<%= Request["ClassID"]%>";
        var editId = "<%= Request["EditID"]%>";
        var paperId;
        $(function () {
            if (!editId) {//如果不是编辑模式
                var postData = { CMD: "GetModel", TypeName: "ClassInfo", ModelId: classId };
                AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                    $("#txtClassName").val(returnVal.Name);
                    $("#txtTeacherName").val(returnVal.TeacherName);
                });
            }
            else {
                var postData = { CMD: "GetModel", TypeName: "Exam", ModelId: editId };
                AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                    paperId = returnVal.PaperID;
                    classId = returnVal.ClassID;
                    $("#txtClassName").val(returnVal.ClassName);
                    $("#txtTeacherName").val(returnVal.Teacher);
                    $("#txtPaperTitle").val(returnVal.Title);
                    $("#txtQuestionCount").val(returnVal.QuestionCount);
                    $("#txtTotalScore").val(returnVal.TotalScore);
                    $("#txtPassScore").val(returnVal.PassScore);
                    $("#txtStartDate").val(returnVal.StartDate);
                    $("#txtEndDate").val(returnVal.EndDate);
                });
            }
            $("#txtPaperTitle").click(function () {
                $("#txtPaperTitle").blur();
                OL_ShowLayer(2, "选择试卷", 850, 660, "CommonSelect/PaperSelect.aspx", function (returnVal) {
                    if (returnVal) {
                        paperId = returnVal.ID;
                        $("#txtPaperTitle").val(returnVal.Title);
                        $("#txtQuestionCount").val(returnVal.QuestionCount);
                        $("#txtTotalScore").val(returnVal.TotalScore);
                        $("#txtPassScore").val(returnVal.PassScore);
                    }
                });
            });

            $("#btnSave").click(function () {
                var flag = true;
                var startDate = $("#txtStartDate").val();
                var endDate = $("#txtEndDate").val();
                if (!classId) {
                    LayerTips("请选择班级!", "txtClassName");
                    return;
                }
                if (!paperId) {
                    LayerTips("请选择试卷!", "txtPaperTitle");
                    return;
                }
                if (startDate == "") {
                    LayerTips("请选择开考日期!", "txtStartDate");
                    return;
                }
                if (endDate == "") {
                    LayerTips("请选择截至日期!", "txtEndDate");
                    return;
                }
                if (!editId) {//如果不是编辑模式
                    var model = { ClassID: classId, PaperID: paperId, StartDate: startDate, EndDate: endDate };
                    var postData = { CMD: "AddModel", TypeName: "Exam", Model: JSON.stringify(model) };
                    AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                        LayerAlert("添加成功！从" + startDate + "起即可开始考试!", function () {
                            history.back();
                        });
                    });
                }
                else {
                    var model = { ID: editId, ClassID: classId, PaperID: paperId, StartDate: startDate, EndDate: endDate };
                    var postData = { CMD: "EditModel", TypeName: "Exam", Model: JSON.stringify(model) };
                    AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                        LayerAlert("修改成功！", function () {
                            history.back();
                        });
                    });
                }
            });

            $("#btnCancel").click(function () {
                history.back();
            });
        })
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <table class="tableEdit MendEdit" style="width: 100%;margin:16px;">
            <tr>
                <th>班级名称&nbsp;<span style="color:Red">*</span></th>
                <td style="width:250px;">
                    <input type="text" id="txtClassName" class="input_part" readonly="readonly" /></td>
                <th>教师</th>
                <td>
                    <input type="text" id="txtTeacherName" class="input_part" readonly="readonly" /></td>
            </tr>
            <tr>
                <th>试卷名称&nbsp;<span style="color:Red">*</span></th>
                <td style="width:250px;">
                    <input type="text" id="txtPaperTitle" class="input_part BlueFont" style="cursor:pointer;color:blue !important;" value="点击选择试卷" readonly="readonly"/><input type="button" value="选择" class="button_s" id="sltPaper" style="display:none;"/></td>
                <th>题量</th>
                <td>
                    <input type="text" id="txtQuestionCount" class="input_part" readonly="readonly" /></td>
            </tr>
            <tr>
                <th>总分</th>
                <td style="width:250px;">
                    <input type="text" id="txtTotalScore" class="input_part" readonly="readonly" /></td>
                <th>合格分</th>
                <td>
                    <input type="text" id="txtPassScore" class="input_part" readonly="readonly" /></td>
            </tr>
            <tr>
                <th>开考时间&nbsp;<span style="color:Red">*</span></th>
                <td style="width:250px;">
                    <input type="text" id="txtStartDate" style="cursor:pointer;" onclick="WdatePicker({ isShowClear: true, readOnly: true, dateFmt: 'yyyy-MM-dd HH:mm:ss', autoPickDate: true });" class="input_part" /></td>
                <th>截止时间&nbsp;<span style="color:Red">*</span></th>
                <td>
                    <input type="text" id="txtEndDate" style="cursor:pointer;" onclick="WdatePicker({ isShowClear: true, readOnly: true, dateFmt: 'yyyy-MM-dd HH:mm:ss', autoPickDate: true });" class="input_part" /></td>
            </tr>
        </table>
    </div>
    <div class="main_button_part">
        <input type="button" value="保存" class="button_s" id="btnSave"/>
        <input type="button" value="返回" class="button_n" id="btnCancel"/>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    添加考试
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    添加考试
</asp:Content>
