<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_OrderInfoManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoManager.CO_wp_OrderInfoManagerUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<div id="order" class="Confirmation_order">
    <!--页面名称-->
    <h1 class="Page_name">订单管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <%-- <td>
                    <asp:Button ID="btnSure" runat="server" Text="确认订单" OnClick="btnSure_Click" /></td>--%>

            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="btnToday" runat="server" CssClass="Enable">今日订单</asp:LinkButton><asp:LinkButton ID="btnOld" runat="server" OnClick="btnOld_Click" CssClass="Disable">历史订单</asp:LinkButton><asp:LinkButton ID="btnAllOrder" runat="server" OnClick="btnAllOrder_Click" CssClass="Disable">所有订单</asp:LinkButton><asp:LinkButton ID="btnTotilByPic" runat="server" OnClick="btnTotilByPic_Click" CssClass="Disable">图表统计</asp:LinkButton></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <div class="content">
            <div class="OrderManager_form"> 
                <div class="my_order">
                 <div id="slide">
                        <!--菜单区域-->
                        <ul>
                <asp:ListView ID="lvDateOrder" runat="server" >
                    <EmptyDataTemplate>
                        <li>亲，暂无订单记录！</li>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <a class="order_click" href="#"><i><%#Eval("add") %></i>
                                <span class="Td_con"><span class="T_data">
                                    <asp:Label ID="created" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label></span></span>
                            </a>
                            <div class="order_con" style='display: <%#Eval("IsShow")%>'>
                                <asp:ListView ID="lvMealOrder" runat="server" ItemPlaceholderID="mealorderplaceholder">
                                    <EmptyDataTemplate>
                                        亲，暂无订单记录！
                               
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>

                                        <asp:PlaceHolder ID="mealorderplaceholder" runat="server"></asp:PlaceHolder>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="Food_order">
                                            <h2 class="food_tit"><span class="F_name"><%# Eval("OrderType") %></span><span><i><%# Eval("ordercount") %></i>份菜品</span>｜<span>总计<i>￥<%# Eval("totalmoney") %></i></span>
                                                <asp:HiddenField ID="OrderTypeID" runat="server" Value='<%# Eval("OrderTypeID") %>' />
                                            </h2>
                                            <div class="clear"></div>

                                            <asp:ListView ID="lvorder" runat="server" OnItemCommand="lvorder_ItemCommand">
                                                <EmptyDataTemplate>
                                                    亲，暂无订单记录！
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="OM_form">
                                                        <tr class="OM_trth">
                                                            <!--表头tr名称-->
                                                            <th class="Dishes">编号</th>
                                                            <th class="Dishes">菜品</th>
                                                            <th class="Price">价格</th>
                                                            <th class="Number">数量</th>
                                                            <th class="Subtotal">小计</th>
                                                            <th class="Operation">操作</th>
                                                        </tr>

                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td class="Dishes"><%#Eval("ID")%>
                                                        </td>
                                                        <td class="Dishes"><%#Eval("Menu")%>
                                                        </td>
                                                        <td class="Price">￥<%#Eval("Price")%></td>
                                                        <td class="Type"><%#Eval("Number")%>
                                                        </td>
                                                        <td class="Subtotal"><%#Eval("Subtotal") %></td>
                                                        <td class="Operation">
                                                            <asp:HiddenField ID="hfOrderType" runat="server" Value='<%# Eval("OrderTypeID") %>' />
                                                            <asp:LinkButton ID="btndelete" CssClass="btncancle" CommandName="del" CommandArgument='<%# Eval("MenuID") %>' OnClientClick="return confirm('你确定取消吗？')" runat="server">取消</asp:LinkButton></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="Dishes"><%#Eval("ID")%>
                                                        </td>
                                                        <td class="Dishes"><%#Eval("Menu")%>
                                                        </td>
                                                        <td class="Price">￥<%#Eval("Price")%></td>
                                                        <td class="Type"><%#Eval("Number")%>
                                                        </td>
                                                        <td class="Subtotal"><%#Eval("Subtotal") %></td>
                                                        <td class="Operation">
                                                            <asp:HiddenField ID="hfOrderType" runat="server" Value='<%# Eval("OrderTypeID") %>' />
                                                            <asp:LinkButton ID="btndelete" CssClass="btncancle" CommandName="del" CommandArgument='<%# Eval("MenuID") %>' OnClientClick="return confirm('你确定取消吗？')" runat="server">取消</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
                            </ul>
                     </div>
                    </div>
            </div>
        </div>
    </div>
</div>
