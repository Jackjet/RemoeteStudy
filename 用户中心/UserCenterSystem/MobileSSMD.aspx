<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileSSMD.aspx.cs" Inherits="UserCenterSystem.MobileSSMD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>师生名单</title>
    <style type="text/css">
        table.gridtable {
	font-family: verdana,arial,sans-serif;
	font-size:11em;
	color:#333333;
	border-width: 1em;
	border-color: #666666;
	border-collapse: collapse;
}
table.gridtable th {
	border-width: 1em;
	padding: 8em;
	border-style: solid;
	border-color: #666666;
	background-color: #dedede;
}
table.gridtable td {
	border-width: 1em;
	padding: 8em;
	border-style: solid;
	border-color: #666666;
	background-color: #ffffff;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>班级：</span>
        <asp:DropDownList ID="ddlBJ" runat="server" >
            <asp:ListItem Text="全部" />
            <asp:ListItem Text="高二一班" />
            <asp:ListItem Text="高二二班" />
        </asp:DropDownList>
        <span>归档：</span>
        <asp:DropDownList ID="DropDownList1" runat="server" >
            <asp:ListItem Text="全部" />
            <asp:ListItem Text="未归档" />
            <asp:ListItem Text="归档" />
        </asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" Text="查询"  BackColor="Wheat" />&nbsp;

        <table style="width:100%;" class="gridtable">
            <tr>
                <th colspan="2" >学生</th>
            </tr>
            <tr >
                <th>姓名</th>
                <th>是否归档</th>
            </tr>
            <tr >
                <td>王雨晴</td>
                <td>是</td>
            </tr>
            <tr >
                <td>段定言 </td>
                <td>否</td>
            </tr>
        </table>
        
        <table style="width:100%;" class="gridtable">
            <tr>
                <th colspan="3" >教师</th>

            </tr>
                                    
            <tr>
                <th>姓名</th>
                <th>是否归档</th>
                <th>查看信息</th>
            </tr>
            <tr style="text-align:center;">
                <td>姬爱鸣</td>
                <td>是</td>
                <td><input type="button"  value="查看信息" onclick="javascript: window.location.href = 'TeacherInfo.aspx'" style="background-color: wheat;" /></td>
            </tr>
            <tr style="text-align:center;">
                <td>李春佳  </td>
                <td>否</td>
                <td><input type="button"  value="查看信息" onclick="javascript: window.location.href = 'TeacherInfo.aspx'" style="background-color: wheat;" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
