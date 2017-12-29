<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenDetails.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.OpenDetails" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="details_content">
                    <div class="details_part">
                    	<div class="photo_part f_l">
                          <%--<img width="324" height="216" src="images/classes_pic06.png">--%>
                            <asp:Image ID="ImgUrl"  onerror="this.src='/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png'" runat="server" Width="324" Height="216" />
                    	</div>
                        <div class="text_part f_l">
                        	<h4 class="text_title"><asp:Label ID="LabelTitle" runat="server" /></h4>
                            <p class="text_tip">课程总计：<span class="number"><asp:Label ID="LabelCount" runat="server" /></span>章</p>
                            <div class="text_content">
                            	<p class="txt"><asp:Label ID="LabelDescription" runat="server" /></p>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="details_list">
                    	<div class="list_left f_l">
                        	<h4 class="list_title">课程列表</h4>
                                <label  id="LableChapter" runat="server" />
                        </div>
                        <div class="list_right f_l">
                        	<h4 class="list_title">相关课程</h4>
                          	<div class="list_right_p">
                            	 <label id="LabelXiangGuan" runat="server" />
                                <div class="clear"></div>
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
