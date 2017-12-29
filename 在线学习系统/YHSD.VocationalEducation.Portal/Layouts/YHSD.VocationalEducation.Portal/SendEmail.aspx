<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.SendEmail" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>发送邮件</title>
    <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />    
    <script src="js/Jquery.easyui/jquery.min.js" type="text/javascript"></script>  
    <script src="js/layer/layer.js" type="text/javascript"></script>
    <script src="js/layer/OpenLayer.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form" runat="server">
      
        <table class="tableEdit MendEdit" id="percite" style="margin:auto;">            
            <tr>
                <th>收件人邮箱地址</th>
                <td>
                    <asp:TextBox ID="TUser" runat="server" Text="" CssClass="inputPart"></asp:TextBox></td>
            </tr>
            <tr>
                <th>主题</th>
                <td>
                    <asp:TextBox ID="TxtTitle" runat="server" CssClass="inputPart"></asp:TextBox></td>
            </tr>
            <tr>
                <th>内容</th>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" CssClass="inputPart"></asp:TextBox></td>
            </tr>
            <tr>
                <th>发件人邮箱地址</th>
                <td>
                    <asp:TextBox ID="txtFromUser" runat="server" CssClass="inputPart"></asp:TextBox></td>
            </tr>
           
        </table>
        <table width="100%" class="MendEdit">
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="发送邮件" OnClick="Button1_Click" CssClass="ms-ButtonHeightWidth button_s"/><br />
                    <span style="color:red;font-size:14px;">*请正确填写收件人、发件人邮件地址，邮件内容过短会视为垃圾邮件</span>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>

