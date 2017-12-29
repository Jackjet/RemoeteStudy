<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceConfigManager.aspx.cs" Inherits="UserCenterSystem.InterfaceConfigManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/page.css" rel="stylesheet" />
    <title>接口权限管理</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script type="text/javascript">
        var wbox;
        function funExcelImportStu(id) {
            wbox = $("#EditButton").wBox({
                iframeWH: {
                    width: 500, height: 250
                },
                requestType: "iframe",
                title: "编辑",
                target: "InterfaceConfigManagerAdd.aspx?id=" + id + "&t=" + new Date().getTime()
            });
            wbox.showBox();
        }
        function funaddImportStu() {
            wbox = $("#btnaddInfo").wBox({
                iframeWH: {
                    width: 500, height: 250
                },
                requestType: "iframe",
                title: "编辑",
                target: "InterfaceConfigManagerAdd.aspx?t=" + new Date().getTime()
            });
            wbox.showBox();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>接口权限管理</h3>
            </div>
            <div id="contentAll" class="con_list">
                <input id="btnaddInfo" type="button" value="添加" style="background-color: wheat; margin: 5px; width: 51px; height: 28px;" onclick="funaddImportStu()" />
                <asp:ListView ID="lvSystem" runat="server" DataKeyNames="ID" OnItemInserting="lvSystem_ItemInserting" OnItemUpdating="lvSystem_ItemUpdating" OnPagePropertiesChanging="lvSystem_PagePropertiesChanging" OnItemEditing="lvSystem_ItemEditing" OnItemCanceling="lvSystem_ItemCanceling"   Style="width: 100%;" OnItemCommand="lvSystem_ItemCommand"> 
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>未返回数据。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate> 
                    <ItemTemplate>
                        <tr style="text-align: center">

                            <td>
                                <%# Eval("ID") %>
                            </td>
                            <td>
                                <%# Eval("SystemName") %>
                            </td>
                            <td>
                                <%# Eval("InterfaceName") %>
                            </td>
                            <td>
                                <%# Eval("DataItems") %>
                            </td>
                            <td>
                                <%# Eval("TableName") %>
                            </td>
                            <td>

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
                                <%# Eval("SystemName") %>
                            </td>
                            <td>
                                <%# Eval("InterfaceName") %>
                            </td>
                            <td>
                                <%# Eval("DataItems") %>
                            </td>
                            <td>
                                <%# Eval("TableName") %>
                            </td>
                            <td>

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
                                <th runat="server" style="width: 90px;">系统名称</th>
                                <th runat="server" style="width: 100px;">接口名称</th>
                                <th runat="server" style="width: 400px;">返回数据项</th>
                                <th runat="server" style="width: 80px;">数据表</th>
                                <th runat="server" style="width: 90px;">操作</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div class="pagination" style="margin: 0px auto; width: 495px;">
                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="8" PagedControlID="lvSystem">
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
