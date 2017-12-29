<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterEditUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.EnterEdit.EnterEditUserControl" %>

<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>

<style type="text/css">
    .tip {
        color: red;
        display: block;
        position: absolute;
        left: 220px;
    }
</style>
<div id="Editdiv" class="yy_dialog" style="display: block;top: 0; left: 0">
    <%--<div class="t_title">
        <h3 class="t_h3">修改企业信息</h3>
        <a href="#" class="t_close" onclick="closeDiv('Editdiv')">X</a>
    </div>
    <div id="Edit_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>--%>

    <div class="t_content">
        <div class="yy_tab">
            <div class="content">
                <div class="tc">
                    <div class="t_message">
                        <div class="message_con">

                            <table class="m_top" id="edit">
                                <tr>
                                    <td class="mi"><span class="m">企业名称：</span></td>
                                    <td class="ku">
                                        <asp:Label ID="isEdit" runat="server" Text="0" Visible="false"></asp:Label>
                                        <asp:Label ID="lbEnterID" runat="server" Text="" Visible="false"></asp:Label>

                                        <input id="txtName1" runat="server" class="hu" placeholder=" 请输入企业名称" onchange="confirmE(this)" />
                                        <i class="iconfont tishi true_t">&#xe654;</i></td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">企业账号：</span></td>
                                    <td class="ku">
                                        <input type="text" id="UserID1" runat="server" class="hu" placeholder=" 请输入企业账号" onchange="confirmE(this)">
                                        <i class="iconfont tishi true_t">&#xe654;</i></td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">密码：</span></td>
                                    <td class="ku">
                                        <input type="text" placeholder=" 请输入密码" id="UserPwd1" runat="server" onchange="confirmE(this)">
                                        <i class="iconfont tishi true_t">&#xe654;</i></td>
                                </tr>
                            </table>
                            <table class="c_line">
                            </table>
                            <table class="m_bottom">
                                <tr>
                                    <td class="mi"><span class="m">负责人：</span></td>
                                    <td class="ku">
                                        <input type="text" class="hu" placeholder=" 请输入负责人名称" id="RelationName1" runat="server" onchange="confirmE(this)">
                                        <i class="iconfont tishi true_t">&#xe654;</i></td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">电话：</span></td>
                                    <td class="ku">
                                        <input id="txtPhone1" runat="server" placeholder=" 请输入电话" onchange="phone(this)" />
                                        <i class="iconfont tishi true_t">&#xe654;</i>
                                        <span class="tip"></span>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi"><span class="m">电子邮箱：</span></td>
                                    <td class="ku">
                                        <input id="tbEmail1" runat="server" onchange="Email(this)" placeholder=" 请输入邮箱" />
                                        <i class="iconfont tishi true_t">&#xe654;</i>
                                        <span class="tip"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <div class="t_btn">
                        <asp:Button ID="btEdit" runat="server" Text="保存" OnClientClick="return isTrue()" OnClick="btEdit_Click" />
                    </div>
                </div>

            </div>
        </div>

    </div>

</div>