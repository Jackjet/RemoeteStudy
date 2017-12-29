<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="RechargeCard.aspx.cs" Inherits="Moblie.RechargeCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
	<title>Document</title>
	<link rel="stylesheet" href="css/common.css"/>
	<link rel="stylesheet" href="css/jxpt.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="rfmain">
		<dl class="tj_table">
			<h2>充值卡管理</h2>
			<dt class="sousuo">				
    		<span>学生姓名<input type="text" /></span>
    		<span class="ss"><input type="button" value="搜索" /></span>
			</dt>
			<dd>
				<table class="tj_form">
					<tr><!--表头tr名称-->
					  <th>姓名</th>
					  <th>年级</th>
						<th>班级</th>						
						<th>消费总金额</th>														
					</tr>													
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>						
						<td>1050.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>						
						<td>540.00</td>				
					</tr>
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>						
						<td>1050.00</td>				
					</tr>									
				</table>
			</dd>
		</dl>
		<div class="page">
	   <span class="pageup"><a href="#">上一页</a></span><span class="number"><a href="#">1</a></span><span class="pagedown"><a href="#">下一页</a></span><span class="count"><a href="#">共1页</a></span> <span class="count"><a href="#">3条</a></span> 
	</div>
   </div>
</asp:Content>
