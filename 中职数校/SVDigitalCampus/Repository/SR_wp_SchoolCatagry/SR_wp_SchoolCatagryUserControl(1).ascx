<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SR_wp_SchoolCatagryUserControl.ascx.cs" Inherits="SVDigitalCampus.Repository.SR_wp_SchoolCatagry.SR_wp_SchoolCatagryUserControl" %>

<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<div style="margin: 20px;">
    <div style="float: left; width: 200px; height: auto;">
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ExpandDepth="1">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </div>
    <div class="right_add fl">
        <a href="#" class="right_add add" onclick="showDiv('Idiv', 'mou_head');"><i class="iconfont">&#xf000a;</i>添加</a>
    </div>
    <div class="Enterprise_information_feedback">
        <div style="float: left; width: 70%" class="Whole_display_area">
            <div class="Display_form" style="width: 100%">
                <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" Style="width: 100%;" OnItemCommand="ListView1_ItemCommand">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td colspan="3">没有数据</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                            <tr class="trth">
                                <th>选择</th>
                                <th>节点名称</th>
                                <th>节点类别</th>
                                <th>操作</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td>
                                <asp:CheckBox ID="ckNew" runat="server" /></td>
                            <td>
                                <asp:Label ID="lbName" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>

                            <td>
                                <asp:Label ID="lbCtype" runat="server" Text='<%#Eval("CType")%>'></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lbDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#x3474;</i></asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Double">
                            <td>
                                <asp:CheckBox ID="ckNew" runat="server" /></td>
                            <td>
                                <asp:Label ID="lbName" runat="server" Text='<%#Eval("Title")%>'></asp:Label></td>

                            <td>
                                <asp:Label ID="lbCtype" runat="server" Text='<%#Eval("CType")%>'></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lbDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>'><i class="iconfont">&#x3474;</i></asp:LinkButton></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
            </div>
            <div class="page">
                <asp:DataPager ID="DataPager1" runat="server" PageSize="8" PagedControlID="ListView1">
                    <Fields>
                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                        <asp:NextPreviousPagerField
                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                        <asp:TemplatePagerField>
                            <PagerTemplate>
                                <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                </span>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
    </div>

</div>
<div id="Idiv" class="blk" style="display: none; position: absolute; z-index: 1000; background: #67a3d9;">
    <div class="head">
        <div class="head-right"></div>
    </div>
    <div class="main" style="width: 600px; background-color: white; border: 1px solid #374760">
        <div style="height: 50px; background: #374760; line-height: 50px; font-size: 15px;">
            <span style="float: left; color: white; margin-left: 20px;">添加目录</span>
            <span style="float: right; margin-right: 20px;">
                <input type="button" value="关闭" onclick="closeDiv('Idiv')" />
            </span>
        </div>
        <div id="mou_head" style="z-index: 1001; position: absolute;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
        <div style="margin: 20px; border: 1px solid #374760">
            <table style="width: 100%; margin: 10px;">
                <tr style="border-bottom: 0">
                    <td class="lftd">名称：</td>
                    <td>

                        <asp:TextBox ID="Name" runat="server"></asp:TextBox></td>
                    <td colspan="2">
                        <asp:Button ID="Ok" runat="server" Text="确定" OnClick="Ok_Click" /></td>
                </tr>
            </table>

        </div>
    </div>
</div>
