<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeLibAdd.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.KnowledgeLibAdd" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>知识库 添加/编辑</title>
	<link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <script src="js/Jquery.easyui/jquery.min.js" type="text/javascript"></script>
    <script src="js/Jquery.easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/layer/layer.js" type="text/javascript"></script>
    <script src="js/layer/OpenLayer.js" type="text/javascript"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="Form" runat="server">
    <script type="text/javascript">
        function onSave() {
            if (document.getElementById("<%=this.TB_Question.ClientID %>").value.trim() == "") {
                LayerTips("问题必须填写！", "<%=this.TB_Question.ClientID %>");
                return false;
            }
            if (document.getElementById("<%=this.TB_Answer.ClientID %>").value.trim() == "") {
                LayerTips("答案必须填写！", "<%=this.TB_Answer.ClientID %>");
                return false;
            }
        }
    </script>  
    <table class="tableEdit MendEdit">	    
	    <tr>
            <th>问题&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="TB_Question" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
         <tr>
            <th>答案&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="TB_Answer" runat="server" TextMode="MultiLine" Rows="6" CssClass="inputPart" 
height="50px"></asp:TextBox></td>
        </tr>
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <asp:Button ID="Btn_KnowledgeLibSave" runat="server" Text="确定"  OnClientClick="return onSave()" CssClass="ms-ButtonHeightWidth button_s" OnClick="Btn_KnowledgeLibSave_Click"/>
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();" class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>


