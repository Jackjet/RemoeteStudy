<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_EvaluateTemp_EditUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_Edit.ME_wp_EvaluateTemp_EditUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<style type="text/css">    
    td{
        vertical-align:middle;
    }
</style>
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <td class="wordsinfo">模板名称：</td>
            <td class="ku"><asp:TextBox ID="TB_Title" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">模板说明：</td>
            <td class="ku"><asp:TextBox ID="TB_Content" TextMode="MultiLine"  class="unvaliwidth" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">是否启用：</td>
            <td class="ku">
                <asp:RadioButton ID="RB_True" runat="server" GroupName="status" Text="启用" Checked="true" />
                <asp:RadioButton ID="RB_False" runat="server" GroupName="status" Text="禁用" />
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">适用对象：</td>
            <td class="ku"><asp:DropDownList ID="DDL_Type" runat="server" CssClass="droplist"></asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Type" runat="server" ControlToValidate="DDL_Type"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_Sure" CssClass="btn btn_sure" runat="server"  Text="确定" OnClick="Btn_Sure_Click"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>