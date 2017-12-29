<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceManagerAdd.aspx.cs" Inherits="UserCenterSystem.InterfaceManagerAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script type="text/javascript">
        function Init() {
            if ($.trim($("#txtInterfaceName").val()).length == 0) {
                alert("接口名称不能为空");
                return false;
            }
            else if ($.trim($("#txtInterfaceDescribe").val()).length == 0) {
                alert("接口描述不能为空");
                return false;
            }
        }
    </script>
    <style type="text/css">
        i {
            color: red;
            font-style: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="winDiv" style="height: 100%; margin: 30px 0 0 42px;">
            <div style="height: 35px; line-height: 35px;">
                <span>接口名称：<i>*</i></span>
                <asp:TextBox ID="txtInterfaceName" runat="server" Style="width: 210px; height: 23px;"></asp:TextBox>
            </div>
            <div style="height: 35px; line-height: 35px; margin-top: 10px;">
                <span>接口描述：<i>*</i></span>
                <asp:TextBox ID="txtInterfaceDescribe" runat="server" Style="width: 210px; height: 23px;"></asp:TextBox>
            </div>
      <%--      <div style="height: 35px; line-height: 35px; margin-top: 10px;">
                <span>服务页面：<i>*</i></span>
                <asp:DropDownList ID="ddlservicepage" runat="server" Style="height: 30px;">
                    <asp:ListItem Value="UserInfo.asmx " Selected="True">UserInfo.asmx </asp:ListItem>
                </asp:DropDownList>
            </div>--%>
            <asp:Button ID="btnadd" runat="server" Text="保存" BackColor="Wheat" Style="background-color: wheat; margin: 15px 0 0 92px; width: 60px; height: 27px;" OnClientClick="return Init();" OnClick="btnadd_Click" />
        </div>
    </form>
</body>
</html>
