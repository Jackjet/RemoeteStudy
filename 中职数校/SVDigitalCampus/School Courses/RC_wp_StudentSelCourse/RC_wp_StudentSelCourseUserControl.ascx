<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_StudentSelCourseUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_StudentSelCourse.RC_wp_StudentSelCourseUserControl" %>
<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/popwin.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/F5.js"></script>


<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <h1 class="Page_name">学生选课统计</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <%-- <div class="left_choice fl">
                <ul>
                    <li class="option">
                        <asp:DropDownList ID="rbfk" runat="server" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbfk_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Selected="True">反馈情况</asp:ListItem>
                            <asp:ListItem Value="1">已反馈</asp:ListItem>
                            <asp:ListItem Value="0">未反馈</asp:ListItem>
                        </asp:DropDownList></li>
                    <li class="option">
                        <asp:DropDownList ID="rbfp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbfp_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Selected="True">分配情况</asp:ListItem>
                            <asp:ListItem Value="1">已分配</asp:ListItem>
                            <asp:ListItem Value="0">未分配</asp:ListItem>
                        </asp:DropDownList></li>
                    <li class="Sear">
                        <input type="text" id="txtStudent" placeholder=" 请输入学生姓名" class="search" name="search" runat="server" /><i class="iconfont"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button1_Click">&#xe62d;</asp:LinkButton></i>
                    </li>
                </ul>
            </div>
            <div class="right_add fr">
                <a href="<%=rootUrl %>/SitePages/StudentDepart.aspx" class="add" style="display: block; float: left;"><i class="iconfont">&#xe642;</i>分配学生</a>
            </div>--%>
        </div>
        <div class="clear"></div>
        <div class="Display_form">
            <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="3"></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                        <tr class="trth">
                            <th>序号</th>
                            <th>姓名</th>
                            <th>性别</th>
                            <th>专业</th>
                            <th>选课数量</th>

                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td>
                            <%# Container.DataItemIndex + 1%>
                        </td>
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("SFZJH") %>' Visible="false"></asp:Label>
                        <td><%#Eval("XM")%></td>
                        <td><%#Eval("XBM")%></td>
                        <td><%#Eval("ZYMC")%></td>
                        <td><%#Eval("KC")%></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td>
                            <%# Container.DataItemIndex + 1%>
                        </td>
                        <asp:Label ID="lbID" runat="server" Text='<%# Eval("SFZJH") %>' Visible="false"></asp:Label>
                        <td><%#Eval("XM")%></td>
                        <td><%#Eval("XBM")%></td>
                        <td><%#Eval("ZYMC")%></td>
                        <td><%#Eval("KC")%></td>

                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>
<div class="page">
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
</div>
