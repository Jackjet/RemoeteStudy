<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamineInfo.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.ExamineInfo" %>
<script type="text/javascript">
    
</script>
<div class="TopToolbar">
    <span>申请类型</span>&nbsp;<asp:DropDownList ID="DDL_ApplyType" runat="server">
        <asp:ListItem Selected="True">不限</asp:ListItem>
        <asp:ListItem>注册用户</asp:ListItem>
        <asp:ListItem>注册课程</asp:ListItem>
        <asp:ListItem>加入班级</asp:ListItem>
        <asp:ListItem>加入论坛</asp:ListItem>
        <asp:ListItem>加入学习小组</asp:ListItem>
        <asp:ListItem>实训室预约</asp:ListItem>
                           </asp:DropDownList>&nbsp;&nbsp;
    
    <span>审批状态</span>&nbsp;<asp:DropDownList ID="DDL_ExamineResult" runat="server">
        <asp:ListItem Selected="True">不限</asp:ListItem>
        <asp:ListItem>审批通过</asp:ListItem>
        <asp:ListItem>审批拒绝</asp:ListItem>
        <asp:ListItem>待审批</asp:ListItem>
                           </asp:DropDownList>&nbsp;&nbsp;
    <asp:Button ID="Btn_Search" runat="server" Text="搜索" OnClick="Btn_Search_Click" />
</div>
<div class="main_list_table">

    <asp:ListView ID="LV_Examine" runat="server" OnPagePropertiesChanging="LV_Examine_PagePropertiesChanging" OnItemCommand="LV_Examine_ItemCommand">
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td style="text-align: center">暂无审批！
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table class="list_table" cellspacing="0" cellpadding="0" border="0">
                <tr class="tab_td">
                    <!--表头tr名称-->
                    <th class="td_tit">编号
                    </th>
                    <th class="td_tit_left">申请名称
                    </th>
                    <th class="td_tit_left">申请类型
                    </th>

                    <th class="td_tit_left">申请人

                    </th>

                    <th class="td_tit_left">申请时间
                    </th>

                    <th class="td_tit_left">审批状态
                    </th>
                    <th class="td_tit">操作
                    </th>

                </tr>
                <tr id="itemPlaceholder" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class="tab_td td_bg">
                <td class="td_tit">
                    <%# Container.DataItemIndex + 1%>
                                                            
                </td>
                <td class="td_tit_left">
                    <%#Eval("Title")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ExamineType")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ApplyUser")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ApplyTime")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ExamineResult")%>
                </td>
                <td class="td_tit">
                    
                    <asp:LinkButton ID="LB_Pass" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Pass"><i class="iconfont">&#xe629;</i>审批通过</asp:LinkButton>

                    <asp:LinkButton ID="LB_Refuse" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Refuse"><i class="iconfont">&#xe629;</i>审批拒绝</asp:LinkButton>

                </td>
            </tr>

        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="tab_td">

                <td class="td_tit">
                    <%# Container.DataItemIndex + 1%>
                                                            
                </td>
                <td class="td_tit_left">
                    <%#Eval("Title")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ExamineType")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ApplyUser")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ApplyTime")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("ExamineResult")%>
                </td>
                <td class="td_tit">
                    <asp:LinkButton ID="LB_Pass" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Pass"><i class="iconfont">&#xe629;</i>审批通过</asp:LinkButton>

                    <asp:LinkButton ID="LB_Refuse" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Refuse"><i class="iconfont">&#xe629;</i>审批拒绝</asp:LinkButton>

                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <div class="page" style="margin:auto;">
        <asp:DataPager ID="DP_Examine" runat="server" PageSize="8" PagedControlID="LV_Examine">
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
    <%--<table id="tab" class="list_table" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr class="tab_th top_th">
                        <th class="th_tit">序号</th>
                        <th class="th_tit_left">资源名称</th>
                        <th class="th_tit_left">分类</th>
                        <th class="th_tit_left">主讲人</th>
                        <th class="th_tit_left">责任者</th>
                        <th class="th_tit">拍摄时间</th>
                        <th class="th_tit_left">格式</th>
                        <th class="th_tit_right">时长</th>
                        <th class="th_tit">操作</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>--%>
</div>
<div style="text-align: center;">
</div>
