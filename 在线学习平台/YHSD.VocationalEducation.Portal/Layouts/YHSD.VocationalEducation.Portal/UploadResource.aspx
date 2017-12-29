<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadResource.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.UploadResource" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <link href="js/uploadify/uploadify.css" rel="stylesheet" />
    <link href="css/edit.css" rel="stylesheet" />
    <script src="js/layer/OpenLayer.js"></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <table style="width: 100%; margin: 16px;" class="tableEdit">
            <tr>
                <th>资源名称&nbsp;<span style="color: Red">*</span></th>
                <td>
                    <input type="text" id="txtName" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>资源分类&nbsp;<span style="color: Red">*</span></th>
                <td id="DivCFTip">
                    <select id="txtCfName" class="easyui-combotree" style="width: 412px; height: 29px;"
                        data-options="url:'<%=YHSD.VocationalEducation.Portal.Code.Common.CommonUtil.GetChildWebUrl()%>/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumInfoTree.aspx',required:false">
                    </select>
                </td>
            </tr>
            <tr>
                <th>主讲人</th>
                <td>
                    <input type="text" id="txtSpeechMaker" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>责任人</th>
                <td>
                    <input type="text" id="txtPersonLiable" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>拍摄时间</th>
                <td>
                    <input type="text" id="txtScreenTime" onclick="WdatePicker({ isShowClear: true, readOnly: true });" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>格式</th>
                <td>
                    <input type="text" id="txtFormat" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>时长</th>
                <td>
                    <input type="text" id="txtDuration" class="inputPart" style="width: 400px;" />
                </td>
            </tr>
            <tr>
                <th>简介</th>
                <td>
                    <textarea rows="6" cols="50" id="txtSummary" class="inputPart" style="height: 80px; width: 400px; overflow-x: hidden"></textarea>
                </td>
            </tr>
            <tr>
                <th>视频文件&nbsp;<span style="color: Red">*</span></th>
                <td style="width: 400px;">
                    <span id="uploadState" style="margin: 10px;">未上传</span><a href="javascript:;" id="DelResource">删除</a>
                </td>
            </tr>
            <tr>
                <th></th>
                <td style="width: 400px;" id="DivAttTip">
                    <input type="file" name="uploadify" id="uploadify" style="display: none;" />
                    <div id="fileQueue"></div>
                </td>
            </tr>
            <tr>
                <th>视频图片</th>
                <td>
                    <img alt="视频截图" id="videoImg" src="/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png" onerror="ReSetImgUrl(this,'images/NoImage.png')" width="170" height="100" /></td>
            </tr>
        </table>
        <div>
            <input type="hidden" id="hfFilePath" />
            <input type="hidden" id="hfFileName" />
            <input type="hidden" id="hfClassificationID" value="<%=Request["id"] %>" />
            <input type="hidden" id="hfSPImgUrl" />
            <input type="hidden" id="hfAttachmentID" />
            <input type="hidden" id="hfAttachmentModel" />
            <input type="hidden" id="hfOldEntity" />
        </div>
        <div class="buttomToolbar">
            <input type="button" value="抓图" onclick="GetImg()" class="button_s" style="display: none" />
            <input type="button" value="上传" onclick="StartUpload()" id="btnSave" class="button_s" />
            <input type="button" value="保存" onclick="Save()" class="button_s" />
            <input type="button" value="返回" onclick="javascript: window.history.back()" class="button_n" />
        </div>
        <script src="js/uploadify/jquery.uploadify.min.js"></script>
        <script src="js/DatePicker/WdatePicker.js"></script>
        <script type="text/javascript">
            var uploadSuccess = false;
            var editId = "<%=Request["id"]%>";
            var isNewAttach = false, isNewClassification = false;
            var oldCFId;//保存原有资源分类ID,以便在修改资源分类时做修改

            function GetImg() {
                var filePath, fileName, attachmentID, classificationID;
                filePath = $("#hfFilePath").val();//本地全路径
                fileName = $("#hfFileName").val();//文件名
                classificationID = $("#hfClassificationID").val();//分类ID
                attachmentID = $("#hfAttachmentID").val();//附件ID

                if (filePath == "" || fileName == ""  || attachmentID == "") {
                    LayerAlert("上传视频后才可截图!");
                    return;
                }
                var postData = { CMD: "CatchImg", FilePath: filePath, FileName: fileName, ClassificationID: classificationID, AttachmentID: attachmentID };
                var loadIndex = layer.load(2);
                AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                    $("#videoImg").attr("src", returnVal.ImgUrl);
                    $("#hfSPImgUrl").val(returnVal.ImgUrl);
                    isNewClassification = false;//新上传的资源不需要移动
                    layer.close(loadIndex);
                }, function (msg) {
                    layer.close(loadIndex);
                    LayerAlert(msg);
                })
            }
            function StartUpload() {
                //因为文件保存在临时文件夹,所以不需要预先选择分类
                //if ($("#hfClassificationID").val() == "") {
                //    LayerAlert("请选择分类！");
                //    return;
                //}
                javascript: $('#uploadify').uploadify('upload');
            }
            function Add() {
                var entity = {
                    Name: $("#txtName").val(),
                    ClassificationID: $("#hfClassificationID").val(),
                    AttachmentID: $("#hfAttachmentID").val(),
                    PhotoUrl: $("#hfSPImgUrl").val(),
                    Duration: $("#txtDuration").val(),
                    Format: $("#txtFormat").val(),
                    PersonLiable: $("#txtPersonLiable").val(),
                    SpeechMaker: $("#txtSpeechMaker").val(),
                    Summary: $("#txtSummary").val(),
                    ScreenTime: $("#txtScreenTime").val()
                };
                if (entity.ClassificationID == "") {
                    LayerTips("资源分类不能为空!", "DivCFTip");
                    return;
                }
                if (entity.Name == "") {
                    LayerTips("资源名称不能为空!", "txtName");
                    return;
                }
                if (entity.Name.length > 50) {
                    LayerTips("资源名称不能超过50个字符！", "txtName");
                    return;
                }
                if (entity.Duration.length > 25) {
                    LayerTips("时长不能超过25个字符！", "txtDuration");
                    return;
                }
                if (entity.Format.length > 25) {
                    LayerTips("格式不能超过25个字符！", "txtFormat");
                    return;
                }
                if (entity.Summary.length > 100) {
                    LayerTips("简介不能超过100个字符！", "txtSummary");
                    return;
                }
                if (entity.AttachmentID == "") {
                    LayerTips("请先上传文件!", "DivAttTip");
                    return;
                }
                if (entity.PhotoUrl == "") {
                    var ConfirmIndex = LayerConfirm("视频截图为空，是否继续？", function () {
                        var postData = { CMD: "AddResource", Entity: JSON.stringify(entity), AttachmentModel: $("#hfAttachmentModel").val() };
                        AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                            LayerAlert("保存成功!", function () {
                                window.history.back();
                            })
                        });
                    }, function () {
                        layer.close(ConfirmIndex);
                    });
                }
                else {
                    var postData = { CMD: "AddResource", Entity: JSON.stringify(entity), AttachmentModel: $("#hfAttachmentModel").val() };
                    AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                        LayerAlert("保存成功!", function () {
                            window.history.back();
                        })
                    });
                }
            }
            function Edit() {
                var entity = {
                    ID: editId,
                    Name: $("#txtName").val(),
                    ClassificationID: $("#hfClassificationID").val(),
                    AttachmentID: $("#hfAttachmentID").val(),
                    PhotoUrl: $("#hfSPImgUrl").val(),
                    Duration: $("#txtDuration").val(),
                    Format: $("#txtFormat").val(),
                    PersonLiable: $("#txtPersonLiable").val(),
                    SpeechMaker: $("#txtSpeechMaker").val(),
                    Summary: $("#txtSummary").val(),
                    ScreenTime: $("#txtScreenTime").val()
                };
                if (entity.ClassificationID == "") {
                    LayerTips("资源分类不能为空!", "DivCFTip");
                    return;
                }
                if (entity.Name == "") {
                    LayerTips("资源名称不能为空!", "txtName");
                    return;
                }
                if (entity.Name.length > 50) {
                    LayerTips("资源名称不能超过50个字符！", "txtName");
                    return;
                }
                if (entity.Duration.length > 25) {
                    LayerTips("时长不能超过25个字符！", "txtDuration");
                    return;
                }
                if (entity.Format.length > 25) {
                    LayerTips("格式不能超过25个字符！", "txtFormat");
                    return;
                }
                if (entity.Summary.length > 100) {
                    LayerTips("简介不能超过100个字符！", "txtSummary");
                    return;
                }
                if (entity.AttachmentID == "") {
                    LayerTips("请上传文件!", "DivAttTip");
                    return;
                }
                var oldEntity = $("#hfOldEntity").val();
                var postData = { CMD: "EditResource", Entity: JSON.stringify(entity), IsNewAttach: isNewAttach, OldEntity: oldEntity };
                //if (isNewAttach)
                postData.AttachmentModel = $("#hfAttachmentModel").val();

                AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                    LayerAlert("修改成功!", function () {
                        window.history.back();
                    });
                });
            }
            function Save() {
                if (editId == "") {
                    Add();
                } else {
                    Edit();
                }
            }
            function LoadEditInfo() {
                if (editId == "")
                    return;
                var postData = { CMD: "GetModel", EditID: editId };
                AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                    $("#hfAttachmentModel").val(returnVal.AttachmentInfoModel);//附件信息保存到隐藏域
                    $("#hfOldEntity").val(returnVal.Model);//附件信息保存到隐藏域

                    var model = JSON.parse(returnVal.Model);//资源信息

                    var attachmentModel = JSON.parse(returnVal.AttachmentInfoModel);
                    $("#uploadState").text(attachmentModel.FileName);

                    $("#txtName").val(model.Name);
                    oldCFId = model.ClassificationID;
                    $("#hfClassificationID").val(model.ClassificationID);
                    $("#hfAttachmentID").val(model.AttachmentID);
                    $("#hfSPImgUrl").val(model.PhotoUrl);
                    $("#txtDuration").val(model.Duration);
                    $("#txtFormat").val(model.Format);
                    $("#txtPersonLiable").val(model.PersonLiable);
                    $("#txtSpeechMaker").val(model.SpeechMaker);
                    $("#txtSummary").val(model.Summary);
                    $("#txtScreenTime").val(model.ScreenTime);
                    $("#txtCfName").combotree("setValue", model.ClassificationID);
                    if (model.PhotoUrl == "")
                        $("#videoImg").attr("src", "/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png");
                    else
                        $("#videoImg").attr("src", model.PhotoUrl);
                });
            }
            $(function () {
                LoadEditInfo();
                $("#DelResource").click(function () {
                    if ($("#hfAttachmentID").val() == "")
                        return;
                    uploadSuccess = false;
                    $("#uploadState").text("未上传");
                    $("#hfFilePath").val("");
                    $("#hfFileName").val("");
                    $("#hfSPImgUrl").val("");
                    $("#hfAttachmentID").val("");
                    $("#hfAttachmentModel").val("");
                    $("#videoImg").attr("src", "/_layouts/15/YHSD.VocationalEducation.Portal/images/NoImage.png");
                    //var postData = { CMD: "DelAttachment", DelID: $("#hfAttachmentID").val() };
                    //var layerIndex = LayerConfirm("确定删除此视频吗?", function () {
                    //    LayerAlert("删除成功!", function () {
                    //    });
                    //    //AjaxRequest("Handler/UploadResourceHandler.aspx", postData, function (returnVal) {
                    //    //});
                    //});
                })
                $('#txtCfName').combotree({
                    onSelect: function (node) {
                        $("#hfClassificationID").val(node.id);
                        isNewClassification = true;
                    }
                });
                $("#uploadify").uploadify({
                    'fileSizeLimit': '200MB',
                    'swf': 'js/uploadify/uploadify.swf',
                    'uploader': 'Handler/UploadResourceHandler.aspx',
                    'fileTypeDesc': '请选择视频文件',
                    'fileTypeExts': '*.mp4;*.flv',
                    'auto': false,
                    'multi': false,
                    'queueSizeLimit': 1,
                    'buttonText': '视频选择',
                    'onUploadSuccess': function (file, data, response) {
                        if (data != null && data != "") {
                            data = JSON.parse(data);
                            if (data.Success == true) {
                                uploadSuccess = true;
                                isNewAttach = true;
                                $('#' + file.id).find('.data').html('&nbsp-&nbsp上传成功,即将自动截图...');

                                $("#uploadState").text(file.name);
                                $("#hfFilePath").val(data.Path);
                                $("#hfFileName").val(data.FileName);
                                $("#hfAttachmentID").val(data.AttachmentID);
                                $("#hfAttachmentModel").val(data.AttachmentModel);
                                setTimeout("GetImg()", 1500);
                            }
                            if (data.Success == false) {//未捕获的错误及数据异常
                                uploadSuccess = false;
                                console.error(data.Msg);
                                console.error(data.StackTrace);
                                LayerAlert("上传失败:" + data.Msg);
                            }
                            else if (data.Business == false) {//业务逻辑出错
                                uploadSuccess = false;
                                LayerAlert(data.Msg);
                            }
                        }
                        else {
                            LayerAlert("上传失败，服务器未返回任何消息！");
                        }
                    },
                    'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                        if (bytesUploaded == bytesTotal) {
                            $('#' + file.id).find('.data').html('&nbsp-&nbsp正在处理,请稍候...');
                        }
                    },
                    'onUploadStart': function (file) {
                        $("#uploadify").uploadify("settings", "formData", { CMD: "Upload", ClassificationID: $("#hfClassificationID").val() });
                    },
                    'onSelectError': uploadify_onSelectError,
                    'onUploadError': uploadify_onUploadError
                });
            });
        </script>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    资源信息
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    资源信息
</asp:Content>
