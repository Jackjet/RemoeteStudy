<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_AddCourceUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_AddCource.XXK_wp_AddCourceUserControl" %>
<script src="../../../../_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/uploadFile1.js"></script>

<div class="listdv">
    <table>
        <tr>
            <th>学年学期：</th>
            <td>
                <asp:DropDownList ID="dpSection" runat="server"></asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_Title" runat="server" ControlToValidate="dpSection"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>上课年级：</th>
            <td>
                <asp:DropDownList ID="StudyGrade" runat="server">
                   
                </asp:DropDownList>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="StudyGrade"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th>选修课名称：</th>
            <td>

                <asp:TextBox ID="CourceName" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TB_ResearchField"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>

        <tr>
            <th>上课周次：</th>
            <td>
                <asp:DropDownList ID="StudyWeeks" runat="server">
                    <asp:ListItem Value="1">一次</asp:ListItem>
                    <asp:ListItem Value="2">二次</asp:ListItem>
                    <asp:ListItem Value="3">三次</asp:ListItem>
                    <asp:ListItem Value="4">四次</asp:ListItem>

                </asp:DropDownList>
                <span style="color: red;">
                    <asp:RequiredFieldValidator ID="RFV_StartTime" runat="server" ControlToValidate="TB_StartDate"
                        ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>
        <tr>
            <th></th>
            <td>
                <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="save" runat="server" ValidationGroup="ProjectSubmit" Text="保存" />
                <input type="button" class="cancel" value="取消" onclick="parent.closePages();" />

            </td>
        </tr>
    </table>
</div>
