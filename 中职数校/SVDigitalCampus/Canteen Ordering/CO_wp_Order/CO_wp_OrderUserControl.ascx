<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_OrderUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_Order.CO_wp_OrderUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>


<!--订餐-->
<div class="Confirmation_order">
    <!--页面名称-->
    <h1 class="Page_name">订餐</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--tab切换title-->
        <div class="Order_tab">
            <div class="Order_tabheader" id="ordertitle">
                <ul>
                    <li class="selected"><a href="OrderIndex.aspx"><span class="zong_tit"><span class="num_1">1</span><span class="add_li">挑选菜品</span></span></a></li>
                    <li class="selected"><a href="#"><span class="zong_tit"><span class="num_2">2</span><span class="add_li">确认订单</span></span></a></li>
                    <li><a href="#"><span class="zong_tit"><span class="iconfont num_3">&#xe654;</span><span class="add_li">完成</span></span></a></li>
                </ul>
            </div>
            <div class="content">
                <div class="tc" style="display: none">1</div>
                <div class="tc" style="display: block;">
                    <div class="Order_form">
                        <p class="Remind">提醒：如果要修改订单，请及时点击<a href="OrderIndex.aspx" class="O_back">[返回菜单点餐]</a>，剩余修改时间<span class="Timeshow">【<asp:Label ID="OrderTime" runat="server" Style="color: red;"></asp:Label>】</span>，过时将无法修改！</p>

                        <asp:ListView ID="lv_MorningOrder" runat="server" ItemPlaceholderID="MorningOrderItem" OnItemDataBound="lv_MorningOrder_ItemDataBound" OnItemCommand="lv_MorningOrder_ItemCommand">

                            <LayoutTemplate>
                                <div class="Food_order">

                                    <div class="clear"></div>
                                    <h2 class="food_tit">
                                        <asp:Label ID="MorningMealName" CssClass="F_name" runat="server"></asp:Label>
                                        <span><i>
                                            <asp:Label ID="Morningcount" runat="server" Text="0"></asp:Label></i>份菜品</span>｜<span>总计<i>￥<asp:Label ID="Morningmoney" runat="server" Text="0"></asp:Label></i></span> </h2>
                                    <table class="O_form">
                                        <tr class="O_trth">
                                            <!--表头tr名称-->
                                            <th class="Dishes">菜品</th>
                                            <th class="Type">类型</th>
                                            <th class="Price">单价</th>
                                            <th class="Number">数量</th>
                                            <th class="Subtotal">小计</th>
                                            <th class="Operation">操作</th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="MorningOrderItem" />

                                    </table>
                                </div>
                                <div class="clear"></div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',1);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal">￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',1);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,1,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal" >￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>

                        <asp:ListView ID="lv_LunchOrder" runat="server" ItemPlaceholderID="LunchOrderItem" OnItemDataBound="lv_LunchOrder_ItemDataBound" OnItemCommand="lv_LunchOrder_ItemCommand">

                            <LayoutTemplate>
                                <div class="Food_order">
                                    <h2 class="food_tit">
                                        <asp:Label ID="LunchMealName" CssClass="F_name" runat="server"></asp:Label><span><i><asp:Label ID="Lunchcount" runat="server" Text="0"></asp:Label></i>份菜品</span>｜<span>总计<i>￥<asp:Label ID="Lunchmoney" runat="server" Text="0"></asp:Label></i></span> </h2>
                                    <div class="clear"></div>
                                    <table class="O_form">
                                        <tr class="O_trth">
                                            <!--表头tr名称-->
                                            <th class="Dishes">菜品</th>
                                            <th class="Type">类型</th>
                                            <th class="Price">单价</th>
                                            <th class="Number">数量</th>
                                            <th class="Subtotal">小计</th>
                                            <th class="Operation">操作</th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="LunchOrderItem" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,2,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',2);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,2,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal">￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,2,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',2);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,2,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal">￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>

                        <asp:ListView ID="lv_DinnerOrder" runat="server" ItemPlaceholderID="DinnerOrderItem" OnItemDataBound="lv_DinnerOrder_ItemDataBound" OnItemCommand="lv_DinnerOrder_ItemCommand">

                            <LayoutTemplate>
                                <div class="Food_order">
                                    <h2 class="food_tit">
                                        <asp:Label ID="DinnerMealName" CssClass="F_name" runat="server"></asp:Label><span><i><asp:Label ID="Dinnercount" runat="server" Text="0"></asp:Label></i>份菜品</span>｜<span>总计<i>￥<asp:Label ID="Dinnermoney" runat="server" Text="0"></asp:Label></i></span> </h2>
                                    <div class="clear"></div>
                                    <table class="O_form">
                                        <tr class="O_trth">
                                            <!--表头tr名称-->
                                            <th class="Dishes">菜品</th>
                                            <th class="Type">类型</th>
                                            <th class="Price">单价</th>
                                            <th class="Number">数量</th>
                                            <th class="Subtotal">小计</th>
                                            <th class="Operation">操作</th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="DinnerOrderItem" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',3);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal">￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">
                                    <td class="Dishes"><%#Eval("Menu") %><i class="iconfont ladu"><em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em></i></td>
                                    <td class="Type"><%#Eval("Type") %></td>
                                    <td class="Price">￥<%#Eval("Price") %> <input id="price" type="hidden" runat="server" value='<%# Eval("Price") %>' /></td>
                                    <td class="Number">
                                        <span class="d_jiajian">
                                            <%-- <a href="#" class="d_jian" onclick="ReduceNum(this,1,'<%# Eval("ID") %>','<%#Eval("Price") %>');">-</a>--%>
                                            <asp:LinkButton ID="lbjian" class="d_jian" runat="server" OnClientClick=<%# "ReduceNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>-</asp:LinkButton>
                                            <em>
                                                <input id="txtNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'1')" onafterpaste="this.value=this.value.replace(/\D/g,'1')" onblur=<%# "CheckNumber(this,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"',3);" %> value='<%#Eval("Number") %>' runat="server" class="num" />
                                                <input type="hidden" id="oldnum" value='<%#Eval("Number") %>' />
                                            </em>
                                            <asp:LinkButton ID="lbjia" class="d_jia" runat="server" OnClientClick=<%# "AddNum(this,3,"+ Eval("ID")+","+Eval("Price")+",'"+Eval("Date")+"');return false;"%>>+</asp:LinkButton>
                                        </span>
                                    </td>
                                    <td class="Subtotal">￥<%#Eval("Total") %></td>
                                    <td class="Operation">
                                        <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe627;</i></asp:LinkButton></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                        <div class="clear"></div>
                        <div class="O_total"><span>菜品：<i><asp:Label ID="Totalcount" runat="server" Text="0"></asp:Label></i>份</span><span>总计：<i>¥<asp:Label ID="totalmoney" runat="server" Text="Label"></asp:Label></i></span></div>
                    </div>
                    <div class="To_submit">
                        <input type="button" class="list_back" name="list_back" onclick="ToIndex();" value="返回点餐" />
                        <asp:Button ID="btOrder" runat="server" CssClass="sure_list" Text="确认订单" OnClick="btOrder_Click" />
                    </div>
                </div>
                <div class="tc" id="Success" style="display: none;">
                    <p class="O_finish">
                        <span class="fl icon_o"><i class="iconfont finish_t">&#xe654;</i></span>
                        <span class="fl info_o">请按时到食堂就餐，谢谢合作。<br />
                            订单状态请到<a href="MyOrderList.aspx">［我的订单］</a>中查看</span>
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <!--展示区域-->

