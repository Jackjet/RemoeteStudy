<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="Worknspection.aspx.cs" Inherits="Moblie.Worknspection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
	<title>Document</title>
	<link rel="stylesheet" href="css/common.css">
	<link rel="stylesheet" href="css/jxpt.css">
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="js/tz_slider.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="rfmain">

		<dl class="tj_table xxk">
			<h2>作业提交情况:</h2>
			<dt class="sousuo">
				<span>
					<select name="rqxz" id="rqxz">
            <option value="1">课程类别</option>
            <option value="2">课程类别</option>
            <option value="3">课程类别</option>
            <option value="3">课程类别</option>
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
			<dt class="qiehuan">
				<ul>
					<li class="active">当前作业提交</li>
					<li>历史作业提交</li>
				</ul>
			</dt>	
			<dd>
				<table class="tj_form">
					<tr><!--表头tr名称-->
					  <th></th>
					  <th>阅读理解专项训练</th>
						<th>线性代数第二章节</th>
						<th>古诗词鉴赏</th>
						<th>阅读理解专项训练</th>
						<th>线性代数第二章节</th>
						<th>古诗词鉴赏</th>	
						<th>总计</th>
						<th>总提交</th>												
					</tr>			
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>			
					</tr>
					<tr>
						<td>王小明</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>										
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>			
					</tr>
					<tr>
						<td>王小明</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>					
				</table>
			</dd>
			<dd style="display:none;">
				<table class="tj_form">
					<tr><!--表头tr名称-->
					  <th></th>
					  <th>阅读理解专项训练</th>
						<th>线性代数第二章节</th>
						<th>古诗词鉴赏</th>
						<th>阅读理解专项训练</th>
						<th>线性代数第二章节</th>
						<th>古诗词鉴赏</th>	
						<th>总计</th>		
						<th>共提交</th>										
					</tr>						
					<tr>
						<td>王小明</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>	
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>			
					</tr>
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>			
					</tr>									
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>			
					</tr>
					<tr>
						<td>王小明</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>
					<tr>
						<td>柳依依</td>
						<td>提交</td>
						<td>未提交</td>	
						<td>提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>未提交</td>
						<td>8项</td>
						<td>6项</td>		
					</tr>					
				</table>
			</dd>
		</dl>
		<div class="page">
	   <span class="pageup"><a href="#">上一页</a></span><span class="number"><a href="#">1</a><a href="#">2</a><a href="#">3</a></span><span class="pagedown"><a href="#">下一页</a></span><span class="count"><a href="#">共3页</a></span> <span class="count"><a href="#">24条</a></span> 
	</div>

	</div>
</asp:Content>
