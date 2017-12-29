<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainSite.Master" AutoEventWireup="true" CodeBehind="authorize_ChangePassWord.aspx.cs" Inherits="UserCenterSystem.authorize_ChangePassWord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <link href="css/layout.css" rel="stylesheet" />
    <link href="css/reset.css" rel="stylesheet" />
    <script type="text/javascript">
        function showImage() {
            var image = document.getElementById('<%= ImageCheck.ClientID %>');
            image.src = "ValidateCode.aspx?" + Math.floor(Math.random() * 10000);
        }
        function changePWD() {
            window.location.href = "/ChangePassWord.aspx";
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
    </script>
    <style type="text/css">
        .auto-style1 {
            text-align: left;
            height: 30px;
        }

        .inputdiv {
            width: 306px;
            height: 40px;
            background: url(../images/inputkuang.png) no-repeat;
            padding: 5px;
        }

            .inputdiv:hover {
                width: 306px;
                height: 40px;
                background: url(../images/inputkuang_hover.png) no-repeat;
                padding: 5px;
            }

        .inputdiv2 {
            width: 120px;
            height: 30px;
            background: url(../images/inputkuang120.png) no-repeat;
            padding: 5px;
        }

            .inputdiv2:hover {
                width: 120px;
                height: 30px;
                background: url(../images/inputkuang_hover120.png) no-repeat;
                padding: 5px;
            }

        .txtstyle2 {
            width: 295px;
            height: 30px;
            color: #bbb;
            border-style: none;
            font-family: microsoft yahei ui;
        }

        .txtsty {
            width: 294px;
            height: 30px;
            border-style: none;
            color: #bbb;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1000px; margin: auto;">
        <div style="background: url(../images/drback2_01.gif) no-repeat; width: 1000px; height: 13px;"></div>
        <div style="background: url(../images/drback2_02.gif) repeat-y; height: auto; width: 1000px;">
            <div class="center">
                <p class="top_top" style="padding-bottom: 20px; padding-top: 0px; text-align: center; line-height: 47px; font-size: 23px;">
                    修改密码
                </p>

                <div class="biaodan" style="text-align: center;">
                    <table style="margin-left: 273px;">
                        <tr>
                            <td class="name" style="height: 30px">用户名：</td>
                            <td class="auto-style1">
                                <div class="inputdiv">
                                    <asp:TextBox ID="txtYHM" runat="server" onfocus="if(this.value=='请输入原账号'){this.value='';this.style.color='#555555';}"
                                        onblur="if(this.value==''){this.value='请输入原账号';this.style.color='#bbb'}" Text="请输入原账号" Style="color: #bbb;"></asp:TextBox>
                                </div>

                            </td>
                            <td style="vertical-align: middle; text-align: left;">
                                <asp:RequiredFieldValidator ID="rfvYHM" runat="server" ErrorMessage="*" ControlToValidate="txtYHM" ForeColor="Red" InitialValue="请输入原账号"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">旧密码：</td>
                            <td class="kuang">
                                <div class="inputdiv">
                                    <asp:TextBox ID="txt" runat="server" Text="请输入原密码" onfocus="change(this,'OldPWD','txt')" CssClass="txtstyle2" ClientIDMode="Static"></asp:TextBox>
                                    <asp:TextBox ID="OldPWD" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="txtsty"
                                        onblur="change(this,'OldPWD','txt')" Style="display: none;"></asp:TextBox>
                                </div>
                            </td>
                            <td style="vertical-align: middle; text-align: left;">
                                <asp:RequiredFieldValidator ID="rfvMA" runat="server" ErrorMessage="*" ControlToValidate="OldPWD" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblYH" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">新密码：</td>
                            <td class="kuang">
                                <div class="inputdiv">
                                    <%-- <asp:TextBox ID="txtMA" runat="server" TextMode="Password"></asp:TextBox>--%>

                                    <asp:TextBox ID="txt1" runat="server" Text="6-20位字符,字母开头,支持英文,数字和字符" onfocus="change(this,'txtMA','txt1')" CssClass="txtstyle2" ClientIDMode="Static"></asp:TextBox>


                                    <asp:TextBox ID="txtMA" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="txtsty"
                                        onblur="change(this,'txtMA','txt1')" Style="display: none;"></asp:TextBox>



                                </div>

                            </td>
                            <td style="vertical-align: middle; text-align: left;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMA" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,19}$"
                                    ErrorMessage="新密码格式输入不正确" ControlToValidate="txtMA" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">新确认密码：</td>
                            <td class="kuang">
                                <div class="inputdiv">
                                    <%--<asp:TextBox ID="txtQrma" runat="server" TextMode="Password" ClientIDMode="AutoID"></asp:TextBox>--%>




                                    <asp:TextBox ID="txt2" runat="server" Text="请再次输入密码" onfocus="change(this,'txtQrma','txt2')" CssClass="txtstyle2" ClientIDMode="Static"></asp:TextBox>


                                    <asp:TextBox ID="txtQrma" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="txtsty"
                                        onblur="change(this,'txtQrma','txt2')" Style="display: none;"></asp:TextBox>



                                </div>

                            </td>
                            <td style="vertical-align: middle;">
                                <asp:RequiredFieldValidator ID="rfvQRMA" runat="server" ErrorMessage="*" ControlToValidate="txtQrma" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvRQMA" runat="server" ErrorMessage="两个密码不相同，请重新输入" ControlToCompare="txtMA" ControlToValidate="txtQrma" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name" style="vertical-align: top;">验证码：
                            </td>
                            <td class="kuang">
                                <div class="inputdiv2" style="float: left;">
                                    <asp:TextBox ID="txtYZM" runat="server" Width="115" Height="30" ClientIDMode="Static" onfocus="if(this.value=='请输入验证码'){this.value='';this.style.color='#555555';}"
                                        onblur="if(this.value==''){this.value='请输入验证码';this.style.color='#bbb'}" Text="请输入验证码" Style="color: #bbb; border-style: none;"></asp:TextBox>
                                </div>
                                <div style="float: left;">
                                    <asp:Image runat="server" ID="ImageCheck" ImageUrl="ValidateCode.aspx" Style="width: 80px; height: 38px;"></asp:Image>
                                    <a href="javascript:void(0)" onclick="showImage()" style="font-size: 11px; position: relative; top: 5px;">换一张</a>
                                </div>
                            </td>
                            <td style="vertical-align: middle; text-align: left;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtYZM" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="p2" style="text-align: left;">
                                <asp:Button ID="BtnChangePWD" runat="server" ClientIDMode="Static" OnClick="BtnChangePWD_Click" class="submit" /></td>
                            <td style="text-align: left; vertical-align: middle;">
                                <span style="margin-top: 10px; display: inline-block;"><a href="#" onclick="javascript:window.parent.location.href='Login.aspx';">返回登陆页</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="height: 30px; text-align: left; vertical-align: middle;">
                                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
        <div style="background: url(../images/drback2_03.jpg) no-repeat; width: 1000px; height: 65px;"></div>
    </div>
    <div>
    </div>
</asp:Content>
