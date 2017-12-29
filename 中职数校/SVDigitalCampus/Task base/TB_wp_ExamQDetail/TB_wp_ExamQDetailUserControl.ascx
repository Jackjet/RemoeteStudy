<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TB_wp_ExamQDetailUserControl.ascx.cs" Inherits="SVDigitalCampus.Task_base.TB_wp_ExamQDetail.TB_wp_ExamQDetailUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/KindUeditor/themes/default/default.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<form>
    <div class="Question">
        <table class="tbEdit">
            <tr>
                <td class="mi">专业：</td>
                <td>
                    <asp:Label ID="Major" runat="server" Text="Major"></asp:Label></td>
                <td class="mi">学科：</td>
                <td>
                    <asp:Label ID="Subject" runat="server" Text="Subject"></asp:Label>
                </td>
                <td class="mi">章：</td>
                <td>
                    <asp:Label ID="Chapter" runat="server" Text="Chapter"></asp:Label></td>
                <td class="mi">节：</td>
                <td>
                    <asp:Label ID="Part" runat="server" Text="Part"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="c_line">
        </table>
        <table class="tbEdit">
            <tr>
                <td class="mi">知 识 点：</td>
                <td>
                    <asp:Label ID="Point" runat="server" Text="Point"></asp:Label></td>
                <td class="mi">题    型：</td>
                <td>
                    <asp:Label ID="Type" runat="server" Text="Type"></asp:Label>
                </td>
                <td class="mi">难易程度：</td>
                <td>
                    <asp:Label ID="Difficulty" runat="server" Text="Difficulty"></asp:Label></td>
                <td class="mi">状    态：</td>
                <td>
                    <asp:Label ID="Status" runat="server" Text="Status"></asp:Label>
                </td>
            </tr>
             </table>
        <table class="c_line">
        </table>
        <table class="tbEdit">
            <tr>
                <td class="wen">标   题：</td>
                <td colspan="7" class="ku">
                    <asp:Label ID="Title" runat="server" Text="Title"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="wen">题   文：</td>
                <td colspan="7">
                    <asp:Label ID="Question" runat="server" Text="Question"></asp:Label>
                </td>
            </tr>
            <tr id="Optiontr" runat="server">
                <td id="optiontittd" runat="server" class="wen">选     项：</td>
                <td colspan="7" class="ku" id="optiontd" runat="server">
                    <div id="optiondiv">
                        <asp:Label ID="Option" runat="server" Text=""></asp:Label>
                    </div>

                </td>
            </tr>
            <tr>
                <td class="wen">参考答案：</td>
                <td colspan="7"> 
                    <div id="answerdiv">
                        <asp:Label ID="answer" runat="server" Text="answer"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="wen">解    析：</td>
                <td colspan="7">
                    <asp:Label ID="Analysis" runat="server" Text="Analysis"></asp:Label>
                </td>
            </tr>

        </table>
    </div>
</form>


