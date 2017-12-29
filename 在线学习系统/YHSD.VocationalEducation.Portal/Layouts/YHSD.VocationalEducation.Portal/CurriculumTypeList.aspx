<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurriculumTypeList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CurriculumTypeList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
        <script>
            function onSelect(id) {
                var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + id;
                window.location.href = url;
                return false;
            }
     </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="HidCurriculumType" runat="server" />
       <script>
           $(function () {
               $('#cc').combotree({
                   onSelect: function (node) {
                       location.href = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumTypeList.aspx?ResourceID=" + node.id;
	         },
	         onLoadSuccess: function () {
	             var Postionid = document.getElementById("<%=this.HidCurriculumType.ClientID %>").value;
                    if (Postionid != null && Postionid != "") {
                        $('#cc').combotree("setValue", Postionid);
                    }

                }
            })
           });
       </script>
            <div id="head" class="TopToolbar">
            <span>课程分类&nbsp</span><select id="cc" class="easyui-combotree" style="width:200px;height:29px;"
        data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false"></select>
        </div>
    <div>
       <div class="main_list_table">
<table  class="list_table" cellspacing="0" cellpadding="0" border="0">
        <tr class="tab_th top_th">
            <th class="th_tit">序号</th>
            <th class="th_tit_left">课程名称</th>
            <th class="th_tit">课程分类</th>
            <th class="th_tit">创建日期</th>
        </tr>
                        
                <asp:Repeater ID="RepeaterList" runat="server">
            <ItemTemplate>
                <tr class="tab_td">
               <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
               <td class="td_tit_left" >
               <a style="cursor:pointer;" onclick="onSelect('<%#Eval("Id") %>')"><%#Eval("Title") %></a>
              </td>
              <td class="td_tit"><%#Eval("ResourceName") %></td>
             <td class="td_tit"><%#Eval("CreaterTime") %></td>
                    </tr>
      
                </ItemTemplate>
                     <AlternatingItemTemplate>
                            <tr class="tab_td td_bg">
                                               <td class="td_tit">
<%# Container.ItemIndex + 1 + (this.AspNetPageCurriculum.CurrentPageIndex -1)*this.AspNetPageCurriculum.PageSize %>
              </td>
               <td class="td_tit_left">
              <a style="cursor:pointer;" onclick="onSelect('<%#Eval("Id") %>')"><%#Eval("Title") %></a>
              </td>
              <td class="td_tit"><%#Eval("ResourceName") %></td>
              <td class="td_tit"><%#Eval("CreaterTime") %></td>
                    </tr>
                     </AlternatingItemTemplate>
                </asp:Repeater>
                 
</table>
           </div>
                     <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%"  CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  HorizontalAlign="center" PageSize="2" OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
         </div>
                     </div>

</div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
