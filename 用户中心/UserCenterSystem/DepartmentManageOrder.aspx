<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManageOrder.aspx.cs" Inherits="UserCenterSystem.DepartmentManageOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        function Init() {
            
            var type = /^[0-9]*[1-9][0-9]*$/;
            var re = new RegExp(type);
            if ($("#txtoredr").val().trim().length == 0) {
                alert("请填写顺序序号!");
                return false;
            }
            else if ($("#txtoredr").val().trim().match(re) == null) {
                alert("请输入大于零的整数!");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="winDiv" style="height: 100%; margin: 30px 0 0 42px;">
            <div style="height: 35px; line-height: 35px;">
                <span>显示顺序：<i style="margin-left: 16px; color: red;">*</i></span>
                <asp:TextBox ID="txtoredr" runat="server" Style="height: 23px; width: 190px;"></asp:TextBox>
            </div>
            <asp:Button ID="btnadd" runat="server" Text="保存" BackColor="Wheat" Style="background-color: wheat; margin: 15px 0 0 107px; width: 60px; height: 27px;" OnClientClick="return Init();" OnClick="btnadd_Click" />

        </div> 
    </form>
</body>
</html>
