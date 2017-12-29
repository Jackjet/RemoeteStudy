<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeManage.aspx.cs" Inherits="UserCenterSystem.GradeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                <asp:Panel ID="panelDisp" runat="server">
                    <%--专业展示--%>
                    <div class="con_list" runat="server" id="dis1">
                        <asp:ListView ID="lvDisp" runat="server" DataKeyNames="NJ" OnItemCommand="lvDisp_ItemCommand"
                             OnPagePropertiesChanging="lvDisp_PagePropertiesChanging" OnItemEditing="lvDisp_ItemEditing">
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr class="b_txt alt-row">
                                        <th>序号</th>
                                        <th>专业名称</th>
                                        <th>修改时间</th>
                                        <th>操作</th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%# Container.DataItemIndex+1 %></td>

                                    <td><%#Eval("NJMC") %></td>

                                    <td><%# Eval("XGSJ").ToString()=="" ? "" : DateTime.Parse(Eval("XGSJ").ToString()).ToString("yyyy-MM-dd")%></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                        <asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval(" NJ") %>' />
                                        <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("NJMC") %>' />
                                        <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%# Container.DataItemIndex+1 %></td>

                                    <td><%#Eval("NJMC") %></td>

                                    <td><%# Eval("XGSJ").ToString()=="" ? "" : DateTime.Parse(Eval("XGSJ").ToString()).ToString("yyyy-MM-dd") %></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                        <asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval("NJ") %>' />
                                        <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("NJMC") %>' />
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
                    <div class="pagination" runat="server" id="fenye1" style="text-align: center; padding-top: 10px;">
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
                                    
                                </div>
                </asp:Panel>
                <asp:Panel ID="panelAdd" runat="server" Visible="false">
                    <div style="width: 440px; margin: 50px auto;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    专业名称<span style="color: Red;">*</span>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="tbNJMC" runat="server" Width="350px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <asp:HiddenField ID="hfMC" runat="server" />
                                    <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
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
                <asp:HiddenField ID="hfType" runat="server" />
            </div>
        </div>
    </form>
</body>

</html>
