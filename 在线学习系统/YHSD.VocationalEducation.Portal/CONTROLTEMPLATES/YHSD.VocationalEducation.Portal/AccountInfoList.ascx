<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountInfoList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.AccountInfoList" %>
<script src="/_layouts/15/YHSD.VocationalEducation.Portal/js/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    $(function () {
        $('#cc').combotree({
            onSelect: function (node) {
                document.getElementById("<%=this.HidResourceNameID.ClientID %>").value = node.id;
                   }
               })
           });
</script>
<div>
    <asp:HiddenField ID="HidResourceNameID" runat="server" />
    <div id="head" class="TopToolbar">

        <span>课程分类&nbsp</span><select id="cc" class="easyui-combotree" style="width: 200px; height: 29px;"
            data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false"></select>
        &nbsp&nbsp
            <span>日期&nbsp</span>        
        <input type="text" class="inputpart" readonly="readonly" runat="server" id="TB_StartDate" name="TB_StartDate" onclick="WdatePicker()" style="width: 167px; height: 22px; border: 1px solid #dedede;" /> 至 <input type="text" class="inputpart" readonly="readonly" runat="server" id="TB_EndDate" name="TB_EndDate" onclick="WdatePicker()" style="width: 168px; height: 22px; border: 1px solid #dedede;" />
        <asp:Button ID="searchButton" CssClass="button_s" runat="server" Text="查询" OnClick="BTSearch_Click" />
    </div>
    <div class="main_list_table">
        <table class="list_table" cellspacing="0" cellpadding="0" border="0">
            <tr class="tab_th top_th">
                <th class="th_tit">序号</th>
                <th class="th_tit_left">课程名称</th>
                <th class="th_tit_left">课程分类</th>
                <th class="th_tit_left">金额</th>
                <th class="th_tit_left">备注</th>  
                <th class="th_tit_left">日期</th>                 
            </tr>

            <asp:Repeater ID="RepeaterList" runat="server">
                <ItemTemplate>
                    <tr class="tab_td">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageAccount.CurrentPageIndex -1)*this.AspNetPageAccount.PageSize %>
                        </td>
                        <td class="td_tit_left"><%#Eval("CurriculumName") %></td>
                        <td class="td_tit_left"><%#Eval("ResourceName") %></td>
                        <td class="td_tit_left"><%#Eval("Price") %></td>
                        <td class="td_tit_left"><%#Eval("Remarks") %></td>
                        <td class="td_tit_left"><%#Eval("PayTime") %></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="tab_td td_bg">
                        <td class="td_tit">
                            <%# Container.ItemIndex + 1 + (this.AspNetPageAccount.CurrentPageIndex -1)*this.AspNetPageAccount.PageSize %>
                        </td>
                        <td class="td_tit_left"><%#Eval("CurriculumName") %></td>
                        <td class="td_tit_left"><%#Eval("ResourceName") %></td>
                        <td class="td_tit_left"><%#Eval("Price") %></td>
                        <td class="td_tit_left"><%#Eval("Remarks") %></td>
                        <td class="td_tit_left"><%#Eval("PayTime") %></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>

        </table>
    </div>

    <div style="text-align: center;">
        <div id="containerdiv" style="overflow: hidden; display: inline-block;">
            <webdiyer:aspnetpager id="AspNetPageAccount" cssclass="paginator" showfirstlast="false" prevpagetext="<<上一页" nextpagetext="下一页>>" runat="server" width="100%" currentpagebuttonclass="paginatorPn" alwaysshow="true" horizontalalign="center" pagesize="2" onpagechanged="AspNetPageAccount_PageChanged">
        </webdiyer:aspnetpager>
        </div>
    </div>
</div>
