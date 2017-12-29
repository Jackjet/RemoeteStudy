<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_ShowAssociaePhotoUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_ShowAssociaePhoto.TA_wp_ShowAssociaePhotoUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">

<div class="st_xc" style="width: 100%; overflow-y: auto;">
    <ul style="margin-left:10px;">
        <asp:ListView ID="PhotosList" runat="server">
            <EmptyDataTemplate>
                <table class="W_form" id="emptyAlbum">
                    <tr>
                        <td>相册暂无照片</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <li id='<%# Eval("Photo_ID") %>' style="margin: 10px 10px;">
                    <img src='<%# Eval("PhotoUrl") %>' title='<%# Eval("Title") %>'>
                    <h3><%# Eval("Title") %></h3>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</div>
