.<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TG_wp_ExamineTermUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TG.TG_wp_ExamineTerm.TG_wp_ExamineTermUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script type="text/javascript">
    
</script>
<style type="text/css">
    .exit{
        width:70%;
        margin:auto;
        margin-top:30px;
    }
    .exit table{
        width:100%;
        border:0px;
    }
    .exit table tr{
        height:30px;
        line-height:30px;

    }
    .exit table tr th{
        width:5%;
    }
    .exit table tr td{
        width:10%;
    }
</style>
<div class="dialog_wrap">
<div class="showterm">
     <div class="plantitle">
            <span class="title">
                <asp:Literal ID="Lit_Title" runat="server"></asp:Literal>
            </span>                         
            <span class="zt fr"><asp:Image ID="Img_Status" ImageUrl="/_layouts/15/TeacherImages/wait.png" runat="server" CssClass="ZT_img" /></span>
     </div>
    <table>
        <tr>
            <td colspan="2" style="text-align:center"><span>
                <asp:Literal ID="Lit_LearnYear" runat="server"></asp:Literal></span>
                <span><asp:Literal ID="Lit_PlanType" runat="server"></asp:Literal></span></td>
        </tr>
        <tr>
            <td colspan="2">
                <p>
                    <asp:Literal ID="Lit_PlanContent" runat="server"></asp:Literal>
                </p>
            </td>
        </tr>
        <tr>
            <td>附件：<asp:Literal ID="Lit_Attachment" runat="server"></asp:Literal></td>
            <td>
                <asp:HiddenField ID="Hid_Id" runat="server" />
            </td>            
        </tr>
    </table>
    
</div>
<asp:Panel ID="Pan_ExitResult" runat="server" Visible="false">
    <div class="exit" style="width: 70%;">
    <table class="tabContent">
        <tr>
            <th>审批结果：</th>
            <td>
               <asp:Literal ID="Lit_Status" runat="server"></asp:Literal>
            </td>
            <th>审批人：</th>
            <td>
               <asp:Literal ID="Lit_Auditor" runat="server"></asp:Literal>
            </td>
        </tr>


        <tr class="areatr">
            <th>审批意见：</th>
            <td colspan="3">
                <asp:Literal ID="Lit_AuditOpinion" runat="server"></asp:Literal>
            </td>
        </tr>

        
    </table>
</div>
</asp:Panel>
<asp:Panel ID="Pan_Exit" runat="server">
    <div class="formdv">
    <table class="tabContent" style="display: block;">
        <tr>
            <th>审批结果：</th>
            <td>
                <%--<asp:RadioButtonList ID="RBL_Result" ValidationGroup="ExitResult" runat="server">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:RadioButtonList>--%>
                <asp:RadioButton ID="RB_Pass" Checked="true" GroupName="Exit" runat="server" Text="审批通过" />
                <asp:RadioButton ID="RB_Refuse" GroupName="Exit" runat="server" Text="审批不通过" />
            </td>
        </tr>


        <tr class="areatr">
            <th>审批意见：</th>
            <td>
                <asp:TextBox ID="TB_AuditOpinion" TextMode="MultiLine" Height="82px" runat="server"></asp:TextBox>
                <span style="color: red;">*
                            <asp:RequiredFieldValidator ID="RFV_AuditOpinion" runat="server" ControlToValidate="TB_AuditOpinion"
                                ErrorMessage="必填" ValidationGroup="AuditSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </td>
        </tr>

        <tr class="btntr">
            <th></th>
            <td>
                <asp:Button ID="Btn_InfoSave" OnClick="Btn_InfoSave_Click" CssClass="save" runat="server" ValidationGroup="AuditSubmit" Text="保存" />
                <%--<asp:Button ID="Btn_InfoCancel" OnClick="Btn_InfoCancel_Click" CssClass="cancel" runat="server" Text="取消" />--%>
                <input type="button" class="cancel" value="取消" onclick="parent.closePages();" />
            </td>
        </tr>
    </table>
</div>
</asp:Panel>



</div>
