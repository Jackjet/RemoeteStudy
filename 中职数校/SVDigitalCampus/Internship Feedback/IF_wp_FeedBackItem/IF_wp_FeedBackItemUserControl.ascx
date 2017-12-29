<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IF_wp_FeedBackItemUserControl.ascx.cs" Inherits="SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackItem.IF_wp_FeedBackItemUserControl" %>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/popwin.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/F5.js"></script>
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
    function view() {
        $.webox({
            width: 600, height: 578, bgvisibel: true, title: '反馈表预览', iframe: '<%=rootUrl%>' + "/SitePages/IF_wp_FeedBackModol.aspx?Type=0&" + Math.random + "'"
        });
        //popWin.showWin("600", "578", "反馈表预览", '<%=rootUrl%>' + "/SitePages/IF_wp_FeedBackModol.aspx?Type=0", "no");
    }
    function FeedB() {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        window.location.href = FirstUrl + "/SitePages/FeedList.aspx";
    }
</script>

<div class="Enterprise_information_feedback">
    <h1 class="Page_name"><a style="text-decoration:underline; color:blue;" onclick="FeedB()">反馈信息管理</a>反馈表项目管理</h1>
    <div class="Operation_area">
        <div class="left_choice fl">
            <ul>
                <li class="Batch_operation"></li>
            </ul>
        </div>
        <div class="right_add fr">
            <a href="#" class="add" onclick="view()">预览</a>
        </div>
    </div>
    <div class="clear"></div>

    <div class="Display_form">
        <asp:ListView ID="LV_TermList" runat="server" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing">
            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td colspan="3">暂无反馈项目信息。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                    <tr class="trth">
                        <th>项目名称</th>
                        <th>项目类型</th>
                        <th>项目内容</th>
                        <th>项目排序</th>
                        <th>操作</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">

                    <td>
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                        <%#Eval("Title")%></td>
                    <td><%#Eval("Type")%></td>
                    <td><%#Eval("Result")%></td>
                    <td><%#Eval("Sorts")%></td>
                    <td class="Operation">
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe64c;</asp:LinkButton></i></td>

                    </td>
                </tr>

            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="Double">

                    <td>
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label><%#Eval("Title")%></td>
                    <td><%#Eval("Type")%></td>
                    <td><%#Eval("Result")%></td>
                    <td><%#Eval("Sorts")%></td>
                    <td class="Operation">
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#xe64c;</i></asp:LinkButton></td>

                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>
    </div>
</div>
<div id="Editdiv" class="yy_dialog" style="display: none; position: absolute; z-index: 1000; background: #67a3d9; box-shadow: 0px 0px 5px #999; border: 1px solid #5493d7; width: 600px;">
    <div class="t_title">
        <h3 class="t_h3">反馈表修改</h3>
        <a href="#" class="t_close" onclick="closeDiv('Editdiv')">X</a>
    </div>
    <div class="t_content">
        <div class="yy_tab">
            <div class="content">
                <div class="tc">
                    <div class="t_message">
                        <div class="message_con">
                            <table class="m_top">

                                <tr>
                                    <td class="mi">项目编码：</td>

                                    <td class="ku">
                                        <input id="lbCode" runat="server" class="hu" disabled="disabled" />
                                        <asp:Label ID="EditID" runat="server" Text="" Visible="false"></asp:Label><span style="color: red"></span>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="mi">项目名称：</td>
                                    <td class="ku">
                                        <input id="lbNewName" runat="server" class="hu" placeholder=" 请输入名称" /><span style="color: red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi">项目类型：</td>
                                    <td class="ku">
                                        <asp:DropDownList ID="Type" runat="server" Width="200px">
                                            <asp:ListItem>输入</asp:ListItem>
                                            <asp:ListItem>单选</asp:ListItem>
                                        </asp:DropDownList><span style="color: red"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi">项目内容：</td>
                                    <td class="ku">
                                        <input id="Result" runat="server" class="hu" placeholder=" 请输入结果" /><span style="color: red"></span>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="mi">项目排序：</td>
                                    <td class="ku">
                                        <input id="Sorts" runat="server" class="hu" placeholder=" 请输入序列号" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" /><span style="color: red">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="t_btn">
                                        <asp:Button ID="Edit" runat="server" Text="保存信息" OnClick="Edit_Click" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%--<div id="Viewdiv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #5493D7;">
    <div class="head">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #5493D7; box-shadow: 0 0 5px #999;">
        <div style="height: 40px; background: #5493D7; line-height: 40px; font-size: 14px;">
            <span style="float: left; color: white; margin-left: 20px; font-size: 16px;">反馈表预览</span>
            <span style="float: right; margin-right: 10px; line-height: 34px;">
                <input type="button" value="X" onclick="closeDiv('Viewdiv')" style="border-color: #5493D7; background-color: #5493D7; height: 40px; font-size: 16px; color: #fff;" /></span>
        </div>
        <div id="View_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="padding: 20px;">
            <table class="feedback" id="FeedBack" runat="server">
                <tr>
                    <td align="center" colspan="6" style="font-weight: bold; font-size: 16px;">
                        <asp:Label ID="lbID" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="lbenter" runat="server" Text=""></asp:Label>实习生鉴定表                      
                               
                    </td>
                </tr>
                <tr>
                    <td style="width: 71px; text-align: left;"></td>
                    <td style="width: 100px;"></td>
                    <td style="width: 55px; text-align: right;"></td>
                    <td style="width: 90px"></td>
                    <td style="width: 80px; text-align: right;"></td>
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
                        <input type="button" class="btn" value="提交" />
                        <input type="reset" class="btn" value="取消重填" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</div>--%>
