<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RC_wp_CheckStuUserControl.ascx.cs" Inherits="SVDigitalCampus.School_Courses.RC_wp_CheckStu.RC_wp_CheckStuUserControl" %>
<script src="../../_layouts/15/SVDigitalCampus/Script/JSBase.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<link href="../../../../_layouts/15/SVDigitalCampus/CSS/common.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/animate.css" />
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/tz_slider.js"></script>
<script src="../../../../_layouts/15/SVDigitalCampus/Script/enter/popwin.js"></script>

<script src="../../../../_layouts/15/SVDigitalCampus/Script/F5.js"></script>

<!--企业信息管理-->
<div class="Enterprise_information_feedback">
    <!--页面名称-->
    <%--<h1 class="Page_name">报名审核</h1>--%>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <!--操作区域-->
        <div class="Operation_area">
            <div class="left_choice fl">
            </div>
            <div class="right_add fr">
            </div>
        </div>
        <div class="clear"></div>

        <div class="Display_form">
            <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging" OnItemCommand="LV_TermList_ItemCommand" OnItemEditing="LV_TermList_ItemEditing">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td colspan="3">暂无学生报名。</td>
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
                            <th>学科</th>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td><%# Container.DataItemIndex + 1%></td>
                        <td><%#Eval("XM")%></td>
                        <td><%#Eval("XB")%></td>
                        <td><%#Eval("NJ")%></td>
                        <td><%#Eval("BH")%></td>
                        <td>
                            <asp:LinkButton ID="lbDel" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Del">删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td><%# Container.DataItemIndex + 1%></td>
                        <td><%#Eval("XM")%></td>
                        <td><%#Eval("XB")%></td>
                        <td><%#Eval("NJ")%></td>
                        <td><%#Eval("BJ")%></td>
                        <td>
                            <asp:LinkButton ID="lbDel" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Del">删除</asp:LinkButton>

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
