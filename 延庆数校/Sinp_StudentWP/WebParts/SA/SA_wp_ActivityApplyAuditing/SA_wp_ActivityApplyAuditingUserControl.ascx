<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_ActivityApplyAuditingUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_ActivityApplyAuditing.SA_wp_ActivityApplyAuditingUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" type="text/css" />
<style type="text/css">    
    td{
        vertical-align:middle;
    }
     .winTable tr td.wordsinfo{width:126px;}
</style>
<div class="windowDiv">
    <table class="winTable">      
        <tr>
                    <td class="wordsinfo">活动名称：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Title"></asp:Label></td>
             <td rowspan="4" style="width:120px; text-align: center;padding-right:47px;">
                <asp:Image ID="img_Pic"  CssClass="imgAssoci" runat="server" />
            </td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动范围：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Range"></asp:Label></td>
                </tr>
                <tr>
                    <td class="wordsinfo">活动类型：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_ActivityType"></asp:Label></td>
                </tr>
                 <tr id="TR_MaxCount" runat="server">
                    <td class="wordsinfo">最多报名项目个数：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_MaxCount"></asp:Label></td>                    
                </tr>
                <tr>
                    <td class="wordsinfo">活动简介：</td>
                    <td class="ku" colspan="2">
                        <table class="innerTable">
                            <tr>
                                <td style="padding: 0px;">
                                    <asp:TextBox ID="TB_Introduction" TextMode="MultiLine" CssClass="colwidth content" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 35px; padding: 0px;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="wordsinfo">报名条件：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Condition"></asp:Label></td>                   
                </tr>
                <tr>
                    <td class="wordsinfo">报名日期：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Date"></asp:Label></td>                    
                </tr>                
                <tr>
                    <td class="wordsinfo">报名地点：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Address"></asp:Label></td>                     
                </tr>
                <tr id="TR_Project" runat="server">
                    <td class="wordsinfo">项目名称：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Project"></asp:Label></td>
                </tr>
                <tr>
                    <td class="wordsinfo">发起部门：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_Department"></asp:Label></td>         
                </tr>   
                <tr>
                    <td class="wordsinfo">发起人：</td>
                    <td class="ku"><asp:Label runat="server" ID="LB_OrganizeUser"></asp:Label></td>         
                </tr> 
         <tr>
                    <td class="wordsinfo">审核意见：</td>
                    <td class="ku" colspan="2">
                        <table class="innerTable">
                            <tr>
                                <td style="padding: 0px;">
                                    <asp:TextBox ID="TB_ExamineSuggest" TextMode="MultiLine" CssClass="colwidth content" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 35px; padding: 0px;"><span class="wstar">*<asp:RequiredFieldValidator runat="server" ControlToValidate="TB_ExamineSuggest"
                            ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>             
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_Sure" CssClass="btn btn_sure" runat="server"  Text="确定" OnClick="Btn_Sure_Click" ValidationGroup="ProjectSubmit"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>