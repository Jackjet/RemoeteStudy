
var fileNum = 0;
var uploadName = "fileupload";
var arr1 = new Array();
var num = 0;
var name = $("[id$='Hidden1']").val() == null ? "" : $("[id$='Hidden1']").val();
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
        var arryfile = file.value.split(",");
        fileNum++;

        for (var i = 0; i < arryfile.length; i++) {
            num++;
            var ary = arryfile[i].split("\\");
            var filename = ary[ary.length - 1];
            arr1[num] = arryfile[i];


            // 要增行的TABLE
            t = document.getElementById("idAttachmentsTable");

            objRow = t.insertRow(); // 在TABLE中增加ROW,也即<TR>
            objRow.id = "row" + num;

            objCell = objRow.insertCell(0);  // 在行中增加单元格 也即<TD>：文件名 
            objCell.setAttribute("id", "fliename");
            objCell.innerText = filename;
            name += filename + ",";
            //objCell = objRow.insertCell(1);  // 在行中增加单元格 也即<TD>：文件名 
            //objCell.setAttribute("id", "fliesize");
            //objCell.innerText = arryfile[i].Size;// "100KB";

            objCell = objRow.insertCell(1);  // 在行中增加单元格 也即<TD>：删除
            objCell.setAttribute("id", "del");
            objCell.innerHTML = "<IMG SRC='/_layouts/images/rect.gif'><a href='javascript:RemoveLocal(" + fileNum + "," + num + ",\"" + filename + "\");'>删除</a>";
            objCell = objRow.insertCell(2);  // 在行中增加单元格 也即<TD>：文件名 
        }
        file.style.display = "none";

        var f = document.createElement("input");   //new 控件
        f.type = "file";
        f.name = "fileupload" + fileNum;
        f.id = "fileupload" + fileNum;
        f.className = "hid";
        f.multiple = "multiple";
        f.onchange = function () {
            AddFile();
        }
        oAttachments = document.getElementById("att1");
        oAttachments.appendChild(f);
        var BtFile = document.getElementById("BtFile");
        var file = document.getElementById("fileupload" + fileNum);
        //BtFile.click = null;
        BtFile.onclick = function () {
            file.click();
        }
    }
    $("[id$='Hidden1']").val(name);
}

//删除
function RemoveLocal(fileN, num, filename) {
    var i = fileN - 1;
    var j = num - 1;
    var remname = "fileupload" + i;
    document.getElementById("idAttachmentsTable").deleteRow(document.getElementById("row" + num).rowIndex);
    //var nodeForRemoval = GetAttachElement(remname);
    //nodeForRemoval.removeChild();
    //nodeForRemoval.parentNode.removeChild(nodeForRemoval);
    arr1[j] = "";
    var FName = $("[id$='Hidden1']").val();
    FName = FName.replace(filename + ",", ",");
    $("[id$='Hidden1']").val(FName);
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
