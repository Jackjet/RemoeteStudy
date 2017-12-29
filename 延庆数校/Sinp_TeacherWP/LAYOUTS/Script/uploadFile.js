
var fileNum = 0;
var uploadName = "fileupload";
var arr1 = new Array();
function AddFile() {
    file = GetAttachElement(uploadName + fileNum);
    if (file.value == "") {
        alert("请选择文件!");
        return;
    }
    if (CheckExists(file.value)) {
        alert("重复上传！请重新选择");
    }
    else {
        arr1[fileNum] = file.value;
        var ary = file.value.split("\\");
        var filename = ary[ary.length - 1];

        // 要增行的TABLE
        fileNum++;
        t = document.getElementById("idAttachmentsTable");

        objRow = t.insertRow(); // 在TABLE中增加ROW,也即<TR>
        objRow.id = "row" + fileNum;

        objCell = objRow.insertCell(0);  // 在行中增加单元格 也即<TD>：文件名 
        objCell.setAttribute("id", "fliename");
        objCell.innerText = filename;

        objCell = objRow.insertCell(1);  // 在行中增加单元格 也即<TD>：删除
        objCell.setAttribute("id", "del");
        objCell.innerHTML = "<IMG SRC='/_layouts/images/rect.gif'><a href='javascript:RemoveLocal(" + fileNum + ");'>删除</a>";

        file.style.display = "none";

        var f = document.createElement("input");   //new 控件
        f.type = "file";
        f.name = "fileupload" + fileNum;
        f.id = "fileupload" + fileNum;
        oAttachments = document.getElementById("att1");
        oAttachments.appendChild(f);
    }
}

//删除
function RemoveLocal(fileN) {
    var i = fileN - 1;
    var remname = "fileupload" + i;
    document.getElementById("idAttachmentsTable").deleteRow(document.getElementById("row" + fileN).rowIndex);
    var nodeForRemoval = GetAttachElement(remname);
    nodeForRemoval.parentNode.removeChild(nodeForRemoval);
    arr1[i] = "";
}

function GetAttachElement(elem) {
    var ret = document.getElementById(elem);
    if (ret == null)
        ret = document.getElementsByName(elem).item(0);
    return ret;
}

//检查选项是否存在.
function CheckExists(value) {
    if (arr1 != null) {
        for (var i = 0; i < arr1.length; i++) {
            if (arr1[i] == value) {
                return true;
            }
        }
        return false;
    }
    else
        return false;
}

//弹出页面相关Js
function editpdetail(event, pageTitle, pageName, pageWidth, pageHeight) {
    var gid = $(event).parent().find("[id$=DetailID]").val();
    openPage(pageTitle, pageName + gid, pageWidth, pageHeight);
}
var webUrl = _spPageContextInfo.webAbsoluteUrl;
function openPage(pageTitle, pageName, pageWidth, pageHeight) {

    //var options = {
    //    title: pageTitle,
    //    width: parseInt(pageWidth),
    //    height: parseInt(pageHeight),
    //    showClose: true,
    //    url: webUrl + pageName,
    //    dialogReturnValueCallback: setmessages
    //};
    //SP.UI.ModalDialog.showModalDialog(options);
    popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
}
function setmessages(dialogResult, returnvalue) {
    if (dialogResult == '1') {
        window.location.reload();
    }
}
function closetaskform(flag,para) {
    window.parent.SP.UI.ModalDialog.commonModalDialogClose(flag, para);
}

