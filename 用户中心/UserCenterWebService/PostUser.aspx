<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostUser.aspx.cs" Inherits="Me.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript">
        //全选/反选
        function CheckAllNew() {
            var dom = document.all;
            var el = event.srcElement;
            if (el.id.indexOf("Ckb_AllNew") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("Ckb_SelNew") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }

        //【是否选中】
        function IsExitCheckNew() {
            var dom = document.all;
            var el = event.srcElement;
            var ischecked = false;
            for (i = 0; i < dom.length; i++) {
                if (dom[i].id.indexOf("Ckb_SelNew") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox" && dom[i].checked == true)
                    ischecked = true;
            }
            if (ischecked == false)
                alert("没有选中项！");
            else {

                if (!confirm('确认继续操作?'))
                    ischecked = false;
            }
            return ischecked;
        }

        //全选/反选
        function CheckAll() {
            var dom = document.all;
            // var dom = document.getElementById("Tb_StudentInfoNew").getElementsByTagName("input");
            var el = event.srcElement;
            if (el.id.indexOf("Ckb_All") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("Ckb_Sel") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }

        //【是否选中】
        function IsExitCheck() {
            var dom = document.all;
            var el = event.srcElement;
            var ischecked = false;
            for (i = 0; i < dom.length; i++) {
                if (dom[i].id.indexOf("Ckb_Sel") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox" && dom[i].checked == true)
                    ischecked = true;
            }
            if (ischecked == false)
                alert("没有选中项！");
            else {

                if (!confirm('确认继续操作?'))
                    ischecked = false;
            }
            return ischecked;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>AD域：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Text="SHAREPOINT2013"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>SharePoint站点：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" Text="http://win2012/sites/SP2013/"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>用户组名称：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" Text="教师"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Button ID="Btn_Post" runat="server" Text="导入" OnClick="Btn_Post_Click" />
                    </th>
                </tr>
            </table>
        </div>

        <div>
            <table>
                <tr>
                    <td>SharePoint站点：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" Text="http://win2012/sites/SP2013/"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Button ID="Btn_SeachSharePoint" runat="server" Text="查询" OnClick="Btn_SeachSharePoint_Click" />
                        <asp:Button ID="Btn_DeleteSharepoint" runat="server" Text="删除" OnClientClick="IsExitCheckNew()" OnClick="Btn_DeleteSharepoint_Click" />
                    </th>
                </tr>
            </table>

            <asp:ListView ID="List_InBox" runat="server">
                <EmptyDataTemplate>
                    <table runat="server">
                        <tr>
                            <td>没有符合条件的信息</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="Tb_StudentInfoNew" runat="server" style="border: 1px">
                        <tr class="trth">
                            <th class="theleft" style="width: 10px">
                                <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                                <asp:CheckBox ID="Ckb_AllNew" runat="server" class="Chbout" onclick="CheckAllNew()" />
                            </th>
                            <th class="theleft">类型</th>
                            <th>用户名</th>
                            <th>用户账号</th>
                            <th>用户组</th>
                            <th>用户权限</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <th class="theleft" style="width: 10px">
                            <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                            <asp:CheckBox ID="Ckb_SelNew" runat="server" Checked="False" class="Chbout" />
                        </th>
                        <th><%#Eval("Type") %></th>
                        <th><%#Eval("UserName") %></th>
                        <th><%#Eval("UserNO") %></th>
                        <th><%#Eval("UserGroup") %></th>
                        <th><%#Eval("UserRole") %></th>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="trbg">
                        <th class="theleft" style="width: 10px">
                            <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                            <asp:CheckBox ID="Ckb_SelNew" runat="server" Checked="False" class="Chbout" />
                        </th>
                        <th><%#Eval("Type") %></th>
                        <th><%#Eval("UserName") %></th>
                        <th><%#Eval("UserNO") %></th>
                        <th><%#Eval("UserGroup") %></th>
                        <th><%#Eval("UserRole") %></th>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>

        <div>
            <table>
                <tr>
                    <td>IP：</td>
                    <td>
                        <asp:TextBox ID="Txt_IP" runat="server" Text="111.207.162.129"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>域名：</td>
                    <td>
                        <asp:TextBox ID="txtDomainName" runat="server" Text="SHAREPOINT2013"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>系统账号：</td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Text="admin"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>系统密码：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" Text="yqedu@123"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>学校组织单位：</td>
                    <td>
                        <asp:TextBox ID="Txt_Department" runat="server" Text="十一学校"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>人员组织单位：</td>
                    <td>
                        <asp:TextBox ID="Txt_UserDepartment" runat="server" Text="老师"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Btn_SeachAD" runat="server" Text="查询" OnClick="Btn_SeachAD_Click" /></td>
                    <td>
                        <asp:Button ID="Btn_ADDelete" runat="server" Text="删除" OnClientClick="IsExitCheck()" OnClick="Btn_ADDelete_Click" style="height: 27px" /></td>
                </tr>
            </table>

            <asp:ListView ID="Lit_User" runat="server">
                <EmptyDataTemplate>
                    <table runat="server">
                        <tr>
                            <td>没有符合条件的信息</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="Tb_StudentInfoNew" runat="server" style="border: 1px">
                        <tr class="trth">
                            <th class="theleft" style="width: 10px">
                                <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                                <asp:CheckBox ID="Ckb_All" runat="server" class="Chbout" onclick="CheckAll()" />
                            </th>
                            <th class="theleft">用户账号</th>
                            <th>身份证号</th>
                            <th>显示名称</th>
                            <th>密码最后登录时间</th>
                            <th>账号改变时间</th>
                            <th>账号创建时间</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <th class="theleft" style="width: 10px">
                            <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                            <asp:CheckBox ID="Ckb_Sel" runat="server" Checked="False" class="Chbout" />
                        </th>
                        <th><%#Eval("UserNO") %></th>
                        <th><%#Eval("description") %></th>
                        <th><%#Eval("dispalyname") %></th>
                        <th><%#Eval("pwdLastSet") %></th>
                        <th><%#Eval("whenChanged") %></th>
                        <th><%#Eval("whenCreated") %></th>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="trbg">
                        <th class="theleft" style="width: 10px">
                            <input id="txtID" type="hidden" value='<%#Eval("UserNO")%>' runat="server" />
                            <asp:CheckBox ID="Ckb_Sel" runat="server" Checked="False" class="Chbout" />
                        </th>
                        <th><%#Eval("UserNO") %></th>
                        <th><%#Eval("description") %></th>
                        <th><%#Eval("dispalyname") %></th>
                        <th><%#Eval("pwdLastSet") %></th>
                        <th><%#Eval("whenChanged") %></th>
                        <th><%#Eval("whenCreated") %></th>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
