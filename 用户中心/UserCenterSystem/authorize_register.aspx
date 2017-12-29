<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authorize_register.aspx.cs" Inherits="UserCenterSystem.authorize_register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户注册</title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/jquery.placeholder.min.js"></script>
    <link href="css/authorize_drralout.css" rel="stylesheet" />
    <link href="css/authorize_reset.css" rel="stylesheet" />
    <link href="css/layout.css" rel="stylesheet" />
    <script type="text/javascript">
        function showImage() {
            var image = document.getElementById('<%= ImageCheck.ClientID %>');
            image.src = "ValidateCode.aspx?" + Math.floor(Math.random() * 10000);;
        }
        function changePWD() {
            window.location.href = "/ChangePassWord.aspx";
        }
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
        function change(obj, pwd, txt) {

            obj.style.display = "none";
            if (obj.type == "text") {
                document.getElementById(pwd).style.display = "block";
                document.getElementById(pwd).focus();//加上
            } else if (obj.value == "") {
                document.getElementById(txt).style.display = "block";
            }
            else
                document.getElementById(pwd).style.display = "block";
        }
        $(function () { $('input, textarea').placeholder(); });
    </script>
    <style type="text/css">
        .txtsty {
            width: 294px;
            height: 30px;
            border-style: none;
            color: #bbb;
        }

        .btnflag {
            background-image: url(images/link.png);
            border-style: none;
            width: 81px;
            height: 34px;
            color: white;
        }

        .btnflagok {
            background-image: url(images/visited.png);
            border-style: none;
            width: 81px;
            height: 34px;
            color: white;
        }

        .btnflag:hover {
            background-image: url(images/visited.png);
        }

        .inputdiv {
            width: 306px;
            height: 40px;
            background: url(images/inputkuang.png) no-repeat;
            padding: 5px;
            margin-left: 10px;
        }

            .inputdiv:hover {
                width: 306px;
                height: 40px;
                background: url(../images/inputkuang_hover.png) no-repeat;
                padding: 5px;
                margin-left: 10px;
            }

        .inputdiv2 {
            width: 120px;
            height: 30px;
            background: url(images/inputkuang120.png) no-repeat;
            padding: 5px;
            margin-left: 10px;
        }

            .inputdiv2:hover {
                width: 120px;
                height: 30px;
                background: url(../images/inputkuang_hover120.png) no-repeat;
                padding: 5px;
                margin-left: 10px;
            }

        .txtstyle2 {
            width: 295px;
            height: 30px;
            color: #bbb;
            border-style: none;
            font-family: microsoft yahei ui;
        }

        .name {
            width: 90px;
            text-align: right;
            font-size: 14px;
            line-height: 30px;
            font-family: "微软雅黑";
            color: #767676;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dr_index">
            <div class="wrad">
                <div class="logo">
                    <%--<img src="images/logo_authorize.png">--%>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="width: 1000px; margin: auto;">
                        <div style="background: url(images/drback2_01.gif) no-repeat; width: 1000px; height: 13px;"></div>
                        <div style="background: url(images/drback2_02.gif) repeat-y; height: auto; width: 1000px;">
                          
                            <p style="text-align: center; line-height: 47px; font-size: 23px;">用户注册</p>

                            <p id="imgflag" class="top_top" style="margin: 21px 0px 10px 317px; width: 430px; text-align: left;">
                                <span class="name">用户角色：</span>
                                <asp:Button ID="Btnteacher" runat="server" Text="我是老师" CssClass="btnflag" OnClick="Btnteacher_Click" CausesValidation="false" Style="margin-left: 7px; cursor: pointer;" />
                                <asp:Button ID="Btnstudent" runat="server" Text="我是学生" CssClass="btnflag" OnClick="Btnstudent_Click" CausesValidation="false" Style="margin-left: 15px; cursor: pointer;" />
                                <%--<asp:Button ID="Btnparent" runat="server" Text="我是家长" CssClass="btnflag" OnClick="Btnparent_Click" CausesValidation="false" Style="margin-left: 15px; cursor: pointer;" />--%>
                            </p>
                            <div class="biaodan">
                                <table style="margin-left: 297px;">

                                    <tr style="display:none;">
                                        <td class="name">选择学校：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:DropDownList ID="ddlXX" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlXX_SelectedIndexChanged" Style="width: 296px; height: 31px; border-style: none; border: none;">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr id="trzvsfz" runat="server" visible="false">
                                        <td class="name">子女身份证：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="txtzvsfz" runat="server" AutoPostBack="True" OnTextChanged="txtzvsfz_TextChanged" CssClass="txtsty" placeholder="请输入子女身份证"></asp:TextBox>
                                            </div>

                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvzvsfz" runat="server" ErrorMessage="*" ControlToValidate="txtzvsfz" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblzvsfz" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="name">身份证：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="txtSFZ" runat="server" OnTextChanged="txtSFZ_TextChanged" AutoPostBack="True" CssClass="txtsty" placeholder="请输入身份证"></asp:TextBox>
                                            </div>


                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvSFZ" runat="server" ErrorMessage="*" ControlToValidate="txtSFZ" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblsfz" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="name">姓名：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="txtXM" runat="server" OnTextChanged="txtXM_TextChanged" AutoPostBack="True" CssClass="txtsty" placeholder="请输入姓名"></asp:TextBox>
                                            </div>


                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvXM" runat="server" ErrorMessage="*" ControlToValidate="txtXM" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblXM" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr id="Tr_nation" runat="server" visible="false">
                                        <td class="name">民 族：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox CssClass="TcInputCss txtsty" ID="txtMzm" runat="server" placeholder="请输入民族"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr id="Tr_Sex" runat="server" visible="false">
                                        <td class="name">性别：</td>
                                        <td class="kuang">

                                            <asp:RadioButtonList ID="Rb_Sex" Style="height: 29px; line-height: 24px; display: inline-block; margin-left: 10px;"
                                                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false" CssClass="txtsty">
                                                <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                                <asp:ListItem Value="女">女</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr id="Tr_relation" runat="server" visible="false">
                                        <td class="name">关系：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:DropDownList ID="dp_gxm" runat="server" class="SelectCss txtsty" Style="width: 296px; height: 31px; border-style: none;">
                                                    <asp:ListItem Selected="True" Value="0">--请选择--</asp:ListItem>
                                                    <asp:ListItem Value="父子">父子</asp:ListItem>
                                                    <asp:ListItem Value="母子">母子</asp:ListItem>
                                                    <asp:ListItem Value="其他亲属">其他亲属</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr id="Tr_Phone" runat="server" visible="false">
                                        <td class="name">联系电话：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="Txt_phone" MaxLength="11" runat="server" AutoPostBack="True" OnTextChanged="Txt_phone_TextChanged" CssClass="txtsty" placeholder="请输入联系电话"></asp:TextBox>
                                            </div>

                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfv_phone" runat="server" ErrorMessage="*" ControlToValidate="Txt_phone" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblTxt_phone" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                    <tr id="Tr_address" runat="server" visible="false">
                                        <td class="name">联系地址：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox class="TcInputCss" ID="Txt_address" runat="server" CssClass="txtsty" placeholder="请输入联系地址"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr id="Tr_company" runat="server" visible="false">
                                        <td class="name">工作单位：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox class="TcInputCss" ID="Txt_WorkUnit" runat="server" CssClass="txtsty" placeholder="请输入工作单位"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;"></td>
                                    </tr>
                                    <tr>
                                        <td class="name">用户名：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="txtYHM" runat="server" OnTextChanged="txtYHM_TextChanged" AutoPostBack="True" CssClass="txtsty"
                                                    placeholder="6-15位字符，字母开头，支持英文和数字"
                                                    Style="color: #bbb;"></asp:TextBox>
                                            </div>

                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvYHM" runat="server" ErrorMessage="*" ControlToValidate="txtYHM" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblYHM" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:RegularExpressionValidator ID="revYHM" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]{5,14}$"
                                                ErrorMessage="账号不符合规则" ControlToValidate="txtYHM" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="name" style="vertical-align: top;">密码：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">

                                                <asp:TextBox ID="txt" runat="server" Text="6-20位字符,字母开头,支持英文,数字和字符" onfocus="change(this,'txtMA','txt')" ClientIDMode="Static" CssClass="txtstyle2"></asp:TextBox>


                                                <asp:TextBox ID="txtMA" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="txtsty"
                                                    onblur="change(this,'txtMA','txt')" Style="display: none;"></asp:TextBox>



                                            </div>

                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvMA" runat="server" ErrorMessage="*" ControlToValidate="txtMA" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblpwdCName" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,19}$"
                                                ErrorMessage="密码不符合规则" ControlToValidate="txtMA" ForeColor="Red"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="name" style="vertical-align: top;">确认密码：</td>
                                        <td class="kuang">
                                            <div class="inputdiv">
                                                <asp:TextBox ID="txt2" runat="server" Text="请再次输入密码" onfocus="change(this,'txtQrma','txt2')" CssClass="txtstyle2"></asp:TextBox>

                                                <asp:TextBox ID="txtQrma" runat="server" TextMode="Password" CssClass="txtsty" onblur="change(this,'txtQrma','txt2')" Style="display: none;"></asp:TextBox>
                                            </div>


                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="rfvQRMA" runat="server" ErrorMessage="*" ControlToValidate="txtQrma" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvRQMA" runat="server" ErrorMessage="两个密码不相同，请重新输入" ControlToCompare="txtMA" ControlToValidate="txtQrma" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="name" style="vertical-align: top;">验证码：
                                        </td>
                                        <td class="kuang">
                                            <div class="inputdiv2" style="float: left;">
                                                <asp:TextBox ID="txtYZM" runat="server" Width="108" Height="29" Style="border-style: none;" placeholder="请输入验证码"></asp:TextBox>
                                            </div>
                                            <div style="float: left;">
                                                <asp:Image runat="server" ID="ImageCheck" ImageUrl="ValidateCode.aspx" Style="width: 80px; height: 38px;"></asp:Image>
                                                <a href="javascript:void(0)" onclick="showImage()" style="font-size: 11px; position: relative; top: 5px;">换一张</a>
                                            </div>
                                        </td>
                                        <td style="vertical-align: middle;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtYZM" ForeColor="Red" InitialValue="请输入验证码"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <div class="xuanze" style="width: 100%; margin: 0 auto;">

                                    <p class="p4">
                                        <asp:Button ID="btnRegister" CssClass="btnRegister" runat="server" OnClick="btnRegister_Click" Style="margin-left: 101px;" />
                                    </p>
                                     <span style="right: 175px; bottom: 25px; float: right; position: relative;">已有账号？<a style="color: rgb(66, 166, 36);" href="authorize.aspx">直接登录</a></span>
                                  <%--  <div style="width: 300px; position: relative; top: -20px; left: 431px;">
                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Style="color: red; position: relative; display: inline-block;"></asp:Label>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div style="background: url(images/drback2_03.jpg) no-repeat; width: 1000px; height: 65px;"></div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="footer">
                <div class="foot">
                    <p style="padding: 36px 0 10px;"><a href="javascript:;">关于我们</a> |<a href="javascript:;"> 隐私保护</a> |<a href="javascript:;"> 联系我们</a></p>
                    <p>Copyright © 2015 信息中心</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
