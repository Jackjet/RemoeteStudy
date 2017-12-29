<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenCoursesList.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.OpenCoursesList" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
        <script>
            function onSelect(id) {
                var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/OpenDetails.aspx?id=" + id;
                window.location.href = url;
                return false;
            }
     </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="student_main">
            <div class="all_classes f_l" style="width:798px;">
            	<div class="classes_title">
		<a class="title current_title" >公开课程</a>
                </div>
                <div class="all_classes_content" >
                	<div class="top_content">
                        <p class="f_r">
                        </p>
                        <div class="clear"></div>
                        </div>
                        <asp:Repeater ID="RepeaterCourseList" runat="server">
                            <ItemTemplate> 
                        
                         <div class="classes_part_list f_l"  onclick="onSelect('<%#Eval("Id") %>')" style=" margin-top:15px;    margin-right:5px; margin-left:5px;	background:#fbfbfb;border:1px solid #c7c7c7;cursor:pointer;" >
                        <div><img src="<%#Eval("ImgUrl") %>" title="<%#Eval("Description") %>"  onerror="this.src='/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png'" width="170" height="108" /></div>
                        <p class="list_title" title="<%#Eval("Title") %>"><a class="text"><%#GetRemoveString(Eval("Title").ToString())%></a></p>
                        </div>

                            </ItemTemplate>

                        </asp:Repeater>
                    
                    <div class="clear"></div>
                    
                </div>
                 <div style="text-align:center;"> 
    <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                               <webdiyer:AspNetPager ID="AspNetPageCurriculum" CssClass="paginator" ShowFirstLast="false" PrevPageText="<<上一页" NextPageText="下一页>>"  runat="server" Width="100%" HorizontalAlign="center" PageSize="2" CurrentPageButtonClass="paginatorPn" AlwaysShow="true"  OnPageChanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:AspNetPager>
        </div>
                     </div>
            </div>
                
            <div class="clear"></div>
        </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
