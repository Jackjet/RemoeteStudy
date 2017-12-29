<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParentEdit.aspx.cs" Inherits="UserCenterSystem.ParentEdit" %>

<!DOCTYPE html>
<link type="text/css" href="css/page.css" rel="stylesheet" />
<script src="Scripts/jquery-1.6.min.js"></script>

<script type="text/javascript" src="Scripts/calendar.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="ParentEidt" runat="server">
        <div id="editpart" class="editpart">
            <table style="width: 100%;">
                <tr>
                    <td width="110"><span id="lb_Name">姓名：</span> <span style="color: Red;">*</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="tb_RealName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><span>关系：</span> <span style="color: Red;">*</span></td>
                    <td>
                        <asp:DropDownList ID="dp_gxm" runat="server" Height="25px" class="SelectCss">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>

                            <asp:ListItem Value="父子">父子</asp:ListItem>
                            <asp:ListItem Value="母子">母子</asp:ListItem>
                            <asp:ListItem Value="其他亲属">其他亲属</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><span>子女所在学校：</span><span style="color: Red;">*</span></td>
                    <td>
                        <asp:DropDownList ID="dp_DepartMent" runat="server" class="SelectCss">
                        </asp:DropDownList>
                </tr>
                <tr>
                    <td><span>是否是监护人：</span><span style="color: Red;">*</span></td>
                    <td>
                        <asp:DropDownList ID="dp_sfsjhr" runat="server" class="SelectCss">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td><span id="lb_Sex">性   别：</span><span style="color: Red;">*</span> </td>
                    <td>
                        <asp:RadioButtonList ID="rblXB" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:RadioButtonList>
                        <%--                        <asp:DropDownList ID="dp_Sex" runat="server" Height="25px" Width="133px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>

                            <asp:ListItem Value="男">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td><span id="lb_Nation">民 族：</span> <span style="color: Red;">*</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtMzm" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><span>出生日期：</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="tb_Birthday" onclick="return Calendar('tb_Birthday');" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span id="lb_ContactTel">联系电话：</span></td>

                    <td>
                        <asp:TextBox class="TcInputCss" ID="tb_ContactTel" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span>手机号码：</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtsjhm" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><span>联系地址:</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtlxdz" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><span>健康状况：</span></td>
                    <td>
                        <asp:DropDownList ID="dp_HealthCondition" runat="server" class="SelectCss">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="健康或良好">健康或良好</asp:ListItem>
                            <asp:ListItem Value="暂无">暂无</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><span>工作单位：</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="tb_WorkUnit" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td><span>电子邮箱：</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtdzyx" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><span>学历码：</span></td>
                    <td>
                        <asp:DropDownList ID="dp_xlm" runat="server" class="SelectCss">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="研究生及以上">研究生及以上</asp:ListItem>
                            <asp:ListItem Value="本科">本科</asp:ListItem>
                            <asp:ListItem Value="专科">专科</asp:ListItem>
                            <asp:ListItem Value="专科及以下">专科及以下</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><span>备注：</span></td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtbz" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <input type="hidden" name="hiddenCyxxzzjgh" id="hiddenCyxxzzjgh" /></td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" class="btnsave mr40" />
                        &nbsp;
        <asp:Button ID="btnCancle" runat="server" OnClick="btnCancle_Click" Text="取消" class="btn_concel" />

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>


