<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CertificateInfoList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.CertificateInfoList" %>
<script type="text/javascript">
    $(function () {
        $('#cc').combotree({
            onSelect: function (node) {
                document.getElementById("<%=this.HidResourceNameID.ClientID %>").value = node.id;
            }
        })
    });
    function CreateCertificate(text) {
        if (text == "add") {
            OL_ShowLayerNo(2, "添加证书", 400, 320, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CertificateInfoAdd.aspx", function (returnVal) {
            });
        } else {
            OL_ShowLayerNo(2, "编辑证书", 400, 320, "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CertificateInfoAdd.aspx?itemid=" + text, function (returnVal) {
            });
        }       
    }
    function DeleteCertificate(delId) {
        LayerConfirm("您确定要删除吗?", function () {
            AjaxRequest("<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/Handler/CommonHandler.aspx", { CMD: "DelModel", TypeName: "CertificateInfo", DelId: delId }, function (returnVal) {
                location.href = this.location.href;
                layer.closeAll();
            })
        });
    }
</script>
<div>
    <asp:HiddenField ID="HidResourceNameID" runat="server" />
    <div id="head" class="TopToolbar">
        <span>课程分类&nbsp</span><select id="cc" class="easyui-combotree" style="width: 200px; height: 29px;"
            data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false"></select>
        &nbsp&nbsp
        <asp:Button ID="searchButton" CssClass="button_s" runat="server" Text="查询" OnClick="BTSearch_Click" />
    </div>
    <div class="main_list_table">
        <table class="list_table" cellspacing="0" cellpadding="0" border="0">
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">结业证书号</th>
                <th class="th_tit_left">学员姓名</th>
                <th class="th_tit_left">课程名称</th>
                <th class="th_tit_left">结业时间</th>
                <th class="th_tit_left">发证单位</th>
                <th class="th_tit_left">证书查询网址</th>
                <th class="th_tit">操作</th>     
            </tr>

            <asp:Repeater ID="RepeaterList" runat="server">
                <ItemTemplate>
                    <tr class="tab_td">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageCertificate.CurrentPageIndex -1)*this.AspNetPageCertificate.PageSize %>
                        </td>
                        <td class="td_tit_left"><%#Eval("GraduationNo") %></td>
                        <td class="td_tit_left"><%#Eval("StuName") %></td>
                        <td class="td_tit_left"><%#Eval("CurriculumName") %></td>
                        <td class="td_tit_left"><%#Eval("GraduationDate") %></td>
                        <td class="td_tit_left"><%#Eval("AwardUnit") %></td>
                        <td class="td_tit_left"><a href="<%#Eval("QueryURL") %>" target="_blank"><%#Eval("QueryURL") %></a></td>
                        <td class="td_tit"><a title="编辑" onclick="CreateCertificate('<%#Eval("Id") %>')"><img class="EditImg" /></a>
                              <a  onclick="DeleteCertificate('<%#Eval("Id") %>')" title="删除"><img class="DelImg" /></a></td>   
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tab_td td_bg">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageCertificate.CurrentPageIndex -1)*this.AspNetPageCertificate.PageSize %>
                        </td>
                        <td class="td_tit_left"><%#Eval("GraduationNo") %></td>
                        <td class="td_tit_left"><%#Eval("StuName") %></td>
                        <td class="td_tit_left"><%#Eval("CurriculumName") %></td>
                        <td class="td_tit_left"><%#Eval("GraduationDate") %></td>
                        <td class="td_tit_left"><%#Eval("AwardUnit") %></td>
                        <td class="td_tit_left"><a href="<%#Eval("QueryURL") %>" target="_blank"><%#Eval("QueryURL") %></a></td>
                        <td class="td_tit"><a title="编辑" onclick="CreateCertificate('<%#Eval("Id") %>')"><img class="EditImg" /></a>
                              <a  onclick="DeleteCertificate('<%#Eval("Id") %>')" title="删除"><img class="DelImg" /></a></td>   
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>

        </table>
    </div>

    <div style="text-align: center;">
        <div id="containerdiv" style="overflow: hidden; display: inline-block;">
            <webdiyer:AspNetPager ID="AspNetPageCertificate" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>" runat="server" Width="100%" CurrentPageButtonClass="paginatorPn" AlwaysShow="true" HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCertificate_PageChanged">
            </webdiyer:AspNetPager>
        </div>
    </div>
    <div class="main_button_part">
         <input type="button" value="添加证书" class="button_s" onclick="CreateCertificate('add')" />        
    </div>
</div>
