var pIndex = 1;
var pCount = 0;
var pSize = 10;
function BindData(data) {
    $("#tab tbody").empty();
    $($.parseJSON(data)).each(function () {
        var operater = "";
        if ($("#HStatus").val() == "0") {
            operater = "<a href=\"#\" class=\"shenhe\" onclick=\"Check('" + this.ID
                + "','1')\">通过</a><a href=\"#\" class=\"shenhe\" onclick=\"showshenhe('" + this.ID + "')\";>拒绝</a>";
        }

        var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID +
            "' class='Check_box' name='Check_box' id='subcheck'/></td><td class='name'><i class='file' style='float: left;cursor:pointer' onclick=\"folder('"
            + this.Name + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span style='cursor:pointer' onclick=\"folder('"
            + this.Name + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
            "</span><div class=\"Operation\" style=\"display:none;\">" +
            "<span class=\"Download\" ><a href=\"#\" onclick=\"Down('" + this.ID + "')\"><i class=\"iconfont\">&#xe60a;</i></a></span><span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe61a;</i></a>" +
            "<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"Move('" + this.ID + "','move')\">移动到</a></li><li><a href=\"#\"  onclick=\"Move('" + this.ID +
            "','copy')\">复制到</a></li><li><a href=\"#\" onclick=\"cl(this,'" + this.ID + "','" + this.Title + "')\">重命名</a></li><li><a href=\"#\" onclick=\"Del('" + this.ID + "')\">删除</a></li>"
            + "</ul></span></div>"
            + "</td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td><td style=' padding-left:40px;'>" + operater + "</td></tr>";
        $("#tab tbody").append(tr);
    });
    $(".J-item").hover(function () {
        $(this).find(".Operation").show().end().siblings().find('.M_con').hide();
    }, function () {
        $(this).find(".Operation").hide();
    });
    //分享下载
    $('.more_m').click(function () {
        $(this).siblings('.M_con').show();
    })
}
function ShowData(index) {
    var postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index, Name: $("#txtName").val(), TypeSel: $("#Hidden2").val(), TimeSel: $("#Hidden3").val(), FoldUrl: $("[id$='HFoldUrl']").val(), SeleStatu: $("#HStatus").val() };
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: postData,
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            SetPageCount(Math.ceil(returnVal.PageCount / pSize));
            BindData(returnVal.Data);
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
}
$(function () {
    Bind();
    GetFileType();
    IsAdmin();
    $("#checkAll").click(function () {
        var flag = $(this).attr("checked") == undefined ? false : true;
        $("input[name='Check_box']:checkbox").each(function () {
            $(this).attr("checked", flag);
        })
    })
    $(document).bind("keydown", function (e) {
        e = window.event || e;
        if (e.keyCode == 13) {
            Bind();
        }
    });
});
//子复选框的事件  
function setSelectAll() {
    //当没有选中某个子复选框时，SelectAll取消选中  
    if (!$("#subcheck").checked) {
        $("#checkAll").attr("checked", false);
    }
    var chsub = $("input[type='checkbox'][id='subcheck']").length; //获取subcheck的个数  
    var checkedsub = $("input[type='checkbox'][id='subcheck']:checked").length; //获取选中的subcheck的个数  
    if (checkedsub > 0) {
        $("#checkAll").attr("checked", true);
    }
}

function Bind() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: { CMD: "FullTab", PageSize: pSize, PageIndex: pIndex },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            // BindData(returnVal.Data);
            pCount = returnVal.PageCount;
            LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
    $("#btnQuery").click(function () {
        ShowData(1);
    });
}
function showshenhe(id) {
    showDiv('Messagediv', 'Message_head');
    $("#hdS").val(id);
}

function Check(id, status) {
    var Message = "";
    if (status == "2") {
        id = $("#hdS").val();
        Message = $("#Message").val()
    }
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: { CMD: "Check", "CheckID": id, "Status": status, Message: Message },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert('操作成功！');
                $(".blk").hide();
                $("#mybg").hide();
                Bind();
            }
        },
        error: function (errMsg) {
            alert('操作失败！');
        }
    });
}
function IsAdmin() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: { CMD: "IsAdmin" },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                document.getElementById("daishenhe").style.display = "block";
            }
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
}
//绑定文件类型
function GetFileType() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    $("#F_type").empty();
    $("#F_type").append("<dt>文件类型：</dt><dd class=\"select\"><a href=\"#\" onclick=\"serchType('',this)\">不限</a></dd>");

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: { CMD: "FileType" },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            $(returnVal).each(function () {
                var result = "<dd><a href='#' onclick=\"serchType('" + this.Attr + "',this)\">" + this.Title + "</a></dd>";
                $("#F_type").append(result);
            });
            $("#F_type").append("<dd><i class=\"iconfont\" style=\"cursor:pointer;\" onclick=\"EditType()\")>&#xe631;</i></dd>");
        },
        error: function (errMsg) {
        }
    });
}
function EditType() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    $.webox({
        width: 620, height: 500, bgvisibel: true, title: '修改文件类型', iframe: FirstUrl + "/SitePages/DriveType.aspx?" + Math.random
    });
    //popWin.showWin("620", "500", "修改文件类型", FirstUrl + "/SitePages/DriveType.aspx", "auto");
}
//新增文件夹
function AddFold(em) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var FileName = $("#txt" + em + "").val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: { CMD: "AddFolder", "FileName": FileName, FoldUrl: $("[id$='HFoldUrl']").val() },
        dataType: "text",
        success: function (returnVal) {
            Bind();
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
}
//追加一行（新建文件夹）

