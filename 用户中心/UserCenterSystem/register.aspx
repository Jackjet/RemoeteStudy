<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="UserCenterSystem.register" EnableViewState="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户注册</title>
    <link href="css/layout.css" rel="stylesheet" />
    <link href="css/reset.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript">
        function showImage() {
            var image = document.getElementById('<%= ImageCheck.ClientID %>');
            image.src = "ValidateCode.aspx?" + Math.floor(Math.random() * 10000);;
        }
        function changePWD() {
            window.location.href = "/ChangePassWord.aspx";
        }

        //document.onkeydown = function (e) {
        //    var keyCode;
        //    var element;
        //    /*ie support event&keyCode*/
        //    keyCode = event.keyCode;
        //    element = event.srcElement;
        //    if (keyCode == 13 && element.type == 'text') {
        //        document.getElementById("#btnRegister").click();
        //    }
        //}

    </script>

    <script type="text/javascript">
        function CharMode(iN) {
            if (iN >= 48 && iN <= 57) //数字
                return 1;
            if (iN >= 65 && iN <= 90) //大写字母
                return 2;
            if (iN >= 97 && iN <= 122) //小写
                return 4;
            else
                return 8; //特殊字符
        }
        function bitTotal(num) {
            modes = 0;
            for (i = 0; i < 5; i++) {
                if (num & 1) modes++;
                num >>>= 1;
            }
            return modes;
        }
        function checkStrong(sPW) {
            if (sPW.length <= 5)
                return 0; //密码太短
            Modes = 0;
            for (i = 0; i < sPW.length; i++) {
                Modes |= CharMode(sPW.charCodeAt(i));
            }
            return bitTotal(Modes);
        }
        function AuthPasswd() {
            var PwdBool = false;
            var pwd = $("#txtMA").val();
            if (pwd == null || pwd == '') {
                PwdBool = false;
            }
            else {
                S_level = checkStrong(pwd);
                switch (S_level) {
                    case 0:
                        PwdBool = false;
                        $("#lblMessage").text("密码强度不够");
                        break;
                    case 1:
                        PwdBool = false;
                        $("#lblMessage").text("密码强度不够");
                        break;
                    case 2:
                        PwdBool = false;
                        $("#lblMessage").text("密码强度不够");
                        break;
                    default:
                        PwdBool = true;
                }
            }
            return PwdBool;
        }
    </script>
