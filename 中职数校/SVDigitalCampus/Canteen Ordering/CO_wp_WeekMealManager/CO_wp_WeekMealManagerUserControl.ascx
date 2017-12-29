<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CO_wp_WeekMealManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Canteen_Ordering.CO_wp_WeekMealManager.CO_wp_WeekMealManagerUserControl" %>

<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">

    var AddNewObj;
    function Choice(Type) {
        var WeekDay = $("#weekdate ul .selected a").html();
        if (WeekDay != null && WeekDay != "") {
            WeekDay = WeekDay.substring(parseInt(WeekDay.indexOf("(")) + parseInt(1), WeekDay.indexOf(")"));
            //layer.open({
            //    type:2,
            //    title:"选择菜品",
            //    content:  'http://yfbsp2013:6206/sites/OrderMealSystem/SitePages/AllotMenu.aspx?WeekDay=' + WeekDay + '&Type=' + Type,
            //    area: ['1000px', '500px'],
            //    fix: false,
            //    time:0,
            //    shadeClose: true
            //});
            //AddNewObj = $.dialog({
            //    id: "menu",
            //    title: "选择菜品",
            //    lock: true,
            //    width: 800,
            //    height: 500,
            //    max: false,
            //    min: false,

            //    zIndex: 900,
            //    content: 'url:AllotMenu.aspx?WeekDay=' + WeekDay + '&Type=' + Type
            //});
            popWin.showWin("800", "500", "选择菜品", '<%=SietUrl%>'+"AllotMenu.aspx?Type=" + Type + "&WeekDay=" + WeekDay);
        }
    }

</script>
<div class="Allot_Meal">
    <!--页面名称-->
    <h1 class="title_name">分配菜单    
    </h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <div class="left_choice fl">               
            </div>
            <div class="right_add fr">
                    <asp:LinkButton ID="btnNextWeek" CssClass="add" runat="server" Text="下一周" OnClick="btnNextWeek_Click"></asp:LinkButton>
                    <asp:LinkButton ID="btnLastWeek" CssClass="add" runat="server" Text="上一周" OnClick="btnLastWeek_Click"></asp:LinkButton>
                    <asp:LinkButton ID="btnBackMenu" runat="server" CssClass="add" OnClick="btnBackMenu_Click">菜品管理</asp:LinkButton>
            </div>
        </div>
        
        <!--tab切换title-->
        <div class="Allot_tab">
            <div class="Allot_tabheader" id="weekdate">
                <ul>
                    <asp:ListView ID="lvWeekDay" runat="server" ItemPlaceholderID="weekPlaceHolderID">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="weekPlaceHolderID"></asp:PlaceHolder>

                        </LayoutTemplate>
                        <ItemTemplate>
                            <li id='<%#Eval("TabID") %>' class="<%#Eval("Class") %>"><a href="javascript:void(0);"><%#Eval("WeekDay")%>(<%#Eval("WeekDate")%>)</a></li>
                        </ItemTemplate>
                    </asp:ListView>

                </ul>
            </div>
            <div class="content">
                <asp:ListView ID="lvWeekMeal" runat="server" ItemPlaceholderID="PlaceHolderID">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="PlaceHolderID"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div id='<%#Eval("TabID")%>' class="tc" style='display: <%# Eval("IsShow")%>'>
                            <div class="Allot_form">
                                <asp:HiddenField ID="WeekDate" runat="server" Value='<%#Eval("WeekDate")%>' />
                                <asp:HiddenField ID="WeekDay" runat="server" Value='<%#Eval("WeekDay")%>' />
                                <asp:ListView ID="lvMealMenu" runat="server" ItemPlaceholderID="mealPlaceHolderID">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder runat="server" ID="mealPlaceHolderID"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div class="Food_Allot">

                                            <div class="clear"></div>
                                            <h2 class="food_tit">
                                                <asp:Label ID="MealType" CssClass="F_name" runat="server" Text='<%#Eval("MealType") %>'></asp:Label>
                                                <span> 已选:<i><%#Eval("MealCount") %></i>份菜品</span>
                                                <a href="javascript:void(0);" onclick="Choice('<%#Eval("MealTypestr") %>');" class="F_Choice">挑选菜品</a>
                                                <asp:HiddenField ID="MealTypeID" runat="server" Value='<%#Eval("MealTypeID") %>' />
                                            </h2>
                                            <asp:ListView ID="lv_Menu" runat="server" OnItemCommand="ItemCommand">
                                                <EmptyDataTemplate>
                                                    <h3>亲，暂无菜品！
                                                    </h3>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="A_form">
                                                        <tr class="A_trth">
                                                            <th class="number">编号</th>
                                                            <th class="Dishes">菜品 </th>
                                                            <th class="Type">类型 </th>
                                                            <th class="Price">价格 </th>
                                                            <th class="Operation">操作列 </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td class="Dishes"><%#Eval("ID")%>
                                                        </td>
                                                        <td class="Dishes"><%#Eval("Title")%> <em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em><asp:HiddenField ID="Title" Value='<%#Eval("Title")%>' runat="server" />
                                                        </td>
                                                        <td class="Type"><%#Eval("Type")%>
                                                        </td>
                                                        <td class="Price">￥<%#Eval("Price")%></td>
                                                        <td class="Operation">
                                                            <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("MenuID") %>'><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                                                            <asp:HiddenField ID="MealType" runat="server" Value='<%#Eval("MealType")%>' />
                                                            <asp:HiddenField ID="CurrentWeekDate" runat="server" Value='<%#Eval("WeekDate")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="Dishes"><%#Eval("ID")%>
                                                        </td>
                                                        <td class="Dishes"><%#Eval("Title")%> <em class='<%#Eval("Hot")%>'>&nbsp&nbsp&nbsp&nbsp&nbsp;</em><asp:HiddenField ID="Title" Value='<%#Eval("Title")%>' runat="server" />
                                                        </td>
                                                        <td class="Type"><%#Eval("Type")%>
                                                        </td>
                                                        <td class="Price">￥<%#Eval("Price")%></td>
                                                        <td class="Operation">
                                                            <asp:LinkButton ID="btndelete" class="btn" runat="server" CommandName="del" CommandArgument='<%# Eval("MenuID") %>'><i class="iconfont">&#xe64c;</i></asp:LinkButton>
                                                            <asp:HiddenField ID="MealType" runat="server" Value='<%#Eval("MealType")%>' />
                                                            <asp:HiddenField ID="CurrentWeekDate" runat="server" Value='<%#Eval("WeekDate")%>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:ListView>

            </div>
        </div>
    </div>
</div>
