<%@ Page Title="" Language="C#" MasterPageFile="~/Moblie.Master" AutoEventWireup="true" CodeBehind="PersonalNotes.aspx.cs" Inherits="Moblie.PersonalNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="rfmain">

		<dl class="tj_table">
			<h2>学习笔记</h2>
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
            <option value="1">章节</option>
            <option value="2">第一章</option>
            <option value="3">第二章</option>
            <option value="3">第三章</option>
        	</select>
      	</span>
    		
    		<span class="ss"><input type="button" value="搜索"></span>
			</dt>
			<dd>
				<span><input type="text"></span>
			</dd>
		</dl>
		<div class="page">
	</div>
</asp:Content>
