﻿<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MendEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.MendEdit" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>编辑菜单</title>
	<link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <script src="js/Jquery.easyui/jquery.min.js"></script>
        <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
        <script>
            function onSave() {
                if (document.getElementById("<%=this.Name.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                    LayerTips("菜单名称必须填写！", "<%=this.Name.ClientID %>");
                    return false;
                }

                if (document.getElementById("<%=this.Name.ClientID %>").value.length > 100) {
                    LayerTips("菜单名称不能超过100个字符！", "<%=this.Name.ClientID %>");

                    return false;
                }
                var sequenceValue = document.getElementById("<%=this.Role.ClientID %>").value;

                if (sequenceValue.trim() == "") {
                    LayerTips("显示顺序必须填写！", "<%=this.Role.ClientID %>");
                    return false;
                }
                if (isNaN(sequenceValue)) {
                    LayerTips("显示顺序必须为数字！", "<%=this.Role.ClientID %>");
                    return false;
                }
                var SortNum = parseInt(sequenceValue);
                if (!(SortNum > 0 && SortNum < 999)) {
                    LayerTips("显示顺序内容必须是数字且大于0小于999！", "<%=this.Role.ClientID %>");
                    return false;
                }

            }
            
        </script>
</head>
<body>
<form id="Form" runat="server">
    <asp:HiddenField ID="HDImg" runat="server" />
    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDParentID" runat="server" />

    <table class="tableEdit MendEdit">
        <tr>
            <th>上级菜单&nbsp;&nbsp;&nbsp;</th>
            <td>    <asp:Label ID="PidName" runat="server" /></td>
        </tr>
        <tr>
            <th>菜单名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="Name" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>

         <tr>
            <th>菜单图片&nbsp;</th>
            <td>
                <asp:FileUpload ID="FileMenu" runat="server" />
</td>
        </tr>
        <tr>
            <th>跳转地址&nbsp;</th>
            <td><asp:TextBox ID="Url" runat="server"  Rows="6" CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>顺序&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="Role" runat="server"  Rows="6" CssClass="inputPart"></asp:TextBox></td>
        </tr>
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <asp:Button ID="BTSave" runat="server" Text="确定"  CssClass="button_s"  OnClientClick="return onSave()" OnClick="BTSave_Click"   />
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();"  class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
