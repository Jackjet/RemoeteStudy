<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_FeedBackModolUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackModol.IF_wp_FeedBackModolUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/FeedBack.css" rel="stylesheet" media="print" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery.jqprint.js"></script>
<style type="text/css">
    .feedback {
        width: 100%;
        margin-left: 20px;
    }

        .feedback input {
            padding: 0 2px;
            margin-left: 5px;
            border-radius: 3px;
            line-height: 10px;
        }

        .feedback textarea {
            padding: 0 2px;
            margin-left: 2px;
            border-radius: 3px;
            border: 1px solid #ccc;
        }

        .feedback tr {
            height: 30px;
            line-height: 44px;
        }

        .feedback .btn {
            color: #fff;
            width: 100px;
            border: none;
            border-radius: 3px;
            padding: 6px 0;
            font-size: 14px;
            font-family: "微软雅黑";
            background: #96cc66;
            cursor: pointer;
        }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=print.ClientID%>").click(function () {
            $(".yy_dialog").jqprint({
                debug: false, //如果是true则可以显示iframe查看效果（iframe默认高和宽都很小，可以再源码中调大），默认是false
                importCSS: true, //true表示引进原来的页面的css，默认是true。（如果是true，先会找$("link[media=print]")，若没有会去找$("link")中的css文件）
                printContainer: true, //表示如果原来选择的对象必须被纳入打印（注意：设置为false可能会打破你的CSS规则）。
                operaSupport: true//表示如果插件也必须支持歌opera浏览器，在这种情况下，它提供了建立一个临时的打印选项卡。默认是true
            });
        })
    });
</script>
<%--<input type="button" id="bt" onclick="javascript: printpage('myDiv')"   value="DIV打印" />--%>
<object id="WebBrowser1" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" name="wb"></object>
<div class="yy_dialog" id="Viewdiv" style="top: 0px; left: 0px;">
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #5493D7; box-shadow: 0 0 5px #999;">
        <%--<div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 14px;">
            <span style="float: left; color: white; margin-left: 20px; font-size: 16px;">反馈表预览</span>
            <span style="float: right; margin-right: 10px; line-height: 34px;">
                <input type="button" value="X" onclick="closeDiv('Viewdiv')" style="border-color: #5493D7; background-color: #5493D7; height: 40px; font-size: 16px; color: #fff;" /></span>
        </div>--%>
        <div id="View_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="padding: 10px;">
            <table class="feedback" id="FeedBack" runat="server">
                <tr>
                    <td align="center" colspan="6" style="font-weight: bold; font-size: 16px;">
                        <asp:Label ID="lben" runat="server" Text=""></asp:Label>实习生鉴定表                     
                               
                    </td>
                </tr>
                <tr>
                    <td style="width: 71px; text-align: left;"></td>
                    <td style="width: 20%;"></td>
                    <td style="width: 15%; text-align: right;"></td>
                    <td style="width: 20%"></td>
                    <td style="width: 15%; text-align: right;"></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td style="text-align: left;"></td>
                    <td colspan="5"></td>
                </tr>

                <tr>
                    <td style="text-align: center;" colspan="6"></td>
                </tr>

                <tr>
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center;">
                        <asp:Button ID="btOK" runat="server" Text="提交" OnClick="btOK_Click" CssClass="btn" />

                        <input type="reset" class="btn" value="取消重填" id="reset" runat="server" />
                        <input type="button" class="btn" value="打印预览" id="print" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</div>
