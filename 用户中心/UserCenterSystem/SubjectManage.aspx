<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectManage.aspx.cs" Inherits="UserCenterSystem.SubjectManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <title>学科信息</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>学科信息</h3>
            </div>
            <%--        <div style="background-color:aliceblue;width:100%;text-align:center;font-size:16px;margin-bottom:10px;margin-top:10px;">&nbsp;</div>--%>
            <div class="divMain">
                <asp:Panel ID="panelDisp" runat="server">

                    <asp:Panel ID="AddPanel" runat="server">
                        &nbsp;&nbsp;<asp:Button ID="btAdd" runat="server" Text="添加" OnClick="btAdd_Click" BackColor="Wheat" />
                    </asp:Panel>

                    <div class="con_list" runat="server" id="dis1">
                        <asp:ListView ID="lvDisp" runat="server" DataKeyNames="ID" OnItemCommand="lvDisp_ItemCommand" Visible="false"
                            OnItemEditing="lvDisp_ItemEditing" OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table runat="server" style="width: 100%;" border="1">
                                    <tr>
                                        <td>没有符合条件的信息</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%#Eval("SubjectName") %></td>
                                    <td><%#Eval("SubShortName") %></td>
                                    <td><%# Eval("CreateDate")%></td>
                                    <td><%# Eval("UpdateDate")%></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                                        <%--  <asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval("NJ") %>' />
                                <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("NJMC") %>' />
                                <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%#Eval("SubjectName") %></td>
                                    <td><%#Eval("SubShortName") %></td>
                                    <td><%# Eval("CreateDate")%></td>
                                    <td><%# Eval("UpdateDate")%></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat"></asp:Button>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                                        <%--<asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval("NJ") %>' />
                                <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("NJMC") %>' />
                                <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />--%>

                                         
                                         
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                <tr class="b_txt alt-row">
                                                    <th>学科名称</th>
                                                    <th>学科缩写名称</th>

                                                    <th>创建时间</th>
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

                                </div>
                            </td>
                        </tr>

                    </table>
                    <div class="con_list" runat="server" id="dis2">
                        <asp:ListView ID="lvDisp2" runat="server" DataKeyNames="ID" Visible="false"
                            OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table runat="server" style="width: 100%;" border="1">
                                    <tr>
                                        <td>没有符合条件的信息</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%#Eval("SubjectName") %></td>
                                    <td><%#Eval("SubShortName") %></td>
                                    <td><%# Eval("CreateDate")%></td>
                                    <td><%# Eval("UpdateDate")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%#Eval("SubjectName") %></td>
                                    <td><%#Eval("SubShortName") %></td>
                                    <td><%# Eval("CreateDate")%></td>
                                    <td><%# Eval("UpdateDate")%></td>
                                </tr>
                            </AlternatingItemTemplate>
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%;">
                                                <tr class="b_txt alt-row">
                                                    <th>学科名称</th>
                                                    <th>缩写名称</th>
                                                    <th>创建</th>
                                                    <th>修改时间</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <table style="width: 100%;">
                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                            <td>
                                <div class="pagination" runat="server" id="fenye2">
                                    <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lvDisp2">
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelAdd" runat="server" Visible="false">
                    <asp:HiddenField ID="Flagxiugai" runat="server" Value="" />
                    <div style="margin: 0 auto; width: 600px;">
                        <table>
                            <tr>
                                <td>学科名称：<span style="color: Red;">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtSubjectName" runat="server" Width="350px"></asp:TextBox>
                                    <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>学科缩写名称：</td>
                                <td>
                                    <asp:TextBox ID="txtSubjectShort" runat="server" Width="350px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>备注：</td>
                                <td>
                                    <asp:TextBox ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="350px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <div style="width: 100%;">
                                        <div style="width: 362px; height: 40px; margin-left: 97px;">
                                            <asp:Button ID="btSave" class="btnsave mr40" runat="server" Text="保存" OnClick="btSave_Click" Style="float: left;" />&nbsp;&nbsp;
                                    <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" Style="width: 60px; float: right; margin-top: 10px;" />&nbsp;&nbsp;
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hfDelete" runat="server" Value="0" />
            </div>
        </div>
    </form>
</body>

</html>
