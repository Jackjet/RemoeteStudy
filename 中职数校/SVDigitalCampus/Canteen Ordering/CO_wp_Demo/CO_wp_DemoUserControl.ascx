<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_DemoUserControl.ascx.cs" Inherits="SVDigitalCampus.食堂订餐.CO_wp_Demo.CO_wp_DemoUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<div id="order" class="orderdiv">
    <h1 class="Page_name">我的订单</h1>
    <div class="Whole_display_area">
        <div class="Operation_area">
            <table>
                <tr>
                    <td>日期从：</td>
                    <td>
                        <input type="text" class="Wdate" readonly="readonly" runat="server" id="txtdatebegin" onclick="WdatePicker()" />
                    </td>
                    <td>到：</td>
                    <td>
                        <input type="text" class="Wdate" readonly="readonly" runat="server" id="txtdateend" onclick="WdatePicker()" />
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </div>
        <div class="clear"></div>
        <div class="content">
            <h2 class="MO_total">总共：￥<asp:Label ID="Statistics" runat="server" Text="0"></asp:Label></h2>
            <div class="clear"></div>
            <asp:ListView ID="lvDateOrder" runat="server" OnPagePropertiesChanging="lvDateOrder_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <h2 class="myorderfood_tit">亲，暂无订单记录！</h2>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </LayoutTemplate>
                <ItemTemplate>

                    <h2 class="myorderDate_tit">下单日期：
                                <asp:Label ID="created" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                        合计：￥<%#Eval("Total") %></h2>

                    <asp:ListView ID="lv_MealTypeOrder" runat="server">
                        <EmptyDataTemplate>
                            亲，暂无三餐订单记录！
                        </EmptyDataTemplate>
                        <LayoutTemplate>

                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                        </LayoutTemplate>
                        <ItemTemplate>
                            <h2 class="myorderType_tit">
                                <span class="T_name"><%#Eval("Type") %></span>  金额：￥<%#Eval("Total") %>
                                <asp:HiddenField ID="created" runat="server" Value='<%#Eval("Created") %>' />
                                <asp:HiddenField ID="Type" runat="server" Value='<%#Eval("TypeID") %>' />
                            </h2>
                            <asp:ListView ID="lvorder" runat="server">
                                <EmptyDataTemplate>
                                    亲，暂无订单记录！ 
                                </EmptyDataTemplate>
                                <LayoutTemplate>

                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />

                                </LayoutTemplate>
                                <ItemTemplate>
                                    <h2 class="myorderfood_tit">订单编号 
                                <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>  金额：￥<%#Eval("Total") %></h2>
                                    <asp:ListView ID="lv_Order" runat="server" ItemPlaceholderID="MenuPlaceHolder">
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
                                                            <th class="Dishes">状态</th>
                                                            <th class="Subtotal">小计</th>
                                                        </tr>

                                                        <asp:PlaceHolder runat="server" ID="MenuPlaceHolder" />

                                                    </table>
                                                </div>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
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
                                                <td class="Price">
                                                    <label><%#Eval("Status") %></label></td>
                                                <td class="Subtotal">
                                                    <label>￥<%#Eval("Subtotal") %></label></td>
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
                                                <td class="Price">
                                                    <label><%#Eval("Status") %></label></td>
                                                <td class="Subtotal">
                                                    <label>￥<%#Eval("Subtotal") %></label></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </ItemTemplate>
                            </asp:ListView>

                        </ItemTemplate>
                    </asp:ListView>

                </ItemTemplate>
            </asp:ListView>
            <div class="paging">

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
