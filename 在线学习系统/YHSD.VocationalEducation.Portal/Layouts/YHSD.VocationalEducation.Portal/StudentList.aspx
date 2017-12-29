<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.StudentList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
        <link href="css/Progressbar.css" rel="stylesheet" />
    <script type="text/javascript">
        var ClassID = "<%=Request["ClassID"]%>";
        function BindDataToTab(bindData) {
            $("#tab tbody").empty();
            $(bindData).each(function (k) {
                var tr = "";
                if (this.Percentage == "尚未分配课程") {
                    tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Name + "</td>\
                            <td class='td_tit_left'>" + GetSex(this.Sex) + "</td>\
                            <td class='td_tit_left'>" + this.Telephone + "</td>\
                            <td class='td_tit_left'>" + this.Email + "</td>\
                            <td class='td_tit_left'>" + this.Percentage + "</td>\
                            <td class='td_tit'>\
                                <a title='移除' onclick='DeleteStudent(\"" + this.CUID + "\")'><img class='DelImg'/></a>\
                            </td>\
                          </tr>";
                }
                else {
                    tr = "<tr class='" + GetTrClass(k) + "'>\
                            <td class='td_tit'>" + GetTrNum(k, pageIndex) + "</td>\
                            <td class='td_tit_left'>" + this.Name + "</td>\
                            <td class='td_tit_left'>" + GetSex(this.Sex) + "</td>\
                            <td class='td_tit_left'>" + this.Telephone + "</td>\
                            <td class='td_tit_left'>" + this.Email + "</td>\
                            <td class='td_tit_left'><div class='progress' ><span class='green' style='width:" + this.Percentage + "%;'><span>" + this.Percentage + "%</span></span></div></td>\
                            <td class='td_tit'>\
                                <a title='移除' onclick='DeleteStudent(\"" + this.CUID + "\")'><img class='DelImg'/></a>\
                            </td>\
                          </tr>";
                }
                $("#tab tbody").append(tr);
            });
        }
        function DeleteStudent(id) {
            LayerConfirm("确定从班级移除此学生?", function () {
                AjaxRequest("Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "ClassUser", DelId: id }, function (returnVal) {
                    ShowData(1);
                    layer.closeAll();
                })
            });
        }
        function ShowData(index) {
            var postData = { CMD: "FullTab", PageSize: pageSize, PageIndex: index };
            var model = {};
            if ($("#txtClassName").val().trim() != "") {
                model.Name = $("#txtClassName").val();
            }
            if (ClassID != null && ClassID != "") {
                model.ClassId = ClassID;
            }
            if (PropertyCount(model) > 0) {
                postData.ConditionModel = JSON.stringify(model);
            }
            AjaxRequest("StudentMgrHandler.aspx", postData, function (returnVal) {
                PagerRefresh(index, returnVal.PageCount);
                BindDataToTab($.parseJSON(returnVal.Data));
            })
        }
        function AddStudent() {
            if (!ClassID) {
                LayerAlert("请从班级页面进入此页面");
                return;
            }
            OL_ShowLayer(2, "添加学生", 835, 620, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UserSelect.aspx?notInClassId=" + ClassID, function (returnVal) {
                if (returnVal && returnVal.length > 0) {
                    var insertData = [];
                    for (var i = 0; i < returnVal.length; i++) {
                        insertData.push({ UID: returnVal[i], CID: ClassID });
                    }
                    var postData = { CMD: "InsertUser", ListModel: JSON.stringify(insertData) };
                    AjaxRequest("StudentMgrHandler.aspx", postData, function (returnVal) {
                        if (returnVal.flag) {
                            ShowData(1);
                        }
                    });
                }
            });
        }
        $(function () {
            LoadPageControl(ShowData, "pageDiv");
            ShowData(1);
            $("#txtClassName").keypress(function () {
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
            <span>姓名&nbsp</span><input type="text" id="txtClassName" class="input_part"/><input type="button" value="查询" class="button_s" id="btnQuery" />
        </div>
        <div id="main"  class="main_list_table">
            <table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">姓名</th>
                        <th class="th_tit_left">性别</th>
                        <th class="th_tit_left">电话</th>
                        <th class="th_tit_left">邮箱</th>
                        <th class="th_tit_left">课程学习进度</th>
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
            <input type="button" value="新增" class="button_s" onclick="AddStudent()" /><input type="button" value="返回" onclick="javascript: window.history.back()"  class="button_n"/>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    学员管理
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
