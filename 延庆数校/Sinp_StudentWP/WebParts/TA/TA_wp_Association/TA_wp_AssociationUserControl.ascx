<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_AssociationUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_Association.TA_wp_AssociationUserControl" %>
<link rel="stylesheet"  href="/_layouts/15/Style/common.css" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">	
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script type="text/javascript">
    $(function () {       
        TabChange(".rflf .st_nav", 'active', '.rflf', '.td');
        TabChange(".Gl_tab", 'selected', '.Gl_tab', '.tc2');
    })
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
        <ul style="width:100%;">
            <li class="active"><a href="#" class="first">全部社团</a></li>
            <li><a href="#" class="second">我的社团</a></li>
            <li><a href="#" class="third">社团活动</a></li>
        </ul>
    </div>
    <div  style="width: 100%">
        <div class="td" id="first">
           <div class="tabcontent">
                <div style="height:35px;margin:5px 6px 5px 0px;">
                     <a href="javascript:void(0)" style="width:87px;height:29px;line-height:29px;color:#ffffff;text-align:center;font-size:14px;background: #0da6ec;border-radius: 5px;display: block;float: right;cursor:pointer" onclick="openPage('创建社团','/SitePages/AddAssociae.aspx','600','430');return false;">创建社团</a>
                </div>
                <div class="allst_list">
                    <ul style="border:none;width:100%;">
                        <asp:ListView ID="LV_TermList" runat="server" OnPagePropertiesChanging="LV_TermList_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="W_form">
                                    <tr>
                                        <td>暂无社团。</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <li>
                                    <img src='<%# Eval("Attachment") %>'>
                                    <h3><%# Eval("Title") %></h3>
                                    <div>
                                        <p><a href='AssociationShow.aspx?itemid=<%# Eval("ID") %>'>社团详情</a></p>
                                        <span><a class="iconfont" href="javascript:void(0)" onclick="openPage('申请入团','/SitePages/ApplyAssociae.aspx?itemid=<%# Eval("ID") %>','650','460');return false;">+</a><span>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <div class="page">
                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="8" PagedControlID="LV_TermList">
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
        <div class="td" id="second" style="display: none;">
            <div class="tabcontent">
                <div class="allst_list">
                    <ul style="border:none;width:100%;">
                        <asp:ListView ID="SB_TermList" runat="server">
                            <EmptyDataTemplate>
                                <table class="W_form">
                                    <tr>
                                        <td>您暂未加入社团。</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <li>
                                    <img src='<%# Eval("Attachment") %>'>
                                    <h3><%# Eval("Title") %></h3>
                                    <div class="fz">
                                        <p><a href='AssociationShow.aspx?itemid=<%# Eval("ID") %>'>社团详情</a></p>
                                        <%--<span><a class="iconfont" href="#">+</a><span>--%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
                <div class="page">
                    <asp:DataPager ID="SBDPTeacher" runat="server" PageSize="8" PagedControlID="SB_TermList">
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
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt32(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                                    </span>
                                </PagerTemplate>
                            </asp:TemplatePagerField>
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
        <div class="td" id="third" style="display: none;">
            <div class="Gl_tab">
                        <div class="Gl_tabheader">
                            <ul class="tab_tit">
                                <li class="selected">
                                    <a href="#">全部活动<span class="num_1" id="spActiveCount" name="spActiveCount" runat="server"></span></a>
                                </li>
                                <li>
                                    <a href="#">待审核<span class="num_1" id="spUnAuditCount" name="spUnAuditCount" runat="server"></span></a>
                                </li>
                            </ul>
                        </div>
                        <div class="content">
                            <div class="tc2">
                                <asp:ListView ID="lvAllActivity" runat="server" OnPagePropertiesChanging="lvAllActivity_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr class="trth">
                                                <td colspan="3" style="text-align: center">暂无活动！
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="W_form" id="itemPlaceholderContainer">
                                            <tr class="trth">
                                                <th>活动名称
                                                </th>
                                                <th>所属社团
                                                </th>
                                                <th>参加人
                                                </th>
                                                <th>未参加人
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td>
                                                <%#Eval("Title")%>
                                            </td>
                                            <td>
                                                <%#Eval("AssoName")%>
                                            </td>
                                            <td>
                                                <%#Eval("inMembers")%>
                                            </td>
                                            <td>
                                                <%#Eval("noMembers")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="page">
                                    <asp:DataPager ID="ActivityPager" PageSize="10" runat="server" PagedControlID="lvAllActivity">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" PreviousPageText="上一页"
                                                ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                            <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />
                                            <asp:NextPreviousPagerField ButtonType="Link" NextPageText="下一页" LastPageText="末页"
                                                ShowLastPageButton="true" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    <span class="page">| <%#Container.StartRowIndex/Container.PageSize+1%> /
                                   <%#(Container.TotalRowCount%Container.MaximumRows)>0?Container.TotalRowCount/Container.MaximumRows+1:Container.TotalRowCount/Container.MaximumRows %>页
                                   (共<%#Container.TotalRowCount%>项)
                                                    </span>
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                            <div id="div_unAudit" class="tc2" style="display: none;">
                                <asp:ListView ID="lvUnAudit" runat="server" OnPagePropertiesChanging="lvUnAudit_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="W_form">
                                            <tr class="trth">
                                                <td colspan="3" style="text-align: center">暂无未审核活动！
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="W_form" id="itemPlaceholderContainer">
                                             <tr class="trth">
                                            <th>活动名称
                                            </th>
                                            <th>所属社团
                                            </th>
                                            <th>申请时间
                                            </th>
                                            <th>操作</th>
                                        </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td>
                                                <%#Eval("Title")%>
                                            </td>
                                            <td>
                                                <%#Eval("AssociaeName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Created")%>
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ActivityAuditing.aspx?activeid=<%#Eval("ID")%>&status=审核通过','500','550');return false">通过</a>
                                                <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ActivityAuditing.aspx?activeid=<%#Eval("ID")%>&status=审核拒绝','500','550');return false">拒绝</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="page">
                                    <asp:DataPager ID="UnAuditPager" PageSize="10" runat="server" PagedControlID="lvUnAudit">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" PreviousPageText="上一页"
                                                ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                            <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />
                                            <asp:NextPreviousPagerField ButtonType="Link" NextPageText="下一页" LastPageText="末页"
                                                ShowLastPageButton="true" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    <span class="page">| <%#Container.StartRowIndex/Container.PageSize+1%> /
                                   <%#(Container.TotalRowCount%Container.MaximumRows)>0?Container.TotalRowCount/Container.MaximumRows+1:Container.TotalRowCount/Container.MaximumRows %>页
                                   (共<%#Container.TotalRowCount%>项)
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
