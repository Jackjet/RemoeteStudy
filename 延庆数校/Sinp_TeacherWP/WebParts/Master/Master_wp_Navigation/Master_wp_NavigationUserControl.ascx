<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Master_wp_NavigationUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.Master.Master_wp_Navigation.Master_wp_NavigationUserControl" %>

<asp:Repeater ID="rptSubNav" runat="server">
    <HeaderTemplate>
        <div class="left" id="sliderbox">
            <div class="menubox">
                <div class="aside"></div>
                <div class="menu" id="menubox">
                     <ul>
    </HeaderTemplate>
    <ItemTemplate>
                        <li>
                            <a class="menuclick" href="#"><i>+</i><%#Eval("NavTitle") %></a><%#Eval("SecNav") %>
                        </li>

    </ItemTemplate>
    <FooterTemplate>
                    </ul>
                </div>
            </div>
        </div>
    </FooterTemplate>
</asp:Repeater>
