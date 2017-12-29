<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherAdd.aspx.cs" Inherits="UserCenterSystem.TeacherAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>培训教师基本信息</title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <%--<script src="Scripts/jquery-1.6.min.js"></script>--%>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="Scripts/calendar.js"></script>
    <script type="text/javascript">
        function onCheckEmail() {
            var email = document.getElementById("txtDZXX").value;
            var isemail = /^\w+([-\.]\w+)*@\w+([\.-]\w+)*\.\w{2,4}$/;

            if (email.length > 25) {
                alert("长度太长");
                return false
            }
            if (!isemail.test(email)) {
                alert("邮箱格式不正确！");
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function previewImage(file) {
            var MAXWIDTH = 100;
            var MAXHEIGHT = 100;
            var div = document.getElementById('preview');
            if (file.files && file.files[0]) {
                div.innerHTML = '<img id=imghead>';
                var img = document.getElementById('imghead');
                img.onload = function () {
                    var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
                    img.width = rect.width;
                    img.height = rect.height;
                    img.style.marginLeft = rect.left + 'px';
                    img.style.marginTop = rect.top + 'px';
                }
                var reader = new FileReader();
                reader.onload = function (evt) { img.src = evt.target.result; }
                reader.readAsDataURL(file.files[0]);
            }
            else {
                var sFilter = 'filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src="';
                file.select();
                var src = document.selection.createRange().text;
                div.innerHTML = '<img id=imghead>';
                var img = document.getElementById('imghead');
                img.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = src;
                var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
                status = ('rect:' + rect.top + ',' + rect.left + ',' + rect.width + ',' + rect.height);
                div.innerHTML = "<div id=divhead style='width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;margin-left:" + rect.left + "px;" + sFilter + src + "\"'></div>";
            }
        }
        function clacImgZoomParam(maxWidth, maxHeight, width, height) {
            var param = { top: 0, left: 0, width: width, height: height };
            if (width > maxWidth || height > maxHeight) {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;

                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = Math.round(height / rateWidth);
                } else {
                    param.width = Math.round(width / rateHeight);
                    param.height = maxHeight;
                }
            }

            param.left = Math.round((maxWidth - param.width) / 2);
            param.top = Math.round((maxHeight - param.height) / 2);
            return param;
        }
    </script>
</head>
<body>
    <form id="TeacherAdd" runat="server">

        <asp:HiddenField ID="StudentOldIdCard" runat="server" />
        <div class="teach_add" style="width: 100%; height: 100%; float: left; overflow-x: hidden; overflow-y: auto;">
            <table>
                <tr>
                    <td colspan="4">
                        <h3 class="TeacherInfo">培训教师基本信息</h3>
                    </td>
                </tr>
                <tr>
                    
                    <td class="auto-style1">姓名：<span style="color: red;">*</span></td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXM" runat="server"></asp:TextBox>&nbsp; &nbsp;
                        <asp:RequiredFieldValidator ID="rfvXM" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtXM"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style1"><%--学校：--%><%--组织机构：--%></td>
                    <td class="auto-style2">
                        <%--<asp:DropDownList ID="ddlXX" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="ddlXX_SelectedIndexChanged" Enabled="false"></asp:DropDownList>--%>
                        <%--<asp:DropDownList ID="ddlXX" runat="server" Height="28px" ></asp:DropDownList>--%>
                    </td>
                    <%--<td class="auto-style1" style="display: none">组织机构：</td>
                    <td class="auto-style2" style="display: none">
                        <asp:DropDownList ID="ddlZZ" runat="server" Height="28px"></asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                    <td class="auto-style1">身份证类型：</td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddSFZJLX" runat="server" Height="28px">
                            <asp:ListItem Value="身份证">身份证</asp:ListItem>
                            <asp:ListItem Value="护照">护照</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1">身份证号码：<span style="color: red;">*</span></td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtSFZJH" runat="server" Enabled="false"></asp:TextBox>&nbsp; &nbsp;
                        <asp:RequiredFieldValidator ID="rfvSFZJH" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSFZJH"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtSFZJH" ValidationExpression="^\d{14}\d{3}?\w$"></asp:RegularExpressionValidator>
                        <input id="Hid_SFZJH" type="hidden" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">出生年月：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtCSNY" runat="server" onclick="return Calendar('txtCSNY');"></asp:TextBox>

                    </td>
                    <td class="auto-style1">性别：</td>
                    <td class="auto-style2">
                        <asp:RadioButtonList ID="rblXB" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">民族：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtMZ" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">身份：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtSF" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">政治面貌：</td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddZZMM" runat="server" Height="28px">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="中共党员">中共党员</asp:ListItem>
                            <asp:ListItem Value="共青团员">共青团员</asp:ListItem>
                            <asp:ListItem Value="群众">群众</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1">教师资格证类别:</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtJSZGZ" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="PhotoTextCSS">照片：</td>
                    <td class="PhotoUploadCss">
                        <asp:FileUpload ID="fuZP" runat="server" onchange="previewImage(this)" Width="170px" />
                        <asp:Label ID="lbl_pic" runat="server" class="pic_text"></asp:Label>
                    </td>
                    <td>
                        <div id="preview" class="photo">
                            <asp:Image ID="imghead" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <h3 class="TeacherInfo">培训教师详细信息</h3>
                    </td>
                </tr>
                <%--<tr>
                    <th colspan="4" class="Subtitle">原学历</th>
                </tr>
                <tr>
                    <td class="auto-style1">层次：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYXLCC" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">毕业时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYXLBYSJ" runat="server" onclick="return Calendar('txtYXLBYSJ');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">毕业院校：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYXLBYYX" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">专业：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYXLZY" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">现学历</th>
                </tr>
                <tr>
                    <td class="auto-style1">层次：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXXLCC" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">毕业时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtXXLBYSJ" runat="server" onclick="return Calendar('txtXXLBYSJ');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">毕业院校：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXXLBYYX" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">专业：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtXXLZY" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">现进修学历</th>
                </tr>
                <tr>
                    <td class="auto-style1">层次：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXJXCC" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">毕业时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtXJXBYSJ" runat="server" onclick="return Calendar('txtXJXBYSJ');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">毕业院校：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXJXBYYX" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">专业：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtXJXZY" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">专业技术职务</th>
                </tr>
                <tr>
                    <td class="auto-style1">所负责专业：</td>

                    <td class="auto-style2">
                        <asp:DropDownList ID="Drp_Grade" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="Drp_Grade_SelectedIndexChanged"></asp:DropDownList></td>
                    <td class="auto-style1">
                        所负责班级：
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="Drp_Class" runat="server" Height="28px"></asp:DropDownList>
                    </td>

                </tr>--%>
                <tr>
                    <%--<asp:ListView ID="lvStu" runat="server">
                        <ItemTemplate>
                            <tr class="TeacherSubjectCss">
                                <td class="SubjectCss">
                                    <asp:Label ID="Lb_Subject" runat="server" Text="担任学科"></asp:Label>
                                </td>
                                <td class="GreadCss">
                                    <%# Eval("NJMC") %>
                                </td>
                                <td class="SubjectsCss">
                                    <asp:CheckBoxList ID="cblXK" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                    <asp:HiddenField ID="hfGread" runat="server" Value='<%# Eval("NJ") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <tr runat="server" class="b_txt alt-row">
                                <th runat="server"></th>
                                <th runat="server"></th>
                                <th runat="server"></th>
                                <th runat="server"></th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </LayoutTemplate>
                    </asp:ListView>--%>
                </tr>
                <%--<tr>
                    <td class="auto-style1">系列：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">职称：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtZC" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">级别：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtJB" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">评定时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtPDSJ" runat="server" onclick="return Calendar('txtPDSJ');"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">工资</th>
                </tr>
                <tr>
                    <td class="auto-style1">序列：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtdl" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">等级：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtDJ" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">现具体岗位</th>
                </tr>
                <tr>
                    <td class="auto-style1">类别：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtLB" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">主要任课学段：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtZYRKXD" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">现具体岗位：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXJTGW" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">兼课或兼职：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtJKJZ" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">周总课时数：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtZZKSS" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">骨干类别：</td>
                    <td>
                        <asp:DropDownList ID="ddlGGLB" runat="server" Height="28px">
                            <asp:ListItem Value="0">--请选择--</asp:ListItem>
                            <asp:ListItem Value="市骨">市骨</asp:ListItem>
                            <asp:ListItem Value="县骨">县骨</asp:ListItem>
                            <asp:ListItem Value="校骨">校骨</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th colspan="4" class="Subtitle">参加研究生课程班学习</th>
                </tr>
                <tr>
                    <td class="auto-style1">层次：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYJSBCC" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">毕业时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYJSBBYSJ" runat="server" onclick="return Calendar('txtYJSBBYSJ');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">毕业院校：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYJSBBYYX" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">专业：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYJSBZY" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <th colspan="4" class="Subtitle">家庭住址、联系电话</th>
                </tr>
                <tr>
                    <td class="auto-style1">籍贯：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtJG" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">手机号：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtSJH" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revSJH" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtSJH" ValidationExpression="^1[3|4|5|8][0-9]\d{4,8}$"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">家庭电话：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtJTDH" runat="server"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revJTDH" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtJTDH" ValidationExpression="^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$"></asp:RegularExpressionValidator>--%>
                    </td>
                    <td class="auto-style1">现住址：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtXZZ" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <th colspan="4" class="Subtitle">配偶情况</th>
                </tr>
                <tr>
                    <td class="auto-style1">配偶姓名：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtPOXM" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">配偶工作单位：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtPOGZDW" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="auto-style1">备注：</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtBZ" runat="server" Width="570" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <!-- 新增信息 -->
                <tr>
                    <th colspan="4" class="Subtitle">详细信息</th>
                </tr>
                <%--<tr>
                    <td class="auto-style1">英文姓名：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtYWXM" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">姓名拼音：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXMPY" runat="server"></asp:TextBox>
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">曾用名：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtCYM" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">参加工作时间：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtCJGZSJ" runat="server" onclick="return Calendar('txtCJGZSJ');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">出生地：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtCSDM" runat="server"></asp:TextBox>

                    </td>
                    <td class="auto-style1">婚姻状况：</td>
                    <td>
                        <asp:RadioButtonList ID="rbHYZKM" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="未婚" Selected="True">未婚</asp:ListItem>
                            <asp:ListItem Value="已婚">已婚</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">国籍：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtGJDQM" runat="server"></asp:TextBox>

                    </td>
                    <td class="auto-style1">港澳台侨外：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtGATQWM" runat="server"></asp:TextBox>

                    </td>
                </tr>--%>
                <tr>
                    <td class="auto-style1">健康状况：</td>
                    <td style="padding-right: 180px">
                        <asp:RadioButtonList ID="rblJKZKM" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="健康" Selected="True">健康</asp:ListItem>
                            <asp:ListItem Value="疾病">疾病</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style1">血型：</td>
                    <td>
                        <asp:DropDownList ID="ddXXM" runat="server" Height="28px">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="A">A</asp:ListItem>
                            <asp:ListItem Value="AB">AB</asp:ListItem>
                            <asp:ListItem Value="B">B</asp:ListItem>
                            <asp:ListItem Value="O">O</asp:ListItem>
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <%--<tr>
                    <td class="auto-style1">宗教信仰：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtXYZJM" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">机构号：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtJGH" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="auto-style1">身份证有效期：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtSFZJYXQ" runat="server" onclick="return Calendar('txtSFZJYXQ');"></asp:TextBox>
                    </td>
                    <td class="auto-style1">家庭住址：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtJTZZ" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">户口性质：</td>
                    <td style="padding-right: 165px">
                        <asp:RadioButtonList ID="rbHKXZM" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="农业" Selected="True">农业</asp:ListItem>
                            <asp:ListItem Value="非农业">非农业</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style1">户口所在地：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtHKSZD" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">来校年月：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtLXNY" runat="server" onclick="return Calendar('txtLXNY');"></asp:TextBox>
                    </td>
                    <td class="auto-style1">从教年月：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtCJNY" runat="server" onclick="return Calendar('txtCJNY');"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">编制类别：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtBZLBM" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">邮政编码：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtYZBM" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revYZBM" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtYZBM" ValidationExpression="[1-9]\d{5}(?!\d)"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">档案编号：</td>
                    <td class="auto-style2">
                        <asp:TextBox class="TcInputCss" ID="txtDABH" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">档案文本：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtDAWB" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">电子信箱：</td>
                    <td style="padding-right: 12px">
                        <asp:TextBox class="TcInputCss" ID="txtDZXX" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDZXX" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtDZXX" ValidationExpression="^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style1">主页地址：</td>
                    <td>
                        <asp:TextBox class="TcInputCss" ID="txtZYDZ" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revZYDZ" runat="server" ErrorMessage="格式不正确！" ForeColor="Red" ControlToValidate="txtZYDZ" ValidationExpression="^(((file|gopher|news|nntp|telnet|http|ftp|https教程|ftps|sftp)://)|(www.))+(([a-zA-Z0-9._-]+.[a-zA-Z]{2,6})|([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}))(/[a-zA-Z0-9&%_./-~-]*)?$"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">特长：</td>
                    <td colspan="3">
                        <asp:TextBox class="TcInputCss" ID="txtTC" runat="server" Width="570" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="btnAdd" runat="server" Text="提交数据" OnClick="btnAdd_Click" class="btnsave mr40" Width="80px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" CausesValidation="false" Style="width: 80px;" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
