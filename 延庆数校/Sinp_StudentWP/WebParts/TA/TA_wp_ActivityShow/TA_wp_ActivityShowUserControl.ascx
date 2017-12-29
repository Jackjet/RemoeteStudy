<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TA_wp_ActivityShowUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.TA.TA_wp_ActivityShow.TA_wp_ActivityShowUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst_content.css">
<link rel="stylesheet" href="/_layouts/15/Stu_css/ico/iconfont.css">
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<div class="stxq_index">
			<div class="hd_cjxq">
				<img src="/_layouts/15/Stu_images/nopic.png" alt="" runat="server" id="img_Pic">
				<ul>
					<li><h2><asp:Literal ID="Lit_Title" runat="server" Text="--" /></h2></li>
					<li><i class="iconfont">&#xe604;</i><asp:Literal ID="Lit_Date" runat="server" Text="--" /></li>
					<li><i class="iconfont">&#xe607;</i><asp:Literal ID="Lit_Address" runat="server" Text="--" /></li>
					<li><i class="iconfont">&#xe60b;</i><asp:Literal ID="Lit_Count" runat="server" Text="0" />人参加</li>
					<li><span><asp:LinkButton runat="server" ID="Join" Text="参加" OnClick="Join_Click"/></span></li>
				</ul>
			</div>
			<dl>
				<dt><i class="iconfont">&#xe60c;</i>参加的人</dt>
				<dd>
                    <asp:ListView ID="LV_TermList" runat="server">
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr>
                                    <td>暂无人参加,赶紧参加吧</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <img src='<%# Eval("U_Pic") %>' title='<%# Eval("Name") %>' />
                        </ItemTemplate>
                    </asp:ListView>
				</dd>
			</dl>
			<dl>
				<dt><p></p>活动内容</dt>
				<dd>
					<p><asp:Literal ID="Lit_Content" runat="server" /></p>
				</dd>
			</dl>
		</div>
