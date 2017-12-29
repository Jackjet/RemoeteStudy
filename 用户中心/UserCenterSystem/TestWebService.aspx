<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWebService.aspx.cs" Inherits="UserCenterSystem.TestWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 <div>
        <table>
            <tr>
                <td>
                    登录名：
                </td>
                <td>
                    <asp:TextBox ID="tbLogoinName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    请求列：
                </td>
                <td>
                    <asp:TextBox ID="tbColumns" runat="server" Rows="2" TextMode="MultiLine" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    学校组织机构号：
                </td>
                <td>
                    <asp:TextBox ID="tbXXZZJGH" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    表名称：
                </td>
                <td>
                    <asp:TextBox ID="tbTableName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btGetConfig" runat="server" Text="查询用户配置" OnClick="btGetConfig_Click" />
                </td>
                <td>
                    <asp:Button ID="btSQL" runat="server" Text="测试WebService" OnClick="btSQL_Click" />
                </td>
            </tr>
        </table>
       
        <br />
        <br />
        <div>
            <asp:Label ID="lbConfig" runat="server" BackColor="#3366ff" ForeColor="White" Text="用户接口配置信息如下："></asp:Label><br />
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
            <asp:GridView ID="gvConfig" runat="server"></asp:GridView>
        </div>
        <br />
        <div>
            <asp:Label ID="lbResult" runat="server" BackColor="#3366ff" ForeColor="White" Text="查询结果如下："></asp:Label><br />
            <asp:Label ID="lbOut" runat="server"></asp:Label><br />
            <asp:GridView ID="gv" runat="server"></asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
