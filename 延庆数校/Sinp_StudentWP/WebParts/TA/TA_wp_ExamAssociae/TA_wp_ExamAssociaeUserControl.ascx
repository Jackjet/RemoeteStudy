<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_ExamAssociaeUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_ExamAssociae.TA_wp_ExamAssociaeUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">	
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".st_nav li").click(function () {
            $(".st_nav li").removeClass("active");
            $(this).addClass("active");
            var index = $(".st_nav li").index($(this));
            $(".tb").eq(index).show().siblings().hide();
        });
        $(".tb").eq(0).show().siblings().hide();
        $(".tb").each(function () {
            var _this = $(this);
            _this.find(".tab_tit li").click(function () {
                $(this).addClass("selected").siblings().removeClass("selected");
                var index = _this.find(".tab_tit li").index($(this));
                _this.find(".tc").eq(index).show().siblings().hide();
            });
            _this.find(".tc").eq(0).show().siblings().hide();
        });
    });

    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
</script>
<div class="rflf" style="width: 100%">
    <div class="st_nav" style="width: 100%">
        <ul style="width: 100%">
            <li id="one1" class="active"><a href="javascript:void(0)" class="first">创建社团申请</a></li>
            <%--<li id="one2" ><a href="javascript:void(0)" class="second">解散社团申请</a></li>--%>
        </ul>
    </div>
    <div class="st_dt" style="width: 100%">
        <div class="tb" id="first">
            <!---创建团选项卡-->
            <div class="Gl_tab">
                <div class="Gl_tabheader">
                    <ul class="tab_tit">
                        <li class="selected"><a href="javascript:void(0)">待审核</a></li>
                        <li><a href="javascript:void(0)">审核未通过</a></li>
                    </ul>
                </div>
                <div class="content">
                    <!--创建团待审核-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="TempListView" runat="server" OnPagePropertiesChanging="TempListView_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="4">暂无待审核的创建团申请</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">社团名称</th>
                                                <th>申请人</th>
                                                <th>性别</th>
                                                <th>年龄</th>
                                                <th>班级</th>
                                                <th>申请日期</th>
                                                <th>操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Name")%></td>
                                            <td><%#Eval("Gender")%></td>
                                            <td><%#Eval("Age")%></td>
                                            <td><%#Eval("Class")%></td>
                                            <td><%#Eval("Created")%></td>
                                            <td>
                                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=开放','600','480');return false">通过</a>
                                                <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=关闭','600','480');return false">拒绝</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Name")%></td>
                                            <td><%#Eval("Gender")%></td>
                                            <td><%#Eval("Age")%></td>
                                            <td><%#Eval("Class")%></td>
                                            <td><%#Eval("Created")%></td>
                                            <td>
                                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=开放','600','480');return false">通过</a>
                                                <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=关闭','600','480');return false">拒绝</a>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DPApply" runat="server" PageSize="10" PagedControlID="TempListView">
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
                    <!--入团审核未通过-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="LV_RefuseApply" runat="server" OnPagePropertiesChanging="RefuseApply_PagePropertiesChanging" >
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="3">暂无申请拒绝的待审核</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">社团名称</th>
                                                <th>申请人</th>
                                                <th>性别</th>
                                                <th>年龄</th>
                                                <th>班级</th>
                                                <th>申请日期</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_RefuseApply" runat="server" PageSize="10" PagedControlID="LV_RefuseApply">
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
        <div class="tb" id="second">
            <!---退团选项卡-->
            <div class="Gl_tab">
                <div class="Gl_tabheader">
                    <ul class="tab_tit">
                        <li class="selected"><a href="javascript:void(0)">待审核</a></li>
                        <li><a href="javascript:void(0)">审核未通过</a></li>
                    </ul>
                </div>
                <div class="content">
                    <!--退团待审核-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="LV_Quit" runat="server" OnPagePropertiesChanging="Quit_PagePropertiesChanging"  OnItemCommand="Quit_ItemCommand">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="4">暂无待审核的解散社团申请</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">社团名称</th>
                                                <th>申请人</th>
                                                <th>性别</th>
                                                <th>年龄</th>
                                                <th>班级</th>
                                                <th>申请日期</th>
                                                <th>操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                            <td>
                                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','500','420');return false">通过</a>
                                                <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','500','420');return false">拒绝</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                            <td>
                                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','500','420');return false">通过</a>
                                                <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ExamAssociaeAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','500','420');return false">拒绝</a>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_Quit" runat="server" PageSize="10" PagedControlID="LV_Quit">
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
                    <!--退团审核未通过-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="LV_RefuseQuit" runat="server" OnPagePropertiesChanging="RefuseQuit_PagePropertiesChanging" >
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="3">暂无申请拒绝的待审核</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">社团名称</th>
                                                <th>申请人</th>
                                                <th>性别</th>
                                                <th>年龄</th>
                                                <th>班级</th>
                                                <th>申请日期</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title") %></td>
                                            <td><%#Eval("Name") %></td>
                                            <td><%#Eval("Gender") %></td>
                                            <td><%#Eval("Age") %></td>
                                            <td><%#Eval("Class") %></td>
                                            <td><%#Eval("Created") %></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_RefuseQuit" runat="server" PageSize="10" PagedControlID="LV_RefuseQuit">
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