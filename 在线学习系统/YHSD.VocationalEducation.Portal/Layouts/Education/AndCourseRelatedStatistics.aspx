﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AndCourseRelatedStatistics.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.Education.ArchiveInformationStatistics.aspx.AndCourseRelatedStatistics" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="css/common.css" rel="stylesheet" />
<link href="css/jxpt.css" rel="stylesheet" />
<script src="js/jquery-1.9.1.min.js"></script>
<script src="js/zhuxingtu.js"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="AndCourseRelatedStatistics">
        
		<dl class="tj_table">
			<h2>课课程管理-和课程相关的统计:</h2>
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
			<dd>
				<table class="tj_form">
					<tr><!--表头tr名称-->
					  <th>姓名</th>
					  <th>年级</th>
						<th>班级</th>
						<th>课程类别</th>
						<th>授课教师</th>												
						<th>上传时间</th>	
						<th>学习模式</th>
						<th>课程定价</th>	
					</tr>			
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>大学英语</td>
						<td>李老师</td>	
						<td>2015-11-30 12:19</td>
						<td>免费</td>
						<td>0.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>高等数学</td>
						<td>洪老师</td>	
						<td>2015-11-31 12:19</td>
						<td>收费</td>
						<td>230.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>古诗词鉴赏</td>
						<td>线老师</td>	
						<td>2015-11-28 12:19</td>
						<td>收费</td>
						<td>190.00</td>		
					</tr>
					<tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>大学英语</td>
						<td>李老师</td>	
						<td>2015-11-30 12:19</td>
						<td>免费</td>
						<td>0.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>高等数学</td>
						<td>洪老师</td>	
						<td>2015-11-31 12:19</td>
						<td>收费</td>
						<td>230.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>古诗词鉴赏</td>
						<td>线老师</td>	
						<td>2015-11-28 12:19</td>
						<td>收费</td>
						<td>190.00</td>		
					</tr><tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>大学英语</td>
						<td>李老师</td>	
						<td>2015-11-30 12:19</td>
						<td>免费</td>
						<td>0.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>高等数学</td>
						<td>洪老师</td>	
						<td>2015-11-31 12:19</td>
						<td>收费</td>
						<td>230.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>古诗词鉴赏</td>
						<td>线老师</td>	
						<td>2015-11-28 12:19</td>
						<td>收费</td>
						<td>190.00</td>		
					</tr><tr>
						<td>柳依依</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>大学英语</td>
						<td>李老师</td>	
						<td>2015-11-30 12:19</td>
						<td>免费</td>
						<td>0.00</td>				
					</tr>
					<tr>
						<td>李善武</td>
						<td>七年级</td>
						<td>三班</td>	
						<td>高等数学</td>
						<td>洪老师</td>	
						<td>2015-11-31 12:19</td>
						<td>收费</td>
						<td>230.00</td>				
					</tr>
					<tr>
						<td>王小明</td>
						<td>九年级</td>
						<td>一班</td>	
						<td>古诗词鉴赏</td>
						<td>线老师</td>	
						<td>2015-11-28 12:19</td>
						<td>收费</td>
						<td>190.00</td>		
					</tr>	
				</table>
			</dd>
		</dl>
		<div class="page">
	   <span class="pageup"><a href="#">上一页</a></span><span class="number"><a href="#">1</a><a href="#">2</a><a href="#">3</a></span><span class="pagedown"><a href="#">下一页</a></span><span class="count"><a href="#">共3页</a></span> <span class="count"><a href="#">24条</a></span> 
	</div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
课程管理-和课程相关的统计
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
课程管理-和课程相关的统计
</asp:Content>
