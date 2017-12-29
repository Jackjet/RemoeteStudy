<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_OrderInfoListOfAdminUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoListOfAdmin.CO_wp_OrderInfoListOfAdminUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<div id="order" class="orderdiv">
    <div class="Confirmation_order">
        <!--页面名称-->
        <h1 class="Page_name">订单管理</h1>
        <!--整个展示区域-->
        <div class="Whole_display_area">
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="">
                            <div class="qiehuan fl">
                                <span class="qijin">
                                    <asp:LinkButton ID="btnToday" runat="server" OnClick="btnToday_Click" CssClass="Disable">今日订单</asp:LinkButton><asp:LinkButton ID="btnOld" runat="server" CssClass="Enable">历史订单</asp:LinkButton><asp:LinkButton ID="btnAllOrder" runat="server" OnClick="btnAllOrder_Click" CssClass="Disable">所有订单</asp:LinkButton><asp:LinkButton ID="btnTotilByPic" runat="server" OnClick="btnTotilByPic_Click" CssClass="Disable">图表统计</asp:LinkButton></span>
                            </div>
                        </li>

                    </ul>
                </div>
                <div class="right_add fr">
                </div>
            </div>
            <div class="clear"></div>
            <div class="times">
                <span class="times_input">
                    <input type="text" class="Wdate time" readonly="readonly" runat="server" id="txtdatebegin" onclick="WdatePicker()" /><i class="iconfont"></i></span>-
                        <span class="times_input">
                            <input type="text" class="Wdate time" readonly="readonly" runat="server" id="txtdateend" onclick="WdatePicker()" /><i class="iconfont"></i></span>
                <asp:Button ID="btnSearch" class="b_query" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </div>
            <div class="Display_form">
                <div class="my_order">
                    <h1>统计：<span class="o_num">订单<i><asp:Label ID="onum" runat="server" Text="0"></asp:Label></i>,</span><span class="o_menu">菜品<i><asp:Label ID="omenu" runat="server" Text="0"></asp:Label>份,</i></span><span class="o_count">总计<i><asp:Label ID="Totalnum" runat="server" Text="0"></asp:Label></i></span></h1>
                    <%--<h1 class="MO_total">统计：￥<asp:Label ID="Statistics" runat="server" Text="0"></asp:Label></h1>--%>
                    <div id="slide">
                        <!--菜单区域-->
                        <ul>

                            <asp:ListView ID="lvDateOrder" runat="server" OnPagePropertiesChanging="lvDateOrder_PagePropertiesChanging">
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
                                                <asp:Label ID="created" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label></span>｜<span class="T_num"><%#Eval("Count") %></i> 份菜品</span>  ｜<span class="T_total"> 总计 ￥<%#Eval("Total") %></span></span>
                                        </a>
                                        <div class="order_con" style='display: <%#Eval("IsShow")%>'>
                                            <asp:ListView ID="lv_MealTypeOrder" runat="server">
                                                <EmptyDataTemplate>
                                                    亲，暂无三餐订单记录！
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>

                                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <div class="Food_order">
                                                        <h2 class="food_tit">
                                                            <span class="F_name"><%#Eval("Type") %></span>  金额：￥<%#Eval("Total") %>
                                                            <asp:HiddenField ID="created" runat="server" Value='<%#Eval("OrderDate") %>' />
                                                            <asp:HiddenField ID="Type" runat="server" Value='<%#Eval("TypeID") %>' />
                                                        </h2>
                                                        <asp:ListView ID="lv_Order" runat="server">
                                                            <EmptyDataTemplate>
                                                                亲，暂无订单记录！ 
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>

                                                                <table class="O_form">
                                                                    <tr class="O_trth">
                                                                        <!--表头tr名称-->
                                                                        <!--表头tr名称-->
                                                                        <th class="Dishes">编号</th>
                                                                        <th class="Dishes">菜品</th>
                                                                        <th class="Number">数量</th>
                                                                        <th class="Price">价格</th>
                                                                        <th class="Subtotal">小计</th>
                                                                        <th class="Dishes">状态</th>
                                                                    </tr>

                                                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <%-- <h2 class="myorderfood_tit">订单编号 
                                <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>  金额：￥<%#Eval("Total") %></h2>--%>
                                                                <%--  <asp:ListView ID="lv_Order" runat="server" ItemPlaceholderID="MenuPlaceHolder">
                                        <EmptyDataTemplate>
                                            亲，暂无订单菜品记录！
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="MyOrder_form">
                                                <div class="Food_order">
                                                    <table class="MO_form">
                                                        <tr class="MO_trth">
                                                            <!--表头tr名称-->
                                                            <th class="Dishes">编号</th>
                                                            <th class="Dishes">菜品</th>
                                                            <th class="Number">数量</th>
                                                            <th class="Price">价格</th>
                                                            <th class="Subtotal">小计</th>
                                                            <th class="Dishes">状态</th>
                                                        </tr>

                                                        <asp:PlaceHolder runat="server" ID="MenuPlaceHolder" />

                                                    </table>
                                                </div>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>--%>
                                                                <tr class="Single">
                                                                    <td class="Dishes">
                                                                        <label><%#Eval("Count") %></label>
                                                                    </td>
                                                                    <td class="Dishes">
                                                                        <label><%#Eval("Menu") %></label></td>
                                                                    <td class="Number">
                                                                        <label><%#Eval("Number") %></label></td>
                                                                    <td class="Price">
                                                                        <label>￥<%#Eval("Price") %></label></td>
                                                                    <td class="Subtotal">
                                                                        <label>￥<%#Eval("Subtotal") %></label></td>
                                                                    <td class="Price">
                                                                        <label><%#Eval("Status") %></label></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="Double">
                                                                    <td class="Dishes">
                                                                        <label><%#Eval("Count") %></label>
                                                                    </td>
                                                                    <td class="Dishes">
                                                                        <label><%#Eval("Menu") %></label></td>
                                                                    <td class="Number">
                                                                        <label><%#Eval("Number") %></label></td>
                                                                    <td class="Price">
                                                                        <label>￥<%#Eval("Price") %></label></td>
                                                                    <td class="Subtotal">
                                                                        <label>￥<%#Eval("Subtotal") %></label></td>
                                                                    <td class="Price">
                                                                        <label><%#Eval("Status") %></label></td>
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
                    <%-- </ItemTemplate>
            </asp:ListView>--%>
                    <div class="page">

                        <asp:DataPager ID="dpOrderDate" runat="server" PageSize="4" PagedControlID="lvDateOrder">
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
                <div class="clear"></div>
            </div>

        </div>
    </div>

</div>
