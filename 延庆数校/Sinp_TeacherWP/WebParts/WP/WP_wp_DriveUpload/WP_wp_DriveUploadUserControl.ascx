<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WP_wp_DriveUploadUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.WP.WP_wp_DriveUpload.WP_wp_DriveUploadUserControl" %>
<link href="../../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="../../../../_layouts/15/uploadify/uploadify.css" rel="stylesheet" />
<script src="../../../../_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script src="../../../../_layouts/15/uploadify/jquery.uploadify-3.1.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var FirstUrl = window.location.href;
        FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

        var formData = { 'HFoldUrl': GetRequest().HFoldUrl, 'UrlName': GetRequest().UrlName, 'hSubject': GetRequest().hSubject, 'hContent': GetRequest().hContent, 'HStatus': GetRequest().HStatus };
        $("#uploadify").uploadify({
            'swf': '../../../../_layouts/15/uploadify/uploadify.swf',
            'uploader': FirstUrl + '/_layouts/15/hander/upload.aspx',
            'buttonText': '选择文件',
            //'cancelImg': 'upload/uploadify-cancel.png',
            //'buttonImage': '/img/bgimg.jpg',
            // 'folder': '/jxdBlog/photos',
            'auto': false,
            'multi': true,
            'wmode': 'transparent',
            'queueID': 'fileQueue',
            'width': '100%',
            'height': 80,
            'fileSizeLimit': '2047MB',
            'fileTypeDesc': '文件',       //图片选择描述  
            'queueSizeLimit': 10,                   //一个队列上传文件数限制  
            'removeTimeout': 1,                  //完成时清除队列显示秒数,默认3秒  
            //'fileTypeExts': '*.docx;*.doc;*.ppt;*.pptx;*.pdf;*.caj;*.txt;*.rar;*.zip;*.jpg;*.gif;',
            //'fileTypeDesc:': '请选择docx doc ppt pptx pdf caj txt rar zip jpg gif文件',
            'progressData': 'all',

            'formData': formData, //参数
            'removeCompleted': true,
            'overrideEvents': ['onUploadProgress'],
            onUploadSuccess: function (file, data, response) {
                //上传完成时触发（每个文件触发一次）
                if (data.indexOf('错误提示') > -1) {
                    $("#" + file.id).find(".data").html("上传失败：" + data);
                }

                else {
                    $("#" + file.id).find(".data").html("上传成功");
                    parent.Bind();
                }

            },
            //上传汇总  
            'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                var persent = parseInt(100 * bytesUploaded / bytesTotal);

                $("#" + file.id).find(".data").html(persent + "%");
                $("#" + file.id).find(".uploadify-progress-bar").css("width", persent + "%");

            },
            //返回一个错误，选择文件的时候触发             
            'onUploadError': function (file, errorCode, errorMsg, errorString) {
                alert('文件 ' + file.name + '上传失败: ' + errorString);
            },
            //检测FLASH失败调用              
            'onFallback': function () {
                alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
            },
            'onAllComplete': function (event, data) {
                alert(data.filesUploaded + '个图片上传成功');
            }

        });
    });
    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串

        var theRequest = new Object();

        if (url.indexOf("?") != -1) {

            var str = url.substr(1);

            strs = str.split("&");

            for (var i = 0; i < strs.length; i++) {

                theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
            }

        }

        return theRequest;

    }

</script>
<div class="yy_dialog" style="top: 0px; left: 0px;">
    <div class="t_content">
        <!---选项卡-->
        <div class="yy_tab">
            <div class="content">
                <div class="internship">
                    <div class="t_message">
                        <div class="message_con">
                            <div class="Upload">
                                <div class="list_add">
                                    <div class="add_jia">
                                        <table class="jia_c">
                                            <tr>
                                                <td id="att1">
                                                    <input type="file" name="uploadify" id="uploadify" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="fileup_w">
                                        <a href="javascript:$('#uploadify').uploadify('upload', '*')" class="fileup">开始上传</a>
                                        <a href="javascript:jQuery('#uploadify').uploadify('cancel')" class="fileup">取消上传</a>

                                    </div>
                                    <div class="T_list">
                                        <div id="fileQueue" style="width: 100%; height: auto; border: 2px solid #5493d7;"></div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>

    </div>
</div>

