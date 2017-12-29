<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceClassificationEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ResourceClassificationEdit" %>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>编辑分类信息</title>

</head>
<body>
    <div style="display:none;">
<form id="Form" runat="server">
    <script type="text/javascript">
        function onSave() {
            if (document.getElementById("<%=this.txtRName.ClientID %>").value.trim() == "") {
                alert("分类名称必须填写！");
                return false;
            }
        }
        
        function SelectParent() {
            var id = document.getElementById("<%=this.HDID.ClientID %>").value;
            var organization = window.showModalDialog("/_layouts/15/YHSD.VocationalEducation.Portal/ComPanySelect.aspx?type=0&id=" + id, window, 'dialogWidth:420px;dialogHeight:550px;edge:raised;scroll:auto;status:no;');

            if (organization) {
                document.getElementById("<%=this.HDParentID.ClientID %>").value = organization.Id;
                document.getElementById("ParentNameId").innerText = organization.Name;
            }
        }
    </script>
    <asp:HiddenField ID="HDID" runat="server" />
    <asp:HiddenField ID="HDParentID" runat="server" />
    <input type="hidden" id="hdPath" runat="server" />
    <table class="tableEdit">
        <tr>
            <th>上级分类</th>
            <td><asp:DropDownList runat="server" ID="ddlParent"></asp:DropDownList></td>
        </tr>
        <tr>
            <th>分类名称</th>
            <td><asp:TextBox ID="txtRName" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="BtnSave" runat="server" Text="确定"  OnClientClick="return onSave()"  CssClass="ms-ButtonHeightWidth" OnClick="BtnSave_Click" />
                &nbsp;&nbsp;<input type="button" value="取消" onclick="window.frameElement.cancelPopUp(); return false;"  class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
        </div>
    
<div id="EditDiv" style="display: none;">
    <table class="tableEdit">
        <tr>
            <th>上级分类</th>
            <td>
                <select id="cc" class="easyui-combotree" style="width: 200px; height: 29px;" data-options="url:'../../../_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false"></select></td>
        </tr>
        <tr>
            <th>分类名称&nbsp;<span style="color: Red">*</span></th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <input type="button" class="button_s" value="确定" /><input type="button" class="button_n"  value="取消" /></td>
        </tr>
    </table>
</div>
</body>
<script type="text/javascript">
    document.getElementById("hdPath").value = window.frameElement.dialogArgs;
</script>
</html>