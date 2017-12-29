<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CourseCheckUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CourseCheck.RC_wp_CourseCheckUserControl" %>
<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<link href="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.css" rel="stylesheet" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/Enter/jquery-webox.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/popwin.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/F5.js"></script>

<!--企业信息管理-->
<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <h1 class="Page_name">课程审核</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
                <ul>
                    <li class="Select">
                        <asp:DropDownList ID="dpStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dpStatus_SelectedIndexChanged">
                            <asp:ListItem Value="">全部状态</asp:ListItem>
                            <asp:ListItem Value="0">待传资料</asp:ListItem>
                            <asp:ListItem Value="1">待提交审核</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">待审核</asp:ListItem>
                            <asp:ListItem Value="3">审核失败</asp:ListItem>
                            <asp:ListItem Value="4">预约教室</asp:ListItem>
                            <asp:ListItem Value="5">其他</asp:ListItem>
                        </asp:DropDownList>

                    </li>
                    <%--<li class="Sear">
                        <input type="text" id="EnterName" placeholder=" 请输入公司名称" class="search" name="search" runat="server" /><i class="iconfont"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="Button1_Click">&#xe62d;</asp:LinkButton></i>
                    </li>--%>
                </ul>
            </div>
            <div class="right_add fr">
                <%--<a href="#" class="add" onclick="add()"><i class="iconfont">&#xe630;</i>添加企业</a>--%>
            </div>
        </div>
        <div class="clear"></div>

        <div class="Display_form">
            <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing" OnItemDataBound="LV_TermList_ItemDataBound">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="3">暂无课程信息。</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" style="width: 100%;" class="D_form">
                        <tr class="trth">
                            <th>序号</th>
                            <th>课程名称</th>
                            <th>上课时间</th>
                            <th>主讲老师</th>
                            <th>课程状态</th>
                            <th>审批意见</th>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td><%# Container.DataItemIndex + 1%></td>
                        <td><%#Eval("Title")%></td>
                        <td><%#Eval("BeginTime")%></td>
                        <td><%#Eval("TeacherName")%></td>
                        <td><%#Eval("StatusName")%></td>
                        <td><%#Eval("CheckMessage")%></td>
                        <td>
                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                            <asp:LinkButton ID="Check" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Check">审核</asp:LinkButton>
                        </td>

                    </tr>

                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td><%# Container.DataItemIndex + 1%></td>
                        <td><%#Eval("Title")%></td>
                        <td><%#Eval("BeginTime")%></td>
                        <td><%#Eval("TeacherName")%></td>
                        <td><%#Eval("StatusName")%></td>
                        <td><%#Eval("CheckMessage")%></td>

                        <td>
                            <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>

                            <asp:LinkButton ID="Check" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Check">审核</asp:LinkButton>

                        </td>
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
                    <span class="pageup">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                    </span>
                </PagerTemplate>
            </asp:TemplatePagerField>
        </Fields>
    </asp:DataPager>
</div>





