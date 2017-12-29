<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassCurriculumList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ClassCurriculumList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        var ClassID = "<%=Request["ClassID"]%>";
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'>\
                                <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                                <td class='td_tit_left' >" + this.Title + "</td>\
                                <td class='td_tit_left'>" + this.ClickNumber + "</td>\
                                <td class='td_tit'>\
                                    <a href=\"javascript:ShowWork('" + this.CurriculumID + "')\">查看</a>\
                                </td>\
                                <td class='td_tit'>\
                                    <a href=\"javascript:ShowRelation('" + this.CurriculumID + "')\">查看</a>\
                                </td>\
                                <td class='td_tit'>\
                                    <a title='移除课程' onclick='DeleteCurriculum(\"" + this.Id + "\")'><img class='DelImg'/></a>\
                                </td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function DeleteCurriculum(id) {
            LayerConfirm("确定从班级移除此课程?", function () {
                var postData = { CMD: "DelCurriculum", DelID: id };
                AjaxRequest("Handler/ClassCurriculumHandler.aspx", postData, function () {
                    layer.closeAll();
                    ShowData(1);
                })
            });
        }
        function ShowRelation(id) {
            location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumRelationList.aspx?CurriculumID=" + id;
        }
        function ShowWork(id) {
            location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/WorkGuanLi.aspx?id=" + id + "&classid=" + ClassID
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
            var model = {};
            if ($("#txtCurriculumName").val().trim() != "") {
                model.Title = $("#txtCurriculumName").val();
            }
            if (ClassID != "") {
                model.ClassId = ClassID;
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("Handler/ClassCurriculumHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        function AddStudent() {
            var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/SelectCurriculum.aspx";
            OL_ShowLayer(2, "选择课程", 720, 600, url, function (organization) {
                if (organization) {
                    var id = organization.Id;//课程ID
                    var postData = { CMD: "AddCurriculum", CID: ClassID, CUID: id };
                    AjaxRequest("Handler/ClassCurriculumHandler.aspx", postData, function (returnVal) {
                        ShowData(1);
                    })
                }
            });
        }

        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);

            $("#txtCurriculumName").keypress(function () {
                EnterEvent("btnQuery");
            })
            $("#btnQuery").click(function () {
                ShowData(1);
            })
        })

    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div>
        <div class="TopToolbar">
            <span>课程名称&nbsp</span><input type="text" id="txtCurriculumName" class="input_part" style="width:150px;"/><input type="button" value="查询" class="button_s" id="btnQuery" />
        </div>
        <div class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">课程名称</th>
                        <th class="th_tit_left">课程类别</th>
                        <th class="th_tit">学生作业</th>
                        <th class="th_tit">相关课程</th>
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
            <input type="button" value="新增" class="button_s" onclick="AddStudent()" /><input type="button" value="返回" onclick="    javascript: window.history.back()"  class="button_n"/>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
班级课程
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
班级课程
</asp:Content>
