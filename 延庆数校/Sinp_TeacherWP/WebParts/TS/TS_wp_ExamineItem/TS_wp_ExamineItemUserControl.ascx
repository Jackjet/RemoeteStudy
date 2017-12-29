<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TS_wp_ExamineItemUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TS.TS_wp_ExamineItem.TS_wp_ExamineItemUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script src="/_layouts/15/Script/My97DatePicker/WdatePicker.js"></script>

<div class="listdv">
    <table>
        <tr>
            <th>审批结果：</th>
            <td>
                <asp:RadioButton ID="RB_Agree" runat="server" Checked="true" GroupName="Exam" Text="通过" />
                <asp:RadioButton ID="RB_Refuse" runat="server" GroupName="Exam" Text="不通过" />
            </td>
        </tr>
        
        <tr class="areatr">
            <th>审核意见：</th>
            <td>
                <asp:TextBox ID="TB_ExamSuggest" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_ExamSuggest" runat="server" ControlToValidate="TB_ExamSuggest"
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