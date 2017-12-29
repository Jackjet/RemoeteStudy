<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectInfoManage.aspx.cs" Inherits="UserCenterSystem.SubjectInfoManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学科管理</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //全选或全不选  
            $("#checkAll").click(function () {
                $("#cblSubject input[type='checkbox']").attr("checked", this.checked);
            });
            var $subBox = $("#cblSubject input[type='checkbox']");
            $subBox.click(function () {
                $("#checkAll").attr("checked", $subBox.length == $("#cblSubject input[type='checkbox']:checked").length ? true : false);
            }); 
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>学科管理</h3>
            </div>
            <div class="divMain">
                <table style="width: 100%;">
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="panelLeft" runat="server" CssClass="divLeft" Visible="false">
                                <%--<div id="divSchool">
                                        学校：<asp:DropDownList ID="ddlSchool" runat="server" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                   </div>--%>
                                <asp:Panel ID="panelDepartment" runat="server" Visible="false">
                                    <div style="height: 500px; overflow-y: auto; overflow-x: hidden;">
                                        <%--<asp:TreeView ID="tvDepartment" runat="server" ShowLines="true" SelectedNodeStyle-BackColor="#99ccff"
                                            OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" Width="250px">
                                        </asp:TreeView>--%>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                        <td style="width: 100%;" valign="top">
                            <asp:Panel ID="panelRight" runat="server" Visible="true" Style="width: 100%; float: right; vertical-align: top;">
                                <asp:Button ID="btAdd" runat="server" Text="添加" OnClick="btAdd_Click" BackColor="Wheat" />

                                <div class="con_list">
                                    <asp:ListView ID="lvDisp" runat="server" OnItemCommand="lvDisp_ItemCommand" 
                                        OnItemEditing="lvDisp_ItemEditing" OnPagePropertiesChanging="lvDisp_PagePropertiesChanging">
                                        <EmptyDataTemplate>
                                            <table runat="server" border="1" style="width: 100%;">
                                                <tr>
                                                    <td>没有符合条件的信息</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: center;">
                                                <td><%#Eval("NJMC") %></td>
                                                <td><%#Eval("SubjectName") %></td>
                                                <td><%#Eval("CreateDate","{0:d}") %></td>
                                                <td><%#Eval("UpdateDate","{0:d}") %></td>
                                                <td>
                                                    <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="Javascript:return confirm('确定将此记录删除？');"></asp:Button>
                                                    <asp:HiddenField ID="NJ" runat="server" Value='<%#Eval("NJ") %>' />
                                                    <asp:HiddenField ID="SubjectID" runat="server" Value='<%#Eval("SubjectID") %>' />
                                                    <asp:HiddenField ID="GreadID" runat="server" Value='<%#Eval("ID") %>' />
                                                    <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("SubDesc") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="alt-row">
                                                <td><%#Eval("NJMC") %></td>
                                                <td><%#Eval("SubjectName") %></td>
                                                <td><%#Eval("CreateDate","{0:d}") %></td>
                                                <td><%#Eval("UpdateDate","{0:d}") %></td>
                                                <td>
                                                    <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="Javascript:return confirm('确定将此记录删除？');"></asp:Button>
                                                    <asp:HiddenField ID="NJ" runat="server" Value='<%#Eval("NJ") %>' />
                                                    <asp:HiddenField ID="SubjectID" runat="server" Value='<%#Eval("SubjectID") %>' />
                                                    <asp:HiddenField ID="GreadID" runat="server" Value='<%#Eval("ID") %>' />
                                                    <asp:HiddenField ID="hfBZ" runat="server" Value='<%#Eval("SubDesc") %>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <LayoutTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                            <tr class="b_txt alt-row">
                                                                <th>专业</th>
                                                                <th>学科名称</th>
                                                                <th>创建日期</th>
                                                                <th>修改日期</th>
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
                                            <div class="pagination">
                                                <asp:DataPager ID="DataPager1" runat="server" PageSize="10" PagedControlID="lvDisp">
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
                            <asp:Panel ID="panelAdd" runat="server" Visible="false" Style="margin-left: 120px; margin-top: 20px;">
                                <div class="BtnBackCss" style="display: none">
                                    <asp:LinkButton ID="lbBack" runat="server" Text="返回" OnClick="lbBack_Click" CausesValidation="false"></asp:LinkButton>
                                </div>
                                <table style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td class="tdRightBorder">年级：</td>
                                        <td class="tdLeftBorder">
                                            <asp:DropDownList ID="ddlGrade" runat="server" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" AutoPostBack="True" Style="height: 30px;"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="tdRightBorder">学科：<span style="color: Red;">*</span></td>
                                        <td class="tdLeftBorder">
                                            <div style="float: left;">
                                                <input id="checkAll" type="checkbox" style="display: block; float: left;" /><label style="display: block; float: left; line-height: 28px; padding-right: 8px;">全选</label>
                                            </div>
                                            <asp:CheckBoxList ID="cblSubject" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>备注：</td>
                                        <td>
                                            <asp:TextBox ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="350px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div style="margin: 0px auto; width: 446px;">
                                                <asp:Button ID="btSave" class="btnsave mr40" runat="server" Text="保存" OnClick="btSave_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" Style="width: 62px;" />&nbsp;&nbsp;
                            <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="Gread_id" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfDelete" runat="server" Value="0" />
            </div>

            <div>
                <asp:Label ID="lbDept" runat="server"></asp:Label>
            </div>
        </div>
    </form>

</body>
</html>
