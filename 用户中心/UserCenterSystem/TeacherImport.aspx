<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherImport.aspx.cs" Inherits="UserCenterSystem.TeacherImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript">
        //导入验证
        function checkUpload() {
            // 若没有上传文件,则提示上传
            if ($("#fuImport").val() == "") {
                alert("请选择导入的Excel文件！");
                return false;
            }
            else {
                HideSubmit();
                return true;
            }
        }
        //屏蔽提交按钮，显示提示信息
        function HideSubmit() {
            $("#btnImport").css("display", "none");
            $("#labTiShi").css("display", "inline");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 30px">
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                模板下载：
                <asp:Button runat="server" Text="点击下载" ID="downmoban" OnClick="downmoban_Click" />
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <span>要导入的文件：</span>
                <asp:FileUpload ID="fuImport" runat="server" style="width: 185px;"/>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <div style="margin: 0 auto; width: 165px;">
                    <asp:Button runat="server" Text="导入" ID="btnImport" OnClientClick=" return checkUpload();" OnClick="btnImport_Click" class="btnsave mr40" />
                    <asp:Label ID="labTiShi" runat="server" Text="正在上传......" ForeColor="Red" style="display:none;"/>
                    <input type="button" class="wBox_close on btn_concel" value="取消" />
                </div>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 15px;">
                <p>用户提示：</p>
                <%--<p>1.根据选择的学校，下载模板</p>--%>
                <p>完善模板信息，然后上传模板文件</p>
                <div id="Div1" runat="server">
                </div>
            </div>
            <%--<div id="winDiv" style="width: 400px; height: 50px; margin: 50px;">

            <asp:FileUpload ID="fuImport" runat="server" Width="200px" />
            &nbsp;
       <asp:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" BackColor="Wheat" />

            &nbsp;&nbsp;
       <asp:Button ID="downmoban" runat="server" Text="下载模板" OnClick="downmoban_Click" BackColor="Wheat" />
        </div>
        <div runat="server" id="showError"   style="color: red;">
        </div>--%>
        </div>
    </form>
</body>
</html>