</head>
<body>
    <form runat="server" defaultbutton="btnRegister">
        <div class="content">
            <div class="center">
                <p class="top_top">
                    <asp:ImageButton ID="Btnteacher" runat="server" ImageUrl="~/images/teacher.png" OnClick="Btnteacher_Click" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="Btnstudent" runat="server" ImageUrl="~/images/student.png" OnClick="Btnstudent_Click" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<asp:ImageButton ID="Btnparent" runat="server" ImageUrl="~/images/parent.png" OnClick="Btnparent_Click" CausesValidation="false" />--%>
                    <%--  <img src="images/top_top.png" />--%>
                </p>
                <div class="biaodan" style="text-align: center; margin: 15px 0 0 105px;">
                    <table>
                        <tr style="display:none;">
                            <td class="name1">选择学校：</td>
                            <td class="kuang">
                                <asp:DropDownList ID="ddlXX" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlXX_SelectedIndexChanged" Style="width: 224px; height: 30px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trzvsfz" runat="server" visible="false">
                            <td class="name1">子女身份证：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtzvsfz" runat="server" AutoPostBack="True" OnTextChanged="txtzvsfz_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvzvsfz" runat="server" ErrorMessage="*" ControlToValidate="txtzvsfz" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblzvsfz" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">身份证：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtSFZ" runat="server" OnTextChanged="txtSFZ_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSFZ" runat="server" ErrorMessage="*" ControlToValidate="txtSFZ" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblsfz" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">姓名：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtXM" runat="server" OnTextChanged="txtXM_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvXM" runat="server" ErrorMessage="*" ControlToValidate="txtXM" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblXM" runat="server" Text=""></asp:Label>

                            </td>
                        </tr>
                        <tr id="Tr_nation" runat="server" visible="false">
                            <td class="name1">民 族：</td>
                            <td class="kuang">
                                <asp:TextBox class="TcInputCss" ID="txtMzm" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Tr_Sex" runat="server" visible="false">
                            <td class="name1">性别：</td>
                            <td class="kuang">
                                <asp:RadioButtonList ID="Rb_Sex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="Tr_relation" runat="server" visible="false">
                            <td class="name1">关系：</td>
                            <td class="kuang">
                                <asp:DropDownList ID="dp_gxm" runat="server" Height="25px" class="SelectCss" Style="width: 224px; height: 30px;">
                                    <asp:ListItem Selected="True" Value="0">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="父子">父子</asp:ListItem>
                                    <asp:ListItem Value="母子">母子</asp:ListItem>
                                    <asp:ListItem Value="其他亲属">其他亲属</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="Tr_Phone" runat="server" visible="false">
                            <td class="name1">联系电话：</td>
                            <td class="kuang">
                                <asp:TextBox ID="Txt_phone" MaxLength="11" runat="server" AutoPostBack="True" OnTextChanged="Txt_phone_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_phone" runat="server" ErrorMessage="*" ControlToValidate="Txt_phone" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblTxt_phone" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr id="Tr_address" runat="server" visible="false">
                            <td class="name1">联系地址：</td>
                            <td class="kuang">
                                <asp:TextBox class="TcInputCss" ID="Txt_address" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Tr_company" runat="server" visible="false">
                            <td class="name1">工作单位：</td>
                            <td class="kuang">
                                <asp:TextBox class="TcInputCss" ID="Txt_WorkUnit" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">用户名：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtYHM" runat="server" OnTextChanged="txtYHM_TextChanged" AutoPostBack="True"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvYHM" runat="server" ErrorMessage="*" ControlToValidate="txtYHM" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblYHM" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RegularExpressionValidator ID="revYHM" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]{5,14}$"
                                    ErrorMessage="账号不符合规则" ControlToValidate="txtYHM" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">密码：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtMA" runat="server" TextMode="Password" ToolTip="字母开头，长度6-20位，支持大小写字母、数字和括号内标点字符(@#!%$&)"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvMA" runat="server" ErrorMessage="*" ControlToValidate="txtMA" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblpwdCName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,19}$"
                                    ErrorMessage="密码不符合规则" ControlToValidate="txtMA" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name1">确认密码：</td>
                            <td class="kuang">
                                <asp:TextBox ID="txtQrma" runat="server" TextMode="Password" ClientIDMode="AutoID" ToolTip="字母开头，长度6-20位，支持大小写字母、数字和括号内标点字符(@#!%$&)"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvQRMA" runat="server" ErrorMessage="*" ControlToValidate="txtQrma" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvRQMA" runat="server" ErrorMessage="两个密码不相同，请重新输入" ControlToCompare="txtMA" ControlToValidate="txtQrma" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>

                            </td>
                        </tr>
                        <tr>
                            <td class="name1">验证码：
                            </td>
                            <td class="kuang">
                                <asp:TextBox ID="txtYZM" runat="server" Width="110px"></asp:TextBox>
                                <asp:Image runat="server" ID="ImageCheck" ImageUrl="ValidateCode.aspx" Style="width: 71px; height: 30px;"></asp:Image>
                                <a href="javascript:void(0)" onclick="showImage()" style="font-size: 11px;">换一张</a></td>
                        </tr>
                    </table>

                    <div class="xuanze" style="width: 700px; margin: 0 auto;">
                        <div style="width: 338px; padding-left: 140px;">
                            <p class="p2" style="height: 31px; line-height: 31px; display: inline-flex; position: relative;">
                                <asp:Button ID="btnRegister" runat="server" Text="注册" OnClick="btnRegister_Click" />
                                <asp:HyperLink ID="hllogin" runat="server" NavigateUrl="~/ChangePassWord.aspx?flag=0" Style="background: url('../images/submit.png') no-repeat; border: currentColor; border-image: none; width: 129px; height: 32px; color: rgb(255, 255, 255); line-height: 32px; font-family: '微软雅黑'; font-size: 16px; display: inline-block; cursor: pointer; margin-left: 10px;">修改密码</asp:HyperLink>

                            </p>
                        </div>
                        <div style="width: 300px; position: relative; top: -20px; left: 431px;">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Style="color: red; position: relative; display: inline-block;"></asp:Label>
                        </div>
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
                            3、支持大小写字母、数字、标点字符(@#!%$&)
                        </p>
                    </div>
                </div>
                <%--  <tr>
                   
                    <td colspan="2" style="text-align: center;">
                     
                     
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td><td>
                             <asp:HyperLink ID="hllogin" runat="server" NavigateUrl="~/ChangePassWord.aspx">修改密码</asp:HyperLink>
                        <button type="button" id ="BtnChangePWD" onclick="changePWD()"  style="width:76px;background-color:#FF6699; color:#FFFFFF;  text-align:center" >修改密码</button>
                

                                                                                                           </td> 
                </tr>--%>
            </div>
        </div>

    </form>
</body>
</html>
