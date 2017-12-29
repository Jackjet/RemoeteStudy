//#menubox > #box.find(".items") > .class.find("class")>class>tag 	

//email下拉
$(document).ready(function (e) {
    //下拉列表
    $(".slidedown").mouseover(function () {
        $(this).find(".slidecon").show();
        $(this).find("dt").addClass("hover");
    });
    $(".slidedown").mouseout(function () {
        $(this).find(".slidecon").hide();
        $(this).find("dt").removeClass("hover");
    });
});
/*百叶窗*/
$(function () {
    $("#menubox").find(".menuclick").click(function () {
        $(this).parent().toggleClass("selected").siblings().removeClass("selected");
        $(this).next().slideToggle("fast").end().parent().siblings().find(".submenu")
		.addClass("animated flipInX")
		.slideUp("fast").end().find("i").text("+");
        var t = $(this).find("i").text();
        $(this).find("i").text((t == "+" ? "-" : "+"));
    });
});

/*左侧点击隐藏*/
$(function () {
    $('.aside').toggle(function () {
        //alert('aa');
        $('#sliderbox').stop(this, this).animate({ left: "-200px" });
        $('.box .right').stop(this, this).animate({ marginLeft: 0 })
    }, function () {
        $('#sliderbox').stop(this, this).animate({ left: 0 });
        $('.box .right').stop(this, this).animate({ marginLeft: "200px" })

    })

})

//岗位下拉
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
});

//tab切换
$(function () {
    $(".yy_tab .yy_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        if (index == 1&&index==0) {
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".yy_tab").find(".tc").eq(index).show().siblings().hide();
        }
    });
});
//食堂设置tab切换
$(function () {
    $(".ss_tab .yy_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        if (index != "2") {
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".ss_tab").find(".tc").eq(index).show().siblings().hide();
        }
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
//function confirm(e) {
//    var txt = $(e).val();
//    if (txt) {
//        $(e).parent().find("i").html("&#xe61d;");
//        $(e).parent().find("i").removeClass();
//        $(e).parent().find("i").addClass("iconfont tishi true_t");
//    }
//    else {
//        $(e).parent().find("i").html("&#xe619;");
//        $(e).parent().find("i").removeClass();
//        $(e).parent().find("i").addClass("iconfont tishi fault_t");
//    }
//}

//function phone(e) {
//    var txt = $(e).val();
//    if (txt) {
//        if (txt.match(/^1[3578][01379]\d{8}$/) || txt.match(/^1[34578][01256]\d{8}$/) || txt.match(/^(134[012345678]\d{7}|1[34578][012356789]\d{8})$/)) {
//            $(e).parent().find("i").html("&#xe61d;");
//            $(e).parent().find("i").removeClass();
//            $(e).parent().find("i").addClass("iconfont tishi true_t");
//            $(e).parent().find("span").html("");

//        }
//        else {
//            $(e).parent().find("i").html("&#xe619;");
//            $(e).parent().find("i").removeClass();
//            $(e).parent().find("i").addClass("iconfont tishi fault_t");
//            $(e).parent().find("span").html("手机号格式不正确");
//        }
//    }
//}
//function Email(e) {
//    var txt = $(e).val();
//    if (txt) {
//        if (txt.match(/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/)) {
//            $(e).parent().find("i").html("&#xe61d;");
//            $(e).parent().find("i").removeClass();
//            $(e).parent().find("i").addClass("iconfont tishi true_t");
//            $(e).parent().find("span").html("");

//        }
//        else {
//            $(e).parent().find("i").html("&#xe619;");
//            $(e).parent().find("i").removeClass();
//            $(e).parent().find("i").addClass("iconfont tishi fault_t");
//            $(e).parent().find("span").html("邮箱格式不正确");
//        }
//    }
//}
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
//function complete() {
//    $(".yy_tabheader").find("li[id=comple]").addClass("selected").siblings().removeClass("selected");
//    $(".tc").eq(2).show().siblings().hide();
//}
//分配菜单tab切换
$(function () {
    $(".Allot_tab .Allot_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        $(this).parent().addClass("selected").siblings().removeClass("selected");
        $(this).parents(".Allot_tab").find(".tc").eq(index).show().siblings().hide();
    });
});
/*我的订单点击展开百叶窗*/
$(function () {
    $("#slide").find(".order_click").click(function () {
        $(this).parent().toggleClass("selected").siblings().removeClass("selected");
        $(this).next().slideToggle("fast").end().parent().siblings().find(".order_con")
		.addClass("animated bounceIn")
		.slideUp("fast").end().find("i").text("+");
        var t = $(this).find("i").text();
        $(this).find("i").text((t == "+" ? "-" : "+"));
    });
});
//筛选条件经过展开

