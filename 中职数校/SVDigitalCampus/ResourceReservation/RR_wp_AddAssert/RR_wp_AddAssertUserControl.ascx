<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_AddAssertUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_AddAssert.RR_wp_AddAssertUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    function close() {
        //$("#mask,#maskTop", parent.document).fadeOut(function () {
        //    $(this).remove();
        //document.parentWindow.location.reload(true);
        parent.location.reload(true);
        //});
        //parent.location.reload();
    }
    var record = {
        num: ""
    }
    var checkDecimal = function (n) {
        var decimalReg = /^\d{0,8}\.{0,1}(\d{1,2})?$/;//var decimalReg=/^[-\+]?\d{0,8}\.{0,1}(\d{1,2})?$/; 
        if (n.value != "" && decimalReg.test(n.value)) {
            record.num = n.value;
        } else {
            if (n.value != "") {
                n.value = record.num;
            }
        }
    }

</script>
<style type="text/css">
    .t_btn input{
        padding-top:0px;
    }
</style>
<div>
    <form id="form1" enctype="multipart/form-data">
        <div class="MenuDiv">
            <table class="tbEdit">

                <tr>
                    <td class="mi"><asp:Literal ID="Lit_Title" runat="server" Text="资产名称："></asp:Literal>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>

                </tr>
                <tr>
                    <td class="mi">
                        <asp:Literal ID="Lit_Holder" runat="server" Text="持有者："></asp:Literal>
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Holder" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Holder" runat="server" ControlToValidate="TB_Holder"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi"><asp:Literal ID="Lit_BelongSchool" runat="server" Text="所属学校："></asp:Literal></td>
                    <td class="ku">
                        
                        <asp:TextBox ID="TB_BelongSchool" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_BelongSchool" runat="server" ControlToValidate="TB_BelongSchool"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi"><asp:Literal ID="Lit_Department" runat="server" Text="部门："></asp:Literal>
                    </td>
                    <td class="ku">
                        
                        <asp:TextBox ID="TB_Department" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Department" runat="server" ControlToValidate="TB_Department"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>

                    <td class="mi"></td>
                    <td class="ku t_btn">
                        <asp:Button id="btnAdd" ValidationGroup="ProjectSubmit" CssClass="btn" runat="server" OnClick="btnAdd_Click" Text="提交"/>
                    </td>
                </tr>

            </table>
            
        </div>
    </form>
</div>