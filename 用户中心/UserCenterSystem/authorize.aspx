<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authorize.aspx.cs" Inherits="UserCenterSystem.authorize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <title></title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <link href="css/authorize_drralout.css" rel="stylesheet" />
    <link href="css/authorize_reset.css" rel="stylesheet" />
    <script type="text/javascript">
        //function init() {
        //    if (!($("#txtUserName").val().trim().length > 0) || $("#txtUserName").val() == "用户名") {
        //        alert("用户名不能为空！");
        //        return false;
        //    }
        //    else if (!($("#txtPwd").val().trim().length > 0) || $("#txtPwd").val() == "密码") {
        //        alert("密码不能为空！");
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        function change(obj) {
            obj.style.display = "none";
            if (obj.type == "text") {
                document.getElementById('txtPwd').style.display = "block";
                document.getElementById('txtPwd').focus();//加上
            } else if (obj.value == "") {
                document.getElementById('txt').style.display = "block";
            }
            else
                document.getElementById('txtPwd').style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dr_index">
            <div class="wrad">
                <div class="logo">
                    <%--<img src="images/logo_authorize.png" />--%>
                </div>
            </div>
            <div class="dr_title">
                <div class="leftcon fl">
                    <div class="logointo">
                        <h1>用户登录</h1>
                        <div class="minghu">
                            <b></b>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtstyle" onfocus="if(this.value=='用户名'){this.value='';this.style.color='#555555';}"
                                onblur="if(this.value==''){this.value='用户名';this.style.color='#bbb'}"
                                value="用户名"></asp:TextBox>
                        </div>
                        <div >
                            <b class="mima"></b>

                            <%-- <asp:TextBox ID="txtPwd" runat="server" CssClass="txtstyle" TextMode="Password" placeholder="密码"></asp:TextBox>--%>
                            <input id="txt" type="text" value="密码" onfocus="change(this)" class="txtstyle1" style="margin-left:-19px;"/>
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="txtstyle1" TextMode="Password" Style="display: none;" onblur="change(this)"></asp:TextBox>

                             

                        </div>
                        <p class="forget">
                            <span class="tishiyu fl">
                                <asp:Label ID="errortip" runat="server" Text="" Style="color:red;"></asp:Label>
                            </span><span class="fr"><a href="authorize_ChangePassWord.aspx"
                                  style="color: #9b9b9b;">修改密码?</a></span>
                        </p>
                        <asp:Button ID="btnok" runat="server" Text="" CssClass="btnok" OnClick="btnok_Click" />
                    </div>
                </div>
                <div class="rightcon fl">
                    <div class="regist">
                        <h1>没有统一认证系统账号？</h1>
                        <a href="authorize_register.aspx" ></a>
                        <h2>无法注册的用户，请联系系统管理员进行用户数据导入，然后再注册</h2>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <div class="footer">
                <div class="foot">
                    <p style="padding: 36px 0 10px;">
                        <a href="javascript:;">关于我们</a> |<a href="javascript:;"> 隐私保护</a> |<a href="javascript:;"> 联系我们

                        </a>
                    </p>
                    <p>Copyright © 2015 信息中心</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
