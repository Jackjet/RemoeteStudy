<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTrainRoom.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.AddTrainRoom" %>

<link href="/_layouts/15/YHSD.VocationalEducation.Portal/OldCss/common.css" rel="stylesheet" />
<link href="/_layouts/15/YHSD.VocationalEducation.Portal/OldCss/style.css" rel="stylesheet" />
<link href="/_layouts/15/YHSD.VocationalEducation.Portal/OldCss/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/YHSD.VocationalEducation.Portal/OldCss/animate.css" rel="stylesheet" />

<script src="/_layouts/15/YHSD.VocationalEducation.Portal/js/jquery-1.6.2.min.js"></script>

<script type="text/javascript">
    function close() {
        
        parent.location.reload(true);
        
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
                    <td class="mi">
                        实训室名称
                    </td>
                    <td class="ku">
                        <asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Holder" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">
                        实训室地点
                    </td>
                    <td class="ku">
                        
                        <asp:TextBox ID="TB_Place" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_BelongSchool" runat="server" ControlToValidate="TB_Place"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">
                        实训室面积
                    </td>
                    <td class="ku">
                        
                        <asp:TextBox ID="TB_Area" runat="server"></asp:TextBox>
                        <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Department" runat="server" ControlToValidate="TB_Area"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="mi">
                        是否启用
                    </td>
                    <td class="ku">
                        <asp:DropDownList ID="DDL_IsCanUse" runat="server">
                            <asp:ListItem>启用</asp:ListItem>
                            <asp:ListItem>禁用</asp:ListItem>
                        </asp:DropDownList>
                        
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