function appendAfterRow(tableID, RowIndex) {
    var txt1 = document.getElementById("txt1");
    if (txt1 != undefined) {
        txt1.onfocus();
    }
    else {
        //FUNCTION: 向指定行后面增加一行,列数和第一行的列数一样
        var o = document.getElementById(tableID);
        var refRow = RowIndex;
        // var cells = o.rows[0].cells.length;
        if (refRow == "") refRow = getO("nRow").value;
        var v = "<img src='/_layouts/15/images/folder.gif?rev=23' style='float:left; margin-top:4px;'>" +
            "<input type='text' value='新建文件夹' style='float:left;line-height:10px;margin-top:5px' id=\"txt" + RowIndex +
            "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; float: left;cursor:pointer;\" onclick=\"AddFold('" + RowIndex
            + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; float: left;cursor:pointer;\" onclick=\"DelRow()\">&#xe61e;</i>";
        var newRefRow = o.insertRow(refRow);
        //for (var i = 0; i < cells; i++) {
        newRefRow.insertCell(0).innerHTML = "";
        //if (i == RowIndex) {

        newRefRow.insertCell(1).innerHTML = v;
        //}
        newRefRow.insertCell(2).innerHTML = "--";
        var now = new Date();
        var nowStr = now.Format("yyyy-MM-dd");
        newRefRow.insertCell(3).innerHTML = nowStr;
        // else { newRefRow.insertCell(i).innerHTML = ""; }
        // }
    }
}
function getO(id) {
    if (typeof (id) == "string")
        return document.getElementById(id);
}
//删除一行（table）
function DelRow() {
    $('table tr:eq(1)').remove();
}
//文件类型查询点击
function serchType(value, em) {
    $(em).parent().addClass("select").siblings().removeClass("select");
    $("#Hidden2").val(value);
    ShowData(1);

}
//时间查询条件点击事件
function serchTime(value, em) {
    $(em).parent().addClass("select").siblings().removeClass("select");

    $("#Hidden3").val(value);
    ShowData(1);
}
//新建文件夹
function folder(name, url, type, id) {
    $(".navgition").html("全部文件");

    if (type == "文件夹") {

        var html = "<a onclick=\"folder2('','')\">全部文件<a/>";
        $("[id$='HFoldUrl']").val($("[id$='HFoldUrl']").val() + "/" + name);
        var allUrl = $("[id$='HFoldUrl']").val();
        var names = '';
        for (var i = 1; i < allUrl.split('/').length; i++) {
            names += "/" + allUrl.split('/')[i];
            if (names != "" && allUrl.split('/')[i].length > 0) {
                $("[id$='HFoldUrl']").val(names);
                html += "<a onclick=\"folder2('" + names + "','" + id + "')\">>" + allUrl.split('/')[i].substring(0, allUrl.split('/')[i].length - id.length) + "<a/>";
            }
        }
        $(".navgition").html(html);
        Bind();
    }
    else {
        window.open(url);
    }
}

