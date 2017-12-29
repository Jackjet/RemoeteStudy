<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentInfo.aspx.cs" Inherits="UserCenterSystem.StudentInfo" %>

<!DOCTYPE html>
<link type="text/css" href="css/page.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学生信息查看</title>
    <script src="Scripts/jquery-1.6.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="con_wrap">
            <%-- <table width="100%">
                <tr>
                    <td style="width: 30%;">用户账号：</td>
                    <td>
                        <asp:Label ID="lblYhzh" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>证件号：
                    </td>
                    <td>
                        <asp:Label ID="lblsfzjh" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>用户姓名：</td>
                    <td>
                        <asp:Label ID="lblxm" runat="server" Text="暂无"></asp:Label></td>
                </tr>
                <tr>
                    <td>性别：
                    </td>
                    <td>
                        <asp:Label ID="lblxbm" runat="server" Text="暂无"></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td>民族：</td>
                    <td>
                        <asp:Label ID="lblmzm" runat="server" Text="暂无"></asp:Label></td>

                </tr>
                <tr>
                    <td>政治面貌: </td>
                    <td>
                        <asp:Label ID="lblzzmm" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>联系方式：</td>
                    <td>
                        <asp:Label ID="lbllxfs" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>通讯地址：</td>
                    <td>
                        <asp:Label ID="lbltxdz" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>出生日期：  </td>
                    <td>
                        <asp:Label ID="lblcsrq" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>现住址：</td>
                    <td>
                        <asp:Label ID="lblxzz" runat="server" Text="暂无"></asp:Label></td>
                </tr>
                <tr>
                    <td>户口性质：</td>
                    <td>
                        <asp:Label ID="lblhkxz" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>是否是流动人口：</td>
                    <td>
                        <asp:Label ID="lblshsldrk" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="HiddUserid" runat="server" />
                        <br />
                        查看更多<a href="#" onclick="funTurnStuInfoMore();">  点击这里</a>
                    </td>
                </tr>

            </table>--%>

            <table>
                <tr>
                    <td>学校名称：</td>
                    <td>
                        <asp:Label ID="Lb_School" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>教师名称:</td>
                    <td>
                        <asp:Label ID="Lb_Teacher" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>账号:</td>
                    <td>
                        <asp:Label ID="Lb_No" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>密码：</td>
                    <td>
                        <asp:Label ID="Lb_Pw" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>

</body>
</html>
<script>
    //function funTurnStuInfoMore() {
    //    var varuid = $("#HiddUserid").val();

    //    window.open("/StuInfoMore.aspx?uid=" + varuid, 'newwindow');
    //    // parent.$("#wBox").contents().find('.wBox_close').click();

    //}
</script>
