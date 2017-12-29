<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="MyCourseList.aspx.cs" Inherits="Moblie.MyCourseList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="student_main">
        <div class="all_classes f_l">
            <div class="i-tab-title">
                <h2 class="title current_title">我的课程</h2>
                <div class="tab-btn" id="tab1">
                        <p class="mg-auto clearfix">
                            <a href="javascript:;" id="ZaiXue"  class="tab-cur">在学</a>
                            <a href="javascript:;" class="" id="YiXue">学完</a>
                        </p>
                    </div>
            </div>
        <div class="all_classes_content">
           <asp:Repeater ID="RepeaterCourseList" runat="server">
                    <ItemTemplate>

                        <div class="classes_part_list f_l" onclick="<%#Eval("HomepageTiaoZhuan") %>" style="margin-top: 15px; margin-right: 5px; margin-left: 5px; background: #fbfbfb; border: 1px solid #c7c7c7; cursor: pointer;">
                            <div>
                                <img src="http://61.50.119.70:1122/<%#Eval("ImgUrl") %>" title="<%#Eval("Description") %>" onerror='this.src=""' width="170" height="108" />
                            </div>
                            <p class="list_title" title="<%#Eval("Title")%>"><span class="text"><%#GetRemoveString(Eval("Title").ToString())%></span></p>
                            <div class="progress" title="<%#Eval("PercentageDescription") %>">
                                <span class="green" style="width: <%#Eval("Percentage") %>%;"><span><%#Eval("Percentage") %>%</span></span>
                            </div>

                        </div>

                    </ItemTemplate>

                </asp:Repeater>
            <div class="clear"></div>

        </div>
            <div style="text-align: center;">
                <div id="containerdiv" style="overflow: hidden; display: inline-block;">
                    <webdiyer:aspnetpager id="AspNetPageCurriculum" cssclass="paginator" showfirstlast="false" prevpagetext="<<上一页" nextpagetext="下一页>>" runat="server" width="100%" currentpagebuttonclass="paginatorPn" alwaysshow="true" onpagechanged="AspNetPageCurriculum_PageChanged">
        </webdiyer:aspnetpager>
                </div>
            </div>
        </div>

        <div class="right_part f_r">
            <div class="class_library">
                <div class="library_title title2 clearfix area mg-auto">             
                    <h2 class="title">课程库</h2>
                </div>
                <label id="LabelResourceClassification" runat="server" />
            </div>
            <div class="class_library">
                <div class="library_title title2 clearfix area mg-auto">
                    <h2 class="title">我的作业</h2>
                </div>
                <div class="library_main">
                    <ul>
                        <label runat="server" id="LabelExperience" />

                    </ul>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</label>
</label>
</asp:Content>
