<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurriculumInfoSelect.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.CurriculumInfoSelect"  %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>选择分类</title>
<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <link href="css/type.css" rel="stylesheet" />
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
            var name = node.text;
            if (node.attributes) {
                type = node.attributes.type;
            }

            var organization = new Object();

            organization.Id = id;
            organization.Name = name;

            window.returnValue = organization;
            OL_CloseLayerIframe(organization);
        }
        else {
            alert("请选择节点！");
            return;
        }
    }
</script>

<table id="Table">
    <tr>
        <td style="vertical-align:top;">
            <div style="overflow:auto;">
            <div  style="overflow-y:auto;">
                <ul id="organizationTree" class="easyui-tree"></ul>
            </div>
            </div>
            <br/>
            <span style="text-align:right;">
                <input type="button" value="确定" onclick="confirm()"  class="button_s"/>
                <input type="button" value="取消" onclick="OL_CloseLayerIframe()"  class="button_n"/>
            </span>
        </td>
    </tr>
</table>
<script type="text/javascript">
    //加载树
    $(document).ready(function () {
        $("#organizationTree").tree({
            method: 'post',
            animate: true,
            lines: true,
            url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx'
        });
    })
</script>
</form>
</body>
</html>

