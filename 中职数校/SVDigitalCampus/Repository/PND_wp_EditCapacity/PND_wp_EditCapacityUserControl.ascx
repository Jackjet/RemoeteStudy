<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PND_wp_EditCapacityUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.PND_wp_EditCapacity.PND_wp_EditCapacityUserControl" %>
<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>

<div id="Editdiv" class="yy_dialog" style="display: block; top: 0; left: 0">
    <div class="t_content" style="padding:20px;">
        <div class="yy_tab">
            <div class="content">
                <div class="t_message">
                    <div class="message_con">

                        <table>
                            <tr id="user" runat="server">
                                <td class="mi"><span class="m">使用人：</span></td>
                                <td class="kuc">
                                    <asp:Label ID="lbUser" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="mi"><span class="m">原设置值：</span></td>
                                <td class="kuc">
                                    <asp:Label ID="lbOldSet" runat="server" Text=""></asp:Label>M
                                </td>
                            </tr>
                            <tr>
                                <td class="mi"><span class="m">追加容量：</span></td>
                                <td class="kuc">
                                    <asp:TextBox ID="tbAddSet" runat="server"></asp:TextBox>M
                                </td>
                            </tr>
                            <tr>
                                <td class="t_btn" colspan="2">
                                    <asp:Button ID="btOk" runat="server" Text="保存" OnClick="btOk_Click" /></td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
        </div>

    </div>

</div>
