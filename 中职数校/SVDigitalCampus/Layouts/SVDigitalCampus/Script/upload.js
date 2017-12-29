//附件的路径  
var adjunctPath = '';

//附件上传失败则不能保存  
var canSubmit = true;

var uploadify = $("#uploadify");

var uploader = "";

function getRootPath() {
    var pathName = window.document.location.pathname;
    //获取带"/"的项目名，如：/uimcardprj    
    var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
    return (projectName);
}

function prepareUpload(uploadFold, size) {
    uploader = $("#uploadify").uploadify({
        'auto': false,
        'swf': getRootPath() + '/resources/js/upload/uploadify.swf',
        //这个的作用是为了上传  
        'uploader': getRootPath() + '/book/upload',
        'formData': { "fold": uploadFold },
        'queueID': 'fileQueue',//与下面的id对应    
        'queueSizeLimit': size,
        'fileTypeDesc': '*.png;*.gif;*.jpg;*.bmp;*.jpeg;',
        'fileTypeExts': '*.png;*.gif;*.jpg;*.bmp;*.jpeg;', //控制可上传文件的扩展名，启用本项时需同时声明fileDesc    
        'multi': true,
        'buttonText': '添加图片',
        'cancel': getRootPath() + '/resources/js/upload/uploadify-cancel.png',
        'onUploadSuccess': function (file, data, response) {//上传成功后获得回调函数,这里为了取得文件上传成功后的保存路径  
            //          adjunctPath+=data+";";  
            adjunctPath += data;
            //这里就调用了onUploadSuccess()，在另一js中有定义  
            onUploadSuccess();
        },
        'onUploadError': function (file, errorCode, errorMsg, errorString) {//上传错误的信息  
            canSubmit = false;
            onUploadError();
        },
        'onQueueComplete': function (queueData) {//上传队列全部完成后执行的回调函数  
            onQueueComplete();
        },
        'onSelect': function (file) {
            onSelect(file);
        }
    });
}
