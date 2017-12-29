function openDialog(url, width, height, callback) {
    var options = SP.UI.$create_DialogOptions();
    options.width = width;
    options.height = height;
    options.url = url;
    options.dialogReturnValueCallback = Function.createDelegate(this, callback);
    SP.UI.ModalDialog.showModalDialog(options);

    return true;
}

/*关闭模态窗口调用方法*/
/**
*   @result: 结果值
*   @status: 返回状态
*/
function closeCallback(result, status) {
    if (status == "success") {
        alert("保存成功!");
        window.location.reload();
    }
}