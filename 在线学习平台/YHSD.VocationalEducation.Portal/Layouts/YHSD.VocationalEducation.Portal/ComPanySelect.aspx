<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComPanySelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.ComPanySelect" %>


<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>乡镇选择</title>

<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
</head>
<body>
<form id="Form" runat="server">
<asp:HiddenField ID="HDID" runat="server" />
<asp:HiddenField ID="HFType" runat="server" />
<asp:HiddenField ID="HDCompanyID" runat="server" />

<script type="text/javascript">
    function confirm() {
        var node = $('#organizationTree').tree('getSelected');
        if (node) {
            var id = node.id;
            var type = null;
            var name = node.text;
            if (node.attributes) {
                type = node.attributes.type;
            }

            var organization = new Object();

            organization.Id = id;
            organization.Name = name;
            organization.Type = type;
            window.returnValue = organization;
            window.close();
        }
        else
        {
            alert("请选择节点！");
            return;
        }
    }
</script>

<table id="Table">
    <tr>
        <td style="vertical-align:top;">
            <div style="max-height:500px;overflow:auto;">
            <div class="easyui-panel" style="padding:5px;width:400px;height:500px;overflow-y:auto;">
                <ul id="organizationTree" class="easyui-tree"></ul>
            </div>
            </div>
            <br/>
            <span style="width:100%;text-align:right;">
                <input type="button" value="确定" onclick="confirm()"  class="ms-ButtonHeightWidth"/>
                <input type="button" value="取消" onclick="window.close()"  class="ms-ButtonHeightWidth"/>
            </span>
        </td>
    </tr>
</table>
<script type="text/javascript">
        //加载树
        $(document).ready(function () {
            $("#organizationTree").tree({
                method: 'POST',
                animate: true,
                lines: true,
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx'
            });
        })
</script>
</form>
</body>
</html>


