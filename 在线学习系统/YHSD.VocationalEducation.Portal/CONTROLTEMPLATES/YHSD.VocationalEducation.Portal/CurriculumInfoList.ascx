<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurriculumInfoList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.CurriculumInfoList" %>
<script type="text/javascript">
    function CreateCompany() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoNew.aspx";
        window.location.href = url;
        return false;
    }
    function DeleteKeChen(id) {
        // LayerAlert('不能删除角色!!');
        LayerConfirmDelete("确定删除此课程吗?", function () {
            var postData = { DeleteID: id };
            var loadIndex = layer.load(2);
            $.ajax({
                type: "POST",
                url: "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoNew.aspx",
                data: postData,
                dataType: "text",
                success: function (returnVal) {
                    layer.close(loadIndex);
                    if (returnVal != "" && returnVal != "undefined") {
                        if (returnVal == "ok") {
                            LayerAlert("删除成功!!!", function () {
                                window.location.href = window.location.href;
                            });
                        }
                        else {

                            LayerAlert(returnVal);
                        }
                    }
                },
                error: function (errMsg) {
                    LayerAlert('删除失败！！');
                }
            })
        });
    }
</script>
<script>
    $(function () {
        $('#cc').combotree({
            onSelect: function (node) {
                document.getElementById("<%=this.HidResourceNameID.ClientID %>").value = node.id;
                   }
               })
           });
</script>
<style type="text/css">
    
</style>
<div>
    <asp:HiddenField ID="HidResourceNameID" runat="server" />
    <div id="head" class="TopToolbar">

        <span>课程分类&nbsp</span><select id="cc" class="easyui-combotree" style="width: 168px; height: 29px;"
            data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false"></select>
        &nbsp&nbsp
            <span>课程名称&nbsp</span>
        <asp:TextBox ID="CurriculumName" runat="server" Style="width: 168px; height: 22px; border: 1px solid #dedede;"></asp:TextBox>
        <asp:Button ID="searchButton" CssClass="button_s" runat="server" Text="查询" OnClick="BTSearch_Click" />
        <asp:Button ID="Btn_ExportClass" CssClass="button_s" runat="server" Text="导出静态课程" OnClick="Btn_ExportClass_Click" />

    </div>
    <div class="main_list_table">
        <table class="list_table" cellspacing="0" cellpadding="0" border="0">
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">课程名称</th>
                <th class="th_tit_left">课程分类</th>
                <th class="th_tit_left">是否公开</th>
                <th class="th_tit_left">课程知识点</th>
                <th class="th_tit">操作</th>
            </tr>

            <asp:Repeater ID="RepeaterList" runat="server">
                <ItemTemplate>
                    <tr class="tab_td">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
                        </td>
                        <td class="td_tit_left">
                            <%#Eval("Title") %>
                        </td>
                        <td class="td_tit_left"><%#Eval("ResourceName") %></td>
                        <td class="td_tit_left"><%#Eval("IsOpenCourses") %></td>
                        <td class="td_tit_left"><a href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=<%#Eval("Id")%>" target="_self">知识点管理</a></td>

                        <td class="td_tit"><a title="编辑课程" href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoNew.aspx?id=<%#Eval("Id")%>" target="_self">
                            <img class="EditImg" /></a><a onclick="DeleteKeChen('<%#Eval("Id") %>')" title="删除课程"><img class="DelImg" /></a></td>
                    </tr>

                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tab_td td_bg">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
                        </td>
                        <td class="td_tit_left">
                            <%#Eval("Title") %>
                        </td>
                        <td class="td_tit_left"><%#Eval("ResourceName") %></td>
                        <td class="td_tit_left"><%#Eval("IsOpenCourses") %></td>
                        <td class="td_tit_left"><a href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=<%#Eval("Id")%>" target="_self">知识点管理</a></td>
                        <td class="td_tit"><a title="编辑课程" href="<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoNew.aspx?id=<%#Eval("Id")%>" target="_self">
                            <img class="EditImg" /></a><a onclick="DeleteKeChen('<%#Eval("Id") %>')" title="删除课程"><img class="DelImg" /></a></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>

        </table>
    </div>

    <div style="text-align: center;">
        <div id="containerdiv" style="overflow: hidden; display: inline-block;">
            <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>" runat="server" Width="100%" CurrentPageButtonClass="paginatorPn" AlwaysShow="true" HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCurriculum_PageChanged">
            </webdiyer:AspNetPager>
        </div>
    </div>
    <div class="main_button_part">
        <asp:Button ID="BTAdd" runat="server" Text="添加课程" CssClass="button_s" OnClientClick="return CreateCompany()" />
    </div>
</div>
