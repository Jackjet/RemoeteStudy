<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XB_SchoolMajorUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XB.XB_SchoolMajor.XB_SchoolMajorUserControl" %>
<script src="../../../../_layouts/15/Script/jquery-1.8.0.js"></script>
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/xb/tz_slider.js"></script>

<style type="text/css">
    .add_input {
        padding: 10px;
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
                            <div class="Add_list">
                                <span class="select">
                                    <asp:DropDownList ID="dpMajor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dpMajor_SelectedIndexChanged">
                                        <asp:ListItem Value="1">专业</asp:ListItem>
                                        <asp:ListItem Value="2">学科</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                                <span class="select" id="dpSubject1" runat="server" visible="false">
                                    <asp:DropDownList ID="dpSubject" runat="server" DataTextField="Title" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="dpSubject_SelectedIndexChanged">
                                        <asp:ListItem Value="0">请选择专业</asp:ListItem>

                                    </asp:DropDownList>
                                </span>
                                <span class="Search">
                                    <input type="text" placeholder="请输入名称" value="" class="add_sear" style="height: 25px" runat="server" id="txtName"></span>
                                <span class="add">
                                    <asp:Button ID="btadd" runat="server" Text="添加" OnClick="btadd_Click" CssClass="b_add" />
                                </span>
                            </div>
                            <table class="add_internship">
                                <asp:DataList ID="lvjob" runat="server" RepeatColumns="3" RepeatLayout="Flow" OnItemCommand="lvjob_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                            <td class="tt">
                                                <%-- <span class="m">--%>
                                                <asp:TextBox ID="tbJob" runat="server" Text='<%#Eval("Title") %>' CssClass="add_sear"></asp:TextBox><%--</span>--%>
                                                <i style="margin-left: -22px; color: #5493d7">
                                                    <asp:LinkButton ID="lbDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe65c;</i></asp:LinkButton></i>

                                                <i style="margin-left: -44px; color: #5493d7">
                                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe60a;</i></asp:LinkButton></i>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:DataList>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</div>
