<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurriculumRelationList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CurriculumRelationList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        var CurriculumID = "<%=Request["CurriculumID"]%>";
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Title + "</td>\
                            <td class='td_tit_left'>" + this.ResourceID + "</td>\
                          </tr>";
                $("#tab tbody").append(tr);
            });
        }
        function ShowData(index) {
            var postData = { CMD: "RelationCurriculum", PageSize: pageSize, PageIndex: index };
            var model = {};
            if (CurriculumID != null && CurriculumID != "") {
                model.Id = CurriculumID;
            } else {
                LayerAlert("数据异常!", function () {
                    history.back();
                    return;
                });
            }
            if ($("#txtCurriculumName").val().trim() != "") {
                model.Name = $("#txtCurriculumName").val();
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("Handler/ClassCurriculumHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
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
            <span>课程名称&nbsp</span><input type="text" id="txtCurriculumName" class="input_part" style="width: 150px;" /><input type="button" value="查询" class="button_s" id="btnQuery" />
        </div>
        <div class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">课程名称</th>
                        <th class="th_tit_left">课程类别</th>
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
            <input type="button" value="返回" onclick="javascript: window.history.back()" class="button_n" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    关联课程
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    关联课程
</asp:Content>
