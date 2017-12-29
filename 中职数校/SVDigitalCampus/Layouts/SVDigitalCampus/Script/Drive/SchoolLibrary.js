

var pIndex = 1;
var pCount = 0;
var pSize = 10;
//绑定文件内容
function BindData(data) {
    $("#tab tbody").empty();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "IsAdmin", Hadmin: Hadmin },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "True") {
                $($.parseJSON(data)).each(function () {
                    var operater = "";
                    if ($("[id$='HStatus']").val() == "-1") {
                        operater = "<a href=\"#\" class=\"shenhe\" onclick=\"Check('" + this.ID +
                            "','1')\";>通过</a><a href=\"#\" class=\"shenhe\" onclick=\"showshenhe('" + this.ID + "')\";>拒绝</a>";
                        document.getElementById("Operations").style.display = "block";
                    }
                    else { document.getElementById("Operations").style.display = "none"; }

                    var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID + "' class='Check_box' name='Check_box'  id='subcheck' onclick='setSelectAll()'/></td><td class='name'><i class='file' style='float: left;cursor:pointer' onclick=\"folder('" + this.Name
                        + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span style=\"cursor:pointer\" onclick=\"folder('" + this.Name
                        + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
                        "</span><div class=\"Operation\" style=\"display:none;\">" +
                        "<span class=\"Download\" ><a href=\"#\" onclick=\"Down('" + this.ID + "')\"><i class=\"iconfont\">&#xe60a;</i></a></span><span class=\"more\"><a href=\"#\" class=\"more_m\"><i class=\"iconfont\">&#xe61a;</i></a>" +
                        "<ul class=\"M_con\" style=\"display:none;\"><li><a href=\"#\" onclick=\"Move('" + this.ID + "','move')\">移动到</a></li><li><a href=\"#\"  onclick=\"Move('" + this.ID +
                        "','copy')\">复制到</a></li><li><a href=\"#\" onclick=\"cl(this,'" + this.ID + "','" + this.Title + "')\">重命名</a></li><li><a href=\"#\" onclick=\"DelFile('" + this.ID + "')\">删除</a></li>"
                        + "</ul></span></div>"
                        + "</td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td><td class='Operations'>" +
                        operater + "</td></tr>";
                    $("#tab tbody").append(tr);

                })
            }
            else {
                $($.parseJSON(data)).each(function () {
                    var operater = "";
                    if ($("[id$='HStatus']").val() == "-1") {
                        operater = "<a href=\"#\" class=\"shenhe\" onclick=\"Check('" + this.ID + "','1')\";>通过</a><a href=\"#\" class=\"shenhe\" onclick=\"showshenhe('" + this.ID + "')\";>拒绝</a>";
                    }

                    var tr = "<tr class='J-item Single'><td class='Check'><input type='checkbox' value='" + this.ID + "' class='Check_box' name='Check_box' /></td><td class='name'><i class='file' style='float: left; cursor:pointer' onclick=\"folder('" + this.Name
                        + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\"><img src='" + this.Image + "'></i><span onclick=\"folder('" + this.Name
                        + "','" + this.Url + "','" + this.Type + "','" + this.ID + "')\">" + this.Title +
                        "</span></td><td class='File_size'>" + this.Size + "</td><td class='Change_date'>" + this.Created + "</td><td class='Operations'>" +
                        operater + "</td></tr>";
                    $("#tab tbody").append(tr);

                })
            }
            $(".J-item").hover(function () {
                $(this).find(".Operation").show().end().siblings().find('.M_con').hide();
            }, function () {
                $(this).find(".Operation").hide();
            });
            //分享下载
            $('.more_m').click(function () {
                $(this).siblings('.M_con').show();
            });

        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });

    // });

}
/*function over(em) {
    $(em).parent().find("li").removeClass("off");
    $(em).parent().find("li").addClass("on");
    $(em).addClass("on");
    $(em).nextAll().removeClass("on").addClass("off");
}*/
//绑定文件内容（分页）
function ShowData(index) {
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

    var postData = { CMD: "FullTab", PageSize: pSize, PageIndex: index, Hadmin: Hadmin, Name: $("#txtName").val(), TypeSel: $("#Hidden2").val(), TimeSel: $("#Hidden3").val(), FoldUrl: $("[id$='HFoldUrl']").val(), SeleStatu: $("[id$='HStatus']").val(), hSubject: $("[id$='hSubject']").val(), hContent: $("[id$='hContent']").val() };
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: postData,
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            if (returnVal != null) {
                SetPageCount(Math.ceil(returnVal.PageCount / pSize));
                BindData(returnVal.Data);
            }
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
}
$(function () {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    GetFileType();
    IsAdmin(FirstUrl);
    BindMajor(FirstUrl);

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
function btnQuery() {
    ShowData(1);
}
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
//审核
function showshenhe(id) {
    showDiv('Messagediv', 'Message_head');
    $("#hdS").val(id);
}

function Bind() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());


    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "FullTab", PageSize: pSize, PageIndex: pIndex, Hadmin: Hadmin, hSubject: $("[id$='hSubject']").val(), hContent: $("[id$='hContent']").val(), SeleStatu: $("[id$='HStatus']").val(), FoldUrl: $("[id$='HFoldUrl']").val() },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            if (returnVal != null) {
                pCount = returnVal.PageCount;
                LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
            }
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });

}
//审核成功
function Check(id, status) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "Check", "CheckID": id, "Status": status },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert('操作成功！');
                ShowData(1);
            }
        },
        error: function (errMsg) {
            alert('操作失败！');
        }
    });
}
//审核失败
function shenhe() {
    var id = $("#hdS").val();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))


    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "Shenhe", ShenheID: id, Message: $("#Message").val() },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert("操作成功");
                ShowData(1);
            }
            else
                alert("操作失败");
        },
        error: function (errMsg) {
            alert('操作失败！');
        }
    });
}
//管理员可以查看审核页面
function IsAdmin(FirstUrl) {
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "IsAdmin", Hadmin: Hadmin },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "True") {

                document.getElementById("daishenhe").style.display = "block";
                document.getElementById("plcz").style.display = "block";
                document.getElementById("xjwjj").style.display = "block";
                document.getElementById("toptianjia").style.display = "inline";
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
    $("#F_type").append("<dt>文件类型：</dt><dd class=\"selected\"><a href=\"#\" onclick=\"serchType('',this)\">不限</a></dd>");

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
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
//修改文件类型
function EditType() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    $.webox({
        width: 600, height: 467, bgvisibel: true, title: '修改文件类型', iframe: FirstUrl + "/SitePages/DriveType.aspx?" + Math.random
    });

    //popWin.showWin("600", "467", "修改文件类型", FirstUrl + "/SitePages/DriveType.aspx", 'auto');
}
//新增文件夹
function AddFold(em) {
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));

    var FileName = $("#txt" + em + "").val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",

        data: { CMD: "AddFolder", "FileName": FileName, FoldUrl: $("[id$='HFoldUrl']").val(), Hadmin: Hadmin, SubJectID: $("[id$='hSubject']").val(), CatagoryID: $("[id$='hContent']").val(), TypeName: $("[id$='HStatus']").val() },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert("添加成功");
                Bind();
            }
            else
                alert("添加失败");
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
            "<input type='text' value='新建文件夹' style='float:left;line-height:10px;margin-top:5px;' id=\"txt" + RowIndex +
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
        newRefRow.insertCell(4).innerHTML = "";
        newRefRow.insertCell(5).innerHTML = "";
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
function serchType(attr, em) {
    $(em).parent().addClass("selected").siblings().removeClass("selected");
    $("#Hidden2").val(attr);
    ShowData(1);
    $(".File_type").hide();
}
//时间查询条件点击事件
function serchTime(tim, em) {
    $("#Hidden3").val(tim);
    $(em).parent().addClass("selected").siblings().removeClass("selected");
    $(".File_type").hide();
    Bind();
}
//点击文件夹展开
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

        ShowData(1);
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

    ShowData(1);
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
//移动文件
function FileMoveMore(url, id, type) {
    $("#maskTop").remove();
    $("#mask").remove();
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

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
        var hSubject = $("[id$='hSubject']").val();
        var hContent = $("[id$='hContent']").val();
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
            data: { CMD: "MoveMore", "MoveIDs": ids, "Url": url, "Type": type, hSubject: hSubject, hContent: hContent, Hadmin: Hadmin },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert("操作成功");
                    ShowData(1);
                }
                else { alert("文件已存在") }
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
    if (type == "copy") {
        $.webox({
            width: 400, height: 300, bgvisibel: true, title: '复制到', iframe: FirstUrl + "/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=copy&" + Math.random
        });
        //popWin.showWin('400', '300', '复制到', FirstUrl + '/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=copy', 'no');
        window.location.href = window.location.href;

    }
    else {
        $.webox({
            width: 400, height: 300, bgvisibel: true, title: '复制到', iframe: FirstUrl + "/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=move&" + Math.random
        });
       // popWin.showWin('400', '300', '移动到', FirstUrl + '/SitePages/MySchoolLibraryTree.aspx?ID=' + id + '&Type=move', 'no');
        window.location.href = window.location.href;

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
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
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
function DelFile(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    if (confirm("确认删除文件吗")) {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
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
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
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
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
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
function TabSel(SelType) {
    if (SelType == "0" || SelType == "-1") {
        document.getElementById("opertion").style.display = "none";
    }
    else {
        document.getElementById("opertion").style.display = "block";
    }
    // opertion
    $("[id$='HFoldUrl']").val("");
    $(".navgition").html("全部文件");
    $("#Hidden2").val("");//文件类型
    $("#Hidden3").val("");//上传时间
    $("[id$='HStatus']").val(SelType);//tab条件
    $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
    $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");

    Bind();
}
$(window).resize(function () {
    var height = $("#Major").height();
    if (height > 40) {
        $("#Major").append("<span class=\"more\">展开</span>");
        //筛选条件经过展开
        $("#selectList").find(".more").toggle(function () {
            $(this).parent().parent().removeClass("dlHeight");
        }, function () {
            $(this).parent().parent().addClass("dlHeight");
        });
    }
    else {
        $("#Major").find(".more").remove();
    }
});
//绑定学科
function BindMajor() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

    var i = 0;
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "Major", Hadmin: Hadmin },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            $(returnVal).each(function () {
                var result = "";
                if (i == 0) {
                    result = "<li><a href='#'  class='selected' onclick=\"serchMajor('" + this.NJ + "',this)\">" + this.NJMC + "</a></li>";
                    serchMajor(this.NJ, "");
                    $("#hMajor").val(this.NJ);
                }
                else {
                    result = "<li><a href='#' onclick=\"serchMajor('" + this.NJ + "',this)\">" + this.NJMC + "</a></li>";
                }
                $("#Major").append(result);
                i++;
            });
            var height = $("#Major").height();
            if (height > 40) {
                $("#Major").append("<span class=\"more\">展开</span>");
                //筛选条件经过展开
                $("#selectList").find(".more").toggle(function () {
                    $(this).parent().parent().removeClass("dlHeight");
                }, function () {
                    $(this).parent().parent().addClass("dlHeight");
                });
            }
        },
        error: function (errMsg) {
        }
    });

}
//绑定专业
function serchMajor(id, em) {
    $("#Major li a").removeClass("selected");
    $(em).toggleClass("selected");
    $("#hMajor").val(id);

    $("#SubJect").empty();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var j = 0;
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "SubJect", "MajorID": id },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            if (returnVal == null) {
                $("[id$='hSubject']").val("")
            }
            $(returnVal).each(function () {
                var result = "";
                if (j == 0) {
                    result = "<li><a  class='selected' href='#' onclick=\"serchSubNev('" + this.ID + "',this)\">" + this.Title + "</a></li>";
                    serchSubNev(this.ID, "");
                    $("[id$='hSubject']").val(this.ID)
                    $("[id$='hContent']").val("");
                    $("#Hidden2").val("");
                    $("#Hidden3").val("");

                    Bind();
                }
                else {
                    result = "<li><a href='#' onclick=\"serchSubNev('" + this.ID + "',this)\">" + this.Title + "</a></li>";
                }
                $("#SubJect").append(result);

                j++;
            });
        },
        error: function (errMsg) {
        }
    });
}



