<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CultivateArchivesManage.aspx.cs" Inherits="UserCenterSystem.CultivateArchivesManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <title>培训档案管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>培训档案管理</h3>
            </div>
            <div class="divMain">
                <asp:Panel ID="AddPanel" runat="server">
                    课程名称：<asp:TextBox ID="txtKCMC" runat="server" />&nbsp;&nbsp;
                    归档：<asp:DropDownList ID="ddlGD1" runat="server">
                        <asp:ListItem Text="全部" Value="" />
                         <asp:ListItem Text="否" Value="0" />
                        <asp:ListItem Text="是" Value="1" />
                       </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" BackColor="Wheat" />
                    &nbsp;&nbsp;<asp:Button ID="btAdd" runat="server" Text="添加" OnClick="btAdd_Click" BackColor="Wheat" />&nbsp;
                    <input type="button" id="hidbtnEcxelImport111" value="统计" onclick="javascript: window.location.href = 'TJPXKC.html'" style="background-color: wheat;" />&nbsp;
                    <input type="button" onclick="javascript: history.back(1);" value="返回" class="btn_concel" />
                </asp:Panel>
                <asp:Panel ID="panelDisp" runat="server">
                    <%--专业展示--%>
                    <div class="con_list" runat="server" id="dis1">
                        <asp:ListView ID="lvDisp" runat="server" DataKeyNames="id" OnItemCommand="lvDisp_ItemCommand"
                             OnPagePropertiesChanging="lvDisp_PagePropertiesChanging" OnItemEditing="lvDisp_ItemEditing">
                            <LayoutTemplate>
                                <table style="width: 100%;">
                                    <tr class="b_txt alt-row">
                                        <th>序号</th>
                                        <th>课程名称</th>
                                        <th>时长</th>
                                        <th>是否归档</th>
                                        <th>操作</th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center;">
                                    <td><%# Container.DataItemIndex+1 %></td>

                                    <td><%#Eval("KCMC") %></td>

                                    <td><%# Eval("SC")%></td>
                                    <td><%# Eval("GD").ToString().Trim()=="" ? "否" : Eval("GD").ToString()%></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <%--<asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>--%>
                                        <asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval("id") %>' />
                                        <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("KCMC") %>' />
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("GD") %>' />
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("KHJG") %>' />
                                        <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("SC") %>' />
                                        <asp:HiddenField ID="HiddenField4" runat="server" Value='<%#Eval("KCID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="alt-row">
                                    <td><%# Container.DataItemIndex+1 %></td>

                                    <td><%#Eval("KCMC") %></td>

                                    <td><%# Eval("SC")%></td>
                                    <td><%# Eval("GD").ToString().Trim()=="" ? "否" : Eval("GD").ToString()%></td>
                                    <td>
                                        <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                        <%--<asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>--%>
                                        <asp:HiddenField ID="hfNJ" runat="server" Value='<%#Eval("id") %>' />
                                        <asp:HiddenField ID="hfNJMC" runat="server" Value='<%#Eval("KCMC") %>' />
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("GD") %>' />
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("KHJG") %>' />
                                        <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("SC") %>' />
                                        <asp:HiddenField ID="HiddenField4" runat="server" Value='<%#Eval("KCID") %>' />
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
                                    课程名称<span style="color: Red;">*</span>
                                    
                                </td>
                                <td>
                                    <asp:DropDownList ID="sssss" runat="server" >
                                       <%--<asp:ListItem Text="语文" Value="1" />
                                        <asp:ListItem Text="数学" Value="2" />
                                        <asp:ListItem Text="英语" Value="3" />
                                        <asp:ListItem Text="化学" Value="4" />
                                        <asp:ListItem Text="物理" Value="5" />--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    时长<span style="color: Red;"></span>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    归档<span style="color: Red;"></span>
                                    
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlgd" runat="server" >
                                        <asp:ListItem Text="否" Value="0" />
                                        <asp:ListItem Text="是" Value="1" />
                                    </asp:DropDownList>
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
                                <td>考核结果</td>
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
