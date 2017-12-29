<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="UserCenterSystem.StudentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学员档案</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <link href="Scripts/wbox/wbox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <script src="Scripts/wbox.js"></script>

    <script type="text/javascript">
        //批量分班 
        function MyFun() {
            var XXZZJGH = $("#hiddenxxzzjgh").val();
            var wBoxExcelD = jQuery("#btnDivideClass").wBox({
                iframeWH: {
                    width: 350, height: 600
                },
                requestType: "iframe", // 此例在iframe里面使用
                title: "Excel分班",
                target: "/StuDivideClass.aspx"
            });
            wBoxExcelD.showBox();
        }

        //全选
        function GetAllCheckBox(cbAll) {
            var items = document.getElementsByTagName("input");
            for (i = 0; i < items.length; i++) {
                if (items[i].type == "checkbox") {
                    items[i].checked = cbAll.checked;
                }
            }
        }

        //点击姓名查看学生信息
        function funShowUserInfo(uid) {
            var wBox = jQuery("#selectuser" + uid).wBox({
                iframeWH: {
                    width: 350, height: 600
                },
                requestType: "iframe", // 此例在iframe里面使用
                title: "学生信息查看",
                target: "/StudentInfo.aspx?sfzjh=" + uid
            });
            wBox.showBox();
        }

        //execl分班
        function funExcelClassStu() {
            var wBoxExcel = jQuery("#btnExcelDivideClass").wBox({
                iframeWH: {
                    width: 350, height: 300
                },
                requestType: "iframe", // 此例在iframe里面使用
                title: "Excel分班",
                target: "/StuDivideClassByExcel.aspx"
            });
            wBoxExcel.showBox();
        }

        //execl 导入
        function funExcelImportStu() {
            var wBoxExcelImport = jQuery("#hidbtnEcxelImport").wBox({
                iframeWH: {
                    width: 400, height: 300
                },
                requestType: "iframe", // 此例在iframe里面使用
                title: "Excel学生导入",
                target: "/StuImport.aspx"
            });
            wBoxExcelImport.showBox();
        }


    </script>
