<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CE_wp_WdScoreUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.CE.CE_wp_WdScore.CE_wp_WdScoreUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/StuAssociate.css" rel="stylesheet" type="text/css" />
<style type="text/css">    
    td{ vertical-align:middle; }
    .title { padding:0 5px;width:390px;height:100px }
</style>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#TB_Score").change(function () {
            var va = parseInt($(this).val());
            if (va > 0 && va < 5) {
                var star = "";
                for (var i = 0; i < va; i++) {
                    star += "★";
                }
                $("#LB_Star").text(star);
            }
            else {
                alert("请输入规定范围内分数");
            }
        });
    })
</script>
<div class="windowDiv">
    <table class="winTable">
        <tr>
            <td class="wordsinfo">课程：</td>
            <td class="ku"><asp:TextBox ID="TB_Title" runat="server" Width="390px" /></td>
        </tr>
        <tr>
            <td class="wordsinfo">得分：</td>
            <td class="ku"><asp:TextBox ID="TB_Score" runat="server" ClientIDMode="Static" />分(0~5)<span style="margin-left:20px;color:seagreen"><asp:Label runat="server" ClientIDMode="Static" ID="LB_Star" /></span></td>
        </tr>
        <tr>
            <td class="wordsinfo">学习评论：</td>
            <td class="ku"><asp:TextBox ID="TB_Text" TextMode="MultiLine" CssClass="title" runat="server" /></td>
        </tr>
    </table>
    <div class="t_btn">
        <asp:Button ID="Btn_Sure" CssClass="btn btn_sure" runat="server"  Text="确定" OnClick="Btn_Sure_Click"/>
        <input type="button" class="btn btn_cancel" value="取消" onclick="parent.closePages();" />
    </div>
</div>