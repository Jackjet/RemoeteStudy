<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ClassEdit" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="css/edit.css" rel="stylesheet" />
    <script type="text/javascript">

        var editId = "<%= Request["EditID"]%>";
        var teacherId;
        var editModel;
        $(function () {
            if (editId){
                var postData = { CMD: "GetModel", TypeName: "ClassInfo", ModelId: editId };
                AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                    editModel = returnVal;
                    teacherId = returnVal.Teacher;
                    $("#txtClassName").val(returnVal.Name);
                    $("#txtTeacher").val(returnVal.TeacherName);
                    $("#txtComment").val(returnVal.Comment);
                });
            }
            $("#txtTeacher").click(function () {
                $("#txtTeacher").blur();
                OL_ShowLayer(2, "选择教师", 850, 660, "TeacherSelect.aspx", function (returnVal) {
                    if (returnVal) {
                        teacherId = returnVal.id;
                        $("#txtTeacher").val(returnVal.name);
                    }
                });
            });
            $("#btnSave").click(function () {
                var className = $.trim($("#txtClassName").val());
                var comment = $("#txtComment").val();
                //添加代码开始
                var fenggeresult;
                var fengge = $("#fengge1").attr("checked");
                if (fengge=="checked"||fengge=="true")
                {
                    fenggeresult = "风格一";
                }
                else
                {
                    fenggeresult = "风格二";
                }
                var starttimeresult;
                var starttimevalue;
                var starttime = $("#time1").attr("checked");
                //开班时间
                starttimeval = $("#starttime").val();

                if (starttime == "checked" || starttime == "true") {
                    starttimevalue = "开班";
                }
                else {
                    starttimevalue = "以后开班";
                }
                //添加代码结束

                if (className=="") {
                    LayerTips("请输入班级名称!", "txtClassName");
                    return;
                }
                if (className.length > 50) {
                    LayerTips("班级名称不能超过50个字符!", "txtClassName");
                    return;
                }
                if (!teacherId) {
                    LayerTips("请选择教师!", "txtTeacher");
                    return;
                }
                if (!editId) {//如果不是编辑模式
                    var model = { Name: className, Teacher: teacherId, Comment: comment };
                    var postData = { CMD: "AddModel", TypeName: "ClassInfo", Model: JSON.stringify(model) };
                    AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                        LayerAlert("班级创建成功!", function () {
                            history.back();
                        });
                    });
                }
                else {
                    editModel.Name = className;
                    editModel.Teacher = teacherId;
                    editModel.Comment = comment;
                    var postData = { CMD: "EditModel", TypeName: "ClassInfo", Model: JSON.stringify(editModel) };
                    AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                        LayerAlert("修改成功!", function () {
                            history.back();
                        });
                    });
                }
            });
        })
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <table class="tableEdit MendEdit" style="width: 100%;margin:16px;">
            <tr>
                <th>班级名称&nbsp;<span style="color:Red">*</span></th>
                <td>
                    <input type="text" id="txtClassName" style="width:400px;" class="inputPart" />
                </td>
            </tr>
            <tr>
                <th>教师&nbsp;<span style="color:Red">*</span></th>
                <td>
                    <input type="text" id="txtTeacher" class="inputPart" style="width:400px;" readonly="readonly"/>
                </td>
            </tr>
            <tr>
                <th>简介</th>
                <td>
                    <textarea rows="6" cols="50" id="txtComment" class="inputPart" style="height: 80px; width: 400px; overflow-x: hidden"></textarea>
                </td>
            </tr>
            <tr>
                <th>门户风格</th>
                <td>
                    
                    <input type="radio" name="fengge" value="fengge1" id="fengge1" checked="checked" />风格一

                    <input style="margin-left:10px;" type="radio" name="fengge" value="fengge2" id="fengge2" />风格二
                </td>
            </tr>
            <tr>
                <th>开班时间</th>
                <td>
                    
                    <input type="radio" name="time1" value="time1" id="time1" checked="checked" />开班时间
                    <input type="text" style="cursor:pointer;" onclick="WdatePicker({ isShowClear: true, readOnly: true, dateFmt: 'yyyy-MM-dd', autoPickDate: true });" id="starttime" />
                   
                    <input style="margin-left:10px;" type="radio" name="time1" value="time2" id="time2" />根据选课人数确认开班时间
                </td>
            </tr>
        </table>
    </div>
    <div class="main_button_part">
        <input type="button" value="保存" class="button_s" id="btnSave"/>
        <input type="button" value="返回" class="button_n" onclick="javascript: history.back();"/>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    班级维护
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