</head>
<body>
    <form id="StudentListForm" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center">
                <h3>学员档案<asp:Literal ID="Lb_DepartMent" runat="server" Visible="false"/></h3>
            </div>
            <div class="divMain">
                <table style="width:100%;">
                    <tr>
                        <td valign="top" style="display:none;">
                            <div class="divLeft">
                                <asp:TreeView ID="tvOU" runat="server" OnSelectedNodeChanged="tvOU_SelectedNodeChanged" NodeWrap="True"
                                    SelectedNodeStyle-BackColor="#99ccff" ShowLines="True" Width="200px" Style="height: 470px; overflow-y: auto; overflow-x: hidden;" >
                                </asp:TreeView>
                            </div>
                        </td>
                        <td style="width: 100%;" valign="top">
                            <div id="container">
                                <table>

                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="姓名：" ID="lb_RealName"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_RealName" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Text="账号：" ID="lb_UserName"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_UserName" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td class="CardCss">
                                            <asp:Label runat="server" Text="证件号：" ID="lb_UserIdentity"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_UserIdentity" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Text="归档：" ID="Label1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGD1" runat="server">
                                            <asp:ListItem Text="全部" Value="" />
                                             <asp:ListItem Text="否" Value="0" />
                                            <asp:ListItem Text="是" Value="1" />
                                           </asp:DropDownList>
                                        </td>
                                        <%--<td>
                                            <asp:Label ID="lb_IsDelete" runat="server" Text="状态：" Style="margin-left: 10px;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dp_IsDelete" runat="server" Style="width: 80px; height: 28px; border: 1px solid #ccc; border-radius: 3px; color: #888; display: inline-block; box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset; transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;">
                                                <asp:ListItem Value="" Selected="True">所有</asp:ListItem>
                                                <asp:ListItem Value="0">正常</asp:ListItem>
                                                <asp:ListItem Value="1">禁用</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td id="Td_LbGRADYATEDATE" runat="server" visible="false">
                                            <asp:Label ID="Lb_GRADYATEDATE" runat="server" Text="毕业时间："></asp:Label>
                                        </td>
                                        <td id="Td_DrGRADYATEDATE" runat="server" visible="false">
                                            <asp:DropDownList ID="Drp_GRADYATEDATE" runat="server" Style="width: 80px; height: 28px; border: 1px solid #ccc; border-radius: 3px; color: #888; display: inline-block; box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset; transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="bt_Search" runat="server" OnClick="bt_Search_Click" Text="查询" BackColor="Wheat" Style="margin-left: 14px;" />&nbsp;
                                            <input type="button" id="hidbtnEcxelImport" value="导入" onclick="funExcelImportStu();" style="background-color: wheat;" />&nbsp;
                                            <input type="button" id="hidbtnEcxelImport111" value="统计" onclick="javascript: window.location.href = 'TJXS.html'" style="background-color: wheat;" />&nbsp;
                                            <input type="button" onclick="javascript: history.back(1);" value="返回" class="btn_concel" />
                                        </td>
                                    </tr>
                                    <%--   <td>
                                            <asp:Button ID="BtnDivideAllClass" runat="server" Text="批量分班" Width="70px" OnClick="BtnDivideAllClass_Click" BackColor="Wheat" />
                                            <input type="hidden" id="btnDivideClass" />
                                        </td>--%>
                                    <tr>
                                        <td >
                                            <asp:Button ID="bt_AddUser" runat="server" OnClick="bt_AddUser_Click" Width="40px" Text="新增" BackColor="Wheat" Visible="false" />
                                            <input type="button" id="hidbtnEcxelDivideClass" value="分班" onclick="funExcelClassStu();" style="background-color: wheat;display:none;" />
                                            <asp:Button ID="btnUpGrade" runat="server" Text="升级" OnClientClick="return confirm('您确定要升级所有学生吗?');" OnClick="btnUpGrade_Click" BackColor="Wheat" style="display:none;"/>
                                            <asp:Button ID="btnDownGrade" runat="server" Text="降级" OnClientClick="return confirm('您确定要降级所有学生吗?');" OnClick="btnDownGrade_Click" BackColor="Wheat" style="display:none;"/>
                                        </td>
                                    </tr>
                                </table>
                                <%--  <div>
                                   

                                </div>--%>
                            </div>
                            <div class="con_list">
                                <asp:ListView ID="lvStu" runat="server" DataKeyNames="sfzjh" OnItemCommand="lvStu_ItemCommand" OnPagePropertiesChanging="lvStu_PagePropertiesChanging" OnPreRender="lvStu_PreRender" >
                                    <EmptyDataTemplate>
                                        <table runat="server" style="border-collapse: collapse; border: 1px #F3F3F3 solid;width: 100%;">
                                            <tr>
                                                <td style="text-align: center">暂无记录            
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr style="text-align: center;">
                                            <td>
                                                <%# Eval("xm")%> 
                                            </td>
                                            <td>
                                                <%# Eval("yhzh") %>
                                                <asp:HiddenField ID="hiddenYHZH" runat="server" Value='<%# Eval("yhzh") %>' />
                                            </td>

                                            <td>
                                                <%# Eval("mzm") %>
                                            </td>
                                            <td>
                                                <%# Eval("xbm") %>
                                            </td>
                                            <td>
                                                <%# Eval("sfzjh") %>
                                            </td>

                                            <td>
                                                <%# Eval("P_id").ToString().Trim()=="" ? "否" : Eval("P_id").ToString() %>
                                            </td>
                                            <%--<td>
                                                <%# Eval("yhzt") %>
                                            </td>--%>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" CommandName="DL" CommandArgument="加入队列" Text="加入队列" BackColor="Wheat" />
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                <%--<asp:Button ID="btnOperationDel" Width="40px" runat="server" CommandName="BtnDisEnable" CommandArgument='<%# Eval("sfzjh") %>' Text="禁用" OnClientClick="return confirm('您确定要禁用该用户吗?');" />--%>
                                                <%--<asp:Button ID="btnOperationOK" runat="server" Width="40px" Text="启用" CommandName="BtnEnable" CommandArgument='<%# Eval("sfzjh") %>' OnClientClick="return confirm('您确定要启用该用户吗?');" />--%>
                                                <%--<asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />--%>
                                                <%--<asp:Button ID="btnOperationUnlock" runat="server" Width="40px" Text="解绑" CommandName="UnLock" CommandArgument='<%# Eval("sfzjh") %>' OnClientClick="return confirm('您确定解绑该用户吗?');" />--%>
                                                <asp:HiddenField ID="HidNo" runat="server" Value='<%# Eval("yhzh") %>' />
                                                <asp:HiddenField ID="hfHiddenDel" runat="server" Value='<%# Eval("yhzt") %>' />
                                                <asp:HiddenField ID="HiddenFielduserid" runat="server" Value='<%# Eval("sfzjh") %>' />
                                                <asp:HiddenField ID="HidName" runat="server" Value='<%# Eval("xm")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="alt-row">
                                            <td>
                                                <%# Eval("xm")%>
                                            </td>
                                            <td>
                                                <%# Eval("yhzh") %>
                                                <asp:HiddenField ID="hiddenYHZH" runat="server" Value='<%# Eval("yhzh") %>' />
                                            </td>

                                            <td>
                                                <%# Eval("mzm") %>
                                            </td>
                                            <td>
                                                <%# Eval("xbm") %>
                                            </td>
                                            <td>
                                                <%# Eval("sfzjh") %>
                                            </td>

                                            <td>
                                               <%# Eval("P_id").ToString().Trim()=="" ? "否" : Eval("P_id").ToString() %>
                                            </td>
                                            <%--<td>
                                                <%# Eval("yhzt") %>
                                            </td>--%>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" CommandName="DL" CommandArgument="加入队列" Text="加入队列" BackColor="Wheat" />
                                                <asp:Button ID="lbtnEdit" Width="40px" runat="server" CommandName="Edit" CommandArgument="修改" Text="修改" BackColor="Wheat" />
                                                <%--<asp:Button ID="btnOperationDel" Width="40px" runat="server" CommandName="BtnDisEnable" CommandArgument='<%# Eval("sfzjh") %>' Text="禁用" OnClientClick="return confirm('您确定禁用该用户吗?');" />--%>
                                                <%--<asp:Button ID="btnOperationOK" runat="server" Width="40px" Text="启用" CommandName="BtnEnable" CommandArgument='<%# Eval("sfzjh") %>' OnClientClick="return confirm('您确定启用该用户吗?');" />--%>
                                                <%--<asp:Button ID="Btn_PassWord" runat="server" CommandName="Enable" CommandArgument="重置密码" OnClientClick="return confirm('您确定重置密码吗?');" Text="重置密码" class="BtnBackGroundColorCss" />--%>
                                                <%--<asp:Button ID="btnOperationUnlock" runat="server" Width="40px" Text="解绑" CommandName="UnLock" CommandArgument='<%# Eval("sfzjh") %>' OnClientClick="return confirm('您确定解绑该用户吗?');" />--%>
                                                <asp:HiddenField ID="HidNo" runat="server" Value='<%# Eval("yhzh") %>' />
                                                <asp:HiddenField ID="hfHiddenDel" runat="server" Value='<%# Eval("yhzt") %>' />
                                                <asp:HiddenField ID="HiddenFielduserid" runat="server" Value='<%# Eval("sfzjh") %>' />
                                                <asp:HiddenField ID="HidName" runat="server" Value='<%# Eval("xm")%>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <LayoutTemplate>
                                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%; text-align: center; border: 1px #F3F3F3 solid">
                                            <tr runat="server" class="b_txt alt-row">
                                                <th runat="server">姓名</th>
                                                <th runat="server">用户账号</th>
                                                <th runat="server">民族</th>
                                                <th runat="server">性别</th>
                                                <th runat="server">证件号</th>
                                                <th runat="server">是否归档</th>
                                                <%--<th runat="server">用户状态</th>--%>
                                                <th runat="server">操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                        <%--</table>--%>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                            <div>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: center;">
                                            <div class="pagination">
                                                <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="lvStu">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hiddenNJ" runat="server" />
            <asp:HiddenField ID="hiddenBH" runat="server" />
            <asp:HiddenField ID="hiddenxxzzjgh" runat="server" />
        </div>
    </form>


</body>
</html>
