<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Moblie.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="content-box shadow">
        <div class="content">
            <div class="container no-bottom">
                <!--焦点图轮播-->
                <div id="slider" class="swipe">
                    <div class="swipe-wrap">
                        <div class="wrap">
                            <img class="swipe-image responsive-image" src="images/2.jpg" alt="img">
                            <p class="swipe-text">使用简单</p>
                        </div>
                        <div class="wrap">
                            <img class="swipe-image responsive-image" src="images/banner1.png" alt="img">
                            <p class="swipe-text">开启学习新时代</p>
                        </div>
                        <div class="wrap">
                            <img class="swipe-image responsive-image" src="images/3.jpg" alt="img">
                            <p class="swipe-text">在线授课，365天不间断</p>
                        </div>
                    </div>
                    <!-- <ul id="position">
				<li id="one"		class="on"></li>
                    <li id="two"		class=""></li>
                    <li id="three" 		class=" "></li>
                </ul>                   
                <a href="#" class="prev-but-swipe">上页</a>
                <a href="#" class="next-but-swipe">下页</a>    -->
                </div>
            </div>


            <!--快捷入口-->
            <div class="i-icon mg-auto overflow area clearfix">
                <ul class="clearfix">
                    <li><a href="MyCourseList.aspx" class="i-icon-tao">
                        <img src="images/app_01.png"></a></li>
                    <li><a href="ExamMgr.aspx" class="i-icon-game">
                        <img src="images/app_04.png"></a></li>
                    <li><a href="wallpaper/default.htm" class="i-icon-bizhi">
                        <img src="images/app_03.png"></a></li>
                    <li><a href="topic/list_1000.html" class="i-icon-zhuan">
                        <img src="images/app_02.png"></a></li>
                </ul>
            </div>
            <div class="decoration"></div>
            <div class="i-tab mg-auto overflow area clearfix">
                <div class="i-tab-title">
                    <h2>最新动态</h2>
                    <div class="tab-btn" id="tab1">
                        <p class="mg-auto clearfix">
                            <a href="javascript:;" class="tab-cur">社区动态</a>
                            <a href="javascript:;">班级动态</a>
                        </p>
                    </div>
                </div>
                <div class="tab-content clearfix overflow" id="con1">
                    <div class="clearfix tab-con2" style="display: block">
                        <ul class="clearfix" id="gamecat">

                            <asp:Repeater ID="CommunityDynamicsRepeater" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a href="" class="tab-con-href2 clearfix">
                                            <div class="img2">
                                                <img src="<%#Eval("ImgUrl") %>" original="images/oYYBAFJuHIWIQZneAAMDzuV89eEAAA_CQBwAN8AAwPm633.png" alt="<%#Eval("Title")%>" title="<%#Eval("Title")%>"></div>
                                            <h4><%#Eval("Title")%>(<%#Eval("Count")%>)</h4>
                                            <p><i><%#Eval("Description") %></i></p>
                                        </a>
                                    </li>

                                </ItemTemplate>

                            </asp:Repeater>

                        </ul>
                        <%--<div class="i-more">
                            <a href="game_fenlei/default.htm">查看更多分类</a>
                        </div>--%>
                    </div>
                    <div class="clearfix tab-con2 tab-con-info">
                        <ul class="clearfix" id="gamecat">
                            <asp:Repeater ID="ClassDynamicsRepeater" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a href="" class="tab-con-href2 clearfix">
                                            <div class="img2">
                                                <img src="<%#Eval("ImgUrl") %>" original="images/oYYBAFJuHIWIQZneAAMDzuV89eEAAA_CQBwAN8AAwPm633.png" alt="<%#Eval("Title")%>" title="<%#Eval("Title")%>"></div>
                                            <h4><%#Eval("Title")%>(<%#Eval("Count")%>)</h4>
                                            <p><i><%#Eval("Description") %></i></p>
                                        </a>
                                    </li>

                                </ItemTemplate>

                            </asp:Repeater>
                        </ul>
                        <%--<div class="i-more">
                            <a href="game_fenlei/default.htm">查看更多分类</a>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!--全部课程-->
            <div class="title2 clearfix area mg-auto">
                <h2>课程</h2>
            </div>
            <div class="txtimg-nav clearfix area mg-auto">
                <div class="clearfix course">
                    <div class="tab-btn columtab" id="tab2">
                        <p class="mg-auto clearfix">
                            <a href="javascript:;" class="tab-cur">全部课程</a>
                            <a href="javascript:;">已注册</a>
                            <a href="javascript:;">可注册</a>
                            <a href="javascript:;">匿名访问</a>

                        </p>
                    </div>
                    <div class="tab-content clearfix overflow" id="con2">
                        <div class="clearfix tab-con" style="display: block">
                            <div class="column">
                                <asp:Repeater ID="RepeaterCourseList" runat="server">
                                    <ItemTemplate>
                                        <div class="portfolio-item-thumb one-half">
                                            <a href="http://61.50.119.70:1122/<%#Eval("ImgUrl") %>">
                                                <img class="responsive-image" src="http://61.50.119.70:1122/<%#Eval("ImgUrl") %>" alt="<%#Eval("Description") %>">
                                                <%#Eval("Title")%>
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </div>


                        </div>
                        <div class="clearfix tab-con2 tab-con-info">
                            <div class="column">
                                <asp:Repeater ID="Repeater3" runat="server">
                                    <ItemTemplate>
                                        <div class="portfolio-item-thumb one-half">
                                            <a href="images/003_4312.jpg">
                                                <img class="responsive-image" src="<%#Eval("ImgUrl") %>" alt="<%#Eval("Description") %>">
                                                <%#Eval("Title")%>
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>

                                    </ItemTemplate>

                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="clearfix tab-con3 tab-con-info">
                            <div class="column">
                                <asp:Repeater ID="NotFreeRepeaterCourseList" runat="server">
                                    <ItemTemplate>
                                        <div class="portfolio-item-thumb one-half">
                                            <a href="images/003_4312.jpg">
                                                <img class="responsive-image" src="<%#Eval("ImgUrl") %>" alt="<%#Eval("Description") %>">
                                                <%#Eval("Title")%>
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>

                                    </ItemTemplate>

                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="clearfix tab-con4 tab-con-info">
                            <div class="column">
                                <asp:Repeater ID="AnonymousAccessRepeaterCourseList" runat="server">
                                    <ItemTemplate>
                                        <div class="portfolio-item-thumb one-half">
                                            <a href="images/003_4312.jpg">
                                                <img class="responsive-image" src="<%#Eval("ImgUrl") %>" alt="<%#Eval("Description") %>">
                                                <%#Eval("Title")%>
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>

                                    </ItemTemplate>

                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