//*********************************
//导航

//绑定左侧导航
function serchSubNev(id, em) {
    $("[id$='HFoldUrl']").val("");
    $(".navgition").html("全部文件");
    $("#SubJect li a").removeClass("selected");
    $(em).toggleClass("selected");

    $("[id$='hSubject']").val(id);//学科
    $("[id$='hContent']").val("");//
    $("#Hidden2").val("");//
    $("#Hidden3").val("");
    $("[id$='HStatus']").val("0");
    $(".resourses li:first").addClass("selected").siblings().removeClass("selected");
    $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
    $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
    $("#Tree").empty();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var div = "";
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: { CMD: "Tree", "SubJectID": id, Hadmin: Hadmin },
        dataType: "text",
        success: function (returnVal) {
            $("#Tree").append(returnVal);

            initTree();
            Bind();
        },
        error: function (errMsg) {
        }
    });

}
function navTopClick() {
    $("[id$='HFoldUrl']").val("");
    $(".navgition").html("全部文件");

    $("#quanbu").css("background", "#F6F6F6");
    $("#quanbu").children(".icon").eq(0).css("background", "#0da6ea");
    $("#quanbu").children(".icon").eq(0).css("border", "1px solid #0da6ea");
    $("[id$='hContent']").val("");

    Bind();

}
//左侧导航点击绑定内容
function NavClick(id, em) {
    $("[id$='HFoldUrl']").val("");
    $(".navgition").html("全部文件");

    $("#quanbu").css("background", "#fff");
    $("#quanbu").children(".icon").eq(0).css("background", "#fff");
    $("#quanbu").children(".icon").eq(0).css("border", "1px solid #666");

    $(".select-box").find(".i-menu").css("background", "#fff");

    $(".select-box").find(".ici-menu").css("background", "#fff");

    $(".select-box").find(".icic-item").css("background", "#fff");
    $(".select-box").find(".icon").css("background", "#fff");
    $(".select-box").find(".icon").css("border", "1px solid #666");

    $(em).css("background", "#F6F6F6");
    $(em).children(".icon").eq(0).css("background", "#0da6ea");
    $(em).children(".icon").eq(0).css("border", "1px solid #0da6ea");

    $("[id$='hContent']").val(id);
    $("#Hidden2").val("");
    $("#Hidden3").val("");

    $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
    $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");

    Bind();
}
//左侧导航点击、鼠标经过事件
function initTree() {

    $(".i-menu").each(function () {
        var _this = $(this);
        $(this).hover(function () {
            _this.find(".btn-area").show();
        }, function () {
            _this.find(".btn-area").hide();
        });
        var i_con = $(this).next(".ici-con")[0];
        if (i_con != null) {
            i_con.style.display = "none";
        }
    });
    $(".ici-menu").each(function () {
        var _this = $(this);
        $(this).hover(function () {
            _this.find(".btn-area").show();
        }, function () {
            _this.find(".btn-area").hide();
        });
        var i_con = $(this).next(".ici-con")[0];
        if (i_con != null) {
            i_con.style.display = "none";
        }

    });
    $(".icic-item").each(function () {
        var _this = $(this);
        $(this).hover(function () {
            _this.find(".btn-area").show();
        }, function () {
            _this.find(".btn-area").hide();
        });
    });
    $(".i-menu").click(function () {
        $(".i-menu").next().hide();
        var dis = $(this).next().css("display");
        if (dis == "none") {
            $(this).next().show();
        }
        else {
            $(this).next().hide();
        }

    });
    $(".ici-menu").click(function () {
        $(".ici-menu").next().hide();
        var dis = $(this).next().css("display");
        if (dis == "none") {
            $(this).next().show();
        }
        else {
            $(this).next().hide();
        }
    });
}

