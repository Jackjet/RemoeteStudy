var ol_CB;
function OL_IframeCallBack(obj) {
    layer.closeAll();
    ol_CB(obj);
}

//显示一个Iframe(在调用此方法前请先引入jquery1.8、layer1.8.5或以上版本)
function ShowLayerIframe(title, width, height, url, callBack) {
    ol_CB = callBack;
    layer.open({
        type: 2,
        title: title,
        shift: 5,
        area: [width + 'px', height + 'px'],
        content: url //iframe的url
    });
}
//关闭当前Iframe
function OL_CloseLayerIframe(returnVal) {
    if ("OL_IframeCallBack" in window.parent) {//判断父页面是否有IframeCallBack这个方法
        window.parent.OL_IframeCallBack(returnVal);
    }
}
function OL_CloserLayerAlert(value)
{
    var index = parent.layer.getFrameIndex(window.name);
    parent.LayerAlert(value, function () {
        parent.location.href = parent.location.href;
        parent.layer.close(index);
    });
}
//弹出窗并返回索引
//ol_type:0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
//ol_content:可以是一个dom对象，也可以是一个url
//ol_callBack:回调函数，iframe关闭时或页面层关闭时调用
function OL_ShowLayer(ol_type, ol_title, ol_width, ol_height, ol_content, ol_callBack) {
    if (ol_callBack)
        ol_CB = ol_callBack;
    var index = layer.open({
        type: ol_type,
        skin: 'layui-layer-molv',
        title: ol_title,
        shift: 5,
        area: [ol_width + 'px', ol_height + 'px'],
        content:ol_content,
        end: function () {
            if (ol_type == 1 && ol_callBack) {
                ol_callBack();
            }
        }
    });
    return index;
}
//弹出没有遮罩的层并返回索引
//ol_type:0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
//ol_content:可以是一个dom对象，也可以是一个url
//ol_callBack:回调函数，iframe关闭时或页面层关闭时调用
function OL_ShowNoShadeLayer(ol_type, ol_title, ol_width, ol_height, ol_content, ol_callBack) {
    if (ol_callBack)
        ol_CB = ol_callBack;
    var index = layer.open({
        type: ol_type,
        skin: 'layui-layer-molv',
        title: ol_title,
        shade: [0.01, '#ddd'],
        shift: 5,
        area: [ol_width + 'px', ol_height + 'px'],
        content: ol_content,
        end: function () {
            if (ol_type == 1 && ol_callBack) {
                ol_callBack();
            }
        }
    });
    return index;
}
function OL_ShowLayerNo(ol_type, ol_title, ol_width, ol_height, ol_content, ol_callBack) {
    if (ol_callBack)
        ol_CB = ol_callBack;
    var index = layer.open({
        type: ol_type,
        skin: 'layui-layer-molv',
        title: ol_title,
        shift: 5,
        area: [ol_width + 'px', ol_height + 'px'],
        content: [ol_content, 'no'],
        end: function () {
            if (ol_type == 1 && ol_callBack) {
                ol_callBack();
            }
        }
    });
    return index;
}
function OL_ShowLayerClose(ol_type, ol_title, ol_width, ol_height, ol_content,ol_Close, ol_callBack) {
    if (ol_callBack)
        ol_CB = ol_callBack;
    var index = layer.open({
        type: ol_type,
        skin: 'layui-layer-molv',
        title: ol_title,
        shift: 5,
        area: [ol_width + 'px', ol_height + 'px'],
        closeBtn: ol_Close,
        content: [ol_content,'no'],
        end: function () {
            if (ol_type == 1 && ol_callBack) {
                ol_callBack();
            }
        }
    });
    return index;
}

//根据索引关闭弹出窗,如果没有传入索引则关闭当前页面弹出的所有层
function OL_CloseLayer(index) {
    if (index) {
        layer.close(index);
    }
    else {
        layer.closeAll();
    }
}

//关闭指定类型的弹出层，默认关闭所有
//dialog 关闭信息框，page 关闭所有页面层，iframe 关闭所有的iframe层，loading 关闭加载层，tips 关闭所有的tips层
function OL_CloseLayerByType(type) {
    if (!type) {
        layer.closeAll();
    }
    else {
        layer.closeAll(type);
    }
}
//普通溅出提示框
function LayerAlert(content, callBack) {
    layer.alert(content, {
        skin: 'layui-layer-molv',
        shift: 5 //动画类型
    }, function (index) {
        layer.close(index);
        if (callBack) {
            callBack();
        }
    });
}

function LayerLoad() {
    return layer.load(2, { skin: 'layui-layer-molv' });
}
function LayerTips(content, ClientID) {
    layer.tips(content, "#" + ClientID, { tips: [1, '#49B700'] });
}
//删除警告提示框
function LayerAlertDelete(content) {
    layer.alert(content, {
        skin: 'layui-layer-molv',
        shift: 6 //动画类型
    });
}
function LayerConfirm(content, EnterBack,CancelBack) {
    var index=layer.confirm(content, { skin: 'layui-layer-molv', shift: 5 }, function () {
        EnterBack();
    }, function () {
        if (CancelBack) {
            CancelBack();
        }
    });
    return index;
}
function LayerConfirmDelete(content,EnterBack, callBack) {
    layer.confirm(content, { skin: 'layui-layer-molv', shift: 6 }, function () {
        EnterBack();
    }, function () {
        if (callBack) {
            callBack();
        }
    });
}
var uploadify_onSelectError = function (file, errorCode, errorMsg) {
    var msgText = "上传失败!!\n";
    switch (errorCode) {
        case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
            //this.queueData.errorMsg = "每次最多上传 " + this.settings.queueSizeLimit + "个文件";  
            msgText += "每次最多选择 " + this.settings.queueSizeLimit + "个文件";
            break;
        case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
            msgText += "文件大小超过限制( " + this.settings.fileSizeLimit + " )";
            break;
        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
            msgText += "文件大小为0";
            break;
        case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
            msgText += "文件格式不正确，仅限 " + this.settings.fileTypeExts;
            break;
        default:
            msgText += "错误代码：" + errorCode + "\n" + errorMsg;
    }
    layer.msg(msgText);
};

var uploadify_onUploadError = function (file, errorCode, errorMsg, errorString) {
    // 手工取消不弹出提示  
    if (errorCode == SWFUpload.UPLOAD_ERROR.FILE_CANCELLED
            || errorCode == SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED) {
        return;
    }
    var msgText = "上传失败\n";
    switch (errorCode) {
        case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
            msgText += "HTTP 错误\n" + errorMsg;
            break;
        case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
            msgText += "上传文件丢失，请重新上传";
            break;
        case SWFUpload.UPLOAD_ERROR.IO_ERROR:
            msgText += "IO错误";
            break;
        case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
            msgText += "安全性错误\n" + errorMsg;
            break;
        case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
            msgText += "每次最多上传 " + this.settings.uploadLimit + "个";
            break;
        case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
            msgText += errorMsg;
            break;
        case SWFUpload.UPLOAD_ERROR.SPECIFIED_FILE_ID_NOT_FOUND:
            msgText += "找不到指定文件，请重新操作";
            break;
        case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
            msgText += "参数错误";
            break;
        default:
            msgText += "文件:" + file.name + "\n错误码:" + errorCode + "\n"
                     + errorMsg + "\n" + errorString;
    }
    layer.msg(msgText);
}