<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SA_wp_ActivityManageUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SA.SA_wp_ActivityManage.SA_wp_ActivityManageUserControl" %>
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
        TabChange(".Tab_apply", "selected_apply", ".Tab_apply", ".tc_apply");
        TabChange(".Tab_quit", "selected_quit", ".Tab_quit", ".tc_quit");
    });
    function TabChange(navcss, selectedcss, parcls, childcls) { //选项卡切换方法，navcss选项卡父级，selectedcss选中样式，parcls内容的父级样式，childcls内容样式
        $(navcss).find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass(selectedcss).siblings().removeClass(selectedcss);//为单击的选项卡添加选中样式，同时与当前选项卡同级的其他选项卡移除选中样式
            $(this).parents(parcls).find(childcls).eq(index).show().siblings().hide();//找到与选中选项卡相同索引的内容，使其展示，同时设置其他同级内容隐藏。
        });
    }
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
            <li id="one1" class="active"><a href="javascript:void(0)" class="first">活动管理</a></li>
            <li id="one2"><a href="javascript:void(0)" class="second">招新审核</a></li>
        </ul>
    </div>
    <div class="st_dt" style="width: 100%">
        <div class="tb" id="first">
            <!---活动管理选项卡-->
            <div class="Gl_tab">
                <div class="Gl_tabheader">
                    <ul class="tab_tit" style="width: 100%;">
                        <li class="selected"><a href="javascript:void(0)">待审核</a></li>
                        <li><a href="javascript:void(0)">已发布</a></li>                    
                    </ul>
                </div>
                <div class="content">
                    <!--待审核-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="LV_WaitActivity" runat="server" OnPagePropertiesChanging="LV_WaitActivity_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="4">暂无待审核的活动</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">活动名称</th>
                                                <th>活动范围</th>
                                                <th>活动类型</th>
                                                <th>最多报名项目个数</th>
                                                <th>报名开始日期</th>
                                                <th>报名截止日期</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Range")%></td>
                                            <td><%#Eval("Type")%></td>
                                            <td><%#Eval("MaxCount")%></td>
                                            <td><%#Eval("BeginDate")%></td>
                                            <td><%#Eval("EndDate")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Range")%></td>
                                            <td><%#Eval("Type")%></td>
                                            <td><%#Eval("MaxCount")%></td>
                                            <td><%#Eval("BeginDate")%></td>
                                            <td><%#Eval("EndDate")%></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_WaitActivity" runat="server" PageSize="10" PagedControlID="LV_WaitActivity">
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
                    <!--已发布-->
                    <div class="tc">
                        <div class="writing_form">
                            <div class="content_list">
                                <asp:ListView ID="LV_FinishActivity" runat="server" OnPagePropertiesChanging="LV_FinishActivity_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr>
                                                <td colspan="3">暂无已发布的活动</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" class="W_form">
                                            <tr class="trth">
                                                <th class="theleft">活动名称</th>
                                                <th>活动范围</th>
                                                <th>活动类型</th>
                                                <th>最多报名项目个数</th>
                                                <th>报名开始日期</th>
                                                <th>报名截止日期</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Range")%></td>
                                            <td><%#Eval("Type")%></td>
                                            <td><%#Eval("MaxCount")%></td>
                                            <td><%#Eval("BeginDate")%></td>
                                            <td><%#Eval("EndDate")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td class="theleft"><%#Eval("Title")%></td>
                                            <td><%#Eval("Range")%></td>
                                            <td><%#Eval("Type")%></td>
                                            <td><%#Eval("MaxCount")%></td>
                                            <td><%#Eval("BeginDate")%></td>
                                            <td><%#Eval("EndDate")%></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="page">
                                <asp:DataPager ID="DP_FinishActivity" runat="server" PageSize="10" PagedControlID="LV_FinishActivity">
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
            <div class="Gl_tab">
                <div class="Gl_tabheader">
                    <ul class="tab_tit" style="width: 100%;">
                        <li class="selected"><a href="javascript:void(0)" class="first">入部申请</a></li>
                        <li><a href="javascript:void(0)" class="second">退部申请</a></li>
                    </ul>
                </div>
                <div class="st_dt"  style="width: 100%">
                    <div class="tc">
                        <!---入部申请选项卡-->
                        <div class="Tab_apply">
                            <div class="Gl_tabheader">
                                <ul class="tab_tit_apply">
                                    <li class="selected_apply"><a href="javascript:void(0)">待审核</a></li>
                                    <li><a href="javascript:void(0)">已审核</a></li>
                                </ul>
                            </div>
                            <div class="content">
                                <!--待审核-->
                                <div class="tc_apply">
                                    <div class="writing_form">
                                        <div class="content_list">
                                            <asp:ListView ID="LV_InAudit" runat="server" OnPagePropertiesChanging="LV_InAudit_PagePropertiesChanging">
                                                <EmptyDataTemplate>
                                                    <table class="W_form">
                                                        <tr>
                                                            <td colspan="4">暂无待审核的入部申请</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" class="W_form">
                                                        <tr class="trth">
                                                            <th class="theleft">部门名称</th>
                                                            <th>活动名称</th>
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
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td>
                                                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','550','515');return false">通过</a>
                                                            <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','550','515');return false">拒绝</a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td>
                                                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','550','515');return false">通过</a>
                                                            <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','550','515');return false">拒绝</a>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="page">
                                            <asp:DataPager ID="DP_InAudit" runat="server" PageSize="10" PagedControlID="LV_InAudit">
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
                                <!--已审核-->
                                <div class="tc_apply" style="display:none;">
                                    <div class="writing_form">
                                        <div class="content_list">
                                            <asp:ListView ID="LV_InAlreadyAudit" runat="server" OnPagePropertiesChanging="LV_InAlreadyAudit_PagePropertiesChanging">
                                                <EmptyDataTemplate>
                                                    <table class="W_form">
                                                        <tr>
                                                            <td colspan="3">暂无已审核的入部申请</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" class="W_form">
                                                        <tr class="trth">
                                                            <th class="theleft">部门名称</th>
                                                            <th>活动名称</th>
                                                            <th>申请人</th>
                                                            <th>性别</th>
                                                            <th>年龄</th>
                                                            <th>班级</th>
                                                            <th>申请日期</th>
                                                            <th>状态</th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td><%#Eval("ExamineStatus") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td><%#Eval("ExamineStatus") %></td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="page">
                                            <asp:DataPager ID="DP_InAlreadyAudit" runat="server" PageSize="10" PagedControlID="LV_InAlreadyAudit">
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
                    <div class="tc" style="display:none;">
                        <!---退部申请选项卡-->
                        <div class="Tab_quit">
                            <div class="Gl_tabheader">
                                <ul class="tab_tit_quit">
                                    <li class="selected_quit"><a href="javascript:void(0)">待审核</a></li>
                                    <li><a href="javascript:void(0)">已审核</a></li>
                                </ul>
                            </div>
                            <div class="content">
                                <!--待审核-->
                                <div class="tc_quit">
                                    <div class="writing_form">
                                        <div class="content_list">
                                            <asp:ListView ID="LV_OutAudit" runat="server" OnPagePropertiesChanging="LV_OutAudit_PagePropertiesChanging">
                                                <EmptyDataTemplate>
                                                    <table class="W_form">
                                                        <tr>
                                                            <td colspan="4">暂无待审核的部门申请</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" class="W_form">
                                                        <tr class="trth">
                                                            <th class="theleft">部门名称</th>
                                                            <th>活动名称</th>
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
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td>
                                                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','550','515');return false">通过</a>
                                                            <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','550','515');return false">拒绝</a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td>
                                                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核通过','550','515');return false">通过</a>
                                                            <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/RecruitApplyAuditing.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','550','515');return false">拒绝</a>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="page">
                                            <asp:DataPager ID="DP_OutAudit" runat="server" PageSize="10" PagedControlID="LV_OutAudit">
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
                                <!--已审核-->
                                <div class="tc_quit" style="display:none;">
                                    <div class="writing_form">
                                        <div class="content_list">
                                            <asp:ListView ID="LV_OutAlreadyAudit" runat="server" OnPagePropertiesChanging="LV_OutAlreadyAudit_PagePropertiesChanging">
                                                <EmptyDataTemplate>
                                                    <table class="W_form">
                                                        <tr>
                                                            <td colspan="3">暂无已审核的退部申请</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" class="W_form">
                                                        <tr class="trth">
                                                            <th class="theleft">部门名称</th>
                                                            <th>活动名称</th>
                                                            <th>申请人</th>
                                                            <th>性别</th>
                                                            <th>年龄</th>
                                                            <th>班级</th>
                                                            <th>申请日期</th>
                                                            <th>状态</th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="Single">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td><%#Eval("ExamineStatus") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="Double">
                                                        <td class="theleft"><%#Eval("Title") %></td>
                                                        <td><%#Eval("ActivityName") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Gender") %></td>
                                                        <td><%#Eval("Age") %></td>
                                                        <td><%#Eval("Class") %></td>
                                                        <td><%#Eval("Created") %></td>
                                                        <td><%#Eval("ExamineStatus") %></td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="page">
                                            <asp:DataPager ID="DP_OutAlreadyAudit" runat="server" PageSize="10" PagedControlID="LV_OutAlreadyAudit">
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
        </div>
    </div>
</div>
<div style="display:none">
    <asp:HiddenField ID="IDS" runat="server" />
</div>