function folder2(name, id) {
    $(".navgition").html("");

    var html = "<a onclick=\"folder2('','')\">全部文件<a/>";
    $("[id$='HFoldUrl']").val(name);
    var allUrl = $("[id$='HFoldUrl']").val();
    var names = '';
    for (var i = 1; i < allUrl.split('/').length; i++) {
        names += "/" + allUrl.split('/')[i];
        if (names != "") {
            $("[id$='HFoldUrl']").val(names);
            html += "<a onclick=\"folder2('" + names + "','" + id + "')\">>" + allUrl.split('/')[i].substring(0, allUrl.split('/')[i].length - id.length) + "<a/>";
        }
    }
    $(".navgition").html(html);

    Bind();
}
//格式化时间
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function MoveMore(url, id, type) {
    $("#maskTop").remove();
    $("#mask").remove();

    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    var ids = "";
    if (id == "") {
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
    }
    else {
        ids = id;
    }
    if (ids == "") {
        alert("请选择要移动的文件");
    }
    else {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
            data: { CMD: "MoveMore", "MoveIDs": ids, "Url": url, "Type": type },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    ShowData(1);

                }
                else {
                    alert("文件已存在");
                }
            },
            error: function (errMsg) {
                alert('操作失败！');
            }
        });
    }
}
function Move(id, type) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    //var timespan = new Date().getTime();
    if (type == "copy") {
        $.webox({
            width: 400, height: 300, bgvisibel: true, title: '复制到', iframe: FirstUrl + "/SitePages/LibraryTree.aspx?ID=' + id + '&Type=copy&" + Math.random
        });
        //popWin.showWin('400', '300', '复制到', FirstUrl + '/SitePages/LibraryTree.aspx?ID=' + id + '&Type=copy&radom=' + timespan, 'no');
    }
    else {
        $.webox({
            width: 400, height: 300, bgvisibel: true, title: '复制到', iframe: FirstUrl + "/SitePages/LibraryTree.aspx?ID=' + id + '&Type=move&" + Math.random
        });
       // popWin.showWin('400', '300', '移动到', FirstUrl + '/SitePages/LibraryTree.aspx?ID=' + id + '&Type=move&radom=' + timespan, 'no');
    }
}
//批量删除
function DelMore() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    var ids = "";
    $("input[name='Check_box']:checkbox").each(function () {
        if ($(this).attr("checked") == "checked") {
            ids += $(this).attr('value') + ",";
            //Del($(this).attr('value'));
        }
    });
    if (confirm("确认删除文件吗")) {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
            data: { CMD: "DelMore", "DelIDs": ids },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("删除成功");
                }
                ShowData(1);
            },
            error: function (errMsg) {
                alert('删除失败！');
            }
        });
    }
}
//删除
function Del(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    if (confirm("确认删除文件吗")) {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
            data: { CMD: "Del", "DelID": id },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("删除成功");
                }
                ShowData(1);
            },
            error: function (errMsg) {
                alert('删除失败！');
            }
        });
    }
}
//取消修改文件名称
function EditNameQ(em, name) {
    $(em).parents("td").children("span").html(name);
}
//修改文件名称
function EditName(em, id, oldname) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    var name = $("#txt" + id).val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
        data: {
            CMD: "EditName", "NameID": id, "NewName": name
        },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                $(em).parents("td").children("span").html(name);
            }
            else {

                $(em).parents("td").children("span").html(oldname);
            }
        },
        error: function (errMsg) {
            $(em).parents("td").children("span").html(oldname);
        }
    });
}
function cl(em, id, title) {

    var name = $(em).parents("td").children("span").html();
    if (name = title) {
        var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px;margin-top:5px' id=\"txt" + id
                    + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; cursor:pointer;\" onclick=\"EditName(this,'" + id
                    + "','" + name + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"EditNameQ(this,'" + name
                    + "')\">&#xe61e;</i>";

        $(em).parents("td").children("span").html(v);
        $(em).parents("td").children("span").removeAttr("onclick");
    }
}
//文档下载
function Down(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    var ids = "";
    if (id == "") {
        $("input[name='Check_box']:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                ids += $(this).attr('value') + ",";
            }
        });
    }
    else {
        ids = id;
    }
    if (ids == "") {
        alert("请选择要下载的文件");
    }
    else {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Library.aspx",
            data: { CMD: "Down", downID: ids },
            dataType: "text",
            success: function (url) {
                if (url != "") {
                    window.open(url);
                }
                else {
                    alert('没有选择任何文件！');
                }
            },
            error: function (errMsg) {
                alert('下载失败！');
            }
        });
    }
}
function TabSel(Status) {
    if (Status != "") {
        document.getElementById("opertion").style.display = "none";
    }
    else {
        document.getElementById("opertion").style.display = "block";
    }
    $("#opertion")
    $("[id$='HFoldUrl']").val("");
    $(".navgition").html("全部文件");

    $("#Hidden2").val("");
    $("#Hidden3").val("");
    $("#HStatus").val(Status);
    ShowData(1);
}
function uploader() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

    var HFoldUrl = $("[id$='HFoldUrl']").val();
    var urlName = encodeURIComponent("资源库");
    var url= FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=\'\'&HStatus=\'\'&hContent=\'\'&HFoldUrl=' + HFoldUrl+'&capacity=\'\'';
    $.webox({
        width: 600, height: 550, bgvisibel: true, title: '文件上传', iframe: url
    });
   // popWin.showWin('600', '550', '文件上传',url, "no");
}