</div>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Order.js"></script>
<script type="text/javascript">
    var date ='<%=weekday%>';
    GetNewTime(date);
    var int = setInterval("workNewTime();", 1000);

    function workNewTime() {
        var date = '<%=weekday%>';
        var newtime = $("*[id$=OrderTime]").text();
        if (newtime != null && newtime != "") {
            var hour = parseInt(newtime.split("小时")[0].toString());
            var minute = parseInt(newtime.split("小时")[1].split("分")[0].toString());
            var sec = parseInt(newtime.split("小时")[1].split("分")[1].split("秒")[0].toString());
            if (parseInt(sec) - parseInt(1) >= parseInt(0)) {
                sec = parseInt(sec) - parseInt(1);
            } else if (parseInt(minute) - parseInt(1) >= parseInt(0)) {
                minute = parseInt(minute) - parseInt(1);
                sec = 59;
            } else if (parseInt(hour) - parseInt(1) >= parseInt(0)) {
                hour = parseInt(hour) - parseInt(1);
                minute = 59;
                sec = 59;
            }
            var nowtime = hour + "小时" + minute + "分" + sec + "秒";
            $("*[id$=OrderTime]").text(nowtime);
            if (nowtime == "0小时0分0秒") {
                if (confirm("当前餐下单截止时间已到,是否提交当前餐购物车信息?")) {
                    submitorder(date);
                }
                int = window.clearInterval(int);//停止刷新时间
                //$("*[id$=btOrder]").attr("readonly", "readonly");
                //$("*[id$=btOrder]").css("backgroud", "gray");
              
            }
        }
        return nowtime;
    }
    function submitorder(date) {
        jQuery.ajax({
            url: '<%=layouturl%>' + "CommDataHandler.aspx?action=SubmitOrder&" + Math.random(),   // 提交的页面
            type: "POST",                   // 设置请求类型为"POST"，默认为"GET"
            async: false,
            data: { "date": date },
            beforeSend: function ()          // 设置表单提交前方法
            {
                //alert("准备提交数据");


            },
            error: function (request) {      // 设置表单提交出错
                //alert("表单提交出错，请稍候再试");
                //rebool = false;
            },
            success: function (data) {
                var ss = data.split("|");
                if (ss[0] == "1") {
                    Success();//提交成功
                } else {
                    ClearCart();//清空购物车
                    window.location.href = "OrderIndex.aspx";

                }

            }

        });
    }
   
  
    function Success() {
        $("#Success").show().siblings().hide();
        $("#ordertitle li:last").addClass("selected");
    }
    function ToIndex() {
        window.location.href = "OrderIndex.aspx";
    }
   
</script>
