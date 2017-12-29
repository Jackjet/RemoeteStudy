// JavaScript Document
//隔行变色
$(function(){
	$(document).ready(function() { 
	$('.table-s1-js tr td').addClass('odd'); 
	$('.table-s1-js tr:even td').addClass('even'); //奇偶变色，添加样式 
	}); 
	})