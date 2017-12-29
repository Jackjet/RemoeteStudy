<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceConfigManagerAdd.aspx.cs" Inherits="UserCenterSystem.InterfaceConfigManagerAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/wbox.js"></script>
    <script type="text/javascript">
        function Init() {
            if ($("#txtSysteName option:selected").text() == "--请选择--") {
                alert("请选择系统名称");
                return false;
            }
            else if ($("#txtInterfaceName option:selected").text() == "--请选择--") {
                alert("请选择接口名称");
                return false;
            }
            else if ($.trim($("#txtretItem").val()).length == 0) {
                alert("返回数据项不能为空");
                return false;
            }
            else if ($.trim($("#txttableData").val()).length == 0) {
                alert("数据表不能为空");
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
                <span>系统名称：<i style="margin-left: 16px;">*</i></span>
                <asp:DropDownList ID="txtSysteName" runat="server" Style="height: 30px; width: 270px;">
                </asp:DropDownList>
            </div>
            <div style="height: 35px; line-height: 35px; margin-top: 10px;">
                <span>接口名称：<i style="margin-left: 16px;">*</i></span>
                <asp:DropDownList ID="txtInterfaceName" runat="server" Style="height: 30px; width: 270px;">
                </asp:DropDownList>
            </div>
            <div style="height: 35px; line-height: 35px; margin-top: 10px;">
                <span>返回数据项：<i>*</i></span>
                <asp:TextBox ID="txtretItem" runat="server" Style="height: 23px; width: 270px;"></asp:TextBox>
            </div>
            <div style="height: 35px; line-height: 35px; margin-top: 10px;">
                <span>数据表：<i style="margin-left: 32px;">*</i></span>
                <asp:TextBox ID="txttableData" runat="server" Style="height: 23px; width: 270px;"></asp:TextBox>
            </div>
            <asp:Button ID="btnadd" runat="server" Text="保存" BackColor="Wheat" Style="background-color: wheat; margin: 15px 0 0 107px; width: 60px; height: 27px;" OnClientClick="return Init();" OnClick="btnadd_Click" />
        </div>
    </form>
</body>
</html>
