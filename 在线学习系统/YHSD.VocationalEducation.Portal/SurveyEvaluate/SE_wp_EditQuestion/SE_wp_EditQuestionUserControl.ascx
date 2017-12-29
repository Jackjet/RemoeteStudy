<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_EditQuestionUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditQuestion.SE_wp_EditQuestionUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/StuAssociate.css" rel="stylesheet" type="text/css" />
<style type="text/css">    
    td{ vertical-align:middle; }
    .title { padding:0 5px;width:390px;height:38px }
</style>
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <td class="wordsinfo">题型：</td>
            <td class="ku"><asp:DropDownList ID="DDL_Type" runat="server" Width="100px" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="DDL_Type_SelectedIndexChanged" /></td>
        </tr>
        <tr>
            <td class="wordsinfo">题目：</td>
            <td class="ku"><asp:TextBox ID="TB_Title" TextMode="MultiLine" CssClass="title" runat="server" />
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TB_Title"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
        <tr runat="server" id="a">
            <td class="wordsinfo">选项A：</td>
            <td class="ku"><asp:TextBox ID="TB_A" runat="server" Width="300px" />（<asp:TextBox ID="SC_A" runat="server" Width="7px" />分）
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_A" runat="server" ControlToValidate="TB_A"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFV_AS" runat="server" ControlToValidate="SC_A"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
        <tr  runat="server" id="b">
            <td class="wordsinfo">选项B：</td>
            <td class="ku"><asp:TextBox ID="TB_B" runat="server" Width="300px" />（<asp:TextBox ID="SC_B" runat="server" Width="7px" />分）
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_B" runat="server" ControlToValidate="TB_B"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFV_BS" runat="server" ControlToValidate="SC_B"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
        <tr  runat="server" id="c">
            <td class="wordsinfo">选项C：</td>
            <td class="ku"><asp:TextBox ID="TB_C" runat="server" Width="300px" />（<asp:TextBox ID="SC_C" runat="server" Width="7px" />分）
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_C" runat="server" ControlToValidate="TB_C"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFV_CS" runat="server" ControlToValidate="SC_C"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
        <tr  runat="server" id="d">
            <td class="wordsinfo">选项D：</td>
            <td class="ku"><asp:TextBox ID="TB_D" runat="server" Width="300px" />（<asp:TextBox ID="SC_D" runat="server" Width="7px" />分）
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_D" runat="server" ControlToValidate="TB_D"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFV_DS" runat="server" ControlToValidate="SC_D"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_Sure" CssClass="btn btn_sure" runat="server"  Text="确定" OnClick="Btn_Sure_Click"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>