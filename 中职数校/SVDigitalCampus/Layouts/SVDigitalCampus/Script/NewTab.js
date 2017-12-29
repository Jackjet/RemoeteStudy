$(function () {
    $(".tz_tab .tz_tabheader").find("a").click(function () {
        var index = $(this).parent().index();
        $(this).parent().addClass("selected").siblings().removeClass("selected");
        $(this).parents(".tz_tab").find(".tc").eq(index).show().siblings().hide();
    });
});