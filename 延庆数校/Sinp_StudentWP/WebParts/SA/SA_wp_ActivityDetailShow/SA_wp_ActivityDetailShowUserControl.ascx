<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_ActivityDetailShowUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_ActivityDetailShow.SA_wp_ActivityDetailShowUserControl" %>
<link href="/_layouts/15/Stu_css/ico/iconfont.css" rel="stylesheet">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/animate.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/StuAssociate.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script>
    /*组卷百叶窗*/
    $(function () {
        $("#slide").find(".Topic_click").click(function () {
            $(this).parent().toggleClass("selected").siblings().removeClass("selected");
            $(this).next().slideToggle("fast").end().parent().siblings().find(".Topic_con").addClass("animated bounceIn").slideUp("fast").end().find("i").text("+");
            var t = $(this).find("i").text();
            $(this).find("i").text((t == "+" ? "-" : "+"));
        });
    });    
    function openPage(pageTitle, pageName, pageWidth, pageHeight) { 
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
</script>

<div class="act_detail">
    <ul>
        <li style="line-height:28px;">
            <div class="music_kc">
                <asp:Image runat="server" ID="Img_Activity" />
                <div class="music_nr">
                    <h2>
                        <asp:Literal ID="Lit_Title" runat="server" Text="--" /></h2>
                    <div>
                        <span><i class="iconfont">&#xe604;</i><asp:Literal ID="Lit_Date" runat="server" Text="--" /></span>
                        <span><i class="iconfont">&#xe607;</i><asp:Literal ID="Lit_Address" runat="server" Text="--" /></span>
                        <span><i class="iconfont">&#xe60b;</i><asp:Literal ID="Lit_Count" runat="server" Text="--" /></span>
                    </div>
                    <p id="P_Introduction" runat="server"></p>
                </div>
            </div>
        </li>
    </ul>
    <dl>
        <dt class="project_dt">项目信息：<asp:HiddenField ID="HF_RegistCount" runat="server"/></dt>
        <dd class="Topic_tcon">
            <div id="slide">
                <ul>
                    <asp:ListView ID="LV_ProjectList" runat="server" OnItemCommand="LV_ProjectList_ItemCommand" OnItemEditing="LV_ProjectList_ItemEditing">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>暂无活动项目</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="Topic_click">
                                    <span class="Topic_tit fl"><i class="fl">+</i><span class="tit_name"><%# Eval("Title") %></span></span><span class="Topic_btn fr"><span class="fl"><asp:HiddenField ID="HF_hasRegist" runat="server" Value='<%# Eval("hasRegist") %>' />
                                        <asp:LinkButton runat="server" CssClass="link_join" CommandName="Join" CommandArgument='<%# Eval("ID") %>' ID="JoinPro">报名</asp:LinkButton></span></span>
                                </div>
                                <div class="Topic_con" style="display: none;">
                                        <%# Eval("JoinMembers") %>
                                    </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </dd>
    </dl>
</div>
