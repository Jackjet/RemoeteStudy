
$(function () {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    serchSubNev();
    Bind("left", 0);

    $("#checkAll").click(function () {
        var flag = $(this).attr("checked") == undefined ? false : true;
        $("input[name='Check_box']:checkbox").each(function () {
            $(this).attr("checked", flag);
        })
    })
    //$(document).bind("keydown", function (e) {
    //    e = window.event || e;
    //    if (e.keyCode == 13) {
    //        Bind();
    //    }
    //});

});
var pIndex = 1;
var pCount = 0;
var pSize = 10;
function BindData(data) {
    $("#tabroom").empty();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var i = 0;
    var trfir = "<tr class='trth'><th class='Account'>编号</th><th class='name'>名称</th><th class='Head'>类型</th><th class='Contact'>发布时间</th><th class='State'>状态</th><th class='Operation'>操作</th></tr>";
    $("#tabroom").append(trfir);
    if (data == "[]") {
        $("#tabroom").append("<tr><td colspan='6' style='text-align:center'>暂时没有相关数据</td></tr>");
    }
    $($.parseJSON(data)).each(function () {

        if (parseInt(i) % 2 == 0) {
            var tr = "<tr class='Single'><td class='Account'>" + (i + 1) + "</td><td class='name'>" + this.Title + "</td><td class='Account'>" + this.ResourcesType + "</td><td class='Account'>" + this.Created + "</td><td class='State'>" +
            "<span class='qijin'><a href='#' class='" + this.qStatus + "' onclick='UpdateItem(" + this.ID + ")'>启用</a><a href='#' onclick='UpdateItem(" + this.ID + ")' class='" + this.jStatus + "'>禁用</a></span></td><td class='Operation'><a onclick='editpdetail1(\"编辑资源\",\"/SitePages/AddResource.aspx?itemid=" + this.ID + "\",\"700\",\"500\");' style='color: blue;'><i class='iconfont'>&#xe629;</i></a><a class='btn'  onclick='delitem(" + this.ID + ")'><i class='iconfont'>&#xe64c;</i></a></td></tr>";
            $("#tabroom").append(tr);
        }
        else {
            var tr = "<tr class='Double'><td class='Account'>" + (i + 1) + "</td><td class='name'>" + this.Title + "</td><td class='Account'>" + this.ResourcesType + "</td><td class='Account'>" + this.Created + "</td><td class='State'>" +
           "<span class='qijin'><a href='#' class='" + this.qStatus + "' onclick='UpdateItem(" + this.ID + ")'>启用</a><a href='#' onclick='UpdateItem(" + this.ID + ")' class='" + this.jStatus + "'>禁用</a></span></td><td class='Operation'><a onclick='editpdetail1(\"编辑资源\",\"/SitePages/AddResource.aspx?itemid=" + this.ID + "\",\"700\",\"500\");' style='color: blue;'><i class='iconfont'>&#xe629;</i></a><a class='btn' onclick='delitem(" + this.ID + ")'><i class='iconfont'>&#xe64c;</i></a></td></tr>";

            $("#tabroom").append(tr);
        }
        i++;

    })
}
function Bind(operate, id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
        data: { CMD: "FullTab", Operate: operate, TypeId: id, PageSize: pSize, PageIndex: pIndex, hContent: $("[id$='hContent']").val() },
        dataType: "text",
        success: function (returnVal) {
            returnVal = $.parseJSON(returnVal);
            if (returnVal != null) {
                pCount = returnVal.PageCount;
                LoadPageControl(ShowData, "pageDiv", pIndex, pSize, Math.ceil(pCount / pSize), pCount);
                if (pCount > pSize) {
                    $("#pageDiv").show();
                }
                else {
                    $("#pageDiv").hide();
                }
            }
        },
        error: function (errMsg) {
            alert('数据加载失败！');
        }
    });
}
function ShowData(index) {
    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());

    var postData = { CMD: "FullTab", Operate: $("[id$='hOperate']").val(), PageSize: pSize, PageIndex: index, TypeId: $("[id$='hContent']").val() };
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
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
function serchTime() {
    //$("#Hidden3").val(tim);
    var search = $("#tb_search").val();
    //$(em).parent().addClass("selected").siblings().removeClass("selected");
    //$(".File_type").hide();
    $("[id$='hContent']").val(search);
    $("[id$='hOperate']").val("search");
    Bind("search", search);
    return false;
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
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
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
function delitem(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    if (confirm("你确定删除吗？")) {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
            data: {
                CMD: "DeleteItem", "itemid": id
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert('删除成功');
                    Bind("", "");
                }
            },
            error: function (errMsg) {
                alert('删除失败，请重试');
            }
        });
    }

}

