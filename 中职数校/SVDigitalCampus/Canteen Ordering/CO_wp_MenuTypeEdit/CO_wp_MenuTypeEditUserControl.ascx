<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_MenuTypeEditUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_MenuTypeEdit.CO_wp_MenuTypeEditUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/admin.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">

    function close() {
        alert("关闭修改");
        $("#mask,#maskTop", parent.document).fadeOut(function () {
            $(this).remove();

        });
        //$("div[id=mask]:last,div[id=maskTop]:last").fadeOut(function () {
        //    $(this).remove();

        //});
        parent.location.reload();
    }
</script>
<form id="form1"><div>
    <table border="0" class="tableBoxNew" >
               
                <tr>
                    <td  style="width:15%;text-align:right;">
                        *名称：
                    </td>
                    <td style="width:35%;text-align:left;">
                        <asp:HiddenField id="TypeID" runat="server"/>
                        <asp:TextBox runat="server" ID="txtType" CssClass="disInBlock"></asp:TextBox>
                    </td>
                   <td>
            <asp:Button ID="btnEdit" runat="server" Text="保存" OnClick="btnEdit_Click" /></td></tr>
            </table>
</div></form>