<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExaminationDetailUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExaminationDetail.ES_wp_ExaminationDetailUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>

<div class="Examination">
    <div class="form_test">
        <div class="timer fl">
        </div>
        <div class="centercon_e fl">
            <div class="tit">
                <h2>
                    <asp:Label ID="ExamTitle" runat="server" Text=""></asp:Label></h2>
                <div class="tit_bottom">
                    <span>考试科目：<asp:Label ID="Subject" runat="server" Text=""></asp:Label></span>
                    <span>考试时间：<asp:Label ID="ExamTime" runat="server" Text=""></asp:Label>分钟</span>
                    <span>总分：<asp:Label ID="FullScore" runat="server" Text=""></asp:Label>分</span>
                    <asp:HiddenField ID="ExamID" runat="server" />
                </div>
            </div>
            <div class="AnswerAnalysis">
                <div class="A_Analysis">
                    <asp:ListView ID="lv_ExamQType" runat="server">
                        <ItemTemplate>
                            <div class="firsttit">
                                <a class="menuclick" href="#"><%#Eval("TypeName") %><span class="xiaozi">（<%#Eval("TypeCount") %>题，每题<%#Eval("Score") %>分，总分<%#Eval("SubScore") %>分）</span><i class="typestatus"><%#Eval("StatusShow")%></i></a>
                                <ul class="timu" style='display: <%#Eval("Isshow")%>'>
                                    <li>
                                        <asp:HiddenField ID="TypeID" runat="server" Value='<%#Eval("TypeID") %>' />
                                        <asp:HiddenField ID="Template" runat="server" Value='<%#Eval("OrderID") %>' />
                                        <asp:ListView ID="QuestionTypeOne" runat="server">
                                            <EmptyDataTemplate>
                                                <h1>暂无试题</h1>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <p class="biaoti">
                                                    <%# Eval("Count") %> .<%#Eval("Question") %>
                                                    <asp:HiddenField ID="qID" runat="server" Value='<%#Eval("ID") %>' />
                                                    <asp:HiddenField ID="qType" runat="server" Value='<%#Eval("qType") %>' />
                                                </p>
                                                <p class="xuanxiang" style='display: <%#Eval("IsShow")%>'>
                                                    <span>

                                                        <span style='display: <%#Eval("OptionAIsshow")%>'>

                                                            <%#Eval("OptionA") %></span>

                                                        <span style='display: <%#Eval("OptionBIsshow")%>'>

                                                            <%#Eval("OptionB") %></span>

                                                        <span style='display: <%#Eval("OptionCIsshow")%>'>

                                                            <%#Eval("OptionC") %></span>

                                                        <span style='display: <%#Eval("OptionDIsshow")%>'>

                                                            <%#Eval("OptionD") %></span>

                                                        <span style='display: <%#Eval("OptionEIsshow")%>'>

                                                            <%#Eval("OptionE") %></span>

                                                        <span style='display: <%#Eval("OptionFIsshow")%>'>
                                                            <%#Eval("OptionF") %></span>
                                                    </span>
                                                    <span class="rightanswer">正确答案为：<%#Eval("Answer") %></span>
                                                </p>
                                                <p class="xuanxiangkuang" style='display: <%#Eval("kuangIsShow")%>'>
                                                    <span class="c_answer"><%#Eval("Answer") %></span>
                                                </p>
                                                <p>
                                                    <%#Eval("Analysis") %>
                                                </p>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </li>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</div>
