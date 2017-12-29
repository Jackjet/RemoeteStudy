<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurriculumTypeEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CurriculumTypeEdit"  %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
    <script src="js/Jquery.easyui/jquery.min.js"></script>
     <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
	<base target="_self" />
    <title>编课程分类信息</title>

</head>
<body>
<form id="Form" runat="server">
    <script type="text/javascript">
        function onSave() {
            if (document.getElementById("<%=this.txtTitle.ClientID %>").value.trim() == "") {
                alert("课程分类名称必须填写！");
                return false;
            }

            if (document.getElementById("<%=this.txtTitle.ClientID %>").value.length > 100) {
                alert("课程分类名称不能超过100个字符！");
                return false;
            }

            
        }

    </script>
    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDParentID" runat="server" />
    
    <table class="tableEdit">

         <tr>
            <th>所属上级</th>
            <td><asp:label ID="lbPTitle" runat="server" ></asp:label></td>
        </tr>
         <tr>
            <th>课程分类名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox></td>
        </tr>

    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="BTSave" runat="server" Text="确定"  OnClientClick="return onSave()" CssClass="ms-ButtonHeightWidth"  onclick="BTSave_Click" />
                &nbsp;&nbsp;<input type="button" value="取消" onclick="window.frameElement.cancelPopUp(); return false;"  class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>

