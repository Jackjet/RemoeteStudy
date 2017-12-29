<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceManager.aspx.cs" Inherits="UserCenterSystem.InterfaceManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/page.css" rel="stylesheet" />
    <title>接口信息管理</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script type="text/javascript">
        var wbox;
        function funExcelImportStu(id) {
            wbox = $("#EditButton").wBox({
                iframeWH: {
                    width: 400, height: 200
                },
                requestType: "iframe",
                title: "编辑",
                target: "InterfaceManagerAdd.aspx?id=" + id + "&t=" + new Date().getTime()
            });
            wbox.showBox();
        }
        function funaddImportStu() {
            wbox = $("#btnaddInfo").wBox({
                iframeWH: {
                    width: 400, height: 200
                },
                requestType: "iframe",
                title: "编辑",
                target: "InterfaceManagerAdd.aspx?t=" + new Date().getTime()
            });
            wbox.showBox();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>接口信息管理</h3>
            </div>
            <div id="contentAll" class="con_list">
                <input id="btnaddInfo" type="button" value="添加" style="background-color: wheat; margin: 5px; width: 51px; height: 28px;" onclick="funaddImportStu()" />
                <asp:ListView ID="lvSystem" runat="server" DataKeyNames="ID" OnPagePropertiesChanging="lvSystem_PagePropertiesChanging" OnItemEditing="lvSystem_ItemEditing" OnItemCanceling="lvSystem_ItemCanceling" Style="width: 100%;" OnItemCommand="lvSystem_ItemCommand">
                    <EditItemTemplate>
                        <tr style="">

                            <td>
                                <asp:TextBox ID="txtID" runat="server" ReadOnly="true" Text='<%# Bind("ID") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvName"
                                    runat="server"
                                    ErrorMessage="接口名称不能为空"
                                    ForeColor="Red"
                                    ControlToValidate="txtName"
                                    ValidationGroup="Up"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hfname" runat="server" Value='<%# Eval("Name") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="txtInformation" runat="server" Text='<%# Bind("Information") %>' TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvManager"
                                    runat="server"
                                    ErrorMessage="接口描述不能为空"
                                    ForeColor="Red"
                                    ControlToValidate="txtInformation"
                                    ValidationGroup="Up"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlService" runat="server">
                                    <asp:ListItem Text="UserInfo.asmx" Value="UserInfo.asmx"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfService" runat="server" Value='<%# Eval("ServiceName") %>' />
                            </td>
                            <%--<td>
                                <asp:DropDownList ID="ddlTableName" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfTableName" runat="server" Value='<%# Eval("TableName") %>' />
                            </td>--%>
                            <td>
                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" ValidationGroup="Up" BackColor="Wheat" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" CausesValidation="false" BackColor="Wheat" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>未返回数据。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">

                            <td></td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvName"
                                    runat="server"
                                    ErrorMessage="接口名称不能为空"
                                    ForeColor="Red"
                                    ControlToValidate="txtName"
                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInformation" runat="server" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvInformation"
                                    runat="server"
                                    ErrorMessage="接口描述不能为空"
                                    ForeColor="Red"
                                    ControlToValidate="txtInformation"
                                    ValidationGroup="Add"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlService" runat="server">
                                    <asp:ListItem Text="UserInfo.asmx" Value="UserInfo.asmx"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <%--<td>
                                <asp:DropDownList ID="ddlTableName" runat="server">
                                </asp:DropDownList>
                            </td>--%>
                            <td>
                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" ValidationGroup="Add" BackColor="Wheat" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="text-align: center">

                            <td>
                                <%# Eval("ID") %>
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                            <td>
                                <%# Eval("Information") %>
                            </td>
                            <td>
                                <%# Eval("ServiceName") %>
                            </td>
                            <%--<td>
                                <%# Eval("TableName") %>
                            </td>--%>
                            <td>
                                <%--<asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" CausesValidation="false" BackColor="Wheat" />--%>

                                <input id="EditButton" type="button" value="修改" onclick="funExcelImportStu(<%# Eval("ID") %>);" style="background-color: wheat;" />
                                <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                <asp:HiddenField ID="HidID" runat="server" Value='<%# Eval("ID")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="alt-row">

                            <td>
                                <%# Eval("ID") %>
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                            <td>
                                <%# Eval("Information") %>
                            </td>
                            <td>
                                <%# Eval("ServiceName") %>
                            </td>
                            <%--<td>
                                <%# Eval("TableName") %>
                            </td>--%>
                            <td>
                                <%--<asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" CausesValidation="false" BackColor="Wheat" />--%>
                                <input id="EditButton" type="button" value="修改" onclick="funExcelImportStu(<%# Eval("ID") %>);" style="background-color: wheat;" />
                                <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                <asp:HiddenField ID="HidID" runat="server" Value='<%# Eval("ID")%>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <LayoutTemplate>
                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%">
                            <tr runat="server" class="b_txt alt-row">

                                <th runat="server" style="width: 30px;">ID</th>
                                <th runat="server" style="width: 90px;">接口名称</th>
                                <th runat="server" style="width: 400px;">接口描述</th>
                                <th runat="server" style="width: 60px;">服务页面</th>
                                <%--<th runat="server" style="width: 60px;">数据表</th>--%>
                                <th runat="server" style="width: 90px;">操作</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div class="pagination">
                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="8" PagedControlID="lvSystem" style="margin-left: 350px;">
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
    </form>
</body>
</html>
