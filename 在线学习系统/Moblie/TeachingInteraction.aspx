<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="TeachingInteraction.aspx.cs" Inherits="Moblie.TeachingInteraction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2>班级通知</h2></div>
            </div>
    <div class="library_main">
                <label runat="server" id="LabelKaoShiTongZhi" />
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="">微积分选修</span>
                    <p class='kaiKeDate'>(2016年01月15日开课)</p>
                </h4>
                <h4 class='title_name next_title'><span style='cursor: pointer;' onclick="">德育选修课</span>
                    <p class='kaiKeDate'>(2016年01月25日第八节课所有学生必须参加)</p>
                </h4>
            </div>
    <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2><a href="http://61.50.119.70:1122/sites/StudentEducation/SitePages/MySurveyPaperM.aspx">班级问卷调查</a></h2></div>
            </div>
    <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2>班级论坛</h2></div>
            </div>
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
    <div class="classes_title">
                <div class="title title2 clearfix area mg-auto"><h2><a href="http://61.50.119.70:1133/WebChatRoom.aspx">班级聊天室</a></h2></div>
            </div>
</label>
</asp:Content>
