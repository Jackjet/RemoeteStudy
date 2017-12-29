<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamManage.aspx.cs" Inherits="UserCenterSystem.TeamManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <title>教研组管理</title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        //刷新时滚动条保留位置
        // function ScrollToSelectNode() {
        //  alert(<%=ScrollValue%>);
        //  $("#divLeft").scrollTop(<%=ScrollValue%>);

        // }
        // function bindData() {
        // alert($("#divLeft").scrollTop());
        //  $("#divScrollValue").val($("#divLeft").scrollTop());
        // }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>教研组管理</h3>
            </div>
            <div>
                <table>
                    <tr>
                        <td valign="top">
                            <div id="divleft" class="divLeft" style="height: 500px; overflow-y: auto; overflow-x: hidden;">
                                <asp:TreeView ID="tvTeam" runat="server" ShowLines="true" SelectedNodeStyle-BackColor="#99ccff"
                                    OnSelectedNodeChanged="tvTeam_SelectedNodeChanged" Width="250px">
                                </asp:TreeView>
                            </div>
                        </td>
                        <td style="width: 100%;" valign="top">

                            <asp:Panel ID="AddTeamPanel" runat="server" Visible="false" Style="width: 540px; margin: 20px auto;">
                                <table>
                                    <tr>
                                        <td>教研组名称<span style="color: Red;">*</span></td>
                                        <td>
                                            <asp:TextBox ID="tbJYZMC" runat="server" Width="350px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredJYZM" runat="server" ControlToValidate="tbJYZMC"
                                                ErrorMessage="教研组名称不能为空" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>备注</td>
                                        <td>
                                            <asp:TextBox ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="350px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="width: 362px; margin-left: 76px;">
                                                <asp:Button ID="btSave" class="btnsave mr40" runat="server" Text="保存" OnClick="btSave_Click" Style="float: left;" />&nbsp;&nbsp;
                                        <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" CausesValidation="false" Style="width: 60px; float: right; margin-top: 10px;" />&nbsp;&nbsp;
                                        <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="TeamDispPanel" runat="server" Visible="false">
                                <asp:Panel ID="btAddTeamPanel" runat="server">
                                    <div>
                                        <asp:Button ID="btAddTeam" runat="server" Text="添加教研组" OnClick="btAddTeam_Click" BackColor="Wheat" />
                                    </div>
                                </asp:Panel>
                                <div class="con_list">
                                    <asp:ListView ID="lvTeamDisp" runat="server" OnItemCommand="lvTeamDisp_ItemCommand"
                                        OnPagePropertiesChanging="lvTeamDisp_PagePropertiesChanging">
                                        <EmptyDataTemplate>
                                            <table runat="server" class="BoxCss" style="width: 100%;">
                                                <tr>
                                                    <td>没有符合条件的信息</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: center;">
                                                <td><%#Eval("JYZMC") %></td>
                                                <td><%#Eval("BZ") %></td>
                                                <td>
                                                    <asp:Button ID="btEdit" runat="server" CommandName="editTeam" Text="修改" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="btDel" runat="server" CommandName="delTeam" Text="删除" BackColor="Wheat"></asp:Button>
                                                    <asp:HiddenField ID="hfJYZID" runat="server" Value='<%#Eval("JYZID") %>' />
                                                    <asp:HiddenField ID="hfJYZMC" runat="server" Value='<%#Eval("JYZMC") %>' />
                                                    <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="alt-row">
                                                <td><%#Eval("JYZMC") %></td>
                                                <td><%#Eval("BZ") %></td>
                                                <td>
                                                    <asp:Button ID="btEdit" runat="server" CommandName="editTeam" Text="修改" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="btDel" runat="server" CommandName="delTeam" Text="删除" BackColor="Wheat"></asp:Button>
                                                    <asp:HiddenField ID="hfJYZID" runat="server" Value='<%#Eval("JYZID") %>' />
                                                    <asp:HiddenField ID="hfJYZMC" runat="server" Value='<%#Eval("JYZMC") %>' />
                                                    <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("BZ") %>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <LayoutTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%; text-align: center;">
                                                            <tr class="b_txt alt-row">
                                                                <th>教研组名称</th>
                                                                <th>备注</th>
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
                                <asp:Panel ID="dpTeamDispPanel" runat="server">
                                    <table style="width: 100%;">
                                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                                            <td>
                                                <div class="pagination">
                                                    <asp:DataPager ID="dpTeamDisp" runat="server" PageSize="15" PagedControlID="lvTeamDisp">
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
                            </asp:Panel>

                            <%--下面Panel：添加/删除教研组成员--%>
                            <asp:Panel ID="AddPersonPanel" runat="server" Visible="false">
                                <div>当前所选教研组名称是“<asp:Label ID="lbTeamName" runat="server" ForeColor="Orange"></asp:Label>”，在此页可以添加人员到教研组。</div>
                                <br />
                                <br />
                                <div style="width: 100%;">
                                    <div style="font-weight: bolder;">目前教研组成员：</div>
                                    <div class="con_list">
                                        <asp:ListView ID="lvTeamPersons" runat="server" OnItemCommand="lvTeamPersons_ItemCommand"
                                            OnPagePropertiesChanging="lvTeamPersons_PagePropertiesChanging">
                                            <EmptyDataTemplate>
                                                <table runat="server" border="1" style="width: 100%;">
                                                    <tr>
                                                        <td>没有符合条件的信息</td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr style="text-align: center;">
                                                    <td><%#Eval("XM") %></td>
                                                    <td><%#Eval("XJTGW") %></td>
                                                    <td><%#Eval("JGMC") %></td>
                                                    <%--<td><%#Eval("GH") %></td>--%>
                                                    <td>
                                                        <asp:Button ID="lbClose" runat="server" CommandName="delPerson" Text="删除" BackColor="Wheat"></asp:Button>
                                                        <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="alt-row">
                                                    <td><%#Eval("XM") %></td>
                                                    <td><%#Eval("XJTGW") %></td>
                                                    <td><%#Eval("JGMC") %></td>
                                                    <%--<td><%#Eval("GH") %></td>--%>
                                                    <td>
                                                        <asp:Button ID="lbClose" runat="server" CommandName="delPerson" Text="删除" BackColor="Wheat"></asp:Button>
                                                        <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <LayoutTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%;">
                                                                <tr class="b_txt alt-row">
                                                                    <th>教师姓名</th>
                                                                    <th>现具体岗位</th>
                                                                    <th>所属组织机构</th>
                                                                    <%--<th>工号</th>--%>
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
                                    <asp:Panel ID="dpPanelTeamPersons" runat="server">
                                        <table style="width: 100%;">
                                            <tr style="line-height: 40px; color: #333333; text-align: center;">
                                                <td>
                                                    <div class="pagination">
                                                        <asp:DataPager ID="dpTeamPersons" runat="server" PageSize="15" PagedControlID="lvTeamPersons">
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
                                                        <%--<asp:DataPager ID="dpTeamPersons" runat="server" PageSize="15" PagedControlID="lvTeamPersons">
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
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <br />
                                <br />
                                <div>
                                    <div style="font-weight: bolder;">添加人员到教研组：</div>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>教师姓名：<asp:TextBox ID="tbPersonName" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:Button ID="btSearch" runat="server" Text="搜索" OnClick="btSearch_Click" BackColor="Wheat" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div class="con_list">
                                                    <asp:ListView ID="lvAddPerson" runat="server" OnItemCommand="lvAddPerson_ItemCommand"
                                                        OnPagePropertiesChanging="lvAddPerson_PagePropertiesChanging">
                                                        <EmptyDataTemplate>
                                                            <table runat="server" border="1" style="width: 100%;">
                                                                <tr>
                                                                    <td>没有符合条件的信息</td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <tr style="text-align: center;">
                                                                <td><%#Eval("XM") %></td>
                                                                <td><%#Eval("XJTGW") %></td>
                                                                <td><%#Eval("JGMC") %></td>
                                                                <%--<td><%#Eval("GH") %></td>--%>
                                                                <td>
                                                                    <asp:Button ID="lbAuth" runat="server" CommandName="addPerson" Text="添加" BackColor="Wheat"></asp:Button>
                                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="alt-row">
                                                                <td><%#Eval("XM") %></td>
                                                                <td><%#Eval("XJTGW") %></td>
                                                                <td><%#Eval("JGMC") %></td>
                                                                <%--<td><%#Eval("GH") %></td>--%>
                                                                <td>
                                                                    <asp:Button ID="lbAuth" runat="server" CommandName="addPerson" Text="添加" BackColor="Wheat"></asp:Button>
                                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%#Eval("SFZJH") %>' />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                        <LayoutTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <table id="itemPlaceholderContainer" runat="server" border="1" style="width: 100%; text-align: center;">
                                                                            <tr class="b_txt alt-row">
                                                                                <th>教师姓名</th>
                                                                                <th>现具体岗位</th>
                                                                                <th>所属组织机构</th>
                                                                                <%--<th>工号</th>--%>
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
                                                <asp:Panel ID="dpPanelAddPerson" runat="server">
                                                    <table style="width: 100%;">
                                                        <tr style="line-height: 40px; color: #333333; text-align: center;">
                                                            <td>
                                                                <div class="pagination">

                                                                    <asp:DataPager ID="dpAddPerson" runat="server" PageSize="15" PagedControlID="lvAddPerson">
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


                                                                    <%--<asp:DataPager ID="dpAddPerson" runat="server" PageSize="15" PagedControlID="lvAddPerson">
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
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td valign="top" style="float: right; display: none">
                                                <asp:TreeView ID="tvTeacherDept" runat="server" ShowLines="true"
                                                    OnSelectedNodeChanged="tvTeacherDept_SelectedNodeChanged"
                                                    Width="200px" SelectedNodeStyle-BackColor="#99ccff">
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfJYZID" runat="server" Value="" />
            </div>
        </div>
    </form>
</body>
</html>
