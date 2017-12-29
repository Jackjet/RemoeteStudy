<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentEdit.aspx.cs" Inherits="UserCenterSystem.StudentEdit" %>

<!DOCTYPE html>
<link type="text/css" href="css/page.css" rel="stylesheet" />
<script src="Scripts/jquery-1.6.min.js"></script>

<script type="text/javascript" src="Scripts/calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学员档案修改</title>
    <style type="text/css">
        .auto-style3 {
            width: 225px;
        }
        .auto-style4 {
            width: 170px;
            height: 48px;
        }
        .auto-style5 {
            width: 225px;
            height: 48px;
        }
        .auto-style6 {
            height: 48px;
        }
    </style>
</head>

<body>
    <form id="stuEditForm" runat="server">
        <div style="margin-left: 50px" class="stu_edit">
            <table>
                <tr>
                    <td colspan="4">
                        <h3 class="TeacherInfo">学员档案</h3>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Name" runat="server" Text="姓      名："></asp:Label>
                        <span style="color: Red;">*</span>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_RealName" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_SpellName" runat="server" Text="姓名拼音："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_SpellName" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_IdentityDocuments" runat="server" Text="证件类型："></asp:Label>
                        <span style="color: Red;">*</span>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="dp_IdentityDocuments" runat="server" Height="28px" >
                            <asp:ListItem Selected="True" Value="">请选择
                            </asp:ListItem>
                            <asp:ListItem Value="居民身份证">居民身份证</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_UserIdentity" runat="server" Text="证件号："></asp:Label>
                        <span style="color: Red;">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_UserIdentity" runat="server" ></asp:TextBox>
                        <%--<asp:Button ID="btnRandom" runat="server" OnClick="btnRandom_Click" Text="系统随机生成" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="Label16" runat="server" Text="英文姓名："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtywxm" runat="server"></asp:TextBox></td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Sex" runat="server" Text="性   别："></asp:Label>
                        <span style="color: Red;">*</span>
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="dp_Sex" runat="server" Height="25px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>

                            <asp:ListItem Value="男">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:RadioButtonList ID="rblXB" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Nation" runat="server" Text="民 族："></asp:Label>
                        <span style="color: Red;">*</span> </td>
                    <td class="auto-style3">
                        <%--<asp:DropDownList ID="dp_Nation" runat="server" Height="25px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem> 
                            <asp:ListItem Value="汉">汉</asp:ListItem>
                            <asp:ListItem Value="满">满</asp:ListItem> 
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:TextBox ID="txtmzm" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_PoliticsStatus" runat="server" Height="28px" Text="政治面貌："></asp:Label>
                        <span style="color: Red;">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="dp_PoliticsStatus" runat="server" Style="height: 30px; border-radius: 3px; border: 1px solid #ccc;">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="党员">党员</asp:ListItem>
                            <asp:ListItem Value="党员">团员</asp:ListItem>
                            <asp:ListItem Value="少先队">少先队</asp:ListItem>
                            <asp:ListItem Value="群众">群众</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="Label11" runat="server" Text="归档："></asp:Label>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGD1" runat="server">
                            <asp:ListItem Text="否" Value="0" />
                        <asp:ListItem Text="是" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td class="TcInputCss">

                        <asp:Label ID="Label8" runat="server" Text="学生类别："></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="Dp_xslb" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="普通学生">普通学生</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <h3 class="TeacherInfo">学生详细信息</h3>
                    </td>
                </tr>
                <tr>
                    <%--<td class="TcInputCss">
                        <asp:Label ID="Label11" runat="server" Text="学校："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="dp_DepartMent" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="dp_DepartMent_SelectedIndexChanged" Enabled="false">
                            <asp:ListItem Value="" Selected="True">-学校-</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                    <%--<td class="TcInputCss">
                        <asp:Label ID="Label10" runat="server" Text="专业："></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dp_Grades" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="dp_GradesIndexChanged">
                            <asp:ListItem Value="" Selected="True">-专业-</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="TcInputCss">
                        <asp:Label ID="Label9" runat="server" Text="班级："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="dp_Class" runat="server" Height="28px">
                            <asp:ListItem Value="" Selected="True">-班级-</asp:ListItem>

                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <%--<tr>
                    <td class="TcInputCss">
                        <asp:Label ID="Label18" runat="server" Text="专业："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddlZY" runat="server" Height="28px">
                        </asp:DropDownList>
                    </td>
                    <td class="TcInputCss">
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>--%>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_ContactTel" runat="server" Text="联系方式："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_ContactTel" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_CurrentAddress" runat="server" Text="现住址："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_CurrentAddress" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_BirthDay" runat="server" Text="出生日期："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Birthday" onclick="return Calendar('tb_Birthday');" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_BirthPlace" runat="server" Text="出生地："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_BirthPlace" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td class="TcInputCss">
                        <asp:Label ID="Label15" runat="server" Text="国别："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <%--<asp:DropDownList ID="dp_NativePlace" runat="server" Height="25px" >
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem> 
                            <asp:ListItem Value="中国">中国</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGB" runat="server"></asp:TextBox>
                    </td>

                    <td class="TcInputCss">
                        <asp:Label ID="lb_PostalCode" runat="server" Text="邮政编码："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_PostalCode" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="TcInputCss">

                        <asp:Label ID="Label14" runat="server" Text="电子邮箱："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtdzyx" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">

                        <asp:Label ID="Label13" runat="server" Text="学籍号："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtxjh" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    
                    
                </tr>
                <tr>
                    <%--<td class="TcInputCss">

                        <asp:Label ID="Label7" runat="server" Text="学生状态："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="Dp_xszt" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="在校生">在校生</asp:ListItem>
                            <asp:ListItem Value="毕业生">毕业生</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>

                    <%--<td class="TcInputCss">
                        <asp:Label ID="lb_ResidentCity" runat="server" Text="户口所在地："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_ResidentCity" runat="server"></asp:TextBox>
                    </td>--%>
                </tr>
                <%--<tr>
                    <td class="TcInputCss">

                        <asp:Label ID="Label6" runat="server" Text="户口所在地区县编号："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txthkszdqx" runat="server"></asp:TextBox>
                    </td>

                    <td class="TcInputCss">
                        <asp:Label ID="lb_HouseholdType" runat="server" Text="户口性质：">
                           
                        </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dp_HouseholdType" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="农业户口">农业户口</asp:ListItem>
                            <asp:ListItem Value="非农业户口">非农业户口</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_IsFloatingPeople" runat="server" Text="是否流动人口："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="dp_IsFloatingPeople" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_IsLocalTreat" runat="server" Text="是否按本市户口："></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dp_IsLocalTreat" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_IsSpecialty" runat="server" Text="是否特长生："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="dp_IsSpecialty" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TcInputCss">

                        <asp:Label ID="Label17" runat="server" Text="就读方式："></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="Dp_jdfs" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="走读">走读</asp:ListItem>
                            <asp:ListItem Value="住校">住校</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="auto-style4">
                        <asp:Label ID="lb_HealthCondition" runat="server" Text="健康状况："></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="dp_HealthCondition" runat="server" Height="28px">
                            <asp:ListItem Selected="True" Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="健康或良好">健康或良好</asp:ListItem>
                            <asp:ListItem Value="暂无">暂无</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="lb_Allergies" runat="server" Text="过敏史："></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:TextBox ID="tb_Allergies" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="TcInputCss">

                        <asp:Label ID="lb_MedicalHistory" runat="server" Text="既往病史："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_MedicalHistory" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_EduID" runat="server" Text="教育ID："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_EduID" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_AdmissionDate" runat="server" Text="入学时间："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_AdmissionDate" onclick="return Calendar('tb_AdmissionDate');" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_UsedSchName" runat="server" Text="原学校名称："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_UsedSchName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">

                        <asp:Label ID="Label5" runat="server" Text=" 入学方式："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtrxfsm" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">

                        <asp:Label ID="Label4" runat="server" Text=" 学生来源："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtxslymSource" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="TcInputCss">
                        <asp:Label ID="Label3" runat="server" Text=" 来源地区："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtlydqm" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="Label2" runat="server" Text=" 来处地区："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtlydq" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_FartherName" runat="server" Text="父亲姓名："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_FartherName" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_WorkUnit1" runat="server" Text="工作单位："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_WorkUnit1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Telephone1" runat="server" Text="联系电话："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Telephone1" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_MotherName" runat="server" Text="母亲姓名："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_MotherName" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_WorkUnit2" runat="server" Text="工作单位："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_WorkUnit2" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Telephone2" runat="server" Text="联系电话："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_Telephone2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_GuardianName" runat="server" Text="监护人姓名："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_GuardianName" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_GuardianWorkPlace" runat="server" Text="监护人工作单位："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_GuardianWorkPlace" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_GuardianTele" runat="server" Text="监护人电话："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_GuardianTele" runat="server"></asp:TextBox>
                    </td>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_GuardianDuty" runat="server" Text="监护人职务："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_GuardianDuty" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TcInputCss">
                        <asp:Label ID="lb_Guardianship" runat="server" Text="监护人关系："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tb_Guardianship" runat="server"></asp:TextBox>
                    </td>
                    

                </tr>--%>
                <tr>
                    <td class="TcInputCss" >

                        <asp:Label ID="Label1" runat="server" Text=" 备注："></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtbz" runat="server" TextMode="MultiLine" Width="100%" Rows="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" Width="60px" class="btnsave mr40" />
                        &nbsp;
                <input type="button" onclick="javascript: history.back(1);" value="取消" class="btn_concel" />
                    </td>
                </tr>
            </table>




        </div>
        <asp:HiddenField ID="hiddenschool" runat="server" />
        <asp:HiddenField ID="hiddenGrade" runat="server" />
        <asp:HiddenField ID="hiddenClass" runat="server" />
    </form>

</body>
</html>




