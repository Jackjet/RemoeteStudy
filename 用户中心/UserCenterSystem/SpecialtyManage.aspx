<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialtyManage.aspx.cs" Inherits="UserCenterSystem.SpecialtyManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--  <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    --%>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <title>专业管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>专业管理</h3>
            </div>
            <div class="divMain">
                <asp:Panel ID="AddPanel" runat="server">
                    &nbsp;&nbsp;<asp:Button ID="btAdd" runat="server" Text="添加" OnClick="btAdd_Click" BackColor="Wheat" />
                </asp:Panel>
                <%--Begin 专业展示--%>
                <asp:Panel ID="panelDisp" runat="server">
                    <div class="con_list" runat="server" id="dis1">
                        <asp:ListView ID="lvDisp" runat="server" DataKeyNames="ID" OnItemCommand="lvDisp_ItemCommand"
                            OnPagePropertiesChanging="lvDisp_PagePropertiesChanging" OnItemEditing="lvDisp_ItemEditing">
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                <tr class="b_txt alt-row">
                                                    <th>专业</th>
                                                    <th>添加时间</th>
                                                    <th>修改时间</th>
                                                    <th>操作</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%#Eval("ZYMC") %></td>

                                    <td><%# Eval("TJSJ").ToString()=="" ? "" : DateTime.Parse(Eval("TJSJ").ToString()).ToString("yyyy-MM-dd") %></td>

                                    <td><%# Eval("XGSJ").ToString()=="" ? "" : DateTime.Parse(Eval("XGSJ").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:HiddenField ID="hfZYMC" runat="server" Value='<%#Eval("ZYMC") %>' />
                                        <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%#Eval("ZYMC") %></td>

                                    <td><%# Eval("TJSJ").ToString()=="" ? "" : DateTime.Parse(Eval("TJSJ").ToString()).ToString("yyyy-MM-dd") %></td>

                                    <td><%# Eval("XGSJ").ToString()=="" ? "" : DateTime.Parse(Eval("XGSJ").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:HiddenField ID="hfZYMC" runat="server" Value='<%#Eval("ZYMC") %>' />
                                        <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="width: 100%;" border="1">
                                    <tr>
                                        <td>没有符合条件的信息</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                    <table style="width: 100%;">
                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                            <td>
                                <div class="pagination" runat="server" id="fenye1">
                                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvDisp">
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
                                    <%--  <asp:DataPager ID="DPTeacher" runat="server" PageSize="15" PagedControlID="lvDisp">
                                        <Fields>
                                            <asp:NextPreviousPagerField  
                                                ButtonType="Link" ShowNextPageButton="false" ShowPreviousPageButton="true"
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
                                </div>
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
                <%--End 专业展示--%>
                <%--Begin 新增、修改--%>
                <asp:Panel ID="panelAdd" runat="server" Visible="false">
                    <div style="width: 440px; margin: 50px auto;">
                        <table style="width: 100%;">
                            <tr>
                                <td>专业名称<span style="color: Red;">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbZYMC" runat="server" Width="350px"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr><td></td><td><asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label></td></tr>
                            <tr>
                                <td>备注</td>
                                <td>
                                    <asp:TextBox ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="350px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="margin-left: 153px;">
                                        <asp:Button ID="btSave" class="btnsave mr40" runat="server" Text="保存" OnClick="btSave_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" />&nbsp;&nbsp;
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <%--End 新增、修改--%>
                <asp:HiddenField ID="hfSaveID" runat="server" />
                <asp:HiddenField ID="hidOperation" runat="server" Value="操作" />
            </div>
        </div>
    </form>
</body>

</html>
