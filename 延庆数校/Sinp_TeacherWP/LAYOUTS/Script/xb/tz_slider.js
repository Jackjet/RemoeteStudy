//#menubox > #box.find(".items") > .class.find("class")>class>tag 	

//email下拉
//$(document).ready(function (e) {
//    //下拉列表
//    $(".slidedown").mouseover(function () {
//        $(this).find(".slidecon").show();
//        $(this).find("dt").addClass("hover");
//    });
//    $(".slidedown").mouseout(function () {
//        $(this).find(".slidecon").hide();
//        $(this).find("dt").removeClass("hover");
//    });
//});
/*百叶窗*/
$(function () {
    $(".tab_switch .sw_tabheader .nav_list span span li:first").addClass("selected");

    $("#menubox").find(".menuclick").click(function () {
        $(this).parent().toggleClass("selected").siblings().removeClass("selected");
        $(this).next().slideToggle("fast").end().parent().siblings().find(".submenu")
		.addClass("animated flipInX")
		.slideUp("fast").end().find("i").text("+");
        var t = $(this).find("i").text();
        $(this).find("i").text((t == "+" ? "-" : "+"));
    });

    $('.aside').toggle(function () {
        //alert('aa');
        $('#sliderbox').stop(this, this).animate({ left: "-200px" });
        $('.box .right').stop(this, this).animate({ marginLeft: 0 })
    }, function () {
        $('#sliderbox').stop(this, this).animate({ left: 0 });
        $('.box .right').stop(this, this).animate({ marginLeft: "200px" })

    })
    $(".yy_tab .yy_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        if (index == 0) {
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".yy_tab").find(".tc").eq(index).show().siblings().hide();
        }
    });

});

//公司岗位查看更多
$(document).ready(function (e) {
    //下拉列表
    $(".Post_post").mouseover(function () {
        $(this).find(".more_info").show();
        $(this).find(".Drop_down").addClass("hover_ul");
    });
    $(".Post_post").mouseout(function () {
        $(this).find(".more_info").hide();
        $(this).find(".Drop_down").removeClass("hover_ul");
    });
    //下拉列表
    $(".slidedown").mouseover(function () {
        $(this).find(".slidecon").show();
        $(this).find("dt").addClass("hover");
    });
    $(".slidedown").mouseout(function () {
        $(this).find(".slidecon").hide();
        $(this).find("dt").removeClass("hover");
    });

    $("ul.nav_list li.last").mouseover(function () {
        $(this).find(".second_nav").show();
        $(this).find("a").addClass("hover");
    });
    $("ul.nav_list li.last").mouseout(function () {
        $(this).find(".second_nav").hide();
        $(this).find("a").removeClass("hover");
    });

    $(".left_choice .Batch_operation").mouseover(function () {
        $(this).find(".B_con").show();
    });
    $(".left_choice .Batch_operation").mouseout(function () {
        $(this).find(".B_con").hide();
    });

    //筛选条件经过展开
    $("#selectList").find(".more").toggle(function () {
        $(this).parent().parent().removeClass("dlHeight");
    }, function () {
        $(this).parent().parent().addClass("dlHeight");
    });
});


//教案 习题 课件 审核 tab切换
$(function () {
    $(".Resources_tab .Resources_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        $(this).parent().addClass("selected").siblings().removeClass("selected");
        $(this).parents(".Resources_tab").find(".tc").eq(index).show().siblings().hide();
    });
});



