<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportUser.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.ImportUser"  %>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="expires" content="0"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<base target="_self" />
    <title>导入人员数据</title>
	<link href="css/edit.css" rel="stylesheet" />
    <link href="css/type.css" rel="stylesheet" />
        <script src="js/Jquery.easyui/jquery.min.js"></script>
  <script src="js/Jquery.easyui/jquery.easyui.min.js"></script>
        <script src="js/layer/layer.js"></script>
    <script src="js/layer/OpenLayer.js"></script>
    <link href="js/Jquery.easyui/themes/default/easyui.css" rel="stylesheet" />
               
</head>
<body>
<form id="Form" runat="server">
    
    <table class="tableEdit MendEdit" style="width: 100%;">

        <tr>
            <th style="width:120px;">导入到以下系统&nbsp<span style="color:Red">*</span></th>
            <td>                <asp:CheckBoxList runat="server" ID="cbsSytem" RepeatDirection="Horizontal">
                </asp:CheckBoxList></td>
        </tr>
        <tr>
            <th style="width:120px;" valign="Top">资源文件&nbsp<span style="color:Red">*</span></th>
            <td><input type="file" name="uploadify"id="uploadify" />
                     <script src="js/uploadify/jquery.uploadify.min.js"></script>
                        <link href="js/uploadify/uploadify.css" rel="stylesheet" />
                 <style type="text/css">
                #cbsSytem td {
                    font-size: 13px;
                    font-family: "微软雅黑","宋体";
                }
                 .uploadify-queue-item {
                    margin-top:-50px !important;
                    margin-left: 110px;
                }
</style>
                    <script type="text/javascript" language="javascript">
                        $(function () {

                            $("#uploadify").uploadify({
                                'auto': false,                      //是否自动上传
                                'swf': 'js/uploadify/uploadify.swf',      //上传swf控件,不可更改
                                'uploader': 'ImportUser.aspx',            //上传处理页面,可以aspx
                               // 'formData': { HDCheckValue: getCheckValue() }, //参数
                                //'fileTypeDesc': '',
                                'fileTypeExts': '*.xls;*.xlsx',   //文件类型限制,默认不受限制
                                'buttonText': '选择文件',//按钮文字
                                // 'cancelimg': 'uploadify/uploadify-cancel.png',
                                'width': 100,
                                'height': 26,
                                //最大文件数量'uploadLimit':
                                'multi': false,//单选            
                                'fileSizeLimit': '200MB',//最大文档限制
                                'queueSizeLimit': 1,  //队列限制
                                'removeCompleted': true, //上传完成自动清空
                                'removeTimeout': 0, //清空时间间隔
                                'overrideEvents': ['onUploadSuccess','onUploadStart','onUploadError','onSelectError'],
                                'onUploadSuccess': function (file, data, response) {
                                    if (data != "") {
                                        OL_CloserLayerAlert('成功导入' + data + '条人员数据!');
                                    }
                                },
                                'onUploadStart': function (file) {
                                    $("#uploadify").uploadify("settings", "formData", { 'HDCheckValue': getCheckValue() });
                                },
                                'onSelectError': uploadify_onSelectError,
                                'onUploadError': uploadify_onUploadError


                            });
                        });
                        function getCheckValue() {
                            var valuelist = ""; //保存checkbox选中值
                            //遍历name以listTest开头的checkbox
                            $("input[name^='cbsSytem']").each(function () {
                                if (this.checked) {
                                    //$(this):当前checkbox对象;
                                    //$(this).parent("span"):checkbox父级span对象
                                    valuelist += $(this).parent("span").attr("alt") + ",";
                                }
                            });
                            if (valuelist.length > 0) {
                                //得到选中的checkbox值序列,结果为400,398
                                valuelist = valuelist.substring(0, valuelist.length - 1);
                            }
                            return valuelist;
                        }
                        function onSave()
                        {
                        if ($("#cbsSytem input:checked").length == 0) {
                            layer.tips("请至少选择一个要添加的系统！", "#" + "<%=this.cbsSytem.ClientID %>");
                                return false;
                        }
                         if ($(".fileName").text() == "" || $(".fileName").text() == null) {
                             layer.tips("请选择Excel文件！", "#uploadify");
                              return false;
                         }
                         $('#uploadify').uploadify('upload', '*')
                       }
                        </script>
            </td>
        </tr>
        <tr>
            <th style="width:120px;">说明</th>
            <td style=" font-size: 13px;font-family:'微软雅黑','宋体';">  <li>导入的用户数据文件必须是Excel文件（.xls和.xlsx）！   
                  <li>导入的用户数据的字段及排列顺序必须和模板文件中的一致！[<A    href="uploads/zjzxUser.zip">点此下载模板文件</A>]   
                  <li>模板文件中的红色字体为必填项，必须填写才能导入成功！   
                  <li>导入时间跟用户数据多少成正比,请耐心等待！
    </table>
    <table width="100%" class="MendEdit">
        <tr>
            <td align="center">
                <input class="button_n" type="button"  value="导入"  onClick="javascript: return onSave();"  
class="ms-ButtonHeightWidth button_s"/>
     
                &nbsp;&nbsp;<input class="button_n" type="button" value="取消" onClick="OL_CloseLayerIframe();"  
class="ms-ButtonHeightWidth"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>