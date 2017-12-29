//#menubox > #box.find(".items") > .class.find("class")>class>tag 	

//email下拉
$(document).ready(function(e) {
	//下拉列表
   $(".slidedown").mouseover(function() {
       $(this).find(".slidecon").show();
	   $(this).find("dt").addClass("hover");
    });
	$(".slidedown").mouseout(function() {
       $(this).find(".slidecon").hide();
	   $(this).find("dt").removeClass("hover");
    });
});
/*百叶窗*/
$(function(){
	$("#menubox").find(".menuclick").click(function(){
		$(this).parent().toggleClass("selected").siblings().removeClass("selected");
		$(this).next().slideToggle().end().parent().siblings().find(".submenu")
		.addClass("animated flipInX")	
		.slideUp("fast").end().find("i").text("+");
		var t = $(this).find("i").text();
		$(this).find("i").text((t=="+"?"-":"+"));
	});	
});
