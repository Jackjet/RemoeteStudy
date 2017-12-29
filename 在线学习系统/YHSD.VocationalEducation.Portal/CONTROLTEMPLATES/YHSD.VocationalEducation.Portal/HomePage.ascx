<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePage.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.HomePage" %>
<style>
    .kaiKeDate {
        font-size: 12px;
        font-family: "微软雅黑","宋体";
        color: #999;
    }
</style>
<script>

    function EditUser(id) {
        OL_ShowLayerNo(2, "编辑个人信息", 770, 400, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/UpdateUser.aspx?id=" + id, function (returnVal) {
            });

        }
</script>
<div class="student_main">
    <div class="f_l">
        <div class="classes_main">
            <div class="classes_title">
                <div class="title">课程列表</div>
            </div>
            <div class="classes_content">
                <asp:Repeater ID="RepeaterCourseList" runat="server">
                    <ItemTemplate>

                        <div class="classes_part_list f_l" onclick="<%#Eval("HomepageTiaoZhuan") %>" style="margin-top: 15px; margin-right: 5px; margin-left: 5px; background: #fbfbfb; border: 1px solid #c7c7c7; cursor: pointer;">
                            <div>
                                <img src="<%#Eval("ImgUrl") %>" title="<%#Eval("Description") %>" onerror='this.src="<%=YHSD.VocationalEducation.Portal.Code.Common.PublicEnum.NoTuPianUrl%>"' width="170" height="108" /></div>
                            <p class="list_title" title="<%#Eval("Title")%>"><span class="text"><%#GetRemoveString(Eval("Title").ToString())%></span></p>
                            <div class="progress" title="<%#Eval("PercentageDescription") %>">
                                <span class="green" style="width: <%#Eval("Percentage") %>%;"><span><%#Eval("Percentage") %>%</span></span>
                            </div>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>
                <div class="clear"></div>

            </div>
        </div>
        <div class="all_classes last_classes">
            <div class="classes_title">
                <div class="title">考试通知</div>
            </div>
            <div class="all_classes_content">
                <div class="classes_part">
                    <label runat="server" id="LabelKaoShiTongZhi" />
                </div>
                <div class="clear"></div>

            </div>
        </div>
    </div>
    <div class="right_part f_r">
        <div class="class_library">
            <div class="library_title">
                <div class="title">个人信息</div>
            </div>
            <label style="text-align: center;" id="GrxxLabel" runat="server" />


        </div>
        <div class="class_library">
            <div class="library_title">
                <div class="title">课程通知</div>
            </div>
            <div class="library_main">
                <label runat="server" id="LabelTongZhi" />
            </div>
        </div>
    </div>
    <div class="clear"></div>
</div>
