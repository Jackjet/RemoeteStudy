<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Certifacat1.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.Certifacat" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>添加证书</title>

    <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <script src="YHSD.VocationalEducation.Portal/js/Jquery.easyui/jquery.min.js"></script>
    <script src="js/Jquery.easyui/jquery.min.js" type="text/javascript"></script>
    <script src="js/Jquery.easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/layer/layer.js" type="text/javascript"></script>
    <script src="js/layer/OpenLayer.js" type="text/javascript"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="Script/jquery-1.8.2.min.js"></script>
        <script type="text/javascript" src="http://html2canvas.hertzen.com/build/html2canvas.js"></script>

</head>
<body>
    <form id="Form" runat="server">
        <script type="text/javascript">


            $(document).ready(function () {
                $(".example1").on("click", function (event) {
                    event.preventDefault();
                    html2canvas(document.body, {
                        allowTaint: true,
                        taintTest: false,
                        onrendered: function (canvas) {
                            canvas.id = "mycanvas";
                            //document.body.appendChild(canvas);
                            //生成base64图片数据
                            var dataUrl = canvas.toDataURL();
                            var newImg = document.createElement("img");
                            newImg.src = dataUrl;
                            document.body.appendChild(newImg);
                            var strDataURI = canvas.toDataURL();
                        }
                    });
                });

            });
        </script>
        <table class="tableEdit MendEdit">
            <tr>
                <th>结业证书号</th>
                <td>
                    <asp:Label ID="TB_GraduationNo" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th>学员姓名</th>
                <td>
                    <asp:Label ID="DDL_StuName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>课程名称</th>
                <td>
                    <asp:Label ID="DDL_CurriculumName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>结业时间</th>
                <td>
                    <asp:Label ID="TB_GraduationDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>发证单位</th>
                <td>
                    <asp:Label ID="TB_AwardUnit" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>证书查询网址</th>
                <td>
                    <asp:Label ID="TB_QueryURL" runat="server"></asp:Label></td>
            </tr>
        </table>
        <table width="100%" class="MendEdit">
            <tr>
                <td align="center">
                    <input class="example1" type="button" value="导出证书">
                    <input class="example1" type="button" value="打印证书">
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
