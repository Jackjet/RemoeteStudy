<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserCenterSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登陆中心</title>
    <link rel="stylesheet" type="text/css" href="/Scripts/CSS/Pages/login.css" />
    <link href="css/login2.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/CSS/login.css" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript">
        function showImage() {
            var image = document.getElementById('<%= ImageCheck.ClientID %>');
             image.src = "ValidateCode.aspx?" + Math.floor(Math.random() * 10000);
         }
        function alter(valueaaa) {
            $('#txtUserID').val(valueaaa);
        }
    </script>
</head>

<body class="body_login">
    <form id="formLogin" name="formLogin" runat="server">
        <div class="login">
            <div class="login_in">
                <div class="login_in_logo">
                    <img src="/Images/logo.png" />
                </div>
                <div class="login_in_con">
                    <table style="width: 330px; width: 280px; display: block" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 10%" class="login_name">用户名

                            </td>
                            <td style="width: 145px">
                                <asp:TextBox ID="txtUserID" runat="server" Width="145px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="login_name">密&nbsp;&nbsp;&nbsp;码
                            </td>
                            <td style="width: 145px">
                                <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password" Width="145px" >123456</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="login_name">验&nbsp;证&nbsp;码
                            </td>
                            <td style="display: block; width: 280px">
                                <asp:TextBox ID="txtYZM" runat="server" Width="70px" ></asp:TextBox>
                                <asp:Image runat="server" ID="ImageCheck" ImageUrl="ValidateCode.aspx" style="vertical-align:top;"></asp:Image>
                                <a href="javascript:void(0)" onclick="showImage()" style="font-size: 11px;">换一张</a>
                                <asp:Label ID="lblog" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td class="tl">
                                <asp:Button ID="btnLogin" runat="server" class="button_login" Text="登录" OnClick="btnLogin_Click" />
                                <input type="button" class="button_login ml10 button_login_no" value="重置" onclick="clearAll()" />
                                <%--<asp:Button ID="btnRegister" runat="server" class="" Text="注册" OnClick="btnRegister_Click"  /> --%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="login_bottom">
            系统管理
        </div>
        <script type="text/javascript">
            //重置
            function clearAll() {
                jQuery("#txtUserID").val("");
                jQuery("#txtUserPwd").val("");
                jQuery("#txtYZM").val("");
                jQuery("#lblog").text("");
            }
        </script>
    </form>
</body>

</html>
