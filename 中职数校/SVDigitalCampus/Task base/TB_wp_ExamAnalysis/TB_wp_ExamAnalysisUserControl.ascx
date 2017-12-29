<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TB_wp_ExamAnalysisUserControl.ascx.cs" Inherits="SVDigitalCampus.Task_base.TB_wp_ExamAnalysis.TB_wp_ExamAnalysisUserControl" %>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>

<div class="Examination">
    <div class="form_test">
        <div class="tit">
            <h2>
                <asp:Label ID="ExamTitle" runat="server" Text=""></asp:Label></h2>
            <div class="tit_bottom">
                <span>考试科目：<asp:Label ID="Subject" runat="server" Text=""></asp:Label></span>
                <span>考试时间：<asp:Label ID="ExamTime" runat="server" Text=""></asp:Label>分钟</span>
                <span>总分：<asp:Label ID="FullScore" runat="server" Text=""></asp:Label>分</span>
            </div>
        </div>
        <div class="Scoreanalysis">
            <h2>
                <span>成绩分析</span>
            </h2>
            <div>
                <table class="S_analysis">
                    <tr>
                        <td>试卷得分：
                            <asp:Label ID="GetScore" runat="server" Text="0"></asp:Label></td>
                        <td>正确题数：
                            <asp:Label ID="RightNum" runat="server" Text="0"></asp:Label></td>
                        <td>回答题数：
                            <asp:Label ID="AnswerNum" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>最高得分：
                            <asp:Label ID="HightScore" runat="server" Text="0"></asp:Label></td>
                        <td>测试题数：
                            <asp:Label ID="TotalQNum" runat="server" Text="0"></asp:Label></td>
                        <td>测试人数：
                            <asp:Label ID="JoinNum" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="AnswerAnalysis">
            <h2 class="a_tit">答案分析</h2>
            <div class="A_Analysis">
                <asp:ListView ID="lv_ExamQType" runat="server">
                    <ItemTemplate>
                        <div class="firsttit">
                            <a class="menuclick" href="#"><%#Eval("TypeName") %><span class="xiaozi">（<%#Eval("TypeCount") %>题，每题<%#Eval("Score") %>分，得分<%#Eval("SubScore") %>分）</span><i class="typestatus"><%#Eval("StatusShow")%></i></a><ul class="timu" style='display: <%#Eval("Isshow")%>'>
                                <li>
                                    <asp:HiddenField ID="TypeID" runat="server" Value='<%#Eval("TypeID") %>' />
                                    <asp:HiddenField ID="Template" runat="server" Value='<%#Eval("OrderID") %>' />
                                    <asp:ListView ID="QuestionTypeOne" runat="server">
                                        <ItemTemplate>
                                            <div class="defen fl">
                                                <span class="defenname">得分</span><br />
                                                <span class="fenzhi">
                                                    <%#Eval("GetScore") %></span>
                                            </div>
                                            <div class="ticon fl">
                                                <p class="biaoti">
                                                    <%# Eval("Count") %> .<%#Eval("Question") %><asp:HiddenField ID="qID" runat="server" Value='<%#Eval("ID") %>' />
                                                </p>
                                                <p class="xuanxiang" style='display: <%#Eval("IsShow")%>'>
                                                    <span>
                                                        <span><%#Eval("YourAnswer") %><%#Eval("Isright") %></span>
                                                        <span style='display: <%#Eval("IsshowAnswer")%>;' class="rightanswer">正确答案为：<%#Eval("Answer") %></span></span>
                                                </p>
                                                <p class="xuanxiangkuang" style='display: <%#Eval("kuangIsShow")%>'>
                                                    <textarea id="textanswer" name="textanswer" readonly="readonly"><%#Eval("YourAnswer") %></textarea>
                                                    <span class="c_answer"><%#Eval("Answer") %></span>
                                                </p>

                                                <p>
                                                    <%#Eval("Analysis") %>
                                                </p>
                                            </div>
                                            <div class="clear"></div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
           <div class="clear"></div>
           <div class="MarkAnalysisdiv"><span class="MarkAnalysis">批 语：</span><asp:Label ID="MarkAnalysis"  runat="server"></asp:Label></div>
    </div>
</div>
