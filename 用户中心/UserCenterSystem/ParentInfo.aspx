<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParentInfo.aspx.cs" Inherits="UserCenterSystem.ParentInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link type="text/css" href="css/page.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                <br />
               
                <asp:Label ID="lb_Name" runat="server" Text="姓      名："></asp:Label>                
                <asp:Label ID="lblcyxm" runat="server" Text="Label"></asp:Label>                  
                <br />
                <br />
                关系码：  <asp:Label ID="lblgxm" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                是否是监护人： <asp:Label ID="lblsfsjhr" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lb_Sex" runat="server" Text="性   别："></asp:Label>               
               <asp:Label ID="lblxbm" runat="server" Text="Label"></asp:Label>                 
                <br />
                <br />
                <asp:Label ID="lb_Nation" runat="server" Text="民 族："></asp:Label>
                <asp:Label ID="lblmzm" runat="server" Text="Label"></asp:Label>                
               <br />
                <br />
                出生日期：<asp:Label ID="lblcsny" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lb_ContactTel" runat="server" Text="联系电话："></asp:Label>
        <asp:Label ID="lbllxdh" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                手机号码： <asp:Label ID="lblsjhm" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                联系地址: 
        <asp:Label ID="lbllxdz" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                健康状况：
                    <asp:Label ID="lbljkzkm" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                工作单位：
                   <asp:Label ID="lblgzdw" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                电子邮箱：
                    <asp:Label ID="lbldzxx" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                学历码：
                  <asp:Label ID="lblxlm" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                备注：
        <asp:Label ID="lblbz" runat="server" Text="Label"></asp:Label>
&nbsp;</form>
</body>
</html>
