﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs" Inherits="Moblie.AccountManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
	<title>Document</title>
	<link rel="stylesheet" href="css/common.css">
	<link rel="stylesheet" href="css/jxpt.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="rfmain">

		<dl class="tj_table">
			<h2>账户管理</h2>
			<dt class="sousuo">
				<span>
					<select name="rqxz" id="rqxz">
            <option value="1">课程类别</option>
            <option value="2">中华传统文学修养</option>
            <option value="3">中华传统文学修养</option>
            <option value="3">中华传统文学修养</option>
					</select>
        </span>
        <span>
					<select name="rqxz1" id="rqxz1">
            <option value="1">年级</option>
            <option value="2">七年级</option>
            <option value="3">八年级</option>
            <option value="3">八年级</option>
        	</select>
      	</span>
    		<span><input type="text"></span>
    		<span class="ss"><input type="button" value="搜索"></span>
			</dt>
			<dd>
				<table class="tj_form">
					<tr><!--表头tr名称-->
					  <th>姓名</th>
					  <th>年级</th>
						<th>班级</th>
						<th>课程类别</th>
						<th>交易时间</th>	
						<th>消费金额</th>														
					</tr>													
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>180.00</td>				
					</tr>
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>180.00</td>				
					</tr>
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>180.00</td>				
					</tr>
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>中华传统文学修养</td>
						<td>2014-12-02 13:54</td>	
						<td>350.00</td>				
					</tr>					
				</table>
			</dd>
		</dl>
		<div class="page">
	   <span class="pageup"><a href="#">上一页</a></span><span class="number"><a href="#">1</a><a href="#">2</a><a href="#">3</a></span><span class="pagedown"><a href="#">下一页</a></span><span class="count"><a href="#">共3页</a></span> <span class="count"><a href="#">24条</a></span> 
	</div>

</asp:Content>
