﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ES_wp_ExamPaperReleaseManagerUserControl.ascx.cs" Inherits="SVDigitalCampus.Examination_System.ES_wp_ExamPaperReleaseManager.ES_wp_ExamPaperReleaseManagerUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script type="text/javascript">
    function Release(qID) {
        if (qID == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        popWin.showWin("700", "400", "考试发布", "ExamPaperRelease.aspx?ExamPaperID=" + qID);
    }
    function Look(qID) {
        if (qID == null) {

            alert("请选择一条记录！");
            return false;

        }
        popWin.scrolling = "auto";
        popWin.showWin("750", "760", "试卷详情", '<%=SietUrl%>' + "ExamPaperDetail.aspx?ExamPaperID=" + qID);
    }


</script>
<div class="ExamPaperManager">
    <!--页面名称-->
    <h1 class="Page_name">试卷管理</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <div class="S_conditions">
                <div id="selectList" class="screenBox screenBackground">
                    <dl class="listIndex dlHeight" attr="terminal_brand_s" id="Majordl" runat="server">
                        <dt>专业</dt>
                        <dd>
                            <asp:ListView ID="lvMajor" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemCommand="lvMajor_ItemCommand">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <label>
                                        <asp:LinkButton ID="majoritem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="showSubject" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton>
                                    </label>
                                </ItemTemplate>
                            </asp:ListView>
                            <span class="more">展开</span>
                        </dd>
                    </dl>
                    <dl class="listIndex" attr="terminal_brand_s" id="subjectdl" runat="server">
                        <dt>学科</dt>
                        <dd>
                            <asp:ListView ID="lvSubject" runat="server" ItemPlaceholderID="subitemPlaceHolder" OnItemCommand="lvSubject_ItemCommand">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="subitemPlaceHolder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <label>
                                        <asp:LinkButton ID="SubjectItem" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="SubjectSearch" CssClass='<%#Eval("class") %>'><%#Eval("Title") %></asp:LinkButton></label>
                                </ItemTemplate>
                            </asp:ListView>
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="clear"></div>
            <div class="Operation_area markover">
                <div class="left_choice fl">
                    <ul>
                        <li class="">
                            <div class="qiehuan fl">
                                <span class="qijin">
                                    <asp:LinkButton ID="btnexampaper" runat="server" CssClass="Disable" OnClick="btnexampaper_Click">已发布试卷</asp:LinkButton><asp:LinkButton ID="btnRelease" runat="server" CssClass="Enable">未发布试卷</asp:LinkButton></span>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>
            <!--操作区域-->
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="Select">
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="所有" Value="0"></asp:ListItem>
                                <asp:ListItem Text="标准" Value="1"></asp:ListItem>
                                <asp:ListItem Text="测试" Value="2"></asp:ListItem>
                                <asp:ListItem Text="作业" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="Sear">
                            <input type="text" placeholder=" 请输入关键字" id="txtkeywords" class="search" name="search" runat="server" /><asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"><i class="iconfont">&#xe62d;</i></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>
            <!--展示区域-->
            <div class="Display_form">
                <asp:ListView ID="lvExam" runat="server" OnPagePropertiesChanging="lvExam_PagePropertiesChanging" >
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td colspan="9" style="text-align: center">亲，暂无试卷记录！
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table class="D_form">
                            <tr class="trth">
                                <!--表头tr名称-->
                                <th class="Account">编号
                                </th>
                                <th class="name">试卷名称
                                </th>
                                <th class="Head">类型
                                </th>
                                <th class="Contact">难度
                                </th>
                                <th class="Contact">组卷人
                                </th>
                                <th class="Contact">组卷时间
                                </th>
                                <th class="State">状态
                                </th>
                                <th class="Operation">操作列
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="Single">
                            <td class="Count">
                                <%#Eval("Count")%>
                            </td>
                            <td class="Title">
                                <%#Eval("Title")%>
                            </td>
                            <td class="Type">
                                <%#Eval("Type")%>
                            </td>
                            <td class="Difficulty">
                                <%#Eval("DifficultyShow")%>
                            </td>
                            <td class="Author"><%#Eval("Author")%></td>
                            <td class="Created">
                                <%#Eval("Created")%>  
                            </td>
                            <td class="State">
                                <%#Eval("StatusShow")%>
                            </td>
                            <td class="Operation">
                                 <a href="javascript:void(0);" onclick="Look('<%# Eval("ID") %>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                                <a href="javascript:void(0);" onclick="Release('<%# Eval("ID") %>');" style="color: blue;">发布</a>

                            </td>
                        </tr>

                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="Double">

                            <td class="Count">
                                <%#Eval("Count")%>
                            </td>
                            <td class="Title">
                                <%#Eval("Title")%>
                            </td>
                            <td class="Type">
                                <%#Eval("Type")%>
                            </td>
                            <td class="Difficulty">
                                <%#Eval("DifficultyShow")%>
                            </td>
                            <td class="Author"><%#Eval("Author")%></td>
                            <td class="Created">
                                <%#Eval("Created")%>  
                            </td>
                            <td class="State">
                                <%#Eval("StatusShow")%>
                            </td>
                            <td class="Operation">
                                 <a href="javascript:void(0);" onclick="Look('<%# Eval("ID") %>');" style="color: blue;" title="详情"><i class="iconfont">&#xe60c;</i></a>
                                <a href="javascript:void(0);" onclick="Release('<%# Eval("ID") %>');" style="color: blue;">发布</a>

                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <div class="page">
                    <asp:DataPager ID="DPExam" runat="server" PageSize="8" PagedControlID="lvExam">
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
