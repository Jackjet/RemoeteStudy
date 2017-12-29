var fileArr = new Array();
var fileString;
var fileShowTb;
var fileUpTd;
//上传
function UploadFile(fileStr, attachmentsTb, attachmentsTd) {
    fileString = fileStr;
    fileShowTb = attachmentsTb;
    fileUpTd = attachmentsTd;

    var fileNum = fileArr.length;
    var file = GetAttachElement(fileString + fileNum);
    if (file.value == "") {
        alert("请选择文件!");
        return;
    }
    if (CheckExists(file.value)) {
        alert("重复上传！请重新选择");
    }
    else {       
        var ary = file.value.split("\\");
        var filename = ary[ary.length - 1];
        var geshi = filename.substring(filename.length - 3).toLowerCase();
        if ((fileShowTb == "idAttachmentsTable2" || fileShowTb == "idAttachmentsTable3") && geshi != "jpg" && geshi != "png") {
            alert("请上传图片格式文件");
        }
        else {
            fileArr[fileNum] = file.value;//将上传文件赋给数组 
            var tb = document.getElementById(attachmentsTb); //TABLE
            var row = tb.insertRow(); // 在TABLE中增加ROW,也即<TR>
            row.id = "row" + fileNum;

            objCell = row.insertCell(0);  // 在行中增加单元格 也即<TD>：文件名 
            objCell.innerText = filename;
            objCell = row.insertCell(1);  // 在行中增加单元格 也即<TD>：删除
            objCell.innerHTML = "<img src='/_layouts/images/rect.gif'><a href='javascript:RemoveAt(" + fileNum + ");'>删除</a>";

            file.style.display = "none"; //隐藏当前
            var f = document.createElement("input"); //创建新的文件控件
            ++fileNum;
            f.type = "file";
            f.name = fileStr + fileNum;
            f.id = fileStr + fileNum;
            var oAttachments = document.getElementById(attachmentsTd);
            oAttachments.appendChild(f);
        }
    }
}
//删除
function RemoveAt(fileNum) {
    document.getElementById(fileShowTb).deleteRow(document.getElementById("row" + fileNum).rowIndex);
    var remname = fileString + fileNum;
    var nodeForRemoval = GetAttachElement(remname);
    document.getElementById(fileUpTd).removeChild(nodeForRemoval);
    fileArr[fileNum] = "";
    //alert(remname);
}

function GetAttachElement(elem) {
    var ret = document.getElementById(elem);
    if (ret == null)
        ret = document.getElementsByName(elem).item(0);
    return ret;
}

function CheckExists(value) {
    if (fileArr != null) {
        for (var i = 0; i < fileArr.length; i++) {
            if (fileArr[i] == value) {
                return true;
            }
        }
        return false;
    }
    else
        return false;
}