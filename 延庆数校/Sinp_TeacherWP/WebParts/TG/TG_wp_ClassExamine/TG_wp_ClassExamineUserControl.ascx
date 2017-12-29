<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TG_wp_ClassExamineUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TG.TG_wp_ClassExamine.TG_wp_ClassExamineUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/leaderExamine.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script type="text/javascript">
    var siteUrl = _spPageContextInfo.webAbsoluteUrl;
    $(function () {
        $(".No_personnel").find(".menuclick").click(function () {
            $(this).parent().toggleClass("select").siblings().removeClass("select");
            $(this).next().slideToggle("fast").end().parent().siblings().find(".submenu")
            .addClass("animated flipInX")
            .slideUp("fast").end().find("i").text("+");
            var t = $(this).find("i").text();
            $(this).find("i").text((t == "+" ? "-" : "+"));
        });
        $(".Gl_tab .Gl_tabheader").find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".Gl_tab").find(".tc").eq(index).show().siblings().hide();
        });

    })
</script>

<div class="Term_wrap">
    <div class="Term_con W_main">
    <h1 class="W_tit">公开课</h1>
    <div class="Query">
        <ul>
            <li>
                <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server"></asp:DropDownList>
            </li>
            <li>
                <asp:TextBox ID="Tb_searchTitle" placeholder="请输入关键字" CssClass="search_w" runat="server"></asp:TextBox>
                <asp:Button ID="Btn_Search" runat="server" Text="搜索" OnClick="Btn_Search_Click" CssClass="sea" />
            </li>
        </ul>
    </div>
    <div class="clear"></div>
    <!---选项卡-->
    <div class="Gl_tab">
        <div class="Gl_tabheader">
            <ul class="tab_tit">
                <li class="selected"><a href="#">待审核</a></li>
                <li><a href="#">已审核</a></li>
            </ul>
        </div>
        <div class="content">
            <!--待审核-->
            <div class="tc">
                <div class="No_personnel">
                    <div class="Gl_form">
                        <div class="Gl_con">
                            <asp:ListView ID="LV_WaitExit" runat="server" OnPagePropertiesChanging="LV_WaitExit_PagePropertiesChanging" OnItemCommand="LV_WaitExit_ItemCommand">
                                <EmptyDataTemplate>
                                    <table class="">
                                        <tr>
                                            <td>暂无待审核的公开课</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="W_form">
                                        <tr class="trth">
                                            <th>公开课名称</th>
                                            <th>申请人</th>
                                            <th>申请时间</th>
                                            <th>公开课级别</th>
                                            <th>操作</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="Single">
                                        <td class="Gl_tit"><%# Eval("Title") %></td>
                                        <td class="Gl_people"><%# Eval("CreateUser") %></td>
                                        <td class="Gl_people"><%# Eval("Created") %></td>
                                        <td><%# Eval("ClassLevel") %></td>
                                        <td class="Operations">
                                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                            <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamTermPlan.aspx?flag=class&itemid=','800','500');return false;">审核</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="Double">
                                        <td class="Gl_tit"><%# Eval("Title") %></td>
                                        <td class="Gl_people"><%# Eval("CreateUser") %></td>
                                        <td class="Gl_people"><%# Eval("Created") %></td>
                                        <td><%# Eval("ClassLevel") %></td>
                                        <td class="Operations">
                                            <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                            <asp:LinkButton ID="LB_Exit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'审核公开课','/SitePages/ExamTermPlan.aspx?flag=class&itemid=','800','500');return false;">审核</asp:LinkButton>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>

                            <div class="paging">

                                <asp:DataPager ID="DP_WaitExit" runat="server" PageSize="8" PagedControlID="LV_WaitExit">
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
                        </div>
                    </div>
                </div>
            </div>
            <!--已审核-->
            <div class="tc" style="display: none;">
                <div class="Gl_form">
                    <div class="Gl_con">
                        <asp:ListView ID="LV_FinishExit" runat="server" OnPagePropertiesChanging="LV_FinishExit_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="">
                                    <tr>
                                        <td>暂无已审核的公开课</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table class="W_form">
                                    <tr class="trth">
                                        <th>公开课名称</th>
                                        <th>申请人</th>
                                        <th>申请时间</th>
                                        <th>公开课级别</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="Single">
                                    <td class="Gl_tit"><%# Eval("Title") %></td>
                                    <td class="Gl_people"><%# Eval("CreateUser") %></td>
                                    <td class="Gl_people"><%# Eval("Created") %></td>
                                    <td><%# Eval("ClassLevel") %></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="Double">
                                    <td class="Gl_tit"><%# Eval("Title") %></td>
                                    <td class="Gl_people"><%# Eval("CreateUser") %></td>
                                    <td class="Gl_people"><%# Eval("Created") %></td>
                                    <td><%# Eval("ClassLevel") %></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>

                        <div class="paging">

                            <asp:DataPager ID="DP_FinishExit" runat="server" PageSize="8" PagedControlID="LV_FinishExit">
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>