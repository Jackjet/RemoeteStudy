<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassWord.aspx.cs" Inherits="UserCenterSystem.ChangePassWord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/layout.css" rel="stylesheet" />
    <link href="css/reset.css" rel="stylesheet" />
    <script type="text/javascript">
        function showImage() {

            var image = document.getElementById('<%= ImageCheck.ClientID %>');
            image.src = "ValidateCode.aspx?" + Math.floor(Math.random() * 10000);;

        }

        function changePWD() {
            window.location.href = "/ChangePassWord.aspx";
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div class="center">
                <p class="top_top" style="padding-bottom: 20px; padding-top: 60px;">
                    <img src="images/xgmm.png" />
                </p>

                <div class="biaodan" style="text-align: center;">
                    <table >
                        <tr>
                            <td class="name1" style="height: 30px">用户名：</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txtYHM" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvYHM" runat="server" ErrorMessage="*" ControlToValidate="txtYHM" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revYHM" runat="server" ValidationExpression="^[a-zA-Z0-9]{6,15}$"
                                    ErrorMessage="账号不正确" ControlToValidate="txtYHM" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="name1">旧密码：</td>
                            <td class="kuang">
                                <asp:TextBox ID="OldPWD" runat="server" TextMode="Password"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvMA" runat="server" ErrorMessage="*" ControlToValidate="OldPWD" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblYH" runat="server" ForeColor="Red"></asp:Label>

                                <%--<asp:RegularExpressionValidator ID="rfvOldPwd" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,19}$"
                                    ErrorMessage="格式不符合规则" ControlToValidate="OldPWD" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                 
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">新密码：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtMA" runat="server" TextMode="Password"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMA" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,19}$"
                                    ErrorMessage="新密码格式输入不正确" ControlToValidate="txtMA" ForeColor="Red"></asp:RegularExpressionValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="name1">新确认密码：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtQrma" runat="server" TextMode="Password" ClientIDMode="AutoID"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvQRMA" runat="server" ErrorMessage="*" ControlToValidate="txtQrma" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvRQMA" runat="server" ErrorMessage="两个密码不相同，请重新输入" ControlToCompare="txtMA" ControlToValidate="txtQrma" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">验证码：
                            </td>
                            <td class="kuang">
                                <asp:TextBox ID="txtYZM" runat="server" Width="75px"></asp:TextBox>

                                <asp:Image runat="server" ID="ImageCheck" ImageUrl="ValidateCode.aspx"></asp:Image>
                                <a href="javascript:void(0)" onclick="showImage()" style="font-size: 11px;">换一张</a></td>


                        </tr>


                    </table>
                    <div class="xuanze">
                        <p class="p3">
                            <asp:Button ID="BtnChangePWD" runat="server" Text="修改密码" OnClick="BtnChangePWD_Click" class="submit" />
                            <asp:Button ID="hllogin" runat="server" OnClientClick="javascript:window.history.back(-1);" class="submit" Style="line-height: 28px; margin: 4px 0 6px 13px" Text="返  回" />
                            <span> 
                                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></span>
                        </p>
                    </div>

                </div>
                <div class="shuoming">
                    <div class="first">
                        <h1>用户名规则说明：</h1>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            1、账号长度6-15
                        </p>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            2、字母开头
                        </p>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            3、支持大小写字母、数字
                        </p>
                    </div>
                    <div class="second">
                        <h1>密码规则说明：</h1>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            1、账号长度6-20
                        </p>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            2、字母开头
                        </p>
                        <p style="margin: 0in; margin-left: .375in; font-family: 微软雅黑; font-size: 10.5pt; color: #CB0000">
                            3、支持大小写字母、数字、标点字符(@#!%$&amp;)&nbsp;
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