function editLeftMenu(id, em, title) {
    var length = $('#Tree input').length;

    if (length == 0) {

        var name = $(em).parent().prev().html();
        if (name = title) {
            var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
                + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; cursor:pointer;\" onclick=\"EditMenuName(this,'" + id
                + "','" + name + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"EditMenuNameQ(this,'" + name
                + "')\">&#xe61e;</i>";
            $(em).parent().prev().html(v);
        }
    }
    else {
        alert("有未完成操作");
        $('#Tree input').focus();
    }
}
//修改目录名称
function EditMenuName(em, id, oldname) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    var name = $("#Menu" + id).val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: {
            CMD: "EditMenuName", "EditMenuID": id, "MenuNewName": name
        },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert("修改成功");

                $(em).parent().html(name);
            }
            else {
                alert("修改失败");
                $(em).parent().html(oldname);
            }
        },
        error: function (errMsg) {
            alert(errMsg);
            $(em).parent().html(oldname);
        }
    });
}
function EditMenuNameQ(em, name) {
    var name = $(em).parent().html(name);
}
function delLeftMenu(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",
        data: {
            CMD: "DelContent", "delMenuID": id, "DelMenuName": name
        },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "0") {
                alert("删除失败");
            }
            if (returnVal == "1") {
                alert("删除成功");
                serchSubNev($("[id$='hSubject']").val());
            }
            if (returnVal == "2") {
                alert("目录不为空");
            }
            if (returnVal == "3") {
                alert("请先删除子节点");
            }
        },
        error: function (errMsg) {
            alert(errMsg);
        }
    });
}
function addLeftMenu(id, em, divid) {
    // var divNode = document.getElementById("div0");
    var length = $('#Tree input').length

    if (length == 0) {
        var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
           + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this,'" + id
           + "','')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; cursor:pointer;\" onclick=\"Del(this)\">&#xe61e;</i>";
        var html = "<div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
        //$("#" + divid).parent().prepend(html);
        var em1 = $("#" + divid).find('.i-con').html();
        var em2 = $("#" + divid).find('.ici-con').html();
        if (em1 == undefined && em2 == undefined) {
            var html = "<div clas\"i-con\"><div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div></div>";
            $("#" + divid).append(html);
        }
        if (em1 != undefined) {
            var html = "<div id='div0' class=\"ic-item\"><div class=\"ici-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
            $("#" + divid).find('.i-con').prepend(html)
        }
        if (em1 == undefined && em != undefined) {
            var html = "<div id='div0' class=\"icic-item\"><span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div>";
            $("#" + divid).find('.ici-con').prepend(html)

        }
    }
    else {
        alert("有未完成操作");
        $('#Tree input').focus();
    }
}
function AddMenu(em, id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    var hSubject = $("[id$='hSubject']").val();

    //var pid = $("[id$='hSubject']").val();
    var FileName = $("#Menu" + id + "").val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SchoolLibrary.aspx",

        data: { CMD: "AddMenu", "hSubject": hSubject, id: id, MenuName: FileName },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert("添加成功");
                $(em).parent().html(FileName);
            }
            else {
                alert("名称重复");
            }
        },
        error: function (errMsg) {
            alert('添加失败！');
        }
    });
}
function AddMenuTop(em) {
    var id = $("[id$='hSubject']").val();
    var length = $('#Tree input').length

    //var divNode = document.getElementById("div0");
    if (length == 0) {
        var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
           + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this," + id + ")\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"Del()\">&#xe61e;</i>";
        var html = "<div id='div0' class=\"item\"><div class=\"i-menu cf\"> <span class=\"fl icon\"></span><span class=\"tit fl\">" + v + "</span></div></div>";
        $("#Tree").prepend(html);
    }
    else {
        alert("有未完成操作");
        $('#Tree input').focus();
    }
}
function Del() {
    $("#div0").remove();
}
//********************************
function EditMajor() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    $.webox({
        width: 600, height: 500, bgvisibel: true, title: '修改专业学科', iframe: FirstUrl + "/SitePages/EditMajor.aspx?" + Math.random
    });
    //popWin.showWin("600", "500", "修改专业学科", FirstUrl + "/SitePages/EditMajor.aspx", "no");
}
function shaixuan() {
    var dis = $(".File_type").css("display");
    if (dis == "none") {
        $(".File_type").show();
    }
    else {
        $(".File_type").hide();
    }
}
function uploader() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));
    var HFoldUrl = $("[id$='HFoldUrl']").val();
    var hSubject = $("[id$='hSubject']").val();
    var HStatus = $("[id$='HStatus']").val();
    var hContent = $("[id$='hContent']").val();
    var urlName = encodeURIComponent("校本资源库");
    var url = FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=' + hSubject + '&HStatus=' + HStatus + '&hContent=' + hContent + '&HFoldUrl=' + HFoldUrl + '&capacity=\'\'';
    //popWin.showWin('600', '550', '文件上传', FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=' + hSubject + '&HStatus=' + HStatus + '&hContent=' + hContent + '&HFoldUrl=' + HFoldUrl + '&capacity=\'\'', "no");
    $.webox({
        width: 600, height: 550, bgvisibel: true, title: '文件上传', iframe: url
    });
    window.opener.location.reload();
}