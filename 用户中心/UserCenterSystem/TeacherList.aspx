<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherList.aspx.cs" Inherits="UserCenterSystem.TeacherList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教师管理</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <%--<link href="css/main.css" rel="stylesheet" />--%>


    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script type="text/javascript">
        function GetAllCheckBox(cbAll) {
            var items = document.getElementsByTagName("input");
            for (i = 0; i < items.length; i++) {
                if (items[i].type == "checkbox") {
                    items[i].checked = cbAll.checked;
                }
            }
        }
        function funExcelImportStu() {
            var wBoxExcelImport = $("#TeacherDR").wBox({
                iframeWH: {
                    width: 400, height: 200
                },
                requestType: "iframe",
                title: "教师导入",
                target: "TeacherImport.aspx"
            });
            wBoxExcelImport.showBox();
        }

    </script>

    <style type="text/css">
        .auto-style3 {
            width: 131px;
        }

        .auto-style4 {
            padding-left: 16px;
            width: 621px;
        }

        #tvDepartment a {
            color: #33abdf;
        }
    </style>
</head>
<body>
    <form id="TeacherListForm" runat="server">
        <a name="startPotion"></a>
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>培训师资<asp:Literal ID="Lb_DepartMent" runat="server" Visible="false"/></h3>
            </div>
            <div class="divMain">
                <table style="width:100%;">
                    <tr>
                        <td style="vertical-align: top;display:none;">
                            <asp:TreeView ID="tvDepartment" runat="server" ShowLines="true"
                                SelectedNodeStyle-BackColor="#99ccff" OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" Style="height: 470px; overflow-y: auto; overflow-x: hidden; width: 200px;" Visible="false">
                            </asp:TreeView>
                        </td>
                        <td style="vertical-align: top; width: 100%;">
                            <asp:Panel ID="TeacherInfo" runat="server" Visible="true">
                                <div id="container">

                                    <span style="margin-left: 10px;">姓名：</span>

                                    <asp:TextBox ID="txtXM" runat="server" Width="150px"></asp:TextBox>
                                    <span>账号：</span>
                                    <asp:TextBox ID="txtZH" runat="server" Width="150px"></asp:TextBox>
                                    <span>身份证：</span>
                                    <asp:TextBox ID="txtSFZJH" runat="server" Width="150px"></asp:TextBox>
                                    <%--<span>状态：</span>
                                    <asp:DropDownList ID="ddlZT" runat="server" Style="height: 29px; width: 80px;">
                                        <asp:ListItem>所有</asp:ListItem>
                                        <asp:ListItem>启用</asp:ListItem>
                                        <asp:ListItem>禁用</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" BackColor="Wheat" Style="margin-left: 5px;" />&nbsp;
                                    <input id="TeacherDR" type="button" value="导入" onclick="funExcelImportStu();" style="background-color: wheat;" />&nbsp;
                                    
                                    <asp:Button ID="TeacherDC" runat="server" Text="导出" OnClick="TeacherDC_Click" BackColor="Wheat" Style="margin-left: 5px;"  OnClientClick="Javascript:return confirm('确定要导出吗？');"/>&nbsp;
                                    <input id="TeacherPLXG" type="button" value="批量修改" onclick="funExcelImportStu();" style="background-color: wheat;" />&nbsp;
                                    <asp:Button ID="Add" runat="server" Text="添加" OnClick="Add_Click" BackColor="Wheat" Visible="false" />&nbsp;
                                    <asp:Button ID="Button2" runat="server" Text="批量通知"  BackColor="Wheat"  />&nbsp;
                                    <asp:Button ID="Btn_Department" runat="server" Text="分配教师" OnClick="Btn_Department_Click" Visible="false" Style="background-color: wheat" />&nbsp;
                                    <input type="button" onclick="javascript: history.back(1);" value="返回" class="btn_concel" />
                                    <%--<div style="margin: 5px 0 0 10px;">
                                        
                                    </div>--%>
                                </div>
                                <div class="con_list">
                                    <asp:ListView ID="lvPeriod" runat="server" DataKeyNames="SFZJH" OnItemCommand="lvPeriod_ItemCommand" OnPagePropertiesChanging="lvPeriod_PagePropertiesChanging" OnPreRender="lvPeriod_PreRender" OnDataBound="lvPeriod_DataBound" OnSelectedIndexChanging="lvPeriod_SelectedIndexChanging">
                                        <EmptyDataTemplate>
                                            <table runat="server" style="width: 100%; border: 1px #F3F3F3 solid">
                                                <tr>
                                                    <td>没有符合条件的信息</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: center;">
                                                <td>
                                                    <%# Eval("XM") %> 
                                                </td>
                                                <td>
                                                    <%# Eval("YHZH") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SFZJH") %>
                                                </td>
                                                <%--<td><%# Eval("XXMC") %></td>--%>
                                                <td>
                                                    <%# Eval("XBM") %>
                                                </td>

                                               <%-- <td>
                                                    <%# Eval("YHZT") %>
                                                </td>--%>
                                                <td>
                                                    <asp:Button ID="Button1"  runat="server" CommandName="加入队列" CommandArgument="加入队列" Text="加入队列" BackColor="Wheat" />
                                                    <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                    <%--<asp:Button ID="lbtnEnable" runat="server" CommandName="Enable" CommandArgument="启用" Text="启用" class="BtnBackGroundColorCss" OnClientClick="return confirm('您确定要启用该用户吗?');"></asp:Button>--%>
                                                    <%--<asp:Button ID="lbtnDisable" runat="server" CommandName="Enable" CommandArgument="禁用" Text="禁用" class="BtnBackGroundColorCss" OnClientClick="return confirm('您确定要禁用该用户吗?');"></asp:Button>--%>
                                                    <%--<asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />--%>
                                                    <%--<asp:Button ID="btnnUnbind" runat="server" CommandName="Unbind" Text="解绑" OnClientClick="return confirm('您确定解绑该用户吗?');" />--%>
                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                                    <asp:HiddenField ID="hfXM" runat="server" Value='<%# Eval("XM") %>' />
                                                    <asp:HiddenField ID="HiddenXXZZJGH" runat="server" Value='<%# Eval("XXZZJGH") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="alt-row">
                                                <td>
                                                    <%# Eval("XM") %> 
                                                </td>
                                                <td>
                                                    <%# Eval("YHZH") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SFZJH") %>
                                                </td>
                                                   <%--<td><%# Eval("XXMC") %></td>--%>
                                                <td>
                                                    <%# Eval("XBM") %>
                                                </td>

                                                <%--<td>
                                                    <%# Eval("YHZT") %>
                                                </td>--%>
                                                <td>
                                                    <asp:Button ID="Button1"  runat="server" CommandName="加入队列" CommandArgument="加入队列" Text="加入队列" BackColor="Wheat" />
                                                    <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                    <%--<asp:Button ID="lbtnEnable" runat="server" CommandName="Enable" CommandArgument="启用" Text="启用" class="BtnBackGroundColorCss" OnClientClick="return confirm('您确定要启用该用户吗?');"></asp:Button>--%>
                                                    <%--<asp:Button ID="lbtnDisable" runat="server" CommandName="Enable" CommandArgument="禁用" Text="禁用" class="BtnBackGroundColorCss" OnClientClick="return confirm('您确定要禁用该用户吗?');"></asp:Button>--%>
                                                    <%--<asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />--%>
                                                    <%--<asp:Button ID="btnnUnbind" runat="server" CommandName="Unbind" Text="解绑" OnClientClick="return confirm('您确定解绑该用户吗?');" />--%>
                                                    <asp:HiddenField ID="hfSFZJH" runat="server" Value='<%# Eval("SFZJH") %>' />
                                                    <asp:HiddenField ID="hfYHZH" runat="server" Value='<%# Eval("YHZH") %>' />
                                                    <asp:HiddenField ID="hfYHZT" runat="server" Value='<%# Eval("YHZT") %>' />
                                                    <asp:HiddenField ID="hfXM" runat="server" Value='<%# Eval("XM") %>' />
                                                    <asp:HiddenField ID="HiddenXXZZJGH" runat="server" Value='<%# Eval("XXZZJGH") %>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <LayoutTemplate>
                                            <table id="itemPlaceholderContainer" runat="server" style="width: 100%; border: 1px #F3F3F3 solid">
                                                <tr runat="server" class="b_txt alt-row">
                                                    <th runat="server">姓名</th>
                                                    <th runat="server">账号</th>
                                                    <th runat="server">身份证号</th>
                                                    <%--<th runat="server">学校名称</th>--%>
                                                    <th runat="server">性别</th>
                                                    <%--<th runat="server">状态</th>--%>
                                                    <th runat="server">操作</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="pagination" style="text-align: center; padding-top: 10px;">
                                    <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvPeriod">
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

                            <%--下面Panel：添加/删除教研组成员--%>
                            <asp:Panel ID="AddPersonPanel" runat="server" Visible="false" Style="width: 100%; float: right; height: 500px; overflow-x: hidden; overflow-y: auto;">
                                <div>
                                    当前所选组织机构名称是“<asp:Label ID="lbTeamName" runat="server" ForeColor="Orange"></asp:Label>”，在此页可以添加人员到组织机构。&nbsp;
                                    <asp:Button ID="Btn_Back" runat="server" Text="返回" OnClick="Btn_Back_Click" Style="background-color: wheat" />
                                </div>
                                <br />
                                <div style="width: 100%;">
                                    <div style="font-weight: bolder;">目前组织机构成员：</div>
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
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <br />
                                <br />
                                <div>
                                    <div style="font-weight: bolder;">添加人员到组织机构：</div>
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

                                                                    <asp:DataPager ID="dpAddPerson" runat="server" PageSize="10" PagedControlID="lvAddPerson">
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
            </div>
        </div>
        <!--公共弹窗 -->

    </form>
    <%--    <a href="#" title="返回顶部" id="goto-top"></a>--%>
</body>
</html>