$(document).ready(function (e) {
    $("#selectList").find(".more").toggle(function () {
        $(this).parent().parent().removeClass("dlHeight");
    }, function () {
        $(this).parent().parent().addClass("dlHeight");
    });
});
//收起  展开
$(function () {
    $(".listIndex .more").click(function () {
        $(this).html($(this).html() == "展开" ? "收起" : "展开");
    });

})
//筛选条件选中添加样式
$(function () {   
    $(".listIndex a").click(function () {
        //alert($(this).html()+"|||siblings:"+$(this).siblings().html());
        $(this).addClass("click").parent().siblings().children().removeClass("click");
    });
});
//筛选类型条件选中添加样式
$(function () {
    $(".File_type .F_type a").click(function () {
        //alert($(this).html()+"|||siblings:"+$(this).siblings().html());
        $(this).addClass("click").parent().siblings().children().removeClass("click");
    });
});
//左侧导航点击下拉
//$(function () {
//    $(".i-menu").each(function () {
//        var _this = $(this);
//        $(this).hover(function () {
//            _this.find(".btn-area").show();
//        }, function () {
//            _this.find(".btn-area").hide();
//        });
//        var i_con = $(this).next(".i-con")[0];
//        i_con.style.display = "none";
//        $(this).click(function () {
//            if (i_con.style.display == "none") {
//                i_con.style.display = "block";
//            } else {
//                i_con.style.display = "none";
//            };
//        });

//    });
//    $(".ici-menu").each(function () {
//        var _this = $(this);
//        $(this).hover(function () {
//            _this.find(".btn-area").show();
//        }, function () {
//            _this.find(".btn-area").hide();
//        });
//        var i_con = $(this).next(".ici-con")[0];
//        i_con.style.display = "none";
//        $(this).click(function () {
//            if (i_con.style.display == "none") {
//                i_con.style.display = "block";
//            } else {
//                i_con.style.display = "none";
//            };
//        });
//    });
//    $(".icic-item").each(function () {
//        var _this = $(this);
//        $(this).hover(function () {
//            _this.find(".btn-area").show();
//        }, function () {
//            _this.find(".btn-area").hide();
//        });
//    });

//        $(".i-menu").click(function () {
//            $(".i-menu").next().hide();
//            var dis = $(this).next().css("display");
//            if (dis == "none") {
//                $(this).next().show();
//            }
//            else {
//                $(this).next().hide();
//            }

//        });
//        $(".ici-menu").click(function () {
//            $(".ici-menu").next().hide();
//            var dis = $(this).next().css("display");
//            if (dis == "none") {
//                $(this).next().show();
//            }
//            else {
//                $(this).next().hide();
//            }
//        });
     
//})
/*组卷百叶窗*/
$(function () {
    $("#slide").find(".Topic_click").click(function () {
        $(this).parent().parent().toggleClass("selected").siblings().removeClass("selected");
        $(this).parent().next().slideToggle("fast").end().parent().siblings().find(".Topic_con")
		.addClass("animated bounceIn")
		.slideUp("fast").end().find("i").text("+");
        var t = $(this).find("i").text();
        $(this).find("i").text((t == "+" ? "-" : "+"));
    });
});
$(function(){
    $(".A_Analysis").find(".menuclick").click(function () {
        $(this).parent().toggleClass("select").siblings().removeClass("select");
        $(this).next().slideToggle("fast").end().parent().siblings().find(".timu")
		.addClass("animated flipInX")	
		.slideUp("fast").end();
        var t = $(this).find("i").text();
        if (t == "+") {
            $(this).find("i").text("-");
            $(this).parent().siblings().each(function () {
                $(this).find(".menuclick").find("i").text("+");
            });
        } else if (t == "-") {
            $(this).find("i").text("+");
            $(this).parent().siblings().each(function () {
                $(this).find(".menuclick").find("i").text("+");
            });
        }
        //$(this).find("i").text((t == "+" ? "-" : "+"));
    });
});