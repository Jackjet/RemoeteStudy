<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParentList.aspx.cs" Inherits="UserCenterSystem.ParentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>家长管理</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <script src="Scripts/wbox.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>家长管理</h3>
            </div>

            <div id="divMain">

                <table>
                    <tr>
                        <td >
                            <asp:Label runat="server" Text="姓名：" ID="lb_RealName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_RealName" runat="server" Width="170px"></asp:TextBox>
                        </td>
                        <td class="CardCss">
                            <asp:Label runat="server" Text="用户账号：" ID="lb_UserName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_UserName" runat="server" Width="170px"></asp:TextBox>
                        </td>

                        <td class="CardCss">
                            <asp:Label runat="server" Text="证件号：" ID="lb_UserIdentity"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_UserIdentity" runat="server" Width="170px"></asp:TextBox>
                        </td>

                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lb_IsDelete" runat="server" Text="用户状态："></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dp_IsDelete" runat="server" class="ParentSelCss">
                                <asp:ListItem Value="" Selected="True">所有</asp:ListItem>
                                <asp:ListItem Value="0">正常</asp:ListItem>
                                <asp:ListItem Value="1">禁用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:DropDownList ID="dp_DepartMent" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dp_DepartMent_SelectedIndexChanged" class="ParentSelCss">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:DropDownList ID="dp_Grades" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dp_GradesIndexChanged" class="ParentSelCss">
                                <asp:ListItem Value="" Selected="True">-年级-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:DropDownList ID="dp_Class" runat="server" class="ParentSelCss">
                                <asp:ListItem Value="" Selected="True">-班级-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <%-- <td colspan="2">
                              &nbsp;&nbsp; <asp:Button ID="bu_DeleteAll" runat="server" Text="批量禁用" Width="60px" OnClick="bu_DeleteAll_Click" />
                              &nbsp;&nbsp;<asp:Button ID="btnEnableAll" runat="server" Text="批量启用" Width="60px" OnClick="btnEnableAll_Click" />
                        </td> --%>
                        <td class="CardCss">
                            <asp:Button ID="bt_Search" runat="server" OnClick="bt_Search_Click" Text="查询" Width="50px" BackColor="Wheat" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="con_list">
                                <asp:ListView ID="lvParent" runat="server" DataKeyNames="cysfzjh" OnItemCommand="lvParent_ItemCommand" OnPagePropertiesChanging="lvParent_PagePropertiesChanging" OnPreRender="lvParent_PreRender">
                                    <EmptyDataTemplate>
                                        <table runat="server" style="width: 100%; border: 1px #F3F3F3 solid">
                                            <tr>
                                                <td style="text-align: center">暂无记录            
               
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr style="text-align: center">
                                            <td>
                                                <%# Eval("cyxm")%> 
                                            </td>
                                            <td>
                                                <%# Eval("yhzh") %>
                                                <asp:HiddenField ID="hiddenYHZH" runat="server" Value='<%# Eval("yhzh") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("cysfzjh") %>
                                            </td>
                                            <td>
                                                <%# Eval("xbm") %>
                                            </td>
                                            <td>
                                                <%# Eval("ChildXm")%> 
                                            </td>
                                            <td>
                                                <%# Eval("NJMC")%> 
                                            </td>
                                            <td>
                                                <%# Eval("BJ")%> 
                                            </td>
                                            <td>
                                                <%# Eval("JGMC")%> 
                                            </td>
                                            <td>
                                                <%# Eval("gxm") %>
                                            </td>
                                            <td>
                                                <%# Eval("yhzt") %>
                                            </td>
                                            <td>
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" Visible="true" />
                                                <asp:Button ID="btnOperationDel" Width="40px" runat="server" CommandName="BtnDisEnable" CommandArgument='<%# Eval("cysfzjh") %>' Text="禁用" OnClientClick="return confirm('您确定要禁用该用户吗?');" />
                                                <asp:Button ID="btnOperationOK" Width="40px" runat="server" Text="启用" CommandName="BtnEnable" CommandArgument='<%# Eval("cysfzjh") %>' OnClientClick="return confirm('您确定要启用该用户吗?');" />
                                                <asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />
                                                <asp:Button runat="server" Text="解绑" Width="40px" CommandName="UnLock" CommandArgument='<%# Eval("cysfzjh") %>' ID="btnOperationUnlock" OnClientClick="return confirm('您确定解绑该用户吗?');" />
                                                <asp:HiddenField ID="hfHiddenDel" runat="server" Value='<%# Eval("yhzt") %>' />
                                                <asp:HiddenField ID="HiddenFielduserid" runat="server" Value='<%# Eval("cysfzjh") %>' />
                                                <asp:HiddenField ID="HidNo" runat="server" Value='<%# Eval("yhzh") %>' />
                                                <asp:HiddenField ID="Hid_Name" runat="server" Value='<%# Eval("cyxm")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="alt-row">
                                            <td>
                                                <%# Eval("cyxm")%> 
                                            </td>
                                            <td>
                                                <%# Eval("yhzh") %>
                                                <asp:HiddenField ID="hiddenYHZH" runat="server" Value='<%# Eval("yhzh") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("cysfzjh") %>
                                            </td>
                                            <td>
                                                <%# Eval("xbm") %>
                                            </td>
                                            <td>
                                                <%# Eval("ChildXm")%> 
                                            </td>

                                            <td>
                                                <%# Eval("NJMC")%> 
                                            </td>
                                            <td>
                                                <%# Eval("BJ")%> 
                                            </td>
                                            <td>
                                                <%# Eval("JGMC")%> 
                                            </td>
                                            <td>
                                                <%# Eval("gxm") %>
                                            </td>

                                            <td>
                                                <%# Eval("yhzt") %>
                                            </td>
                                            <td>
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" Visible="true" />
                                                <asp:Button ID="btnOperationDel" Width="40px" runat="server" CommandName="BtnDisEnable" CommandArgument='<%# Eval("cysfzjh") %>' Text="禁用" OnClientClick="return confirm('您确定要禁用该用户吗?');" />
                                                <asp:Button ID="btnOperationOK" Width="40px" runat="server" Text="启用" CommandName="BtnEnable" CommandArgument='<%# Eval("cysfzjh") %>' OnClientClick="return confirm('您确定要启用该用户吗?');" />
                                                <asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />
                                                <asp:Button runat="server" Text="解绑" Width="40px" CommandName="UnLock" CommandArgument='<%# Eval("cysfzjh") %>' ID="btnOperationUnlock" OnClientClick="return confirm('您确定解绑该用户吗?');" />
                                                <asp:HiddenField ID="hfHiddenDel" runat="server" Value='<%# Eval("yhzt") %>' />
                                                <asp:HiddenField ID="HiddenFielduserid" runat="server" Value='<%# Eval("cysfzjh") %>' />
                                                <asp:HiddenField ID="HidNo" runat="server" Value='<%# Eval("yhzh") %>' />
                                                <asp:HiddenField ID="Hid_Name" runat="server" Value='<%# Eval("cyxm")%>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                            <tr runat="server" class="b_txt alt-row">
                                                <%-- <th runat="server">
                                                <input id="cbSelectAll" type="checkbox" onclick="GetAllCheckBox(this)" />全选
                                            </th>--%>
                                                <th runat="server">家长姓名</th>
                                                <th runat="server">用户账号</th>
                                                <th runat="server">身份证号</th>
                                                <th runat="server">性别</th>
                                                <th runat="server">学生姓名</th>
                                                <th runat="server">年级</th>
                                                <th runat="server">班级</th>
                                                <th runat="server">学校</th>
                                                <th runat="server">亲子关系</th>
                                                <th runat="server">用户状态</th>
                                                <th runat="server">操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </td>
                    </tr>


                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <div class="pagination">
                                <asp:HiddenField ID="hiddenStrxxzzjgh" runat="server" />
                                <asp:DataPager ID="dataPagerParent" runat="server" PageSize="10" PagedControlID="lvParent">
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
                            <%-- <asp:HiddenField ID="hiddenStrxxzzjgh" runat="server" />
                            <asp:DataPager ID="dataPagerParent" runat="server" PageSize="10" PagedControlID="lvParent">
                                <Fields>
                                    <asp:NextPreviousPagerField
                                        ButtonType="Button" ShowNextPageButton="False" ShowPreviousPageButton="true"
                                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                                    <asp:NumericPagerField CurrentPageLabelCssClass="ListView_a" />

                                    <asp:NextPreviousPagerField
                                        ButtonType="Button" ShowPreviousPageButton="False" ShowNextPageButton="true"
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
                            </asp:DataPager>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    //全选
    function GetAllCheckBox(cbAll) {
        var items = document.getElementsByTagName("input");
        for (i = 0; i < items.length; i++) {
            if (items[i].type == "checkbox") {
                items[i].checked = cbAll.checked;
            }
        }
    }

    //开窗显示学生信息
    function funShowUserInfo(uid) {
        var wBox = jQuery("#selectuser" + uid).wBox({
            iframeWH: {
                width: 380, height: 800

            },
            requestType: "iframe", // 此例在iframe里面使用
            title: "用户详细信息",
            target: "/StudentInfo.aspx?sfzjh=" + uid
        });
        wBox.showBox();
    }

    function funShowParentInfo(pid) {

        var PwBox = jQuery("#Pselectuser" + pid).wBox({
            iframeWH: {
                width: 380, height: 800

            },
            requestType: "iframe", // 此例在iframe里面使用
            title: "用户详细信息",
            target: "/ParentInfo.aspx?cysfzjh=" + pid
        });
        PwBox.showBox();
    }



</script>