function yzqy() {
    if (isTrue()) {
        $(".yy_tabheader").find("li[id=gw]").addClass("selected").siblings().removeClass("selected");
        $(".tc").eq(1).show().siblings().hide();
    }
}
function isTrue() {
    var flag = true;
    if ($("[id$='txtName']").val() == "") {
        $("[id$='txtName']").parent().find("i").html("&#xe619;");
        $("[id$='txtName']").parent().find("i").removeClass();
        $("[id$='txtName']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    }
    if ($("[id$='UserID']").val() == "") {
        $("[id$='UserID']").parent().find("i").html("&#xe619;");
        $("[id$='UserID']").parent().find("i").removeClass();
        $("[id$='UserID']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    } if ($("[id$='UserPwd']").val() == "") {
        $("[id$='UserPwd']").parent().find("i").html("&#xe619;");
        $("[id$='UserPwd']").parent().find("i").removeClass();
        $("[id$='UserPwd']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    } if ($("[id$='RelationName']").val() == "") {
        $("[id$='RelationName']").parent().find("i").html("&#xe619;");
        $("[id$='RelationName']").parent().find("i").removeClass();
        $("[id$='RelationName']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    } if ($("[id$='txtPhone']").val() == "") {
        $("[id$='txtPhone']").parent().find("i").html("&#xe619;");
        $("[id$='txtPhone']").parent().find("i").removeClass();
        $("[id$='txtPhone']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    } if ($("[id$='tbEmail']").val() == "") {
        $("[id$='tbEmail']").parent().find("i").html("&#xe619;");
        $("[id$='tbEmail']").parent().find("i").removeClass();
        $("[id$='tbEmail']").parent().find("i").addClass("iconfont tishi fault_t");
        flag = false;
    }
    $(".m_top").find("i").each(function (result) {
        //$("#Editdiv .m_top").find("i").each(function (result) {
        if ($(this).attr("class") == "iconfont tishi fault_t") {
            flag = false;
        }
    });
    $(".m_bottom").find("i").each(function (result) {
        //$("#Editdiv .m_top").find("i").each(function (result) {
        if ($(this).attr("class") == "iconfont tishi fault_t") {
            flag = false;
        }
    });

    return flag;
}
function confirmE(e) {
    var txt = $(e).val();
    if (txt) {
        $(e).parent().find("i").html("&#xe61d;");
        $(e).parent().find("i").removeClass();
        $(e).parent().find("i").addClass("iconfont tishi true_t");
    }
    else {
        $(e).parent().find("i").html("&#xe619;");
        $(e).parent().find("i").removeClass();
        $(e).parent().find("i").addClass("iconfont tishi fault_t");
    }
}

function phone(e) {
    var txt = $(e).val();
    if (txt) {
        if (txt.match(/^1[3578][01379]\d{8}$/) || txt.match(/^1[34578][01256]\d{8}$/) || txt.match(/^(134[012345678]\d{7}|1[34578][012356789]\d{8})$/)) {
            $(e).parent().find("i").html("&#xe61d;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi true_t");
            $(e).parent().find("span").html("");

        }
        else {
            $(e).parent().find("i").html("&#xe619;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi fault_t");
            $(e).parent().find("span").html("手机号格式不正确");
        }
    }
}
function Email(e) {
    var txt = $(e).val();
    if (txt) {
        if (txt.match(/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/)) {
            $(e).parent().find("i").html("&#xe61d;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi true_t");
            $(e).parent().find("span").html("");

        }
        else {
            $(e).parent().find("i").html("&#xe619;");
            $(e).parent().find("i").removeClass();
            $(e).parent().find("i").addClass("iconfont tishi fault_t");
            $(e).parent().find("span").html("邮箱格式不正确");
        }
    }
}
function confirmRepeat(e, funName, Msg) {
    var FirstUrl = window.location.href;
    FirstUrl = FirstUrl.substring(0, FirstUrl.indexOf("SitePages"))
    var txt = $(e).val();
    if (txt) {
        var postData = { Func: funName, Title: txt };
        $.ajax({
            type: "Post",
            url: FirstUrl + "/_layouts/15/SVDigitalCampus/hander/Enterprise.aspx",
            data: postData,
            dataType: "text",
            success: function (returnVal) {
                if (returnVal == 0) {
                    $(e).parent().find("i").html("&#xe61d;");
                    $(e).parent().find("i").removeClass();
                    $(e).parent().find("i").addClass("iconfont tishi true_t");
                    $(e).parent().find("span").html("");
                }
                else {
                    $(e).parent().find("i").html("&#xe619;");
                    $(e).parent().find("i").removeClass();
                    $(e).parent().find("i").addClass("iconfont tishi fault_t");
                    $(e).parent().find("span").html(Msg);
                }
            },
            error: function (errMsg) {
                alert('数据加载失败！');
            }
        });
    }
    else {
        $(e).parent().find("i").html("&#xe619;");
        $(e).parent().find("i").removeClass();
        $(e).parent().find("i").addClass("iconfont tishi fault_t");
    }
}
function GetAddInput() {
    var names = document.getElementsByName("bigcol");
    if (names.length == 0) {
        alert("没有可用的信息！");
        return false;
    }
    var result = '';
    for (var i = 0; i < names.length; i++) {
        result = result + names[i].value + ",";
    }
    var hid = document.getElementsByName("names");
    $("[id$='names']").val(result);
    //complete();
}
function complete() {
    $(".yy_tabheader").find("li[id=comple]").addClass("selected").siblings().removeClass("selected");
    $(".tc").eq(2).show().siblings().hide();
}