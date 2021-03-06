﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_StudentLoginUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_StudentLogin.ES_wp_StudentLoginUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />

<div class="login_dl">
        <div class="logincon">
        <div class="login_tit">
            学生登录           
        </div>
        <div class="condl">
            <table class="contable">
                <tr >
                    <td class="lftd">用&nbsp;户&nbsp;&nbsp;名：</td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lftd">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>

                    </td>
                    
                </tr>
                
            </table>
            <div class="btn">
                <asp:Button ID="Login" runat="server" Text="登陆" BackColor="#76ACDD" Width="90px" Height="30px" OnClick="Login_Click"/>
            </div>
        </div>
    </div>

</div>