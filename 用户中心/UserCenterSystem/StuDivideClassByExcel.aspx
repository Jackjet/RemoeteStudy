<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuDivideClassByExcel.aspx.cs" Inherits="UserCenterSystem.StuDivideClassByExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
    <title></title>
</head>


<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div style="margin-left: 30px">



            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 20px;">
                <span>学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;校：</span>
                <asp:DropDownList ID="dp_DepartMent" runat="server" Width="100px" Style="height: 29px;">
                </asp:DropDownList>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                模板下载：
                   <asp:Button runat="server" Text="点击下载" ID="btnDownExcel" OnClientClick=" return CheckSelected();" OnClick="btnDownExcel_Click" />
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <span>选择文件：</span>
                <input type="file" name="fileUpload" id="fileUpload" onclick=" return CheckSelected();" style="width: 185px;"/>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <div style="margin: 0 auto; width: 165px;">

                    <asp:Button runat="server" Text="上传" OnClientClick="return checkUpload();" ID="btnExcelSave" OnClick="btnExcelSave_Click" class="btnsave mr40" />
                    <asp:Label ID="labTiShi" runat="server" Text="正在上传......" ForeColor="Red" style="display:none;"/>
                    <input type="button" class="wBox_close on  btn_concel" value="取消" />
                </div>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 15px;">
                <p>用户提示：</p>
                <p>1.根据选择的学校，下载模板</p>
                <p>2.完善模板信息，然后上传模板文件</p>
                <div id="Msg" runat="server">
                </div>
            </div>

        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    // 上传文件
    function checkUpload() {
        // 若没有上传文件,则提示 模板 -上传
        if ($("#fileUpload").val() == "") {
            alert("请先选择文件，再进行上传！");
            return false;
        }
        else {
            HideSubmit();
            return true;
        }
    }

    //点击浏览文件时。判断是否选择学校
    function CheckSelected() {
        if ($("#dp_DepartMent option:selected").val() == "") {
            alert("请选择导入学生所在学校！");
            return false;
        }
        else {
            return true;
        }
    }
    function HideSubmit() {
        $("#btnExcelSave").css("display", "none");
        $("#labTiShi").css("display", "inline");
    }
</script>
