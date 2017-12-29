<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateInfoAdd.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CertificateInfoAdd" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>添加证书</title>
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
            if (document.getElementById("<%=this.TB_GraduationNo.ClientID %>").value.trim() == "") {
                LayerTips("结业证书号必须填写！", "<%=this.TB_GraduationNo.ClientID %>");
                  return false;
              }
            if (document.getElementById("<%=this.TB_GraduationNo.ClientID %>").value.trim() == "") {
                LayerTips("结业时间必须填写！", "<%=this.TB_GraduationNo.ClientID %>");
                 return false;
            }
          }
    </script>  
    <table class="tableEdit MendEdit">		
        <tr>
            <th>结业证书号&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="TB_GraduationNo" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
	    <tr>
            <th>学员姓名&nbsp;<span style="color:Red">*</span></th>
            <td><asp:DropDownList ID="DDL_StuName" CssClass="inputPart" runat="server"></asp:DropDownList></td>
        </tr>
         <tr>
            <th>课程名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:DropDownList ID="DDL_CurriculumName" CssClass="inputPart" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <th>结业时间&nbsp;<span style="color:Red">*</span></th>
            <td><input type="text" class="inputPart" readonly="readonly" runat="server" id="TB_GraduationDate" name="TB_GraduationDate" onclick="WdatePicker();"/></td>
        </tr>
         <tr>
            <th>发证单位&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="TB_AwardUnit" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
         <tr>
            <th>证书查询网址&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="TB_QueryURL" runat="server" CssClass="inputPart"></asp:TextBox></td>
        </tr>
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <asp:Button ID="Btn_CertificateSave" runat="server" Text="确定"  OnClientClick="return onSave()" CssClass="ms-ButtonHeightWidth button_s" OnClick="Btn_CertificateSave_Click"/>
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();" class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>

