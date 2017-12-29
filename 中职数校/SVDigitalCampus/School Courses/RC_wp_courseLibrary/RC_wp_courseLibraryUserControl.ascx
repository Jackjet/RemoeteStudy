<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_courseLibraryUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_courseLibrary.RC_wp_courseLibraryUserControl" %>

<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" rel="stylesheet" />
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/uploadFile.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>

<style>
    .Title {text-decoration:underline; color:blue;}
</style>

<div class="School_library">
    <!--页面名称-->
    <%--<h1 class="Page_name">历史课程</h1>--%>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div>
            <!--操作区域-->



            <div class="clear"></div>
            <div class="Operation_area">
                <div class="left_choice fl">
                    <ul>
                        <li class="State">
                            <span class="qijin">
                                <asp:LinkButton ID="Course" runat="server" CssClass="Disable" ForeColor="White" OnClick="Course_Click">我的课程</asp:LinkButton>
                                <asp:LinkButton ID="Task" runat="server" CssClass="Enable" ForeColor="White">历史课程</asp:LinkButton>
                                <asp:LinkButton ID="lbTask" runat="server" CssClass="Disable" ForeColor="White" OnClick="lbTask_Click">任务库</asp:LinkButton>

                            </span>
                        </li>

                    </ul>
                </div>

            </div>
            <div class="clear"></div>
            <!--展示区域-->
            <div class="Display_form">
                <div class="Resources_tab">

                    <div class="Operation_area" style="margin-top: 10px;">
                        <div class="left_choice fl">
                            
                        </div>
                        <div class="right_add fr" id="opertion">
                            <a class="add" onclick="Add()" style="cursor: pointer;"><i class="iconfont">&#xe630;</i>创建课程</a>
                        </div>
                    </div>
                    <div class="content">
                        <div class="tc">
                            <div class="Order_form">
                                <div class="Food_order">
                                    <asp:ListView ID="LV_TermList" runat="server" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="kc_create">
                                                            <h1>无历史课程数据</h1>
                                                            <h2></h2>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <table id="itemPlaceholderContainer" style="width: 100%;" class="">

                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>

                                                    <div class="kc_yylc">
                                                        <div class="kc_yylc_xq">
                                                            <img src='<%#Eval("ImageUrl") %>' alt="">
                                                            <h2>
                                                                <asp:LinkButton ID="lbTitle" runat="server" CommandName="View" CommandArgument='<%#Eval("ID") %>' CssClass="Title"><%#Eval("Title") %></asp:LinkButton>
                                                            </h2>
                                                            <div class="tit_course"><span>主讲教师：<%#Eval("TeacherName")%></span></div>
                                                            <p><%#Eval("Introduc") %></p>
                                                        </div>
                                                        
                                                    </div>

                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</div>

<%--<div class="page">
    <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="LV_TermList">
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
</div>--%>
