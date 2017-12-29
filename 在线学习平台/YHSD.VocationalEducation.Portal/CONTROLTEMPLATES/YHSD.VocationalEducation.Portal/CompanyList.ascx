<%@ Assembly Name="YHSD.VocationalEducation.Portal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2e18f1308c96fd22" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.CompanyList" %>
<script type="text/javascript">
    function CreateCompany() {
        var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyEdit.aspx";
        var node = $('#organizationTree').tree('getSelected');
        var pid = null;
        if (node) {
            var pid = node.id;
        }

        if (pid == null || pid == "")
            pid = "Root";
        url = url + "?parentId=" + pid;
        OL_ShowLayerNo(2, "新建组织结构", 400, 360, url, function (returnVal) {
        });
        //openDialog(url, 500, 300, closeCallback);


        return false;
    }

    function EditCompany() {

        var node = $('#organizationTree').tree('getSelected');
        if (node) {
            var url = "<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyEdit.aspx";
            var id = node.id;
            url = url + "?id=" + id;
            // openDialog(url, 500, 300, closeCallback);
            OL_ShowLayerNo(2, "编辑组织结构", 400, 360, url, function (returnVal) {
            });
        }
        else {
            LayerAlert("请选择要编辑的节点");
            return false;
        }

        return false;
    }

    function DeleteCompany() {
        var node = $('#organizationTree').tree('getSelected');
        if (node) {
            var id = node.id;
            document.getElementById("<%=this.DeleteID.ClientID %>").value = id;
            LayerAlert("不能删除!");
        }
        else {
            LayerAlert("请选择要删除节点");
            return false;
        }
        return true;

    }

</script>

<asp:HiddenField ID="DeleteID" runat="server" />
<div>
    <ul id="organizationTree" class="easyui-tree"></ul>
</div>
<br/>
<span style="width:100%;text-align:left;">
    <asp:Button ID="BTAdd" runat="server" Text="新建"  CssClass="ms-ButtonHeightWidth button_s"  OnClientClick="return CreateCompany()"/>
    <asp:Button ID="BTEdit" runat="server" Text="编辑"  CssClass="ms-ButtonHeightWidth button_s"  OnClientClick="return EditCompany()"/>
    <%--<asp:Button ID="BTDelete" runat="server" Text="删除"  CssClass="ms-ButtonHeightWidth button_n"  OnClientClick="return DeleteCompany()" OnClick="BTDelete_Click"/>--%>
</span>

<script type="text/javascript">
    //加载树
    $(document).ready(function () {
        $("#organizationTree").tree({
                
            method: 'Post',
            animate: true,
            lines: false,
            url: '<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CompanyTree.aspx'
        });

    })
</script>
