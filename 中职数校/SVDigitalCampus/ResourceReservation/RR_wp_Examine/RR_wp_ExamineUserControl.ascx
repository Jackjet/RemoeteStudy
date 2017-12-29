<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RR_wp_ExamineUserControl.ascx.cs" Inherits="SVDigitalCampus.ResourceReservation.RR_wp_Examine.RR_wp_ExamineUserControl" %>
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/common.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/style.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/iconfont.css" />
<link type="text/css" rel="stylesheet" href="/_layouts/15/SVDigitalCampus/CSS/animate.css" />

<script src="/_layouts/15/SVDigitalCampus/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/tz_slider.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/popwin.js"></script>
<script src="/_layouts/15/SVDigitalCampus/Script/uploadFile2.js"></script>
<script type="text/javascript">
    $(function () {
        $(".qijin a").click(function () {
            $(".qijin a").removeClass("Enable");
            $(this).addClass("Enable");
            var ind = $(this).index();
            $(".my_order").eq(ind).show().siblings().hide();
        });
    })
</script>
<div id="order" class="Confirmation_order">
    <!--页面名称-->
    <h1 class="Page_name">资源</h1>
    <!--整个展示区域-->
    <div class="Whole_display_area">
        <div class="Operation_area">
            <%-- <td>
                    <asp:Button ID="btnSure" runat="server" Text="确认订单" OnClick="btnSure_Click" /></td>--%>

            <div class="left_choice fl">
                <ul>
                    <li class="">
                        <div class="qiehuan fl">
                            <span class="qijin">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Enable" OnClick="LinkButton1_Click">公共资源</asp:LinkButton>
                                <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">资产管理</asp:LinkButton>--%>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">专业教室</asp:LinkButton>

                            </span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
        <div class="content">
            <div class="OrderManager_form">
                <div class="my_order">

                    <div class="Display_form">
                        <asp:ListView ID="LV_PublishSouce" runat="server" OnItemCommand="LV_PublishSouce_ItemCommand">
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    暂时没有数据
                                </div>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table class="D_form">
                                    <tr class="trth">
                                        <!--表头tr名称-->
                                        <th class="Account">名称
                                        </th>
                                        <th class="name">申请人
                                        </th>
                                        <th class="Head">所属学校
                                        </th>
                                        <th class="Contact">联系方式
                                        </th>

                                        <th class="State">预定时间
                                        </th>
                                        <th class="State">课节
                                        </th>
                                        <th class="Operation">操作
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamineItem.aspx?itemid=','800','500');return false;">审核</asp:LinkButton>

                                    </td>
                                </tr>

                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">

                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ResourcesType") %>' CommandName="Exit">审核</asp:LinkButton>

                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                        <div class="page">
                            <asp:DataPager ID="DP_PublishSouce" runat="server" PageSize="10" PagedControlID="LV_PublishSouce">
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
                <div class="my_order" style="display: none;">
                    <div class="Display_form">
                        <asp:ListView ID="LV_ClassRoom" runat="server">
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    暂时没有数据
                                </div>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table class="D_form">
                                    <tr class="trth">
                                        <!--表头tr名称-->
                                        <th class="Account">名称
                                        </th>
                                        <th class="name">申请人
                                        </th>
                                        <th class="Head">所属学校
                                        </th>
                                        <th class="Contact">联系方式
                                        </th>

                                        <th class="State">预定时间
                                        </th>
                                        <th class="State">课节
                                        </th>
                                        <th class="Operation">操作
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamineItem.aspx?itemid=','800','500');return false;">审核</asp:LinkButton>

                                    </td>
                                </tr>

                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">

                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamineItem.aspx?itemid=','800','500');return false;">审核</asp:LinkButton>

                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                        <div class="page">
                            <asp:DataPager ID="DP_ClassRoom" runat="server" PageSize="10" PagedControlID="LV_ClassRoom">
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
                <div class="my_order" style="display: none;">
                    <div class="Display_form">
                        <asp:ListView ID="LV_OffRoom" runat="server">
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    暂时没有数据
                                </div>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table class="D_form">
                                    <tr class="trth">
                                        <!--表头tr名称-->
                                        <th class="Account">名称
                                        </th>
                                        <th class="name">申请人
                                        </th>
                                        <th class="Head">所属学校
                                        </th>
                                        <th class="Contact">联系方式
                                        </th>

                                        <th class="State">预定时间
                                        </th>
                                        <th class="State">课节
                                        </th>
                                        <th class="Operation">操作
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamTermPlan.aspx?itemid=','800','500');return false;">审核</asp:LinkButton>

                                    </td>
                                </tr>

                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">

                                    <td class="Account">

                                        <%#Eval("ResourcesType")%>
                                    </td>
                                    <td class="name">
                                        <%#Eval("Title")%>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("BelongSchool") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("ContactPhone") %>
                                    </td>
                                    <td class="Account">
                                        <%#Eval("Data") %>
                                    </td>
                                    <td class="State">
                                        <%#Eval("TimeInterval") %>
                                    </td>
                                    <td class="Operation">
                                        <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                        <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamTermPlan.aspx?itemid=','800','500');return false;">审核</asp:LinkButton>

                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                        <div class="page">
                            <asp:DataPager ID="DP_OffRoom" runat="server" PageSize="10" PagedControlID="LV_OffRoom">
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
    </div>
</div>
