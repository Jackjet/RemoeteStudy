<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionEdit.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.PositionEdit"  %>

<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/metro/easyui.css" rel="stylesheet" />
<link href="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/themes/icon.css" rel="stylesheet" />
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
<script src="../../../_layouts/15/YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.easyui.min.js"></script>
 <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
        <script>
            function onSave() {
                var nodes = $('#PositionTree').tree('getChecked');
                var s = '';
                for (var i = 0; i < nodes.length; i++) {
                    if (s != '') s += ',';
                    s += nodes[i].id;
                }
                document.getElementById("<%=this.PositionName.ClientID %>").value = s;
            if (document.getElementById("<%=this.Name.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                    layer.tips("角色名称必须填写！", "#" + "<%=this.Name.ClientID %>", { tips: 1 });
                    return false;
                }

                if (document.getElementById("<%=this.Name.ClientID %>").value.length > 100) {
                    layer.tips("角色名称不能超过100个字符！", "#" + "<%=this.Name.ClientID %>", { tips: 1 });
                    return false;
                }
                if (document.getElementById("<%=this.PositionName.ClientID %>").value.replace(/(^\s*)|(\s*$)/g, "") == "") {
                    layer.tips("请给角色分配权限！！", "#PositionTree", { tips: 1 });
                    return false;
                }

            }
        </script>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>增加角色</title>
	<link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />

</head>
<body>
<form id="Form" runat="server">

    <asp:HiddenField ID="PositionName" runat="server" />
    <asp:HiddenField ID="HidPositionID" runat="server" />
    <table class="tableEdit MendEdit">
        <tr>
                  <th>角色名称&nbsp;<span style="color:Red">*</span></th>
            <td><asp:TextBox ID="Name" CssClass="inputPart" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
                  <th valign="top" >角色权限&nbsp;<span style="color:Red">*</span></th>
                    
            <td>  <ul id="PositionTree" class="easyui-tree"></ul></td>
        </tr>
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <asp:Button ID="BTSave" runat="server" Text="确定" CssClass="button_s" OnClientClick="return onSave()"   OnClick="BTSave_Click"   />
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();"  class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
    </form>
</body>
    <script type="text/javascript">
        //加载树
        $(document).ready(function () {
            
            $("#PositionTree").tree({
                checkbox: true,
                method: 'post',
                animate: true,
                lines: true,
                url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/PositionJson.aspx?id=' + document.getElementById("<%=this.HidPositionID.ClientID %>").value,
                onLoadSuccess: function (node, data) {
                    $('#PositionTree').tree('collapseAll');
                }
            });

        })
</script>
</html>
