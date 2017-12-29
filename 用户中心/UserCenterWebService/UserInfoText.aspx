<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfoText.aspx.cs" Inherits="ADManager.UserInfoText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Lb_LoginName" runat="server" Text="登陆名称"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_LoginName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Lb_Column" runat="server" Text="列名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Column" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Lb_SchoolCode" runat="server" Text="学校Code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_SchoolCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Lb_Function" runat="server" Text="方法名称"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Function" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Lb_TableName" runat="server" Text="表名称"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_TableName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Btn_Sbmit" runat="server" Text="提交" OnClick="Btn_Sbmit_Click" />
                    </td>
                </tr>
            </table>

            <asp:gridview ID="Gridview1"  runat="server"></asp:gridview>
        </div>
    </form>
</body>
</html>


