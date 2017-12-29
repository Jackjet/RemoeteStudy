﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_AddPracticeEvaluateUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_AddPracticeEvaluate.SG_wp_AddPracticeEvaluateUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
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
            <td class="wordsinfo">学生姓名：</td>
            <td class="ku"><asp:Label runat="server" ID="lbName"></asp:Label> </td>
            <td rowspan="4" style="width:120px; text-align: center;padding-right:47px;">
                <asp:Image ID="img_Pic"  CssClass="imgAssoci" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">活动名称：</td>
            <td class="ku"><asp:Label runat="server" ID="lbActTitle"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">活动时间：</td>
            <td class="ku"><asp:Label runat="server" ID="lbDate"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">活动地点：</td>
            <td class="ku"><asp:Label runat="server" ID="lbAddress"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">活动内容：</td>
            <td colspan="2" class="ku">  
                 <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_Content" TextMode="MultiLine"  class="colwidth content" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td style="width:35px; padding: 0px;"></td>
                    </tr>
                </table>          
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">评价：</td>
            <td colspan="2" class="ku">
                <table class="innerTable">
                    <tr>
                        <td style="padding: 0px;">
                            <asp:TextBox ID="TB_Evaluate" TextMode="MultiLine"  class="colwidth content" runat="server"></asp:TextBox></td>
                        <td style="width:35px; padding: 0px;"><span style="color: red;">*<asp:RequiredFieldValidator ID="RFV_Evaluate" runat="server" ControlToValidate="TB_Evaluate"
                                ErrorMessage="必填" ValidationGroup="ProjectSubmit" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
                        </td>
                    </tr>
                </table>
            </td>           
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_EvaluateSave" CssClass="btn btn_sure" runat="server" ValidationGroup="ProjectSubmit"   Text="确定" OnClick="Btn_EvaluateSave_Click"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>