function UpdateItem(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
        data: {
            CMD: "UpdateItem", "itemid": id
        },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert('更新成功');
                Bind("", "");
            }
        },
        error: function (errMsg) {
            alert('删除失败，请重试');
        }
    });

}
function delitem(id) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    if (confirm("你确定删除吗？")) {
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
            data: {
                CMD: "DeleteItem", "itemid": id
            },
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == "1") {
                    alert('删除成功');
                    Bind("", "");
                }
            },
            error: function (errMsg) {
                alert('删除失败，请重试');
            }
        });
    }

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

    Bind("left", 0);
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

//绑定左侧导航
function serchSubNev() {

    var Hadmin = encodeURIComponent($("[id$='HAdmin']").val());
    $("#Tree").empty();
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var div = "";
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
        data: { CMD: "Tree", Hadmin: Hadmin },
        dataType: "text",
        success: function (returnVal) {
            $("#Tree").append(returnVal);

            initTree();
            //Bind();
        },
        error: function (errMsg) {
            alert("数据加载失败");
        }
    });

}
function navTopClick() {
    $(".navgition").html("全部文件");

    $("#quanbu").css("background", "#F6F6F6");
    $("#quanbu").children(".icon").eq(0).css("background", "#0da6ea");
    $("#quanbu").children(".icon").eq(0).css("border", "1px solid #0da6ea");
    $("[id$='hContent']").val("");

    Bind("left", 0);

}
//左侧导航点击绑定内容
function NavClick(id, em) {
    //$("[id$='HFoldUrl']").val("");
    //$(".navgition").html("全部文件");

    $("#quanbu").css("background", "#fff");
    $("#quanbu").children(".icon").eq(0).css("background", "#fff");
    $("#quanbu").children(".icon").eq(0).css("border", "1px solid #666");

    $(".select-box").find(".i-menu").css("background", "#fff");

    $(".select-box").find(".ici-menu").css("background", "#fff");

    $(".select-box").find(".icic-item").css("background", "#fff");
    $(".select-box").find(".icon").css("background", "#fff");
    $(".select-box").find(".icon").css("border", "1px solid #666");

    $(em).css("background", "#FBEFC4");
    $(em).children(".icon").eq(0).css("background", "#0da6ea");
    $(em).children(".icon").eq(0).css("border", "1px solid #0da6ea");

    $("[id$='hContent']").val(id);
    $("[id$='hOperate']").val("left");


    $(".F_time dd:first").addClass("selected").siblings().removeClass("selected");
    $(".F_type dd:first").addClass("selected").siblings().removeClass("selected");

    Bind("left", id);
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
    var name = $(em).parent().prev().html();
    if (name = title) {
        var v = "<input type='text' value=\"" + name + "\" style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu" + id
            + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352; cursor:pointer;\" onclick=\"EditMenuName(this,'" + id
            + "','" + name + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"EditMenuNameQ(this,'" + name
            + "')\">&#xe61e;</i>";
        $(em).parent().prev().html(v);
    }
}
//修改目录名称
function EditMenuName(em, id, oldname) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    var name = $("#Menu" + id).val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
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
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",
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
                serchSubNev();
                //serchSubNev($("[id$='hSubject']").val());
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
           + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this,'" + id + "')\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72; cursor:pointer;\" onclick=\"Del(this)\">&#xe61e;</i>";
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
function AddMenu(em, pid) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"));


    //var pid = $("[id$='hSubject']").val();
    var FileName = $("#Menu" + pid).val();
    $.ajax({
        type: "Post",
        url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/SecondNav.aspx",

        data: { CMD: "AddMenu", Pid: pid, MenuName: FileName },
        dataType: "text",
        success: function (returnVal) {
            if (returnVal == "1") {
                alert("添加成功");
                serchSubNev();
                //$(em).parent().html(FileName);
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

    var length = $('#Tree input').length;

    //var divNode = document.getElementById("div0");
    if (length == 0) {
        var v = "<input type='text' value='' style='float:left;line-height:10px; width:100px; margin-top:8px;' id=\"Menu0"
           + "\"/> <i class=\"iconfont tishi true_t\" style=\"margin: 2px; color: #87c352;cursor:pointer; \" onclick=\"AddMenu(this,0)\">&#xe61d;</i> <i class=\"iconfont tishi fault_t\" style=\"margin: 2px; color: #ff6d72;cursor:pointer; \" onclick=\"Del()\">&#xe61e;</i>";
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
    ////获取当前节点对象
    //var divNode2 = document.getElementById("Tree");
    ////获取文本节点
    //var textNode = divNode2.childNodes[0];
    ////删除文本节点
    //divNode2.removeChild(textNode);
}
function EditMajor() {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))

    popWin.showWin("600", "500", "修改专业学科", FirstUrl + "/SitePages/EditMajor.aspx", "no");
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

    popWin.showWin('600', '550', '文件上传', FirstUrl + '/SitePages/PND_wp_DriveUpload.aspx?UrlName=' + urlName + '&hSubject=' + hSubject + '&HStatus=' + HStatus + '&hContent=' + hContent + '&HFoldUrl=' + HFoldUrl + '&capacity=\'\'', "no");
    window.opener.location.reload();
}