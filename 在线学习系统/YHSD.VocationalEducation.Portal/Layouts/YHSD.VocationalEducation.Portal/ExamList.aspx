<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ExamList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        var classId = "<%= Request["ClassID"]%>";
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Title + "</td>\
                            <td class='td_tit_left'>" + this.CreateUser + "</td>\
                            <td class='td_tit_right'>" + this.QuestionCount + "</td>\
                            <td class='td_tit'>" + this.StartDate + "</td>\
                            <td class='td_tit'>" + this.EndDate + "</td>\
                            <td class='td_tit'>\
                                <a title='预览试卷' onclick='PreView(\"" + this.PaperID + "\")'><img class='ViewImg' /></a>\
                                <a title='编辑考试' onclick='EditExam(\"" + this.ID + "\")'><img class='EditImg'/></a>\
                                <a title='取消考试' onclick='DelExam(\"" + this.ID + "\")'><img class='DelImg'/></a>\
                            </td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function EditExam(editId) {
            location.href = "ExamAdd.aspx?EditID=" + editId;
        }
        function DelExam(delId) {
            LayerConfirm("确定取消此考试?", function () {
                AjaxRequest("Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "Exam", DelId: delId }, function (returnVal) {
                    ShowData(1);
                    layer.closeAll();
                })
            });
        }
        function PreView(paperId) {
            var layerIndex = OL_ShowLayer(2, "试卷预览", 860, 660, "PaperPreView.aspx?PaperID=" + paperId, function () {});
            layer.full(layerIndex);
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", TypeName: "Exam", PageSize: pageSize, PageIndex: index };
            var model = { ClassID: classId };
            if ($("#txtPaperName").val().trim() != "") {
                model.ClassName = $("#txtPaperName").val();
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("Handler/CommonHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);

            $("#txtPaperName").keypress(function () {
                EnterEvent("btnQuery");
            })
            $("#btnAdd").click(function () {
                location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ExamAdd.aspx?ClassID=<%= Request["ClassID"]%>";
            })
            $("#btnCancel").click(function () {
                history.back();
            })
        })
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div class="TopToolbar">
        <span>试卷名称&nbsp</span><input type="text" class="input_part" id="txtPaperName" /><input type="button" value="查询" class="button_s" id="btnQuery" />
    </div>
    <div id="main_list_table" class="main_list_table">
        <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
            <thead>
                <tr class="tab_th top_th">
                    <th class="th_tit">序号</th>
                    <th class="th_tit_left">试卷名称</th>
                    <th class="th_tit_left">分配人</th>
                    <th class="th_tit_right">题量</th>
                    <th class="th_tit">开考日期</th>
                    <th class="th_tit">截止日期</th>
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
        <input type="button" class="button_s" value="添加考试" id="btnAdd" />
        <input type="button" class="button_n" value="返回" id="btnCancel" />
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
班级考试
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
班级考试
</asp:Content>
