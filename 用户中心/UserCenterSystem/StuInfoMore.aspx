<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuInfoMore.aspx.cs" Inherits="UserCenterSystem.StuInfoMore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学生详细信息查看</title>
     <link type="text/css" href="css/page.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center;">
            <br />
            <span style="font-size:x-large">个人信息</span>
            <table width="100%" border="2">
                <tr>
                    <th width="20%" >用户账号
                    </th>
                    <th width="20%">姓名</th>
                    <th width="30%">性别
                    </th>
                    <th width="30%">民族
                    </th>
                </tr>
                <tr style="text-align:center">
                    <td>&nbsp;&nbsp;&nbsp; <asp:Label ID="lblyhzh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>&nbsp;&nbsp; &nbsp; <asp:Label ID="lblxm" runat="server" Text="暂无"></asp:Label>
                &nbsp;</td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblxbm" runat="server" Text="暂无"></asp:Label>
                        &nbsp;</td>
                    <td>&nbsp;<asp:Label ID="lblmzm" runat="server" Text="暂无"></asp:Label>
      
                        &nbsp;</td>
                </tr>
                <tr>
                    <th width="20%">身份证号
                    </th>
                    <th width="20%">出生年月
                    </th>
                    <th width="30%">现住在</th>
                    <th width="30%">是否是流动人口</th>
                </tr> 
                <tr style="text-align:center">
                    <td>
                        <asp:Label ID="lblsfzjh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblcsrq" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblxzz" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblshsldrk" runat="server" Text="暂无"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th width="20%">户口性质
                    </th>
                    <th width="20%">联系电话
                    </th>
                    <th width="30%">通讯地址</th>
                    <th width="30%">政治面貌</th>
                </tr>
                <tr style="text-align:center">
                    <td>
                        <asp:Label ID="lblhkxz" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lbllxfs" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lbltxdz" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblzzmm" runat="server" Text="暂无"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th width="20%" >健康状况
                    </th>
                    <th width="20%">既往病史
                    </th>
                    <th width="30%">过敏史</th>
                    <th width="30%">是否是独生子女</th>
                </tr>
                <tr style="text-align:center">
                    <td>
                        <asp:Label ID="lbljkzk" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lbljwbs" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblgms" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblsfsdznv" runat="server" Text="暂无"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr style="text-align:center">

                      <th width="20%" >电子邮箱
                    </th>
                    <th width="20%">学籍号
                    </th>
                    <th width="30%">英文姓名</th>
                    <th width="30%">姓名拼音</th>
                </tr>
                    <tr>
                        <td>
                        <asp:Label ID="lbldzyx" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblxjh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td><asp:Label ID="lblywxm" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>&nbsp;<asp:Label ID="lblxmpy" runat="server" Text="暂无"></asp:Label>
                        &nbsp;</td>

                    </tr>

            </table>
            <br />
            <span style="font-size:x-large">学校信息</span>
            <table width="100%" border="2" >
                <tr>
                    <th width="20%" >年级 </th>
                    <th width="20%">班级</th>
                    <th width="30%">学生类别</th>
                    <th width="30%">是否是特长生 </th>
                </tr>
                <tr >
                    <td>
                        <asp:Label ID="lblnj" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblbh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblxslbm" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblsfstcs" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
            
                <tr>
                    <th width="20%" >学生状态 </th>
                    <th width="20%">入学年月</th>
                    <th width="30%">升学准考号</th>
                    <th width="30%">原学校名称 </th>
                </tr>
                <tr >
                    <td><asp:Label ID="lblxszt" runat="server" Text="暂无"></asp:Label>
                       </td>
                    <td>
                        <asp:Label ID="lblrxny" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblsxzkzh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblyxxmc" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
                <tr>
                     <th width="20%" >入学方式 </th>
                    <th width="20%">学生来源</th>
                    <th width="30%">来源地区</th>
                    <th width="30%">来处地区 </th>
                </tr>
               <tr>
                   <td><asp:Label ID="lblrxfsm" runat="server" Text="暂无"></asp:Label>
                       </td>
                    <td>
                        <asp:Label ID="lblxsly" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbllydq" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbllcdq" runat="server" Text="暂无"></asp:Label>
                    </td>
               </tr>
            </table>
            <br />
            <span style="font-size:x-large">家庭信息</span>
            <table width="100%" border="2" >
                <tr>
                    <th width="20%">关系 </th>
                    <th width="20%">父亲姓名</th>
                    <th width="30%">父亲电话</th>
                    <th width="30%">父亲单位 </th>
                </tr>
                <tr >
                    <td>父亲</td>
                    <td>
                        <asp:Label ID="lblfqxm" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblfqdh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblfqdw" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
            
                <tr>
                    <th width="20%">关系 </th>
                    <th width="20%">母亲姓名</th>
                    <th width="30%">母亲电话</th>
                    <th width="30%">母亲单位 </th>
                </tr>
                <tr >
                    <td >母亲</td>
                    <td >
                        <asp:Label ID="lblmqxm" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lblmqdh" runat="server" Text="暂无"></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lblmqdw" runat="server" Text="暂无"></asp:Label>
                    </td>
                </tr>
            </table>
            </div>
    </form>
</body>
</html>
