<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManageList.aspx.cs" Inherits="UserCenterSystem.RoleManageList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link type="text/css" href="css/page.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div style="text-align: center; width: 100%">
                <h3>角色管理</h3>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div id="container">
                                <asp:Label runat="server" Text="角色名称：" ID="lb_RoleName"></asp:Label>
                                <asp:TextBox ID="tb_RealName" runat="server"></asp:TextBox>
                                &nbsp;<asp:DropDownList ID="dp_DepartMent" runat="server" AutoPostBack="True" Style="height: 30px" Width="80px">
                                    <asp:ListItem Value="" Selected="True">-学校-</asp:ListItem>

                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                 <asp:Button ID="bt_Search" runat="server" OnClick="bt_Search_Click" Text="查询" Width="60px" BackColor="Wheat" />
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="bt_AddRole" runat="server" OnClick="bt_AddUser_Click" Text="添加角色" Style="height: 30px" Width="60px" BackColor="Wheat" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="con_list">
                                <asp:ListView ID="lvRole" runat="server" DataKeyNames="RoleId" OnItemCommand="lvRole_ItemCommand" OnPagePropertiesChanging="lvRole_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table runat="server" style="border-collapse: collapse; border: 1px #F3F3F3 solid">
                                            <tr>
                                                <td style="text-align: center">暂无记录            
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr style="text-align: center">

                                            <td style="width: 20%">
                                                <%# Eval("RoleName") %>
                                            </td>
                                            <td style="width: 25%">
                                                <%# Eval("jgjc") %>
                                            </td>
                                            <td style="width: 35%">
                                                <%# Eval("CreateTime") %>
                                            </td>

                                            <td>
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                <asp:Button ID="lbtnDel" Width="40px" runat="server" CommandName="Del" CommandArgument="删除" Text="删除" OnClientClick="return confirm('您确认删除该记录吗?');" BackColor="Wheat" />


                                                <asp:HiddenField ID="hiddenRoleid" runat="server" Value='<%# Eval("RoleId") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>




                                    <AlternatingItemTemplate>
                                        <tr class="alt-row">
                                            <td style="width: 20%">
                                                <%# Eval("RoleName") %>
                                            </td>
                                            <td style="width: 25%">
                                                <%# Eval("jgjc") %>
                                            </td>
                                            <td style="width: 35%">
                                                <%# Eval("CreateTime") %>
                                            </td>

                                            <td>
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                <asp:Button ID="lbtnDel" Width="40px" runat="server" CommandName="Del" CommandArgument="删除" Text="删除" OnClientClick="return confirm('您确认删除该记录吗?');" BackColor="Wheat" />


                                                <asp:HiddenField ID="hiddenRoleid" runat="server" Value='<%# Eval("RoleId") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%;">
                                            <tr runat="server" class="b_txt alt-row">

                                                <th runat="server">角色名称</th>
                                                <th runat="server">学校名称</th>
                                                <th runat="server">上次操作时间</th>
                                                <th runat="server">操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                                <asp:HiddenField ID="hiddenStrxxzzjgh" runat="server" />
                            </div>
                        </td>
                    </tr>

                </table>

                <table style="width: 100%;">
                    <tr style="line-height: 40px; color: #333333; text-align: center;">
                        <td>
                            <div class="pagination">
                                <asp:DataPager ID="DPRole" runat="server" PageSize="10" PagedControlID="lvRole">
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
                            <%--<asp:DataPager ID="DPRole" runat="server" PageSize="15" PagedControlID="lvRole">
                            <Fields>
                                <asp:NextPreviousPagerField
                                    ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                    ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                                <asp:NumericPagerField CurrentPageLabelCssClass="ListView_a" />

                                <asp:NextPreviousPagerField
                                    ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                                    ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                                <asp:TemplatePagerField>
                                    <PagerTemplate>
                                        <span>| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                                              <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                                              (共<%# Container.TotalRowCount %>项)
                                        </span>
                                    </PagerTemplate>
                                </asp:TemplatePagerField>
                            </Fields>
                        </asp:DataPager>--%>
                        </td>
                    </tr>

                </table>

            </div>
        </div>
    </form>
</body>
</html>
