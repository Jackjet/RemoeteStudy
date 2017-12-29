<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Certifacat.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Certifacat" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <title>添加证书</title>

    <link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
    <script src="js/Jquery.easyui/jquery.min.js" type="text/javascript"></script>
    <script src="js/Jquery.easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/layer/layer.js" type="text/javascript"></script>
    <script src="js/layer/OpenLayer.js" type="text/javascript"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="http://html2canvas.hertzen.com/build/html2canvas.js"></script>
    <script src="../Script/jquery.jqprint.js"></script>

</head>
<body>
    <form id="Form" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $(".example1").on("click", function (event) {
                    event.preventDefault();
                    html2canvas($("#percite"), {
                        allowTaint: true,
                        taintTest: false,
                        onrendered: function (canvas) {
                            canvas.id = "mycanvas";
                            //document.body.appendChild(canvas);
                            //生成base64图片数据
                            var dataUrl = canvas.toDataURL();
                            var newImg = document.createElement("img");
                            newImg.src = dataUrl;
                            var w = window.open('about:blank', 'image from canvas');
                            w.document.write("<img src='" + dataUrl + "' alt='from canvas'/>");
                            //document.body.appendChild(newImg);
                            //var strDataURI = canvas.toDataURL();
                        }
                    });
                });

            });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".example2").click(function () {
                    $("#percite").jqprint({
                        debug: false, //如果是true则可以显示iframe查看效果（iframe默认高和宽都很小，可以再源码中调大），默认是false
                        importCSS: true, //true表示引进原来的页面的css，默认是true。（如果是true，先会找$("link[media=print]")，若没有会去找$("link")中的css文件）
                        printContainer: true, //表示如果原来选择的对象必须被纳入打印（注意：设置为false可能会打破你的CSS规则）。
                        operaSupport: true//表示如果插件也必须支持歌opera浏览器，在这种情况下，它提供了建立一个临时的打印选项卡。默认是true
                    });
                })
            });
        </script>
        <table class="tableEdit MendEdit" id="percite" style="background-color: orange; text-align: center">
            <tr>
                <td colspan="2">证书</td>
            </tr>
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
                    <input class="example2" type="button" value="打印证书">
                    <span style="color: red">导出的证书请自行保存</span>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
