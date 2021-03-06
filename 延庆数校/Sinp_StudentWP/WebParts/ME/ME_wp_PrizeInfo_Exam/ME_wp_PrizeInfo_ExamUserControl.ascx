﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_PrizeInfo_ExamUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_PrizeInfo_Exam.ME_wp_PrizeInfo_ExamUserControl" %>
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
            <td class="wordsinfo">姓名：</td>
            <td class="ku"><asp:Label runat="server" ID="lbName"></asp:Label> </td>
            <td rowspan="3" style="width:120px; text-align: center;">
                <asp:Image ID="img_Pic"  CssClass="imgAssoci" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="wordsinfo">获奖名称：</td>
            <td class="ku"><asp:Label runat="server" ID="lbTitle"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">级别等级：</td>
            <td class="ku"><asp:Label runat="server" ID="lbLevel"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">获奖日期：</td>
            <td class="ku"><asp:Label runat="server" ID="lbDate"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">颁奖单位：</td>
            <td colspan="2" class="ku"><asp:Label runat="server" ID="lbUnit"></asp:Label></td>
        </tr>
        <tr>
            <td class="wordsinfo">审核意见：</td>
            <td colspan="2" class="ku">            
               <asp:TextBox ID="txtExamineSuggest" TextMode="MultiLine"  class="unvaliwidth" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_Sure" CssClass="btn btn_sure" runat="server"  Text="确定" OnClick="Btn_Sure_Click"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>
