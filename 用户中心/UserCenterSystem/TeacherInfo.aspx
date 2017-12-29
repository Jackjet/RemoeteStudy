<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherInfo.aspx.cs" Inherits="UserCenterSystem.TeacherInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>培训教师信息</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <%--<script src="Scripts/jquery-1.6.min.js"></script>--%>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    <style type="text/css">
        .aaa{
            text-align:left;
        }
    </style>


</head>
<body>
    <form id="TeacherAdd" runat="server">

        <%--<asp:HiddenField ID="StudentOldIdCard" runat="server" />--%>
        <div class="teach_add" style="width: 100%; height: 100%; float: left; overflow-x: hidden; overflow-y: auto;">
            <table>
         
                
                <tr>
                    <td colspan="2">
                        <h3 class="TeacherInfo" style="text-align:left;">教师资历</h3>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">层次：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYXLCC" runat="server" Enabled="false">本科</asp:TextBox>
                    </td>
                    
                </tr>

                <tr>
                    <td class="auto-style1">毕业时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYXLBYSJ" runat="server"  Enabled="false">1998-10-19</asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">毕业院校：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYXLBYYX" runat="server" Enabled="false">北京教育学院</asp:TextBox>
                    </td>
                    
                </tr>

                <tr>
                    <td class="auto-style1">专业：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYXLZY" runat="server" Enabled="false">中文</asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <h3 class="TeacherInfo" style="text-align:left;">上课信息</h3>
                    </td>
                </tr>
                <tr>
                    <th colspan="4" class="Subtitle">当前</th>
                </tr>
                <tr>
                    <td class="auto-style1">所负责班级：</td>
                        
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="txtXJXZY" runat="server" Enabled="false">高二一班</asp:TextBox>
                    </td>
                    
                </tr>
                <tr><td class="auto-style1">
                        所负责课程：
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox1" runat="server" Enabled="false">语文</asp:TextBox>
                    </td></tr>
                <tr>
                    <th colspan="4" class="Subtitle">历史</th>
                </tr>
                <tr>
                    <td class="auto-style1">所负责班级：</td>
                        
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox2" runat="server" Enabled="false">高一一班</asp:TextBox>
                    </td>
                   
                </tr>
                <tr> <td class="auto-style1">
                        所负责课程：
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox3" runat="server" Enabled="false">语文</asp:TextBox>
                    </td></tr><tr><td></td></tr>
                <tr>
                    <td class="auto-style1">所负责班级：</td>
                        
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox4" runat="server" Enabled="false">高一三班</asp:TextBox>
                    </td>
                    
                </tr>
                <tr><td class="auto-style1">
                        所负责课程：
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox5" runat="server" Enabled="false">英语</asp:TextBox>
                    </td></tr>
                <tr><td></td></tr>
                <tr>
                    <td class="auto-style1">所负责班级：</td>
                        
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox6" runat="server" Enabled="false">高三一班</asp:TextBox>
                    </td>
                    
                </tr>
                <tr><td class="auto-style1">
                        所负责课程：
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox class="txtss" ID="TextBox7" runat="server" Enabled="false">语文</asp:TextBox>
                    </td></tr>
                <tr><td></td><td></td><td></td><td></td></tr>
                
                <tr>
                    <td colspan="2">
                        <h3 class="TeacherInfo" style="text-align:left;">教育信息</h3>
                    </td>
                </tr>
                
                <tr >
                    
                    <td colspan="2" style="width:300px;">
                        <asp:TextBox ID="txtBZ" runat="server" Width="100%" Rows="8" TextMode="MultiLine" Enabled="false">
                            有好教师，才会有好教育。深化教育领域综合改革,让教改成果惠及每个孩子，教师在其中扮演至关重要的角色。每个教师的改革行动虽然不易，却是成为“好教师”的必由之路；每个教师的改革行动看似微小，汇聚起来却是巨大的推动力量。时代在变化，社会在进步，孩子在发展，与之相适应的教育也必须改革。充分发挥教师在教育领域综合改革中的作用，是办好教育的必然选择。
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <input type="button" onclick="javascript: history.back(1);" value="返回" class="btn_concel" />

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
