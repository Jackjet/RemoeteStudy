<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualJobCompletion.aspx.cs" Inherits="YHSD.VocationalEducation.Portal.Layouts.Education.ApplyExamine.aspx.IndividualJobCompletion" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    	<link rel="stylesheet" href="css/common.css">
	<link rel="stylesheet" href="css/jxpt.css">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="IndividualJobCompletion">
        <dl class="tj_table">
			<h2>个人作业完成情况:</h2>
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
					<tr>					  
						<th>所属课程</th>
						<th>作业名称</th>
					  <th>完成时间</th>
						<th>完成情况</th>									
					</tr>			
					<tr>						
						<td>大学英语</td>
						<td>英语专项阅读</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>高等数学</td>
						<td>线性代数第三章</td>
						<td>2015-11-29</td>
						<td>70%</td>
					</tr>
					<tr>						
						<td>语文</td>
						<td>古诗词鉴赏</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>化学</td>
						<td>化合反应综合测试</td>
						<td>2015-11-29</td>
						<td>70%</td>
					</tr>
					<tr>						
						<td>大学英语</td>
						<td>英语专项阅读</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>高等数学</td>
						<td>线性代数第三章</td>
						<td>2015-11-29</td>
						<td>70%</td>
					</tr>
					<tr>						
						<td>语文</td>
						<td>古诗词鉴赏</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>化学</td>
						<td>化合反应综合测试</td>
						<td>2015-11-29</td>
						<td>70%</td>
					</tr><tr>						
						<td>大学英语</td>
						<td>英语专项阅读</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>高等数学</td>
						<td>线性代数第三章</td>
						<td>2015-11-29</td>
						<td>70%</td>
					</tr>
					<tr>						
						<td>语文</td>
						<td>古诗词鉴赏</td>
						<td>2015-11-29</td>
						<td>85%</td>
					</tr>
					<tr>
						<td>化学</td>
						<td>化合反应综合测试</td>
						<td>2015-11-29</td>
						<td>70%</td>
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
个人作业完成情况
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
个人作业完成情况
</asp:Content>
