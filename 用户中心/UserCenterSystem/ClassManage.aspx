<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassManage.aspx.cs" Inherits="UserCenterSystem.ClassManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <title>班级管理</title>
    <style type="text/css">
        .auto-style2 {
            width: 382px;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <div class="divHeader" style="text-align: center; width: 100%">
                <h3>班级管理</h3>
            </div>
            <div class="divMain">
                <table style="width: 100%;">
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="panelLeft" runat="server" CssClass="divLeft">
                                <asp:Panel ID="panelDepartment" runat="server">
                                    <div style="height: 500px; overflow-y: auto; overflow-x: hidden;">
                                        <asp:TreeView ID="tvDepartment" runat="server" ShowLines="true" SelectedNodeStyle-BackColor="#99ccff"
                                            OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" Width="250px">
                                        </asp:TreeView>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                        <td style="width: 100%;" valign="top">
                            <asp:Panel ID="panelRight" runat="server" Visible="false" Style="width: 100%; float: right; vertical-align: top;">
                                <asp:Button ID="btAdd" runat="server" Text="添加" OnClick="btAdd_Click" BackColor="Wheat" />
                                <asp:Button ID="Btn_Add" runat="server" Text="批量添加" BackColor="Wheat" OnClick="Btn_Add_Click" />
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
                                                <td><%#Eval("BH") %></td>
                                                <td><%#Eval("BJ") %></td>
                                                <td><%# DateTime.Parse(Eval("JBNY").ToString()).ToString("yyyy-MM-dd") %></td>
                                                <td>
                                                    <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                                    <asp:HiddenField ID="hfBJBH" runat="server" Value='<%#Eval("BJBH") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="alt-row">
                                                <td><%#Eval("BH") %></td>
                                                <td><%#Eval("BJ") %></td>
                                                <td><%#DateTime.Parse(Eval("JBNY").ToString()).ToString("yyyy-MM-dd")%></td>
                                                <td>
                                                    <asp:Button ID="lbEdit" runat="server" Text="修改" CommandName="edit" BackColor="Wheat"></asp:Button>
                                                    <asp:Button ID="lbDel" runat="server" Text="删除" CommandName="del" BackColor="Wheat" OnClientClick="return confirm('确定将此记录删除?')"></asp:Button>
                                                    <asp:HiddenField ID="hfBJBH" runat="server" Value='<%#Eval("BJBH") %>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <LayoutTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table id="itemPlaceholderContainer" runat="server" style="width: 100%;">
                                                            <tr class="b_txt alt-row">
                                                                <th>班号</th>
                                                                <th>班级</th>
                                                                <th>建班年月</th>
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

                            <asp:Panel ID="panelAdd" runat="server" Visible="false" Style="width: 100%; float: right; vertical-align: top;">
                                <div style="display: none">
                                    <asp:LinkButton ID="lbBack" runat="server" Text="返回" OnClick="lbBack_Click" CausesValidation="false"></asp:LinkButton>
                                </div>

                                <table style="border-collapse: collapse;">
                                    <tr>
                                        <td>专业<span style="color: Red;">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlGrade" runat="server" class="SelectCss" Style="width: 158px; border: 1px solid #ccc; border-radius: 3px; color: #888; display: inline-block; box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset; transition: border 0.2s linear 0s, box-shadow 0.2s linear 0s;">
                                            </asp:DropDownList>
                                        </td>


                                        <td>班主任工号</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBZRGH" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <%--<tr>
                                        <td class="tdRightBorder">建班年月<span style="color:Red;">*</span></td>
                                    <td class="tdLeftBorder">
                                         <asp:TextBox  class="InputCss"   ID="tbJBNY" runat="server" onclick="return Calendar('tbJBNY');"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="requiredJBNY" runat="server" ControlToValidate="tbJBNY" ForeColor="Red"
                                             ErrorMessage="建班年月不能为空" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    </tr>--%>
                                    <tr>
                                        <td>班级<span style="color: Red;">*</span></td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBJ" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredBJ" runat="server" ControlToValidate="tbBJ" ForeColor="Red"
                                                ErrorMessage="班级不能为空" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>班号<span style="color: Red;">*</span></td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBH" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredBH" runat="server" ForeColor="Red"
                                                ErrorMessage="班号不能为空" ControlToValidate="tbBH" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>班级荣誉称号</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBJRYCH" runat="server"></asp:TextBox></td>
                                        <td>班长学号</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBZXH" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>班级类型码</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbBJLXM" runat="server" MaxLength="2" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                        <td>文理类型</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbWLLX" runat="server" MaxLength="2" onkeydown="if(this.value.length >=2){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>少数民族双语教学班</td>
                                        <td>
                                            <asp:CheckBox ID="cbSFSSMZSYJXB" runat="server" /></td>
                                        <td>双语教学模式码</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbSYJXMSM" runat="server" MaxLength="2" onkeydown="if(this.value.length >=1){ this.value=this.value.substr(0,this.value.length-1); }"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="display: none">毕业日期<span style="color: Red;">*</span></td>
                                        <td style="display: none">
                                            <asp:TextBox class="InputCss" ID="tbBYRQ" runat="server" onclick="return Calendar('tbBYRQ');" Visible="false"></asp:TextBox>
                                            <%--     <asp:RequiredFieldValidator ID="requiredBYRQ" runat="server" ControlToValidate="tbBYRQ" ForeColor="Red"
                                             ErrorMessage="毕业日期不能为空" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td>学制</td>
                                        <td>
                                            <asp:TextBox class="InputCss" ID="tbXZ" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>备注</td>
                                        <td colspan="3">
                                            <asp:TextBox class="InputCss" ID="tbBZ" runat="server" TextMode="MultiLine" Rows="6" Width="535px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center;">
                                            <asp:Button ID="btSave" runat="server" Text="保存" class="btnsave mr40" OnClick="btSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btCancel" runat="server" Text="取消" OnClick="btCancel_Click" CausesValidation="false" Style="width: 60px;" />&nbsp;&nbsp;
                                        <asp:Label ID="lbMessage" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="panelAdds" runat="server" Visible="false" Style="margin: 30px auto; width: 656px;">
                                <div style="display: none">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="返回" OnClick="lbBack_Click" CausesValidation="false" class="SelectCss"></asp:LinkButton>
                                </div>
                                <table style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td class="tdRightBorder">专业</td>
                                        <td class="tdLeftBorder">
                                            <asp:DropDownList ID="Dl_Gread" runat="server" Style="height: 30px; border: 1px solid #ccc; border-radius: 3px;">
                                            </asp:DropDownList></td>
                                        <td class="tdRightBorder">班级个数</td>
                                        <td class="tdLeftBorder">
                                            <asp:TextBox class="InputCss" ID="Txt_ClassNum" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center;">
                                            <div style="margin: 20px 0px 0px 70px; width: 314px; float: left;">
                                                <asp:Button ID="Btn_Saves" runat="server" Text="保存" class="btnsave mr40" OnClick="Btn_Saves_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="Button2" runat="server" Text="取消" OnClick="btCancel_Click" CausesValidation="false" Style="width: 60px;" />&nbsp;&nbsp;
                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfDelete" runat="server" Value="0" />
                <asp:HiddenField ID="hfType" runat="server" />
            </div>

            <div>
                <asp:Label ID="lbDept" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>

</html>
