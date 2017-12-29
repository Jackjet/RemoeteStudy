<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuImport.aspx.cs" Inherits="UserCenterSystem.StuImport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="css/page.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.6.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <div style="margin-left: 30px">
            <%--<div style="width: 100%; height: 30px; line-height: 30px; margin-top: 20px;">
                <span>上传学校：</span>
                <asp:DropDownList ID="dpDepartMent" runat="server" Width="100px" Style="height: 29px;"></asp:DropDownList>
                <a href="/Template/学生信息模板.xls" id="DownLoadExcel" style="margin-left: 10px;">下载模板</a>
            </div>--%>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                模板下载：
                <asp:Button runat="server" Text="点击下载" ID="DownLoadExcel" OnClick="btnDownExcel_Click" />
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <span>要导入的文件：</span>
                <input type="file" name="fileUpload" id="fileUpload" onclick="return CheckSelected()" style="width: 185px;"/>
            </div>
            <div style="width: 100%; height: 30px; line-height: 30px; margin-top: 10px;">
                <div style="margin: 0 auto; width: 165px;">
                    <asp:Button runat="server" Text="上传" OnClientClick=" return checkUpload();" ID="btnExcelSave" OnClick="btnExcelSave_Click" class="btnsave mr40" />
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
            <%--  <div runat="server" id="loding" style="width: 100%; height: 32px; text-align: center; margin: 0 auto; background-color: red; ">
                <img src="images/loading.gif" style="width: 32px; height: 32px;" />
                 <asp:Label runat="server" ID="loding" Text="正在上传···" Style="color: green;"></asp:Label> 
            </div>--%>
            <div id="Msg" runat="server" style="color: red;">
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
            alert("请选择导入的Excel文件！");
            return false;
        }
        else {
            HideSubmit();
            return true;
        }
    }
    //点击浏览文件时。判断是否选择学校
    function CheckSelected() {
        if ($("#dpDepartMent option:selected").val() == "") {
            alert("请选择上传学校！");
            return false;
        }
    }
    function HideSubmit() {
        $("#btnExcelSave").css("display", "none");
        $("#labTiShi").css("display", "inline");
    }

</script>
