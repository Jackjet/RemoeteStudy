<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_AddPlanRecordUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_AddPlanRecord.SG_wp_AddPlanRecordUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    td {
        vertical-align: middle;
    }
</style>
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <td class="wordsinfo">记录：</td>
            <td colspan="2" class="ku">
                <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_WordRecord" TextMode="MultiLine"  CssClass="colwidth content"  runat="server"></asp:TextBox></td>
                        <td style="width: 35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_WordRecord" runat="server" ControlToValidate="TB_WordRecord"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">附件：</td>
            <td class="ku">
                <input type="file" id="file_activity" name="file_activity" runat="server" />
            </td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_RecordSave" CssClass="btn btn_sure" runat="server" ValidationGroup="ProjectSubmit" Text="确定" OnClick="Btn_RecordSave_Click" />
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>



