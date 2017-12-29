// JavaScript Document
//控制页面高度
function SetMenuHeight()
{
    var h = $(window).height();
    var h1 = $('.uesr-info-block').innerHeight();
    $('.sidebar').css('height', h - h1 - 68 + 10 - 26);
    $('.con-r').css('height', h - 130);
}

//登录水平居中
function Setlogincenter()
{
    var w = ($(window).width() - 940) * 0.5;
    $('.login').css('left', w);
}

$(window).resize(function ()
{
    SetMenuHeight();
    Setlogincenter();
});
$(document).ready(function ()
{
    SetMenuHeight();
    Setlogincenter();
});
//菜单方法
$(function ()
{
    MenuInit();
});

// 菜单初始化
// 为菜单注册点击、选择、鼠标移动的相关样式
function MenuInit()
{
    $('.first-menu li').click(function ()
    {
        $(this).addClass('active');
        $(this).siblings('li').removeClass('active');
    });
    /*$('.side-menu>li').mouseover(function ()
    {
        $(this).css('background-color', '#454545');
        $('.LeftMenu-open').css('background-color', '#585858');
    });*/
    /*$('.side-menu>li').mouseleave(function ()
    {
        $(this).css('background-color', '#494949');
        $('.LeftMenu-open').css('background-color', '#585858');
    });*/
    $('.haschild>a').click(function ()
    {
        $('#js-closemenu').removeClass('close-all-active');
        $('#js-openmenu').removeClass('open-all-active');
        if ($(this).siblings('.childmenu').is(':hidden'))
        {
            $(this).siblings('.childmenu').slideDown();
            $(this).parent().addClass('LeftMenu-open');
            //var url = $(this).children('img').attr('src');
            //var num = url.length - 4;
            //var newurl = url.substring(0, num) + '_hover.png';
            //$(this).children('img').attr('src', url);
        }
        else
        {
            $(this).siblings('.childmenu').slideUp();
            $(this).parent().removeClass('LeftMenu-open');
            //var url = $(this).children('img').attr('src');
            //var num = url.length - 10;
            //var newurl = url.substring(0, num) + '.png';
            //$(this).children('img').attr('src', url);
        }
    });
    $('.haschild li').click(function ()
    {
        $('.haschild li').removeClass('active');
        $(this).addClass('active');
    });
}

function fraload(uid, url, fid, id) {

    if (fid == undefined || id == undefined) {
    } else {

        $("#OneMenu").text($("#One" + fid).text());
        $("#TwoMenu").text($("#Two" + id).text());
    }
    //    var Rurl = "";
    //    if (url.indexOf("?") != -1) {
    //        Rurl = url + "&_" + (new Date().getTime());
    //    }
    //    else {
    //        Rurl = url + "?_" + (new Date().getTime());
    //    }

    //$("#"+ uid).html("");
    //$("#" + uid).load(url);
    if (url.indexOf("?") == -1)
        $("#" + uid).attr("src",encodeURI(url) + "?_" + (new Date().getTime()));
    else
        $("#" + uid).attr("src",encodeURI(url) + "&_" + (new Date().getTime()));

}
//load方法
function fload(uid, url, fid, id)
{
   
    if (fid == undefined || id == undefined)
    {
    } else
    {

        $("#OneMenu").text($("#One" + fid).text());
        $("#TwoMenu").text($("#Two" + id).text());
    }
    //    var Rurl = "";
    //    if (url.indexOf("?") != -1) {
    //        Rurl = url + "&_" + (new Date().getTime());
    //    }
    //    else {
    //        Rurl = url + "?_" + (new Date().getTime());
    //    }

    //$("#"+ uid).html("");
    //$("#" + uid).load(url);
    if (url.indexOf("?") == -1)
        $("#" + uid).load(encodeURI(url) + "?_" + (new Date().getTime()));
    else
        $("#" + uid).load(encodeURI(url) + "&_" + (new Date().getTime()));

}

//菜单隐藏方法
$(document).ready(function (e)
{
    $('#js-menu-control').click(function ()
    {
        if (!$('#js-menu').is(':hidden'))
        {
            $('#js-menu').hide();
            $('#js-menu-control').addClass('ct-menu-bt-open');
            $('#js-menu-sub1').css('margin-left', '0');
            $('#js-menu-sub2').css('margin-left', '0')
        }
        else
        {
            $('#js-menu').show();
            $('#js-menu-control').removeClass('ct-menu-bt-open');
            $('#js-menu-sub1').css('margin-left', '240px');
            $('#js-menu-sub2').css('margin-left', '250px');
        }
    });
    $('#js-userinfo-control').click(function ()
    {
        $('#js-userinfo').hide();
        $('#js-open-userinfo').removeClass('userinfo-open-un');
        SetMenuHeight();
    });
    $('#js-open-userinfo').click(function ()
    {
        $('#js-userinfo').show();
        $(this).addClass('userinfo-open-un');
        SetMenuHeight();
    });
    //展开菜单
    $('#js-openmenu').click(function ()
    {
        $(this).addClass('open-all-active');
        $('#js-closemenu').removeClass('close-all-active');
        $('.haschild>a').each(function ()
        {
            $(this).siblings('.childmenu').slideDown();
            $(this).parent().addClass('LeftMenu-open');
            var url = $(this).children('img').attr('src');
            $(this).children('img').attr('src', url);
        });
    });
    //合并菜单
    $('#js-closemenu').click(function ()
    {
        $(this).addClass('close-all-active');
        $('#js-openmenu').removeClass('open-all-active');
        $('.haschild>a').each(function ()
        {
            $(this).siblings('.childmenu').slideUp();
            $(this).parent().removeClass('LeftMenu-open');
            var url = $(this).children('img').attr('src');
        });
    });
});

//切换标签:不限切换标签的数目
$(document).ready(function (e)
{
    $('ul.tab-list li:first-child').addClass("active");
    $('div.content div.layout').attr('id', function () { return idNumber("No") + $("div.content div.layout").index(this) });
    $('.tab-list li').click(function ()
    {
        var c = $("ul.tab-list li");
        var index = c.index(this);
        var p = idNumber("No");
        show(c, index, p);
    });
    function show(controlmunu, num, prefix)
    {
        var content = prefix + num;
        $('#' + content).siblings().hide();
        $('#' + content).show();
        controlmunu.eq(num).addClass('active').siblings().removeClass('active');
    };
    function idNumber(prefix)
    {
        var idNum = prefix;
        return idNum;
    }
});