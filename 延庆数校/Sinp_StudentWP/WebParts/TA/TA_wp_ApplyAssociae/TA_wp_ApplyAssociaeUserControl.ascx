<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_ApplyAssociaeUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_ApplyAssociae.TA_wp_ApplyAssociaeUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/Script/uploadFile1.js"></script>
<style type="text/css">
    td {
        vertical-align: middle;
    }
</style>
<div class="windowDiv">
    <div class="assoimgdiv">
        <div style="float:left;">
             <asp:Image runat="server" ID="A_Pic" CssClass="imgradius" />
        </div>       
        <div class="assoinfodiv">
            <h3>
                <asp:Literal ID="Lit_Title" runat="server"></asp:Literal></h3>
            <p>
                <asp:Literal ID="Lit_Slogans" runat="server"></asp:Literal></p>
        </div>
    </div>
    <table class="winTable">
        <tr>
            <td class="wordsinfo" style="width: 70px;"><asp:Literal ID="Lit_ConWord" runat="server" Text="个人介绍："></asp:Literal></td>
            <td class="ku">
                <asp:TextBox ID="TB_Content" TextMode="MultiLine" class="unvaliwidth" Height="160px" runat="server"></asp:TextBox>
            </td>
            <td style="width: 42px;">
                <span class="wstar">*
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TB_Content"
                   ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="btn btn_sure" Width="120px" runat="server" ValidationGroup="ProjectSubmit" Text="申请加入" />
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
    <div style="display: none">
        <asp:HiddenField runat="server" ID="A_Id" />
    </div>
</div>
