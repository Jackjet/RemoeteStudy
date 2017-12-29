<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_IndexUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_Index.CO_wp_IndexUserControl" %>
<link href="../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>

<script type="text/javascript">
    function SysSet() {
        popWin.scrolling = "auto";
        popWin.showWin("600", "520", "基础项配置", '<%=SietUrl%>' + "SystemSetEdit.aspx");
     }
</script>
<!--订餐-->
<div class="Ordering">
    <!--页面名称-->
    <%--<h1 class="Page_name">订餐</h1>--%>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="information_top">
            <div class="Food_info">
                 <div class="information_Food">
                    <h2 class="food_tit fl">
                        <span class="info_nz"><%=CanteenTitle%><i class="State yy_on" id="status"><%=CanteenWorkStatus %></i>  </span>                      
                         <span class="times_zt"><i class="iconfont">&#xe626;</i>营业时间:<em class="Time_slot"><%=CanteenWorkTime%></em></span>
                         <span class="address_zt"><i class="iconfont">&#xe610;</i>食堂地址:<em class="Address"><%=CanteenAddress%></em></span>
                    </h2>
                     <a href="javascript:void(0);" onclick="SysSet();" id="hrefOrderSys" runat="server" class="sysbtn fr">食堂信息设置</a>
                    
                </div>
            </div>
           
        </div>
        <div class="clear"></div>
        <div class="information_bottom">
            <!---选项卡-->
            <div class="dc_tab fl">
                <div class="dc_tabheader">
                    <ul class="tab_tit">
                        <%switch (action)
                          {
                              case "Monday":
              
                        %><li class="selected"><a href="OrderIndex.aspx?action=Monday">周一</a></li>
                        <li><a href="OrderIndex.aspx?action=Tuesday">周二</a></li>
                        <li><a href="OrderIndex.aspx?action=Wednesday">周三</a></li>
                        <li><a href="OrderIndex.aspx?action=Thursday">周四</a></li>
                        <li><a href="OrderIndex.aspx?action=Friday">周五</a></li>
                        <%break;
                              case "Tuesday":%>
                        <li><a href="OrderIndex.aspx?action=Monday">周一</a></li>
                        <li class="selected"><a href="OrderIndex.aspx?action=Tuesday">周二</a></li>
                        <li><a href="OrderIndex.aspx?action=Wednesday">周三</a></li>
                        <li><a href="OrderIndex.aspx?action=Thursday">周四</a></li>
                        <li><a href="OrderIndex.aspx?action=Friday">周五</a></li>
                        <%break;
                              case "Wednesday":%>
                        <li><a href="OrderIndex.aspx?action=Monday">周一</a></li>
                        <li><a href="OrderIndex.aspx?action=Tuesday">周二</a></li>
                        <li class="selected"><a href="OrderIndex.aspx?action=Wednesday">周三</a></li>
                        <li><a href="OrderIndex.aspx?action=Thursday">周四</a></li>
                        <li><a href="OrderIndex.aspx?action=Friday">周五</a></li>
                        <%break;
                              case "Thursday":%>
                        <li><a href="OrderIndex.aspx?action=Monday">周一</a></li>
                        <li><a href="OrderIndex.aspx?action=Tuesday">周二</a></li>
                        <li><a href="OrderIndex.aspx?action=Wednesday">周三</a></li>
                        <li class="selected"><a href="OrderIndex.aspx?action=Thursday">周四</a></li>
                        <li><a href="OrderIndex.aspx?action=Friday">周五</a></li>
                        <%break;
                              case "Friday":%>
                        <li><a href="OrderIndex.aspx?action=Monday">周一</a></li>
                        <li><a href="OrderIndex.aspx?action=Tuesday">周二</a></li>
                        <li><a href="OrderIndex.aspx?action=Wednesday">周三</a></li>
                        <li><a href="OrderIndex.aspx?action=Thursday">周四</a></li>
                        <li class="selected"><a href="OrderIndex.aspx?action=Friday">周五</a></li>
                        <%break;
                          } %>
                    </ul>
                </div>
                <div class="content">
                    <!--周一-->
                    <div class="dc">
                        <div class="Screening_conditions">
                            <dl class="time_food">
                                <dt>时段：</dt>
                                <asp:ListView ID="lvMealType" runat="server" OnItemCommand="lvMealType_ItemCommand" ItemPlaceholderID="mealtypeplaceholder">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="mealtypeplaceholder"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <dd>
                                            <asp:LinkButton ID="btnMealType" runat="server" CssClass=" btn" CommandArgument='<%#Eval("ID") %>' CommandName="MealTypeSearch"><%#Eval("Title") %></asp:LinkButton>
                                        </dd>
                                    </ItemTemplate>
                                </asp:ListView>
                            </dl>
                            <div class="clear"></div>
                            <dl class="dishes">
                                <dt>菜品：</dt>
                                <dd>
                                    <asp:LinkButton ID="txtAll" runat="server" CssClass="btn" OnClick="txtAll_Click">所有</asp:LinkButton></dd>
                                <asp:ListView ID="lvMenutype" runat="server" OnItemCommand="lvMenutype_ItemCommand" ItemPlaceholderID="menutypeplaceholder">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="menutypeplaceholder"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <dd>
                                            <asp:LinkButton ID="btntype" runat="server" CssClass=" btn" CommandArgument='<%#Eval("Type") %>' CommandName="TypeSearch"><%#Eval("Type") %></asp:LinkButton>
                                        </dd>
                                    </ItemTemplate>
                                </asp:ListView>
                            </dl>
                        </div>
                        <div class="clear"></div>
                        <!--菜品-->
                        <div class="food_con">
                            <ul class="foodcontent">
                                <asp:ListView ID="lvMenu" runat="server" OnItemCommand="lvMenu_Add" OnPagePropertiesChanging="lvMenu_PagePropertiesChanging" ItemPlaceholderID="itemPlaceholder" GroupItemCount="2">
                                    <EmptyDataTemplate>
                                        您好，暂无菜品记录！
                                    </EmptyDataTemplate>
                                    <GroupTemplate>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                    </GroupTemplate>
                                    <LayoutTemplate>

                                        <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                           
                                            <span class="img_food">
                                                <img src='<%#Eval("Picture")%>' />
                                            </span>
                                            <span class="name_food">
                                                <%#Eval("Title")%>&nbsp;<em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em><i class="iconfont"></i></span>
                                            <span class="description_food"><%#Eval("Description") %></span>
                                            <span class="money_food"><i class="shuzi">￥<%#Eval("Price")%></i><%--<a href="javascript:void(0);" onclick="addFood('<%# Eval("ID")+","+Eval("Date") %>');"><i class="iconfont fr jia">&#xf0087;</i></a>--%><%--<asp:Button runat="server" ID="btnAdd"  Style="" CssClass=" btn" CommandName="Add" CommandArgument='<%# Eval("ID") %>' />--%><asp:LinkButton ID="btnAdd" Style='<%# "display:"+Eval("IsShow")%>' CssClass="icoadd btn" runat="server" CommandName="Add" CommandArgument='<%# Eval("ID")+"&"+Eval("Date") %>'></asp:LinkButton>
                                                </span>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                            <!--分页-->
                            <div class="page">

                                <asp:DataPager ID="DPMenu" runat="server" PageSize="12" PagedControlID="lvMenu">
                                    <Fields>
                                        <asp:NextPreviousPagerField
                                            ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                            ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" ButtonCssClass="pageup" />

                                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                                        <asp:NextPreviousPagerField
                                            ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                            ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" ButtonCssClass="pagedown" />
                                        
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="count">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
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
            </div>
             <div id="Restaurant_announcement" class="fr">
                <div class="Announcement">
                    <h3 class="announce_tit fl">食堂公告</h3>
                    <ul class="lub fr">
                        <li class="hover"></li>
                    </ul>
                    <div class="tuu">
                        <asp:ListView ID="lv_Notice" runat="server" ItemPlaceholderID="NoticeItem">
                            <EmptyDataTemplate>
                                <ul class="announce_con">
                                    <li>您好，暂无公告！</li>
                                </ul>
                            </EmptyDataTemplate>
                            <LayoutTemplate>

                                <asp:PlaceHolder runat="server" ID="NoticeItem" />

                            </LayoutTemplate>
                            <ItemTemplate>
                                <ul class="announce_con">
                                    <li><%#Eval("Content") %></li>
                                </ul>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
    <!--购物车-->
    <div class="Shopping_cart" runat="server" id="Cart">
                <div class="shopping">
                    <!--购物车 数量-->
                    <h1><span class="shop_name fl">购物车</span><asp:Label ID="Totalcount" CssClass="shop_num fr" runat="server" Text="0"></asp:Label></h1>
                    <div class="clear"></div>
                    <!--购物车订单-->
                    <div class="shopping_food">
                        <!--菜单区域-->
                        <div id="menubox" class="shop_menu">
                            <ul>
                                <li>
                                    <a class="menuclick" href="#">
                                        <span class="lz_name fl"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("1")%> </span>
                                        <span class="rz_con fr"><span class="num_num z_num">
                                            <asp:Label ID="Morningcount" runat="server" Text="0"></asp:Label></span>
                                            <span class="z_total">小计：<em class="money_num">
                                               ￥<asp:Label ID="Morningmoney" runat="server" Text="0"></asp:Label></em></span></span>
                                    </a>
                                    <ul class="submenu" style='display: <%= morningcartIsshow %>'>
                                        <%--<%=morningcartIsshow %>--%>
                                        <asp:ListView ID="lv_MorningCart" runat="server" ItemPlaceholderID="MorningCartItem" OnItemCommand="lv_MorningCart_ItemCommand">
                                            <EmptyDataTemplate>
                                                您好，暂无该餐购物车记录！
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <asp:PlaceHolder runat="server" ID="MorningCartItem" />

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <span class="d_dish"><%#Eval("Menu") %></span>
                                                    <span class="d_jiajian">
                                                        <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                                        <em>
                                                            <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',1);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                            <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                                        </em>
                                                        <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                                    </span>
                                                    <span class="d_money">￥<%#Eval("Total") %></span>
                                                    <span class="d_delete">
                                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><em class="iconfont d_d">&#xe627;</em></asp:LinkButton>

                                                        <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' />
                                                    </span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                </li>
                                <li>
                                    <a class="menuclick" href="#">
                                        <span class="lz_name fl"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("2")%></span>
                                        <span class="rz_con fr"><span class="num_num z_num">
                                            <asp:Label ID="Lunchcount" runat="server" Text="0"></asp:Label></span>
                                            <span class="z_total">小计：<em class="money_num">
                                                ￥<asp:Label ID="Lunchmoney" runat="server" Text="0"></asp:Label></em></span></span>
                                    </a>

                                    <ul class="submenu" style='display: <%= lunchcartIsshow%>'>
                                        <%--<%=lunchcartIsshow %>--%>
                                        <asp:ListView ID="lv_LunchCart" runat="server" ItemPlaceholderID="LunchCartItem" OnItemCommand="lv_LunchCart_ItemCommand">
                                            <EmptyDataTemplate>
                                                您好，暂无该餐购物车记录！
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <asp:PlaceHolder runat="server" ID="LunchCartItem" />

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <span class="d_dish"><%#Eval("Menu") %></span><span class="d_jiajian">

                                                        <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,2,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>

                                                        <em>
                                                            <input id="txtNumber" onblur=<%#"CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',2);" %> class="num" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" value='<%#Eval("Number") %>' type="text" runat="server" />
                                                            <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' /></em>

                                                        <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,2,"+ Eval("ID") +","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                                    </span>
                                                    <span >￥<%#Eval("Total") %></span>
                                                    <span class="d_delete">
                                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><em class="iconfont d_d">&#xe627;</em></asp:LinkButton>
                                                        <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' />
                                                    </span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                </li>
                                <li>
                                    <a class="menuclick" href="#">
                                        <span class="lz_name fl"><%=SVDigitalCampus.Common.MealTypeJudge.GetMealTypeShow("3")%></span>
                                        <span class="rz_con fr"><span class="num_num z_num">
                                            <asp:Label ID="Dinnercount" runat="server" Text="0"></asp:Label></span>
                                            <span class="z_total">小计：<em class="money_num">
                                                ￥<asp:Label ID="Dinnermoney" runat="server" Text="0"></asp:Label></em></span></span>
                                    </a>
                                    <ul class="submenu" style='display: <%=dinnercartIsshow %>'>
                                        <%--<%=dinnercartIsshow %>--%>
                                        <asp:ListView ID="lv_DinnerCart" runat="server" ItemPlaceholderID="DinnerCartItem" OnItemCommand="lv_DinnerCart_ItemCommand">
                                            <EmptyDataTemplate>
                                                您好，暂无该餐购物车记录！
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <asp:PlaceHolder runat="server" ID="DinnerCartItem" />

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <span class="d_dish"><%#Eval("Menu") %></span><span class="d_jiajian">
                                                        <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,3,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                                        <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                                        <em>
                                                            <input id="txtNumber" onblur=<%#"CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',3);" %> class="num" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" value='<%#Eval("Number") %>' runat="server" type="text" />
                                                            <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' /></em>
                                                        <%--<a href="#" class="d_jia" onclick="AddNum(this,3,'<%# Eval("ID") %>','<%#Eval("Price") %>');">+</a>--%>
                                                        <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                                    </span>
                                                    <span >￥<%#Eval("Total") %></span>
                                                    <span class="d_delete">
                                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><em class="iconfont d_d">&#xe627;</em></asp:LinkButton>
                                                        <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <!--end 菜单区域-->

                    </div>
                    <div class="clear"></div>
                    <!--总计钱数 提交-->
                    <div class="Total">
                        <span class="total_money fl">总计：<em class="total_num"><asp:Label ID="totalmoney" runat="server" Text="Label"></asp:Label></em></span><span class="total_submit fr">
                            <asp:LinkButton ID="txtToOrder" runat="server" OnClick="txtToOrder_Click">提交订单</asp:LinkButton></span>
                    </div>
                </div>
     </div>
      
</div>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/OrderIndex.js"></script>
