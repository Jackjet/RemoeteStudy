
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