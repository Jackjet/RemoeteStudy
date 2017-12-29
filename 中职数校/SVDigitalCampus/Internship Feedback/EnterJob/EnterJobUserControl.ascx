<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterJobUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.EnterJob.EnterJobUserControl" %>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>

<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<style type="text/css">
    .add_input {
        padding: 10px;
    }

        .add_input input.add_sear {
            padding: 0 10px;
            border-radius: 3px;
            height: 28px;
        }

        .add_input input.b_add {
            padding: 0 10px;
            border-radius: 3px;
            height: 28px;
            background: #5493d7;
            border: none;
            color: #fff;
            cursor: pointer;
        }

    table {
        border-collapse: collapse;
    }

    .add_sear {
        padding: 0 10px;
        border-radius: 3px;
        height: 28px;
    }
</style>

<div class="yy_dialog" style="top: 0px; left: 0px;">

    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">
            <div class="content">
                <div class="internship">
                    <div class="t_message">
                        <div class="message_con">
                            <div class="add_input">
                                <input type="text" class="add_sear" id="txtJob" value="" placeholder="请输入职位名称" runat="server">
                                <asp:Button ID="Button1" runat="server" CssClass="b_add" Text="添加" OnClick="Button1_Click" />
                            </div>
                            <asp:ListView ID="lvjob" runat="server" OnItemCommand="lvjob_ItemCommand" OnItemEditing="lvjob_ItemEditing">
                                <EmptyDataTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="3"></td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table id="itemPlaceholderContainer" class="add_internship">

                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                        <td class="tt"><span class="m">
                                            <asp:TextBox ID="tbJob" runat="server" Text='<%#Eval("Title") %>' CssClass="add_sear"></asp:TextBox></span>
                                            <i style="margin-left: -22px; color: #5493d7">
                                                <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe631;</i></asp:LinkButton></i>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>
                        </div>

                    </div>

                </div>
            </div>
        </div>

    </div>
</div>
