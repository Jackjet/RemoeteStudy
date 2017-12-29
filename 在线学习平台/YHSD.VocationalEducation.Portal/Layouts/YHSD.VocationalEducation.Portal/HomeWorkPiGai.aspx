<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeWorkPiGai.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.HomeWorkPiGai"  %>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title></title>
    <script src="js/Jquery.easyui/jquery.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
     <link href="css/edit.css" rel="stylesheet" />
     <link href="css/type.css" rel="stylesheet" />
    <style>
        .tableEdit th {

    width: 80px;
    text-align: left;
    }

    </style>
    </head>
<body>
    <form id="Form" runat="server">
      <asp:HiddenField ID="HDID" runat="server" />
      <asp:HiddenField ID="HDUserID" runat="server" />
      <asp:HiddenField ID="HDChapterID" runat="server" />
    <table class="tableEdit MendEdit">
          <tr>
            <th>学员名称</th>
            <td><asp:Label ID="LabelUserName" runat="server"/></td>
        </tr>
        <tr>
            <th>作业详细</th>
            <td><label id="WorkList" runat="server" /></td>
        </tr>
        <tr>
            <th>作业分数<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="txtScore" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
         <tr>
            <th>评论</th>
            <td><asp:TextBox ID="txtComments"  Width="400px" Height="80px" runat="server" TextMode="MultiLine" Rows="6"  CssClass="inputPart"></asp:TextBox></td>
        </tr>
        <tr>
            <th>是否优秀作业</th>
            <td><asp:RadioButton runat="server" GroupName="IsExcellentWork" Text="是" ID="RadioYes" /> <asp:RadioButton  runat="server" GroupName="IsExcellentWork" Text="否" ID="RadioNo" /></td>
        </tr>
    </table>
        <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="BTSave" runat="server" Text="确定" CssClass="button_s" OnClick="BTSave_Click"  />
                &nbsp;&nbsp;<input type="button" value="取消" onClick="OL_CloseLayerIframe();" class="button_n"/>
            </td>
        </tr>
    </table>
        </form>
    </body>

</html>