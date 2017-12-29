var PositionStudent = "Student";
var PositionTeacher = "Teacher";
var PositionAdministrator = "administrator";
var Enum_QuestionSingle = "单选题";
var Enum_QuestionMulti = "多选题";
var Enum_QuestionJudge = "判断题";
var Enum_QuestionEssay = "简答题";

function GetSex(val) {
    if (val == '0') {
        return '女';
    }
    else if (val == '1') {
        return '男';
    }
}

function GetTrNum(k, r) {
    return k + 1 + ((r - 1) * 10);
}
function GetTrClass(val) {
    return val % 2 == 0 ? "tab_td" : "tab_td td_bg";
}
//数字(从0开始)转换为字母
function IntegerToLetter(n) {
    return String.fromCharCode(65 + parseInt(n));
}
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}
if (!Array.indexOf) {
    Array.prototype.indexOf = function (obj) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == obj) {
                return i;
            }
        }
        return -1;
    }
}
//根据saveLength截取传入的字符串 并且加上“...”
function SubText(text, saveLength) {
    if (text.length > saveLength) {
        return text.substring(0, saveLength) + "...";
    }
    return text;
}
//传入数组 ，查找objPropery(属性名)等于objValue(值)的对象
function ArrayFind(arrPerson, objPropery, objValue) {
    var arr = $.grep(arrPerson, function (cur, i) {
        return cur[objPropery] == objValue;
    });
    if (arr && arr.length > 0)
        return arr[0];
    return undefined;
}

function ArrayRemove(arrPerson, objPropery, objValue) {
    return $.grep(arrPerson, function (cur, i) {
        return cur[objPropery] != objValue;
    });
}

//错误时，返回错误字符串
//正确时，返回false
function ErrorJudge(returnVal) {
    if (returnVal.Success != null && returnVal.Success == false)
        return returnVal.Msg;
    return false;
}
//判断对象是否为Function
function IsFunction(obj) {
    if (obj && typeof obj == "function")
        return true;
    return false;
}
//获取对象属性个数
function PropertyCount(obj) {
    var t = typeof obj;
    if (t == 'string') {
        return obj.length;
    }
    else if (t == 'object') {
        var n = 0;
        for (var i in obj) {
            n++;
        }
        return n;
    }
    return false;
}

//发送Ajax请求，依赖于Jquery、Layer、OpenLayer、JSON
function AjaxRequest(url, postData, successCallback, errorCallback) {
    var loadIndex = layer.load(2);
    $.ajax({
        type: "Post",
        url: url,
        data: postData,
        dataType: "text",
        success: function (returnVal) {
            try {
                layer.close(loadIndex);
                returnVal = $.parseJSON(returnVal);
                if (returnVal.Success == false) {//未捕获的错误及数据异常
                    console.error(returnVal.Msg);
                    console.error(returnVal.StackTrace);
                    //LayerAlert("操作失败!");
                    IsFunction(errorCallback) ? errorCallback(returnVal.Msg) : LayerAlert(returnVal.Msg);
                    //LayerAlert(returnVal.Msg);
                    //if (IsFunction(errorCallback)) {
                    //    errorCallback(returnVal.Msg);
                    //}
                }
                else if (returnVal.Business == false) {//业务逻辑出错
                    if (IsFunction(errorCallback)) {
                        errorCallback(returnVal.Msg);
                    }
                    else {
                        LayerAlert(returnVal.Msg);
                    }
                }
                else if (IsFunction(successCallback)) {//成功返回正常数据
                    if (returnVal.SuccessMsg)//如果返回成功消息
                        LayerAlert(returnVal.SuccessMsg);
                    else
                        successCallback(returnVal);
                }
            } catch (e) {
                //LayerAlert('操作失败!!');
               // LayerAlert(e.stack);
                layer.close(loadIndex);
                console.error(e.stack);
            }
        },
        error: function (errMsg) {
            layer.close(loadIndex);
            LayerAlert('向服务器发送请求失败！');
            console.error(errMsg.statusText);
        }
    });
}

//keyPress事件中按下回车时触发事件
//obj：按回车键时的回调函数或者按钮的ID
function EnterEvent(obj) {
    var keyNum = (arguments.callee.caller.arguments[0] || window.event).keyCode;// 兼容IE/Chrome/Firefox
    if (keyNum == 13) {
        switch (typeof (obj)) {
            case 'function':
                obj();
                break;
            case 'string':
                document.getElementById(obj).click();
                break;
            default:
                console.error('传入参数类型("' + typeof (obj) + '")不正确，调用EnterEvent失败!');
                break;
        }
        window.event.returnValue = false;
        if (window.event && window.event.preventDefault)
            window.event.preventDefault();
    }
}

//图片加载失败时显示指定图片,默认尝试3次
//ReSetImgUrl(this,'images/NoImage.png')
function ReSetImgUrl(imgObj, ErrorSrc, maxErrorNum) {
    if (!maxErrorNum)
        maxErrorNum = 3;
    if (maxErrorNum > 0) {
        imgObj.onerror = function () {
            ReSetImgUrl(imgObj, ErrorSrc, maxErrorNum - 1);
        };
        setTimeout(function () {
            imgObj.src = ErrorSrc;
        }, 500);
    } else {
        imgObj.onerror = null;
        imgObj.src = ErrorSrc;
    }
}