// 社团管理活动页面

$(document).ready(function(e) {
     $("#selectList").find(".more").toggle(function(){
        $(this).parent().removeClass("dlHeight");
    },function(){
        $(this).parent().addClass("dlHeight");
	});
});
	//收起  展开
$(function(){
	$(".more").click(function(){
		$(this).html($(this).html() == "展开" ? "收起" : "展开");		
	});

})
// 社团资料_我是团长_编辑人员 页面
// 
$(document).ready(function(e) {
     $("#selectContent").find(".more").toggle(function(){
        $(this).parent().removeClass("ddHeight");
    },function(){
        $(this).parent().addClass("ddHeight");
	});
});



