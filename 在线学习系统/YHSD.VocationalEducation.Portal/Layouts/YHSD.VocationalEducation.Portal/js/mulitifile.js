
/*-------------------------------------------------------------多附件字段开始--------------------------------------------------------*/

/* 删除附件*/
function deleteAttachment(elemThis, filename,delInputId) {
    if (confirm("是否确实要删除此附件?")) {
        $(elemThis).parent().parent().remove();

        var delValue = $("#" + delInputId).val();
        if (delValue == "")
            delValue = filename;
        else
            delValue = delValue + "*" + filename;

        $("#" + delInputId).val(delValue);
    }
}

 

function addFile(elemThis, fileName) {
    var tdFile = $(elemThis).parent().parent();

    var divFile = $("<div style=\"width:100%;\"></div>");
    divFile.appendTo(tdFile);

    var file = $("<input type=\"file\"   name=\"" + fileName + "\" class=\"inputfile\" size=\"45\"/>");
    $(file).appendTo(divFile);

    var a = document.createElement("a");
    a.setAttribute("href", "javascript:");
    a.appendChild(document.createTextNode("删除"));
    a.className = 'contract-btn';
    a.onclick = function () { deleteFileTemp(this) };

    $(a).appendTo(divFile);
}

function deleteFileTemp(img) {
    $(img).parent().remove();
}


function downloadAttachment(sourceUrl, fileName) {
    fileName = escape(fileName);
    sourceUrl = escape(sourceUrl);
    window.open("/_layouts/15/YHSD.Grandtopeak.Portal/FileDownload.aspx?filePath=" + sourceUrl + "&fileName=" + fileName);
}


/*-------------------------------------------------------------多附件字段结束--------------------------------------------------------*/