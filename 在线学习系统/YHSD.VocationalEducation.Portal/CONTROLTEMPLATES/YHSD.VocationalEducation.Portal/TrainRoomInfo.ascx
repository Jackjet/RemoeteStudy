<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrainRoomInfo.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal.TrainRoomInfo" %>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<script src="/_layouts/15/Script/uploadFile2.js"></script>
<script type="text/javascript">
    function jump()
    {
        location.href = "AddTrainRoomPage.aspx";
    }
    function jumpedit(itemid) {
        location.href = "AddTrainRoomPage.aspx?itemid="+itemid;
    }
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
    <input type="button" value="创建实训室" onclick="jump();" />
</div>
<div class="main_list_table">

    <asp:ListView ID="LV_TrainRoom" runat="server" OnPagePropertiesChanging="LV_Examine_PagePropertiesChanging" OnItemCommand="LV_Examine_ItemCommand" OnItemEditing="LV_TrainRoom_ItemEditing">
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
                    <th class="td_tit_left">实训室名称
                    </th>
                    <th class="td_tit_left">实训室地点
                    </th>

                    <th class="td_tit_left">实训室面积

                    </th>

                    <th class="td_tit_left">是否占用
                    </th>

                    <th class="td_tit_left">是否可用
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
                    <%#Eval("Place")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("Area")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("IsUse")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("IsCanUse")%>
                </td>
                <td class="td_tit">
                    
                    <a href="#" onclick='jumpedit(<%# Eval("ID") %>)'><i class="iconfont">&#xe629;</i>编辑</a>
                    <%--<asp:LinkButton ID="LB_Edit" OnClientClick='jumpedit(<%# Eval("ID") %>);return false;' runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Edit"><i class="iconfont">&#xe629;</i>编辑</asp:LinkButton>--%>

                    <asp:LinkButton ID="LB_Del" runat="server" OnClientClick="return confirm('你确定要删除吗？');" CommandArgument='<%# Eval("ID") %>' CommandName="Del"><i class="iconfont">&#xe629;</i>删除</asp:LinkButton>

                    <asp:LinkButton ID="LB_Appoint" OnClientClick='' runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Appoint"><i class="iconfont">&#xe629;</i>预约</asp:LinkButton>

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
                    <%#Eval("Place")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("Area")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("IsUse")%>
                </td>
                <td class="td_tit_left">
                    <%#Eval("IsCanUse")%>
                </td>
                <td class="td_tit">
                    <a href="#" onclick='jumpedit(<%# Eval("ID") %>)'><i class="iconfont">&#xe629;</i>编辑</a>
                    <%--<asp:LinkButton ID="LB_Edit" OnClientClick='jumpedit(<%# Eval("ID") %>);return false;' runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Edit"><i class="iconfont">&#xe629;</i>编辑</asp:LinkButton>--%>

                    <asp:LinkButton ID="LB_Del" OnClientClick="return confirm('你确定要删除吗？');" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del"><i class="iconfont">&#xe629;</i>删除</asp:LinkButton>
                    <asp:LinkButton ID="LB_Appoint" OnClientClick='' runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Appoint"><i class="iconfont">&#xe629;</i>预约</asp:LinkButton>

                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>

    <div class="page" style="margin:auto;">
        <asp:DataPager ID="DP_TrainRoom" runat="server" PageSize="8" PagedControlID="LV_TrainRoom">
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
<div style="text-align: center;">
</div>